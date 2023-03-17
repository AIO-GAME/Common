using System;
using UnityEngine;
using UnityEngine.UI;

namespace AIO.System.Calendar
{
    public class DigitalClockGUI : MonoBehaviour
    {
        // The TimeController that this clock will display
        public TimeManager time_manager;

        // The UI Text Objects
        public Text time_text;
        public Text date_text;

        // Start is called before the first frame update
        void Start()
        {
            if (!time_manager)
            {
                throw new Exception("You must assign a value to the time_manager parameter in the editor for this DigitalClockGUI.");
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Set the time text
            time_text.text = time_manager.date.ToString("t");

            // Set the date text
            date_text.text = time_manager.date.ToString("D");
        }
    }
}