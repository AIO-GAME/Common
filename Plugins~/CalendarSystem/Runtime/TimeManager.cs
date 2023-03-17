using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("AIO.System.Calendar.Editor")]

namespace AIO.System.Calendar
{
    /// <summary>
    /// The TimeManager class travels forward in time and fires functions on events. 
    /// 
    /// The pass_time function is used to move time forward and call events.
    /// 
    /// Events can be repeating, like tick_day, which is called every day in simulation time.
    /// 
    /// There is also an event stack that can call DateTimeEvents. 
    /// 
    /// DateTimeEvents are called once, on their EventDate. They can fire any function when they are called.
    /// 
    /// DateTimeEvents are sorted into the event stack and evaluated in the TimeManager pass_time loop automatically. 
    /// </summary>
    /// How to subscribe to a periodic event:
    /// <code>
    ///    this.tick_day += print_date;            // Event every day
    ///    this.tick_friday += print_date;               // Event every friday
    ///    this.tick_noon += print_date;            // Event every noontime
    /// </code>   
    /// 
    /// 
    /// <code>
    /// Prints the date to the debug.log console. 
    /// public void print_date(object sender, EventArgs e)
    /// {
    ///     // Print the date using DateTime "D" format
    ///     Debug.Log("It is " + last_tick.DayOfWeek.ToString() + ", " + last_tick.ToString("D"));
    /// }
    /// </code>
    /// 
    /// How to add an event to the event stack:
    /// <code>     
    ///     event_stack.Add(DateTimeEvent.InstantiateDateTimeEvent(start_date.AddSeconds(1), myFunctionToCall));
    /// </code>
    /// 
    /// How to create your own custom repeating events.
    /// //  1) Create your custom ticking event handler
    /// <code>
    ///     public event EventHandler tick_custom_function;
    /// </code>
    ///  // 2) Create your custom tick function 
    /// <code>
    ///     protected virtual void OnCustomTick()
    ///     {
    ///          tick_custom_function?.Invoke(this, null);
    ///     }
    /// </code>
    /// // 3) Subscribe some function to your custom ticking event handler
    /// <code>
    ///     tick_custom_function += a_custom_function_to_tick; 
    /// </code>
    /// // 4) Call your custom tick function somewhere in the pass_time loop. You may call it from another ticking function so that it is not tested every update loop.
    ///  
    ///     // For example, if you need to call an event on the 10th day of every month, test it every day by adding it to the OnTickDay() event.
    /// <code>
    /// // Called every day
    /// protected virtual void OnTickDay(DateTime d)
    /// {
    ///     tick_day?.Invoke(this, null);
    ///     if (d.day == 10)
    ///     {
    ///         OnCustomTick();
    ///     }
    /// </code>
    public class TimeManager : MonoBehaviour
    {
        /// <summary>
        /// Set to true to intialize the TimeManager with DateTime.Now
        /// </summary>
        public bool start_now = false;

        // --- Simulation Parameters ---

        #region

        /// <summary>
        /// The maximum time warp.
        /// </summary>
        const int MAXIMUM_TIME_WARP = 60 * 60 * 24 * 365;

        private DateTime _start_date = DateTime.Now;

        /// <summary>
        /// The start date of the simulation set by the start_year, start_month, start_day, start_hour and start_minute.
        /// 
        /// The start date is init'd on start 
        /// </summary>
        public DateTime start_date
        {
            get
            {
                // If start_now is false.
                if (!start_now)
                {
                    try
                    {
                        // Try to make the inputted start DateTime.
                        _start_date = new DateTime(start_year, start_month, start_day, start_hour, start_minute, 0);
                        date = _start_date;
                    }
                    catch
                    {
                        // DateTimes will throw an error if you try to create an invalid date. 
                        // (eg, 0 AD < year < 9999 AD, 0 < Hour < 24)
                        Debug.LogWarning("Entered start DateTime is invalid. Starting TimeManager at DateTime.Now");
                        _start_date = DateTime.Now;
                        date = _start_date;
                    }
                }
                else
                {
                    _start_date = DateTime.Now;
                    date = _start_date;
                }

                return _start_date;
            }
            private set { _start_date = value; }
        }

