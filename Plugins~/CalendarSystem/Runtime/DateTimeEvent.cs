using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIO.System.Calendar
{
    public class DateTimeEvent : ScriptableObject
    {
        [SerializeField] private DateTime _EventTime;

        [SerializeField] private DateTime? _EventTimeEnd = null;


        /// <summary>
        /// The DateTime when Func<object> function will be called.
        /// </summary>
        public DateTime EventTime
        {
            get => _EventTime;
            set => _EventTime = value;
        }

        /// <summary>
        /// The ending DateTime for this event. If not set then the event is assumed to be instantenous.
        /// 
        /// The highlighting effect will be applied for all datetimes between the start and the end time. 
        /// </summary>
        public DateTime? EventTimeEnd
        {
            get => _EventTimeEnd;
            set => _EventTimeEnd = value;
        }

        /// <summary>
        /// The function to call.
        /// </summary>
        [SerializeField] public Action function;

        /// <summary>
        /// The style of the day
        /// </summary>
        [SerializeField] public DAY_STYLE style;

        /// <summary>
        /// Invoke the function when the eventTime is reached. The Calendar controller will fire OnTickEvent when the event_time is reached. 
        /// </summary>
        public void OnTickEvent()
        {
            function?.Invoke();
        }

        /// <summary>
        /// Creates a new DateTimeEvent and returns it.
        /// </summary>
        /// <param name="date">The firing DateTime of the event.</param>
        /// <param name="method">The method to call when the event is ticked.</param>
        /// <returns>An instance of the ticked event.</returns>
        public static DateTimeEvent InstantiateDateTimeEvent(DateTime event_time, Action method, DAY_STYLE style = DAY_STYLE.Invisible)
        {
            DateTimeEvent d = CreateInstance<DateTimeEvent>();

            d.style = style;
            d.function = method;
            d._EventTime = event_time;

            return d;
        }

        /// <summary>
        /// Creates a new DateTimeEvent and returns it.
        /// </summary>
        /// <param name="date">The firing DateTime of the event.</param>
        /// <param name="method">The method to call when the event is ticked.</param>
        /// <returns>An instance of the ticked event.</returns>
        public static DateTimeEvent InstantiateDateTimeEvent(DateTime event_time, DateTime end_time, Action method, DAY_STYLE style = DAY_STYLE.Invisible)
        {
            DateTimeEvent d = CreateInstance<DateTimeEvent>();

            d.style = style;
            d.function = method;
            d._EventTime = event_time;
            d._EventTimeEnd = end_time;

            if (d._EventTimeEnd < d._EventTime)
            {
                Debug.LogError("Cannot create an event with an end DateTime before its start DateTime!");
            }

            return d;
        }
    }

    /// <summary>
    /// DateTimeEventComparer is an extension of IComparer for DateTimeEvent objects.
    /// 
    /// Simply compares the DateTime EventTime for object a to object b using the default DateTime CompareTo function. 
    /// </summary>
    internal class DateTimeEventComparer : IComparer<DateTimeEvent>
    {
        public int Compare(DateTimeEvent a, DateTimeEvent b)
        {
            return a.EventTime.CompareTo(b.EventTime);
        }
    }
}