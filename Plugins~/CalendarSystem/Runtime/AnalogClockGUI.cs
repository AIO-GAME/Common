using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace AIO.System.Calendar
{
    public class AnalogClockGUI : MonoBehaviour
    {
        // Constants
        // Number of hours on a clock
        private const short HRS_ON_CLOCK = 12;

        // Number of radians per hour
        private const float RAD_PER_HOUR = 2 * Mathf.PI / HRS_ON_CLOCK;

        // Parameters
        // The TimeController that this clock will display
        public TimeManager time_manager;

        // The hour and minute hand objects.
        public GameObject hour_hand;
        public GameObject minute_hand;

        // The prefab for the numerals
        public GameObject numeral_prefab;

        public void Start()
        {
            if (!time_manager)
            {
                throw new Exception("You must assign a value to the time_manager parameter in the editor for this AnalogClockGUI.");
            }

            if (!hour_hand)
            {
                throw new Exception("You must assign a value to the hour_hand parameter in the editor for this AnalogClockGUI.");
            }

            if (!minute_hand)
            {
                throw new Exception("You must assign a value to the minute_hand parameter in the editor for this AnalogClockGUI.");
            }
        }

        public void Update()
        {
            float min = time_manager.date.Minute;
            float hour = time_manager.date.Hour + min / 60f;

            hour_hand.transform.eulerAngles = new Vector3(0, 0, -hour / 12 * 360f);
            minute_hand.transform.eulerAngles = new Vector3(0, 0, -min / 60f * 360f);
        }

        // List of text clock numerals
        private Text[] clock_numerals;

        internal void build_clock_face()
        {
            // The numeral parent object is the child of the child of this game object.
            Transform numeral_parent = transform.GetChild(0).GetChild(0);

            Transform[] children = new Transform[numeral_parent.childCount];
            int idx = 0;
            foreach (Transform tempTransform in numeral_parent)
            {
                children[idx] = tempTransform;
                idx++;
            }

            //Destroy all numerals
            foreach (Transform child in children)
            {
                SafeDestroy(child.gameObject);
            }


            clock_numerals = new Text[HRS_ON_CLOCK];

            // Generate clock numerals
            for (int i = 0; i < HRS_ON_CLOCK; i++)
            {
                // Create new numeral prefab
                GameObject g = Instantiate(numeral_prefab);

                // Set the name
                g.name = (i + 1).ToString();

                // Set the parent transform
                g.transform.SetParent(numeral_parent);

                // Get the text component
                Text t = g.GetComponent<Text>();

                // Set the text
                t.text = g.name;

                float clock_radius = GetComponent<RectTransform>().sizeDelta.x / 2 - g.GetComponent<RectTransform>().sizeDelta.x / 4;

                t.rectTransform.anchoredPosition = new Vector2(Mathf.Cos(-(i + 1) * RAD_PER_HOUR + Mathf.PI / 2) * clock_radius, Mathf.Sin(-(i + 1) * RAD_PER_HOUR + Mathf.PI / 2) * 27.5f);

                clock_numerals[i] = t;
            }
        }

        public static T SafeDestroy<T>(T obj) where T : Object
        {
            if (Application.isEditor)
                DestroyImmediate(obj);
            else
                Destroy(obj);

            return null;
        }
    }
}