        /// <summary>
        /// The sim start year. (0 AD < start_year < 9999 AD)
        /// </summary>
        public int start_year = 1762;

        /// <summary>
        /// The sim start month (1 < start_month < 12).
        /// </summary>
        public int start_month = 3;

        /// <summary>
        /// The sim start day (1 < start_day < Days in month)
        /// </summary>
        public int start_day = 14;

        // Parameters for the time manager's initial start time
        /// <summary>
        /// The sim start hour (0 < start_hour < 23)
        /// </summary>
        public int start_hour = 8;

        /// <summary>
        /// The sim start minute (0 < start_minute < 59)
        /// </summary>
        public int start_minute = 0;

        [SerializeField] private DateTime _date;

        /// <summary>
        /// The current date. This value is incremented using Time.DeltaTime, so it is not gaurenteed to be at any specific time.
        /// </summary>
        public DateTime date
        {
            get { return _date; }
            set { _date = value; }
        }

        #endregion

        // --- Event Stack Parameters ---

        #region

        private SortedSet<DateTimeEvent> _event_stack = new SortedSet<DateTimeEvent>(new DateTimeEventComparer());

        /// <summary>
        /// The event stack is a sorted set based on the date. Automatically stores DateTimeEvents in a sorted list based on the date each event should fire.. 
        /// 
        /// The pass_time loop evaluates if the smallest datetime is past the current datetime and fires events accordingly. 
        /// 
        /// Events are removed once they have been fired.
        /// 
        /// <code>
        /// // Add a new datetime event to the stack, 1 day after of the current simulated date.
        /// event_stack.Add(DateTimeEvent.DateTimeEventClass.InstantiateDateTimeEvent(date.AddDays(1), test_event));
        /// </code>     
        /// </summary>
        public SortedSet<DateTimeEvent> event_stack
        {
            get { return _event_stack; }
        }

        /// <summary>
        /// Adds the DateTimeEvent e to the stack. The stack is sorted by event time date.
        /// </summary>
        /// <param name="e">The DateTimeEvent to add to the stack.</param>
        public void AddEventToStack(DateTimeEvent e)
        {
            event_stack.Add(e);
        }

        /// <summary>
        /// Adds the DateTimeEvent e to the stack and highlights the date on a calendar. 
        /// 
        /// </summary>
        /// <param name="e">The DateTimeEvent to add to the stack.</param>
        public void AddEventToStack(DateTimeEvent e, CalendarController c)
        {
            // Add event to the stack
            event_stack.Add(e);

            // If the effect is not invisible.
            if (e.style != DAY_STYLE.Invisible)
            {
                // Highlight the event on the calendar.
                c.highlight_date(e, e.style);
            }
        }

        #endregion

        void Start()
        {
            // Init the starting date
            date = start_date;

            // 
            hrs_ticked += (double)start_date.Minute / 60d + (double)start_date.Second / 60d / 60d + (double)start_date.Millisecond / 60d / 60d / 1000d;
        }

        // --- Repeating Events Region ---

        #region

        // ---      Time Ticking Events     ---

        public event EventHandler<DateTime> tick_minute; // Ticked every minute of game time

        public event EventHandler<DateTime> tick_quarter_hour; // Ticked every quarter hour of game time

        public event EventHandler<DateTime> tick_hour; // Ticked every hour of game time

        public event EventHandler<DateTime> tick_day; // Ticked every day of game time

        public event EventHandler<DateTime> tick_month; // Ticked every month of game time

        public event EventHandler<DateTime> tick_year; // Ticked every year of the game time.

        // ---      Day Ticking Events     ---

        public event EventHandler<DateTime> tick_monday; // Ticked every monday

        public event EventHandler<DateTime> tick_tuesday; // Ticked every tuesday

        public event EventHandler<DateTime> tick_wednesday; // Ticked every wednesday

