#region

using System;
using UnityEditor;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    public partial class GELayout
    {
        public static string IsChangeHorizontal(GTContent label, string current, string currentSource,
                                                float     width = 150)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                if (!current.Equals(currentSource))
                {
                    label.Content.text = $"{label.Content.text}:{currentSource}";
                    if (GUILayout.Button(label.Content, new GUIStyle("ToggleMixed"), GUILayout.Width(width)))
                        return currentSource;
                }
                else
                {
                    EditorGUILayout.LabelField(label.Content, GUILayout.Width(width));
                }

                return EditorGUILayout.TextField(current);
            }
        }

        public static float IsChangeHorizontal(GTContent label, float current, float currentSource, float width = 150)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                if (!current.Equals(currentSource))
                {
                    label.Content.text = $"{label.Content.text}:{currentSource}";
                    if (GUILayout.Button(label.Content, new GUIStyle("ToggleMixed"), GUILayout.Width(width)))
                        return currentSource;
                }
                else
                {
                    EditorGUILayout.LabelField(label.Content, GUILayout.Width(width));
                }

                return EditorGUILayout.FloatField(current, GUILayout.ExpandWidth(true));
            }
        }

        public static int IsChangeHorizontal(GTContent label,    int   current, int currentSource, int minvalue,
                                             int       maxvalue, float width = 150)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                if (!current.Equals(currentSource))
                {
                    label.Content.text = $"{label.Content.text}:{currentSource}";
                    if (GUILayout.Button(label.Content, new GUIStyle("ToggleMixed"), GUILayout.Width(width)))
                        return currentSource;
                }
                else
                {
                    EditorGUILayout.LabelField(label.Content, GUILayout.Width(width));
                }

                return EditorGUILayout.IntSlider(current, minvalue, maxvalue, GUILayout.ExpandWidth(true));
            }
        }

        public static float IsChangeHorizontal(GTContent label,    float current, float currentSource, float minvalue,
                                               float     maxvalue, float width = 150)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                if (!current.Equals(currentSource))
                {
                    label.Content.text = $"{label.Content.text}:{currentSource}";
                    if (GUILayout.Button(label.Content, new GUIStyle("ToggleMixed"), GUILayout.Width(width)))
                        return currentSource;
                }
                else
                {
                    EditorGUILayout.LabelField(label.Content, GUILayout.Width(width));
                }

                return EditorGUILayout.Slider(current, minvalue, maxvalue, GUILayout.ExpandWidth(true));
            }
        }

        public static T IsChangeHorizontal<T>(GTContent label, T current, T currentSource, float width = 150)
        where T : Enum
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                if (!current.Equals(currentSource))
                {
                    label.Content.text = $"{label.Content.text}:{currentSource}";
                    if (GUILayout.Button(label.Content, new GUIStyle("ToggleMixed"), GUILayout.Width(width)))
                        return currentSource;
                }
                else
                {
                    EditorGUILayout.LabelField(label.Content, GUILayout.Width(width));
                }

                return (T)EditorGUILayout.EnumPopup(current, GUILayout.ExpandWidth(true));
            }
        }

        public static int IsChangeHorizontal(GTContent label, int current, int currentSource, float width = 150)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                if (!current.Equals(currentSource))
                {
                    label.Content.text = $"{label.Content.text}:{currentSource}";
                    if (GUILayout.Button(label.Content, new GUIStyle("ToggleMixed"), GUILayout.Width(width)))
                        return currentSource;
                }
                else
                {
                    EditorGUILayout.LabelField(label.Content, GUILayout.Width(width));
                }

                return EditorGUILayout.IntField(current, GUILayout.ExpandWidth(true));
            }
        }
    }
}