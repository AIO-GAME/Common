using System;
using UnityEngine;
using UnityEngine.UI;

namespace AIO.System.Calendar
{
    /// <summary>
    /// CalendarDateItem defines the GUI behaviour of the calendar days. 
    /// 
    /// The color and style of a CalendarDateItem can be changed.
    /// 
    /// The configure(DateTime d) function configures the CalendarDateItem to represent a certian day.
    /// </summary>
    public class CalendarDateItem : MonoBehaviour
    {
        /// <summary>
        /// The date represented by this CalendarDateItem.
        /// </summary>
        public DateTime date;

        /// <summary>
        /// The parent CalendarController component. 
        /// 
        /// It is assumed that the CalendarController component is in a parent, because CalendarControllers usually intst CalendarDateItems.
        /// </summary>
        private CalendarController calendar_controller;

        // --- UI Settings and Properties ---

        #region

        /// <summary>
        /// Text that displays the current day number 
        /// </summary>
        public Text day_label;

        /// <summary>
        /// The background image of the CalendarDateItem.
        /// </summary>
        public Image background;

        private Color _bg_color;

        /// <summary>
        /// The background color of the datetime object.
        /// </summary>
        public Color bg_color
        {
            get { return _bg_color; }
            private set
            {
                _bg_color = value;

                set_background_color(Color.Lerp(_bg_color, _highlight_color, _highlight_color.a), null);
            }
        }

        private Color _fg_color;

        /// <summary>
        /// The foreground color of the datetime object.
        /// </summary>
        public Color fg_color
        {
            get { return _fg_color; }
            private set
            {
                _fg_color = value;

                set_foreground_color(_fg_color, "");
            }
        }

        private Color _highlight_color;

        /// <summary>
        /// The highlight color of the datetime object. 
        /// 
        ///
        /// 
        /// The highlight color effect mixes the components of the background color and the RGB components of the highlight color. 
        /// 
        /// The alpha component of the highlight color controlls the lerp amount.
        /// </summary>
        public Color highlight_color
        {
            get { return _highlight_color; }
            set
            {
                _highlight_color = value;

                // Set the background color as a mix of the highlight color and the bg color.
                // If the highlight color's alpha is set to 0, there is no highlight effect.
                set_background_color(Color.Lerp(_bg_color, _highlight_color, _highlight_color.a), null);
            }
        }

        #endregion

        // ---UI configuration functions---

        #region

        /// <summary>
        /// Configure the date item for the given datetime.
        /// </summary>
        /// <param name="d">Datetime to configure the day for.</param>
        public void configure(DateTime d)
        {
            // Set the date of the calendar date item
            date = d;

            // Set the label text
            set_label_text(d);

            // Set the style to generic
            set_style(DAY_STYLE.Generic);

            // Set the highlight style to invisible.
            set_highlight_style(DAY_STYLE.Invisible);
        }

        /// <summary>
        /// Configure this CalendarDateItem with DateTime d and a style.
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="style">A DAY_STYLE configures the CalendarDateItem to use the CalendarController color palette.</param>
        public void configure(DateTime d, DAY_STYLE style)
        {
            // Set the label text
            set_label_text(d);

            // Set the style
            set_style(style);

            // Set the highlight style to invisible.
            set_highlight_style(DAY_STYLE.Invisible);
        }