        public event EventHandler<DateTime> tick_thursday; // Ticked every thursday

        public event EventHandler<DateTime> tick_friday; // Ticked every friday

        public event EventHandler<DateTime> tick_saturday; // Ticked every saturday

        public event EventHandler<DateTime> tick_sunday; // Ticked every sunday

        // ---      Time Of Day Ticking Events ---

        public event EventHandler<DateTime> tick_midnight; // Ticked at 12am

        public event EventHandler<DateTime> tick_morning; // Ticked at 6am

        public event EventHandler<DateTime> tick_noon; // Ticked at 12pm

        public event EventHandler<DateTime> tick_evening; // Ticked at 6pm 

        #endregion


        // --- Update Loop ---

        #region

        public TimeController time_controller;

        private float _game_time_multiplier = 1;

        /// <summary>
        /// The game time multiplier. 
        /// 
        /// Multiplied by Time.deltaTime to drive the simulation forward.
        /// 
        /// Can be set to 0 to pause the simulation. 
        /// </summary>
        public float game_time_multiplier
        {
            get { return _game_time_multiplier; }
            set
            {
                _game_time_multiplier = value;
                if (time_controller != null)
                {
                    time_controller.update_gui_elements(this);
                }
            }
        }


        /// <summary>
        /// Update is called every frame
        /// </summary>
        void Update()
        {
            // Pass some time.
            pass_time(Time.deltaTime * game_time_multiplier);
        }

        /// <summary>
        /// Variable to track the number of hours we need to tick.
        /// </summary>
        private double hrs_ticked;

        /// <summary>
        /// Passes the amount of time given in seconds. Calls tick month, day, hour, and quarter hour events.
        /// </summary>
        /// <param name="seconds_passed">The number of seconds to pass.</param>
        void pass_time(float seconds_passed)
        {
            // Create new timespan from the seconds that have passed
            TimeSpan dt = new TimeSpan((long)(seconds_passed * TimeSpan.TicksPerSecond));

            // Save the last date
            DateTime last_date = date;

            // Add the time that has passed.
            date = date + dt;

            // Get the number of hours that have past
            hrs_ticked += dt.TotalHours;

            int hrs_to_tick = (int)hrs_ticked;
            for (int i = 1; i <= hrs_to_tick; i++)
            {
                // Tick the hour
                this.OnTickHour(TruncateDateTime(last_date.AddHours(i), DateCatagory.Hour));
            }

            hrs_ticked = hrs_ticked - hrs_to_tick;

            // --- Event Stack Evauluation ---

            // Get the next event in the event stack. Automatically sorts based on date.
            DateTimeEvent next_datetime_event = _event_stack.Min;

            // If there is another event in the stack
            if (next_datetime_event)
            {
                // Test if we have ticked the evenet
                if (last_date < next_datetime_event.EventTime && next_datetime_event.EventTime < date)
                {
                    // Tick it
                    next_datetime_event.OnTickEvent();

                    // Remove it from the event stack.
                    event_stack.Remove(next_datetime_event);
                }
            }
        }

        #endregion

        // --- Repeating Ticking Functions ---

        #region

        /// <summary>
        /// Event called every year.
        /// </summary>
        protected virtual void OnTickYear(DateTime d)
        {
            tick_year?.Invoke(this, d);
        }

        /// <summary>
        /// Event called every month.
        /// </summary>
        protected virtual void OnTickMonth(DateTime d)
        {
            tick_month?.Invoke(this, d);
        }

        /// <summary>
        /// Event called every day.
        /// </summary>
        /// <param name="b"></param>
        protected virtual void OnTickDay(DateTime d)
        {
            tick_day?.Invoke(this, d);
        }

