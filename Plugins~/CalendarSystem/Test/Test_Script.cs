using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AIO.System.Calendar.Test
{
    public class Test_Script : MonoBehaviour
    {
        /// <summary>
        /// This function is called at lunchtime every day. Remember to eat lunch!
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">Event arguments. I have ignored them and passed data through the TimeManager MonoBehaviour. You could implement them if you want to pass custom arguments.</param>
        public void lunchtime(object sender, EventArgs e)
        {
            // Calculate the lunchtime.

            // This datetime represents the actual ticked time irrespective of date variable, which is controlled by Time.deltatime.

            // Print what we are having for lunch
            String lunch_str = "It is lunch, time for";

            // Get a random number
            int rand = Random.Range(0, 3);
            switch (rand)
            {
                case (0):
                {
                    lunch_str += " fish and chips.";
                    break;
                }
                case (1):
                {
                    lunch_str += " a burger and fries.";
                    break;
                }
                case (2):
                {
                    lunch_str += " a healthy salad.";
                    break;
                }
                case (3):
                {
                    lunch_str += " a coffee and a slice of cake.";
                    break;
                }
            }

            Debug.Log(lunch_str);
        }

        /// <summary>
        /// This function ticks on friday. It exlaims relief on the last day of the work week!
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">Event arguments. I have ignored them and passed data through the TimeManager MonoBehaviour. You could implement them if you want to pass custom arguments.</param>
        public void tgif(object sender, EventArgs e)
        {
            // Phew!
            Debug.Log("Thank goodness its Friday!");
        }
    }
}