using UnityEngine;
using UnityEngine.UI;

namespace AIO.System.Calendar
{
    public class TimeController : MonoBehaviour
    {
        /// <summary>
        /// Gameobject that has a TimeManager component
        /// </summary>
        public GameObject time_manager_game_object;

        /// <summary>
        /// The TimeManager should be a component of  the time_manager_game_object
        /// </summary>
        private TimeManager t;

        /// <summary>
        ///  Text object to display the time multiplier
        /// </summary>
        public Text multiplier_label;

        /// <summary>
        /// Text object to display the time multiplier's one second equivalent.
        /// </summary>
        public Text one_second_equivalent_label;

        private void Start()
        {
            // Get the TimeManager compnonent from time_manager_game_object
            t = time_manager_game_object.GetComponent<TimeManager>();

            if (!t)
            {
                // Throw exception if there is no TimeManager component
                throw new MissingComponentException("No TimeManager component found on time_manager_game_object!");
            }
        }

        // --- GUI Functions ---

        #region

        /// <summary>
        /// Updates the gui elements of the time controller.
        /// </summary>
        public void update_gui_elements(TimeManager t)
        {
            // Set the multiplier label text
            multiplier_label.text = t.game_time_multiplier.ToString("N0") + "x";

            // Set the one second equivalent label text
            one_second_equivalent_label.text = multiplier_to_one_second_equivalent(t.game_time_multiplier);
        }

        /// <summary>
        /// Generates a string that calculates the one second equivalent of the current game time multiplier. ie, "1s = 2 day"
        /// </summary>
        /// <param name="multiplier"></param>
        /// <returns></returns>
        private string multiplier_to_one_second_equivalent(float multiplier)
        {
            // If multipler is greater than 1s = 1 day
            if (multiplier >= 60 * 60 * 24)
            {
                return ("1s = " + (multiplier / 60 / 60 / 24).ToString() + " day");
            }
            // If multipler is greater than 1s = 1 hr
            else if (multiplier >= 60 * 60)
            {
                return ("1s = " + (multiplier / 60 / 60).ToString() + " hr");
            }
            else if (multiplier >= 60)
            {
                // If multipler is greater than 1s = 1 min
                return ("1s = " + (multiplier / 60).ToString() + " min");
            }

            return "1s = " + multiplier.ToString() + " s";
        }

        /// <summary>
        /// Alternately toggles the visibility of the TimeController window and the minimized icon.
        /// </summary>
        public void toggle_visibility()
        {
            // b = if the TimeController is visible
            bool b = !gameObject.transform.GetChild(0).gameObject.activeSelf;

            // Toggle the TimeController object
            gameObject.transform.GetChild(0).gameObject.SetActive(b);

            // Toggle the minimized object
            gameObject.transform.GetChild(1).gameObject.SetActive(!b);
        }

        #endregion
    }
}