        /// <summary>
        /// Fires events based on what day of the week it is ie. sunday monday tuesday ect.
        /// </summary>
        /// <param name="b"></param>
        protected virtual void OnTickNamedDay(DateTime d)
        {
            switch (d.DayOfWeek)
            {
                case (DayOfWeek.Sunday):
                {
                    this.OnTickSunday(d);
                    break;
                }
                case (DayOfWeek.Monday):
                {
                    this.OnTickMonday(d);
                    break;
                }
                case (DayOfWeek.Tuesday):
                {
                    this.OnTickTuesday(d);
                    break;
                }
                case (DayOfWeek.Wednesday):
                {
                    this.OnTickWednesday(d);
                    break;
                }
                case (DayOfWeek.Thursday):
                {
                    this.OnTickThursday(d);
                    break;
                }
                case (DayOfWeek.Friday):
                {
                    this.OnTickFriday(d);
                    break;
                }
                case (DayOfWeek.Saturday):
                {
                    this.OnTickSaturday(d);
                    break;
                }
                default:
                    break;
            }
        }

        /// <summary>
        /// Invokes the tick_sunday event
        /// </summary>
        private void OnTickSunday(DateTime d)
        {
            tick_sunday?.Invoke(this, d);
        }

        /// <summary>
        /// Invokes the tick_monday event
        /// </summary>
        private void OnTickMonday(DateTime d)
        {
            tick_monday?.Invoke(this, d);
        }

        /// <summary>
        /// Invokes the tick_tuesday event
        /// </summary>
        private void OnTickTuesday(DateTime d)
        {
            tick_tuesday?.Invoke(this, d);
        }

        /// <summary>
        /// Invokes the tick_wednesday event
        /// </summary>
        private void OnTickWednesday(DateTime d)
        {
            tick_wednesday?.Invoke(this, d);
        }

        /// <summary>
        /// Invokes the tick_thursday event
        /// </summary>
        private void OnTickThursday(DateTime d)
        {
            tick_thursday?.Invoke(this, d);
        }

        /// <summary>
        /// Invokes the tick_friday event
        /// </summary>
        private void OnTickFriday(DateTime d)
        {
            tick_friday?.Invoke(this, d);
        }

        /// <summary>
        /// Invokes the tick_saturday event
        /// </summary>
        private void OnTickSaturday(DateTime d)
        {
            tick_saturday?.Invoke(this, d);
        }

        /// <summary>
        /// Tick the quarter day events(12am, 6am, 12pm, 6pm)
        /// </summary>
        protected virtual void Evaluate_Quarter_Day_Ticks(DateTime d)
        {
            switch (d.Hour)
            {
                case (0):
                {
                    tick_midnight?.Invoke(this, d);
                    break;
                }
                case (6):
                {
                    tick_morning?.Invoke(this, d);
                    break;
                }

                case (12):
                {
                    tick_noon?.Invoke(this, d);
                    break;
                }
                case (18):
                {
                    tick_evening?.Invoke(this, d);
                    break;
                }
                default:
                    break;
            }
        }

        /// <summary>
        /// Event called every hour.
        /// </summary>
        protected virtual void OnTickHour(DateTime d)
        {
            // Save the last day, month, and year so that we can tick events if needed
            DateTime t_minus_1 = d.AddHours(-1);

            // Tick the hour
            tick_hour?.Invoke(this, d);

            // Evaluate the quarter day ticks
            Evaluate_Quarter_Day_Ticks(d);

            // If there is a new day
            if (t_minus_1.DayOfYear != d.DayOfYear)
            {
                // Tick the day.
                OnTickDay(TruncateDateTime(d, DateCatagory.Day));

                // Tick the named day.
                this.OnTickNamedDay(TruncateDateTime(d, DateCatagory.Day));
            }

            // If there is a n
            if (t_minus_1.Month != d.Month)
            {
                // Tick the month.
                this.OnTickMonth(TruncateDateTime(d, DateCatagory.Month));
            }

            // 
            if (t_minus_1.Year != d.Year)
            {
                // Tick the year.
                this.OnTickYear(TruncateDateTime(d, DateCatagory.Year));
            }
        }

        /// <summary>
        /// Event called every 15 miuntes.
        /// </summary>
        /// <param name="b"></param>
        protected virtual void OnTickQuarterHour(DateTime d)
        {
            tick_quarter_hour?.Invoke(this, d);
        }

