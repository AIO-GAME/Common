using UnityEngine;
using UnityEngine.UI;

namespace AIO.System.Calendar
{
    [ExecuteInEditMode]
    public class ColorPalette : MonoBehaviour
    {
        [SerializeField] public Color light_color = new Color(1, 1, 1);
        [SerializeField] public Color white_color = new Color(1, 1, 1);
        [SerializeField] public Color dark_color = new Color(0, 0, 0);
        [SerializeField] public Color alt_color_1 = new Color(1, 0, 0);
        [SerializeField] public Color alt_color_2 = new Color(0, 1, 0);
        [SerializeField] public Color alt_color_3 = new Color();
        [SerializeField] public Color alt_color_4 = new Color();
        [SerializeField] public Color alt_color_5 = new Color();
        [SerializeField] public Color alt_color_6 = new Color();


        private void Awake()
        {
        }

        public void UpdateUIPallette()
        {
            //SetImageAndTextColorByTag("light_color", light_color);
            //SetImageAndTextColorByTag("white_color", white_color);
            //SetImageAndTextColorByTag("dark_color", dark_color);
            //SetImageAndTextColorByTag("alt_color_1", alt_color_1);
            //SetImageAndTextColorByTag("alt_color_2", alt_color_2);
            //SetImageAndTextColorByTag("alt_color_3", alt_color_3);
            //SetImageAndTextColorByTag("alt_color_4", alt_color_4);
            //SetImageAndTextColorByTag("alt_color_5", alt_color_5);
            //SetImageAndTextColorByTag("alt_color_6", alt_color_6);
        }

        void SetImageAndTextColorByTag(string tag, Color c)
        {
            GameObject[] all_with_tag = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject g in all_with_tag)
            {
                Image i = g.GetComponent<Image>();
                if (i)
                {
                    i.color = c;
                }

                Text t = g.GetComponent<Text>();
                if (t)
                {
                    t.color = c;
                }
            }
        }

        public Color get_by_index(int i)
        {
            i = i % 6;

            switch (i)
            {
                case (0):
                    return alt_color_6;
                case (1):
                    return alt_color_1;
                case (2):
                    return alt_color_2;
                case (3):
                    return alt_color_3;
                case (4):
                    return alt_color_4;
                case (5):
                    return alt_color_5;
            }

            return Color.clear;
        }
    }
}