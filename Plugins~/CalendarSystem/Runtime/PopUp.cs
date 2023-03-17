using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AIO.System.Calendar
{
    public class PopUp : MonoBehaviour
    {
        private static int popup_offset = 1;

        Button popup_button;

        Text popup_text;
        Text popup_button_text;

        UnityAction _func;

        public UnityAction main_button_func
        {
            get { return _func; }
            set
            {
                _func = value;
                popup_button.onClick.RemoveAllListeners();
                popup_button.onClick.AddListener(_func);
            }
        }

        private void Start()
        {
            init_vars();
        }

        public void init_vars()
        {
            popup_button = GetComponentInChildren<Button>();
            popup_text = GetComponentInChildren<Text>();
            popup_button_text = popup_button.gameObject.GetComponentInChildren<Text>();
        }

        public void add_action(UnityAction func)
        {
            popup_button.onClick.AddListener(() => popup_offset = popup_offset - 1);
            if (func == null)
            {
                popup_button.onClick.AddListener(() => Destroy(gameObject));
            }
            else
            {
                popup_button.onClick.AddListener(func);
            }
        }

        /// <summary>
        /// Creates a simple popup with text and a button.
        /// </summary>
        /// <param name="PopUpPrefab">The popup prefab to create</param>
        /// <param name="parent">The parent of the popup (Usually a Canvas)</param>
        /// <param name="text">The text prompt in the popup</param>
        /// <param name="button_text">The text prompt in the button</param>
        /// <param name="func">The function to fire when the button is clicked</param>
        /// <returns></returns>
        public static PopUp Create_Popup(GameObject PopUpPrefab, GameObject parent, string text, string button_text, UnityAction func)
        {
            GameObject g = Instantiate(PopUpPrefab, parent.transform);

            g.GetComponent<RectTransform>().localPosition = g.GetComponent<RectTransform>().localPosition + new Vector3(10, -10, 0) * (popup_offset % 12);
            popup_offset += 1;

            PopUp p = g.GetComponent<PopUp>();

            p.init_vars();

            p.popup_text.text = text;
            p.popup_button_text.text = button_text;

            p.main_button_func = func;

            return p;
        }
    }
}