/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-08-09
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIO.UEditor
{
    public static partial class GELayout
    {
        public static void Field<T>(string label,
            IList<T> list,
            Action<T> cb,
            GUIStyle labelStyle = null,
            GUIStyle contextStyle = null
        ) where T : new()
        {
            Vertical(() =>
            {
                Horizontal(() =>
                {
                    Label(label, labelStyle ?? GEStyle.DropzoneStyle);
                    if (Button("+", GTOption.Width(20)))
                    {
                        list.Add(new T());
                    }
                });

                for (var i = list.Count - 1; i >= 0; i--)
                {
                    var i1 = list.Count - 1 - i;
                    Horizontal(() =>
                    {
                        Label((i1 + 1).ToString("00"), GTOption.Width(20));
                        cb?.Invoke(list[i1]);
                        if (Button("-", GTOption.Width(20)))
                        {
                            list.RemoveAt(i1);
                        }
                    });
                }
            }, contextStyle ?? GEStyle.DDHeaderStyle);
        }
    }
}