        /// <summary>
        /// Sets the style of the calandar date item.
        /// </summary>
        /// <param name="style">The style of the day.</param>
        internal void set_style(DAY_STYLE style)
        {
            // Set calendar_controller
            if (calendar_controller == null)
            {
                calendar_controller = this.GetComponentInParent<CalendarController>();
            }

            set_highlight_style(style);

            switch (style)
            {
                case DAY_STYLE.Generic:
                    set_background_color(calendar_controller.pallette.white_color, "white_color");
                    set_foreground_color(calendar_controller.pallette.dark_color, "dark_color");
                    break;
                case DAY_STYLE.In_Month:
                    set_background_color(calendar_controller.pallette.white_color, "white_color");
                    set_foreground_color(calendar_controller.pallette.dark_color, "dark_color");
                    break;
                case DAY_STYLE.Out_Of_Month:
                    set_background_color(calendar_controller.pallette.white_color, "white_color");
                    set_foreground_color(calendar_controller.pallette.light_color, "light_color");
                    break;
                case DAY_STYLE.Weekend:
                    set_background_color(calendar_controller.pallette.light_color, "light_color");
                    set_foreground_color(calendar_controller.pallette.dark_color, "dark_color");
                    break;
                case DAY_STYLE.Invisible:
                    set_background_color(calendar_controller.pallette.light_color, "light_color");
                    set_foreground_color(new Color(0, 0, 0, 0), "clear");
                    break;
                case DAY_STYLE.MajorEvent:
                    set_background_color(calendar_controller.pallette.light_color, "alt_color_2");
                    set_foreground_color(calendar_controller.pallette.dark_color, "dark_color");
                    break;
                case DAY_STYLE.Holiday:
                    set_background_color(calendar_controller.pallette.light_color, "alt_color_3");
                    set_foreground_color(calendar_controller.pallette.dark_color, "dark_color");
                    break;
                case DAY_STYLE.MinorEvent:
                    set_background_color(calendar_controller.pallette.alt_color_5, "alt_color_4");
                    set_foreground_color(calendar_controller.pallette.dark_color, "dark_color");
                    break;
                case DAY_STYLE.Reminder:
                    set_background_color(calendar_controller.pallette.alt_color_4, "alt_color_4");
                    set_foreground_color(calendar_controller.pallette.dark_color, "dark_color");
                    break;
                case DAY_STYLE.Notification:
                    set_background_color(calendar_controller.pallette.alt_color_4, "alt_color_5");
                    set_foreground_color(calendar_controller.pallette.dark_color, "dark_color");
                    break;
                case DAY_STYLE.AltColor1:
                    set_background_color(calendar_controller.pallette.alt_color_4, "alt_color_1");
                    set_foreground_color(calendar_controller.pallette.dark_color, "dark_color");
                    break;
                case DAY_STYLE.AltColor2:
                    set_background_color(calendar_controller.pallette.alt_color_4, "alt_color_2");
                    set_foreground_color(calendar_controller.pallette.dark_color, "dark_color");
                    break;
                case DAY_STYLE.AltColor3:
                    set_background_color(calendar_controller.pallette.alt_color_4, "alt_color_3");
                    set_foreground_color(calendar_controller.pallette.dark_color, "dark_color");
                    break;
                case DAY_STYLE.AltColor4:
                    set_background_color(calendar_controller.pallette.alt_color_4, "alt_color_4");
                    set_foreground_color(calendar_controller.pallette.dark_color, "dark_color");
                    break;
                case DAY_STYLE.AltColor5:
                    set_background_color(calendar_controller.pallette.alt_color_4, "alt_color_5");
                    set_foreground_color(calendar_controller.pallette.dark_color, "dark_color");
                    break;
                case DAY_STYLE.AltColor6:
                    set_background_color(calendar_controller.pallette.alt_color_4, "alt_color_6");
                    set_foreground_color(calendar_controller.pallette.dark_color, "dark_color");
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Sets the highlight style of the CalendarDateItem using a DAY_STYLE to define different highlighting behaviours.
        /// </summary>
        /// <param name="style">a DAY_STYLE</param>
        internal void set_highlight_style(DAY_STYLE style)
        {
            // Set calendar_controller
            if (calendar_controller == null)
            {
                calendar_controller = this.GetComponentInParent<CalendarController>();
            }

            switch (style)
            {
                // Generic: highlight style = white_color which doesn't do anything.
                case DAY_STYLE.Generic:
                    highlight_color = Color.clear;
                    break;

                // In Month: highlight style = alt_color_1
                case DAY_STYLE.In_Month:
                    highlight_color = Color.clear;
                    break;

                // Out_Of_Month highlight style = alt_color_1
                case DAY_STYLE.Out_Of_Month:
                    highlight_color = Color.clear;
                    break;

                // Weekend: highlight style = alt_color_1
                case DAY_STYLE.Weekend:
                    highlight_color = Color.clear;
                    break;

                // Invisible: highlight style = clear
                case DAY_STYLE.Invisible:
                    highlight_color = Color.clear;
                    break;

                // Major Event: highlight style = alt_color_2
                case DAY_STYLE.MajorEvent:
                    highlight_color = calendar_controller.pallette.alt_color_2;
                    break;

                // Holiday:  highlight style = alt_color_3
                case DAY_STYLE.Holiday:
                    highlight_color = calendar_controller.pallette.alt_color_3;
                    break;

                case DAY_STYLE.MinorEvent:
                    highlight_color = calendar_controller.pallette.alt_color_4;
                    break;

                case DAY_STYLE.Reminder:
                    highlight_color = calendar_controller.pallette.alt_color_4;
                    break;

                case DAY_STYLE.Notification:
                    highlight_color = calendar_controller.pallette.alt_color_5;
                    break;

                case DAY_STYLE.AltColor1:
                    highlight_color = calendar_controller.pallette.alt_color_1;
                    break;

                case DAY_STYLE.AltColor2:
                    highlight_color = calendar_controller.pallette.alt_color_2;
                    break;

                case DAY_STYLE.AltColor3:
                    highlight_color = calendar_controller.pallette.alt_color_3;
                    break;

                case DAY_STYLE.AltColor4:
                    highlight_color = calendar_controller.pallette.alt_color_4;
                    break;

                case DAY_STYLE.AltColor5:
                    highlight_color = calendar_controller.pallette.alt_color_5;
                    break;

                case DAY_STYLE.AltColor6:
                    highlight_color = calendar_controller.pallette.alt_color_6;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        public void set_background_color(Color c, string tag = null)
        {
            background.color = c;
            if (tag != null)
            {
                bg_color = c;
                gameObject.tag = tag;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        public void set_foreground_color(Color c, string tag = null)
        {
            day_label.color = c;
            if (tag != null)
            {
                this.transform.GetChild(0).gameObject.tag = tag;
            }
        }

        /// <summary>
        /// Sets the text of the day label.
        /// </summary>
        /// <param name="s"></param>
        private void set_label_text(string s)
        {
            day_label.text = s;
        }

        /// <summary>
        /// Sets the text of the day label.
        /// </summary>
        /// <param name="s"></param>
        private void set_label_text(int i)
        {
            day_label.text = i.ToString();
        }

        /// <summary>
        /// Sets the text of the day label.
        /// </summary>
        /// <param name="s"></param>
        private void set_label_text(DateTime d)
        {
            day_label.text = d.Day.ToString();
        }

        #endregion


        private void Start()
        {
            // Get the CalandarController object that created this calendar date item
            calendar_controller = this.GetComponentInParent<CalendarController>();
        }
    }
}