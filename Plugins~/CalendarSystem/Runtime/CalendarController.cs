using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace AIO.System.Calendar
{
    /// <summary>
    /// Enum representing each day styles. Day styles typically set the text and background colo(u)rs. Daystyles can also be set to highlight properties.
    /// </summary>
    public enum DAY_STYLE
    {
        Generic,
        CurrentDay,
        Notification,
        Reminder,
        In_Month,
        Out_Of_Month,
        Weekend,
        Invisible,
        MinorEvent,
        MajorEvent,
        Holiday,
        AltColor1,
        AltColor2,
        AltColor3,
        AltColor4,
        AltColor5,
        AltColor6
    }

    /// <summary>
    /// Controls a GUI calander. Contains functions to build and control a calendar.
    /// 
    /// If the property time_manager is set, it will display that date. Otherwise it uses datetime.Now to initilize the displayed date.
    /// 
    /// Calendars are made up of CalendarDateItem objects as well as buttons and other UI elements. 
    /// 
    /// This script automatically updates these elements as time passes.
    ///
    /// A working prefab with this script as a component is given in the package.
    /// </summary>
    /// <code>
    /// // Highlight a specific day
    /// highlight_date(display_date.AddDays(1), DAY_STYLE.Generic);
    /// </code>
    /// 
    [ExecuteInEditMode]
    public class CalendarController : MonoBehaviour
    {
        // --- Parameters ---

        #region

        /// <summary>
        /// TimeManager component attached to this CalendarController
        /// </summary>
        public TimeManager time_manager;

        /// <summary>
        /// The displayed DateTime.
        /// </summary>
        private DateTime _display_date = DateTime.Now;

        /// <summary>
        /// The currently displayed date and time. 
        /// </summary>
        public DateTime display_date
        {
            get
            {
                // Return the display date.
                return _display_date;
            }
            set
            {
                // Set the display date
                _display_date = value;
            }
        }

        /// <summary>
        /// The upper text label to display the date and time.
        /// </summary>
        public Text date_header;

        /// <summary>
        /// The background image.
        /// </summary>
        public Image background_image;

        /// <summary>
        /// The prefab for each day item. 
        /// </summary>
        public GameObject day_prefab;

        /// <summary>
        ///     Color Palette For This Calendar 
        /// </summary>
        public ColorPalette pallette;

        #endregion

        /// <summary>
        /// All of the date_items of the currently displayed month
        /// </summary>
        [SerializeField] public List<CalendarDateItem> date_items = new List<CalendarDateItem>();

        /// <summary>
        /// The calendar's rect transform.
        /// </summary>
        private RectTransform calendar_rect_transform;

        void Awake()
        {
            // If a TimeManagerComponent is assigned
            if (time_manager)
            {
                // init the display date as the time manager's date
                display_date = TimeManager.TruncateDateTime(time_manager.start_date, TimeManager.DateCatagory.Day);

                // Subscribe to events
                time_manager.tick_day += advance_day;
                time_manager.tick_month += advance_month;
            }
            else
            {
                // init the display date as the current date
                display_date = DateTime.Now;
            }

            // Build the calander at start.
            build_calendar(display_date);
        }

        void Start()
        {
            // Get the Color Pallette
            pallette = GetComponent<ColorPalette>();

            // If none is found
            if (pallette == null)
            {
                // Create a new one with default values
                pallette = this.gameObject.AddComponent<ColorPalette>();
            }

            //Set the background color to the white color.
            background_image.color = pallette.white_color;
        }

        /// <summary>
        /// Advances the calendar day forward by one. 
        /// 
        /// If a new month is rolled over then the calendar is rebuilt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void advance_day(object sender, DateTime d)
        {
            d = TimeManager.TruncateDateTime(d, TimeManager.DateCatagory.Day);
            display_date = d;

            //  Set the old display date to generic style
            highlight_date(d.AddDays(-1), DAY_STYLE.Generic);

            //  Set the new display date to highlighted
            highlight_date(d, DAY_STYLE.AltColor1);
        }

        /// <summary>
        /// Advances the calendar day forward by one. 
        /// 
        /// If a new month is rolled over then the calendar is rebuilt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void advance_month(object sender, DateTime d)
        {
            display_date = TimeManager.TruncateDateTime(d, TimeManager.DateCatagory.Month);
            build_calendar(display_date);
        }

        /// <summary>
        /// Completely destroys and rebuilds the calendar, according to the current datetime and settings. 
        /// 
        /// If a time_manager component is assigned, then t
        /// </summary>
        internal void build_calendar(DateTime disp_date)
        {
            //Debug.Log("Building");
            // 1) Calender item clearing region

            #region

            int count = gameObject.transform.GetChild(0).GetChild(1).childCount;
            for (int i = 0; i < count; i++)
            {
                // Safe Destroy any game objects in the _calendar_date_items object.
                // Don't move the order of the objects iojn 
                SafeDestroy(gameObject.transform.GetChild(0).GetChild(1).GetChild(0).gameObject);
            }

            date_items.Clear();

            #endregion

            // 2) Calender item creation and positioning region

            #region

            // The rect of the day prefab
            Rect day_rect = day_prefab.GetComponent<RectTransform>().rect;

            // The RectTransform of the calendar object.
            calendar_rect_transform = GetComponent<RectTransform>();

            // The rect of the calendar object.
            Rect r = calendar_rect_transform.rect;

            // Calculate the requried box calendar size.
            r.width = day_rect.size.x * 7 + 20;
            r.height = day_rect.size.y * 5 + 10;

            // Set the size of the calendar rect to 5 rows of 7 days 
            calendar_rect_transform.sizeDelta = new Vector2(r.width, r.height);

            // The start position of the first day objects. ie the offset to the top left corner.
            Vector3 startPos = day_prefab.transform.localPosition + new Vector3(-r.width / 2 + day_rect.width / 2 + 10, r.height / 2 - day_rect.height / 2);

            #endregion

            // 3) Calender item rendering region

            #region

            if (!pallette)
            {
                pallette = GetComponent<ColorPalette>();
            }

            // For loop to build each CalendarDateItem object.
            // 35 days is 7*5 weeks
            for (int i = 0; i < 35; i++)
            {
                // Inst a prefab
                GameObject day_obj = Instantiate(day_prefab);

                // Set the day object's parent, scale, rotation, and position.

                // Set parent transform
                day_obj.transform.SetParent(gameObject.transform.GetChild(0).GetChild(1).GetComponent<Transform>());
                // Scale = 1
                day_obj.transform.localScale = Vector3.one;
                // Rotation = identity
                day_obj.transform.localRotation = Quaternion.identity;
                // Position the datetime gui objects 
                day_obj.transform.localPosition = new Vector3(day_rect.width * (i % 7) + startPos.x,
                    startPos.y - (i / 7) * day_rect.height,
                    startPos.z
                );

                // Get the CalendarDateItem on the inst'd day object
                CalendarDateItem day = day_obj.GetComponent<CalendarDateItem>();

                // Generate datetime for the displayed day.
                DateTime first_of_month = new DateTime(disp_date.Year, disp_date.Month, 1);
                DateTime last_of_month = new DateTime(disp_date.Year, disp_date.Month, 1).AddMonths(+1).AddDays(-1);

                DateTime d = disp_date;

                // Try to add the number of days to the day sentinel 
                try
                {
                    d = d.AddDays(i - disp_date.Day - (int)first_of_month.DayOfWeek + 1);
                }
                // If the new date is unrecognizable (C# DateTime is only valid from 0 AD to 9999 AD)
                catch
                {
                    // Ignore this entry
                    Debug.LogWarning("The calendar cannot display dates beyond 0 AD to 9999 Ad.");

                    continue;
                }

                // Configure the day
                day.configure(d);

                // Add the day to the date_items list
                date_items.Add(day);

                // Name the day object
                day_obj.name = d.ToString("M");

                // If we are generating a day that is in the month of the display date.
                if (d.Month == disp_date.Month)
                {
                    // If the current day is a weekend.
                    if (d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday)
                    {
                        // Set the style to weekend style
                        day.set_style(DAY_STYLE.Weekend);
                    }
                    else
                    {
                        // Set the style to generic in month
                        day.set_style(DAY_STYLE.In_Month);
                    }
                }
                // Else, we are generating a day that is out of the month of the display date.
                else
                {
                    // Set the style of the day
                    day.set_style(DAY_STYLE.Out_Of_Month);
                }

                // Set the date_header text 
                date_header.text = disp_date.ToString("Y");
            }

            // Highlight any events in the event stack.
            if (time_manager)
            {
                if (is_date_displayed(time_manager.date))
                {
                    highlight_date(time_manager.date, DAY_STYLE.AltColor1);
                }

                foreach (DateTimeEvent d in time_manager.event_stack)
                {
                    // If the style of the day is not invisible
                    if (d.style != DAY_STYLE.Invisible)
                    {
                        // Highlight each event in the stack.
                        highlight_date(d, d.style);
                    }
                }
            }

            #endregion
        }

        // --- Year and month increments and decrements for UI buttons ---

        #region

        /// <summary>
        /// Decrements the displayed year.
        /// </summary>
        public void YearPrev()
        {
            display_date = display_date.AddYears(-1);
            build_calendar(display_date);
        }

        /// <summary>
        /// Increments the displayed year.
        /// </summary
        public void YearNext()
        {
            display_date = display_date.AddYears(1);
            build_calendar(display_date);
        }

        /// <summary>
        /// Decrements the displayed month.
        /// </summary>
        public void MonthPrev()
        {
            display_date = display_date.AddMonths(-1);
            build_calendar(display_date);
        }

        /// <summary>
        /// Incremenets the displayed month
        /// </summary>
        public void MonthNext()
        {
            display_date = display_date.AddMonths(1);
            build_calendar(display_date);
        }

        #endregion

        // --- Displayed Date Util Functions ---

        #region

        /// <summary>
        /// Returns whether the current date is displayed on the calendar.
        /// </summary>
        /// <param name="d">The DateTime object to check.</param>
        /// <returns>Returns true if the DateTime object is currently rendered.</returns>
        public bool is_date_displayed(DateTime d)
        {
            d = TimeManager.TruncateDateTime(d, TimeManager.DateCatagory.Day);
            if (get_first_displayed_date() <= d && d <= get_last_displayed_date())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get the first displayed date.
        /// </summary>
        /// <returns>The smallest displayed CalendarDateItem. </returns>
        public DateTime get_first_displayed_date()
        {
            try
            {
                return date_items[0].date;
            }
            catch
            {
                build_calendar(display_date);
                return date_items[0].date;
            }
        }

        /// <summary>
        /// Get the last displayed date.
        /// </summary>
        /// <returns>The largest displayed CalendarDateItem.</returns>
        public DateTime get_last_displayed_date()
        {
            try
            {
                return date_items[date_items.Count - 1].date;
            }
            catch
            {
                build_calendar(display_date);
                return date_items[date_items.Count - 1].date;
            }
        }

        #endregion

        // --- Highlight Date Functions --- 

        #region

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="style"></param>
        public bool highlight_date(DateTime date, DAY_STYLE style)
        {
            foreach (CalendarDateItem d in date_items)
            {
                if (TimeManager.TruncateDateTime(d.date, TimeManager.DateCatagory.Day) == TimeManager.TruncateDateTime(date, TimeManager.DateCatagory.Day))
                {
                    d.set_highlight_style(style);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Highlight DateTime d by setting the highlight color. Highlighted dates can retain their background color. 
        /// </summary>
        /// <param name="date">The date to highlight.
        /// <param name="highlight">The highlight color. 
        /// The highlight color is lerped with the background color by the alpha amount in the highlight color. </param>
        public bool highlight_date(DateTime date, Color highlight)
        {
            // If we are rendering the datetime d.
            if (is_date_displayed(date))
            {
                foreach (CalendarDateItem d in date_items)
                {
                    if (d.date == date)
                    {
                        d.highlight_color = highlight;
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Highlight DateTimeEvent e by setting the highlight style. 
        /// 
        /// Highlighted dates can retain their background color. 
        /// </summary>
        /// <param name="date">The date to highlight.
        /// <param name="highlight">The highlight color. 
        /// The highlight color is lerped with the background color by the alpha amount in the highlight color. </param>
        public bool highlight_date(DateTimeEvent e, DAY_STYLE style)
        {
            // If the event has an end time
            if (e.EventTimeEnd != null)
            {
                DateTime EndTime = e.EventTimeEnd ?? default(DateTime);

                DateTime first_of_month = display_date.AddDays(-display_date.Day + 1);

                DateTime d = e.EventTime;
                while (d < EndTime)
                {
                    d = d.AddDays(1);
                    if (is_date_displayed(d))
                    {
                        get_date_item(d).set_highlight_style(style);
                    }
                }

                return true;
            }

            // If we are rendering the datetime d.
            if (is_date_displayed(e.EventTime))
            {
                DateTime first_of_month = display_date.AddDays(-display_date.Day);

                int idx = e.EventTime.Day + (int)first_of_month.DayOfWeek;

                date_items[idx].set_highlight_style(style);

                return true;
            }

            return false;
        }

        public CalendarDateItem get_date_item(DateTime d)
        {
            d = TimeManager.TruncateDateTime(d, TimeManager.DateCatagory.Day);

            return date_items.Find(cdi => TimeManager.TruncateDateTime(cdi.date, TimeManager.DateCatagory.Day) == d);
        }

        #endregion

        // --- Window Functions ---

        #region

        /// <summary>
        /// Alternately toggles the visibility of the calendar window and the minimized icon.
        /// </summary>
        public void toggle_visibility()
        {
            // b = if the calendar is visible
            bool b = !gameObject.transform.GetChild(0).gameObject.activeSelf;

            // Toggle the calendar object
            gameObject.transform.GetChild(0).gameObject.SetActive(b);

            // Toggle the minimized object
            gameObject.transform.GetChild(1).gameObject.SetActive(!b);
        }

        /// <summary>
        /// Sets the visibilty of the calendar to true or false.
        /// 
        /// The 0th child of the calendar controller gameobject should always be the calendar.
        /// The 1st child of the calendar controller gameobject should be the minimized icons.SA
        /// </summary>
        /// <param name="b">True enables the calendar, false disables the calander and enables the minimized calendar.</param>
        public void set_visibility(bool b)
        {
            // The 0th child of the gameobject should be the calendar.
            gameObject.transform.GetChild(0).gameObject.SetActive(b);

            // The 1st child of the gameobject should be the minimzed icon.
            GameObject calendar_minimized_icon = gameObject.transform.GetChild(1).gameObject;

            // Toggle the minimized object (if there is one)
            if (calendar_minimized_icon != null)
            {
                gameObject.SetActive(!b);
            }
        }

        /// <summary>
        /// Opens the calander window
        /// </summary>
        public void OpenWindow()
        {
            this.gameObject.SetActive(true);
        }

        /// <summary>
        /// Closes the calander window
        /// </summary>
        public void CloseWindow()
        {
            this.gameObject.SetActive(false);
        }

        #endregion

        /// <summary>
        /// Custom safe destroyer function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static T SafeDestroy<T>(T obj) where T : Object
        {
            if (Application.isEditor)
                DestroyImmediate(obj);
            else
                Destroy(obj);

            return null;
        }
    }
}