        /// <summary>
        /// Event called every minute. This will be called 1440 times every simulated day. 
        /// </summary>
        protected virtual void OnTickMinute(DateTime d)
        {
            tick_minute?.Invoke(this, d);
        }

        #endregion


        // --- Time Warping Functions ---

        #region

        /// <summary>
        /// The index of the multiplier_levels array
        /// </summary>
        private int multiplier_idx = 0;

        /// <summary>
        /// Multiplier level array
        /// </summary>
        private int[] multiplier_levels =
        {
            1, // 1s = 1s Real time
            60, // 1s = 1 min
            60 * 60, // 1s = 1 hr
            60 * 60 * 6, // 1s = 6 hr
            60 * 60 * 24, // 1s = 1 day
            60 * 60 * 24 * 4, // 1s = 4 day
            60 * 60 * 24 * 10, // 1s = 10 day
            60 * 60 * 24 * 31 //  1s = 31 day // Be careful going to quickly with too many events.
        };

        /// <summary>
        /// Set the game time multiplyer using an index of the multiplier levels.
        /// </summary>
        public void SetMultiplierIndex(int new_idx)
        {
            // Clamp the value
            multiplier_idx = Mathf.Clamp(multiplier_idx, 0, multiplier_levels.Length - 1);

            // Change the game time multiplier
            game_time_multiplier = multiplier_levels[multiplier_idx];
        }

        /// <summary>
        /// Bump the index of the game time multiplyer array up or down. Changes the game time multiplier to one of the elements stored int he multiplier_levels array, bumping the indedex up or down.
        /// </summary>
        public void BumpMultiplierIndex(int bump)
        {
            // Clamp the value
            multiplier_idx = Mathf.Clamp(multiplier_idx + bump, 0, multiplier_levels.Length - 1);

            // Set the game time multiplier using the multiplier_levels 
            game_time_multiplier = multiplier_levels[multiplier_idx];
        }


        /// <summary>
        /// Increase the time multiplier the referenced TimeManager by multiplying by another multiplier.  
        /// </summary>
        /// <param name="multiplier"></param>
        public void MultiplyMultiplier(float multiplier)
        {
            // Evaluate the multiplier
            int new_multiplier = (int)(game_time_multiplier * multiplier);

            // Clamp the multiplier
            new_multiplier = Mathf.Clamp(new_multiplier, -MAXIMUM_TIME_WARP, MAXIMUM_TIME_WARP);

            // Set the game time multiplier 
            game_time_multiplier = new_multiplier;
        }


        public void SetMultiplier(float multiplier)
        {
            // Clamp the multiplier
            multiplier = Mathf.Clamp(multiplier, -MAXIMUM_TIME_WARP, MAXIMUM_TIME_WARP);

            // Set the game time multiplier 
            game_time_multiplier = multiplier;
        }

        #endregion

        // --- Truncate DateTime ---

        #region

        internal enum DateCatagory
        {
            Year,
            Month,
            Day,
            Hour,
            Minute,
            Second,
            Millisecond
        }

        internal static DateTime TruncateDateTime(DateTime t, DateCatagory c)
        {
            switch (c)
            {
                case (DateCatagory.Year):
                    return new DateTime(t.Year);
                case (DateCatagory.Month):
                    return new DateTime(t.Year, t.Month, 1);
                case (DateCatagory.Day):
                    return new DateTime(t.Year, t.Month, t.Day);
                case (DateCatagory.Hour):
                    return new DateTime(t.Year, t.Month, t.Day, t.Hour, 0, 0);
                case (DateCatagory.Minute):
                    return new DateTime(t.Year, t.Month, t.Day, t.Hour, t.Minute, 0);
                case (DateCatagory.Second):
                    return new DateTime(t.Year, t.Month, t.Day, t.Hour, t.Minute, t.Second);
                case (DateCatagory.Millisecond):
                    return t;
                default:
                    return t;
            }
        }

        #endregion
    }
}