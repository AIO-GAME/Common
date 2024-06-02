using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AIO.RainbowCore.RList.Editor
{
    internal class ReorderableList
    {
        public enum ElementDisplayType
        {
            Auto,
            Expandable,
            SingleLine
        }

        public delegate void DrawHeaderDelegate(Rect rect, GUIContent label);

        public delegate void DrawFooterDelegate(Rect rect);

        public delegate void DrawElementDelegate(Rect               rect,
                                                 SerializedProperty element,
                                                 GUIContent         label,
                                                 bool               selected,
                                                 bool               focused);

        public delegate void ActionDelegate(ReorderableList list);

        public delegate bool ActionBoolDelegate(ReorderableList list);

        public delegate void AddDropdownDelegate(Rect buttonRect, ReorderableList list);

        public delegate Object DragDropReferenceDelegate(Object[]        references,
                                                         ReorderableList list);

        public delegate void DragDropAppendDelegate(Object reference, ReorderableList list);

        public delegate float GetElementHeightDelegate(SerializedProperty element);

        public delegate float GetElementsHeightDelegate(ReorderableList list);

        public delegate string GetElementNameDelegate(SerializedProperty element);

        public delegate GUIContent GetElementLabelDelegate(SerializedProperty element);

        public delegate void SurrogateCallback(SerializedProperty element,
                                               Object             objectReference,
                                               ReorderableList    list);

        private static class Style
        {
            internal const string PAGE_INFO_FORMAT = "{0} / {1}";

            internal static GUIContent iconToolbarPlus;

            internal static GUIContent iconToolbarPlusMore;

            internal static GUIContent iconToolbarMinus;

            internal static GUIContent iconPagePrev;

            internal static GUIContent iconPageNext;

            internal static GUIContent iconPagePopup;

            internal static GUIStyle paginationText;

            internal static GUIStyle pageSizeTextField;

            internal static GUIStyle draggingHandle;

            internal static GUIStyle headerBackground;

            internal static GUIStyle footerBackground;

            internal static GUIStyle paginationHeader;

            internal static GUIStyle boxBackground;

            internal static GUIStyle preButton;

            internal static GUIStyle elementBackground;

            internal static GUIStyle verticalLabel;

            internal static GUIContent expandButton;

            internal static GUIContent collapseButton;

            internal static GUIContent sortAscending;

            internal static GUIContent sortDescending;

            internal static GUIContent listIcon;

            private static string GetIconName(string name)
            {
                if (!iconNames.ContainsKey(name)) return "d_console.erroricon";
                int.TryParse(Application.unityVersion.Split('.')[0], out var ver);
                return iconNames[name].ContainsKey(ver) ? iconNames[name][ver] : "d_console.erroricon";
            }

            static Style()
            {
                iconToolbarPlus     = EditorGUIUtility.TrIconContent("Toolbar Plus", "| Add to list");
                iconToolbarPlusMore = EditorGUIUtility.TrIconContent("Toolbar Plus More", "| Choose to add to list");
                iconToolbarMinus    = EditorGUIUtility.TrIconContent("Toolbar Minus", "| Remove selection from list");
                iconPagePrev        = EditorGUIUtility.TrIconContent("Animation.PrevKey", "| Previous page");
                iconPageNext        = EditorGUIUtility.TrIconContent("Animation.NextKey", "| Next page");
                iconPagePopup       = EditorGUIUtility.TrIconContent("UnityEditor.HierarchyWindow", "| Select page");
                paginationText = new GUIStyle
                {
                    margin   = new RectOffset(2, 2, 0, 0),
                    fontSize = EditorStyles.miniTextField.fontSize,
                    font     = EditorStyles.miniFont,
                    normal =
                    {
                        textColor = EditorStyles.miniTextField.normal.textColor
                    },
                    alignment = TextAnchor.UpperLeft,
                    clipping  = TextClipping.Clip
                };
                pageSizeTextField = new GUIStyle("RL Footer")
                {
                    alignment     = TextAnchor.MiddleLeft,
                    clipping      = TextClipping.Clip,
                    fixedHeight   = 0f,
                    padding       = new RectOffset(3, 0, 0, 0),
                    overflow      = new RectOffset(0, 0, -2, -3),
                    contentOffset = new Vector2(0f, -1f),
                    font          = EditorStyles.miniFont,
                    fontSize      = EditorStyles.miniTextField.fontSize,
                    fontStyle     = FontStyle.Normal,
                    wordWrap      = false,
                    normal =
                    {
                        textColor = EditorStyles.miniTextField.normal.textColor
                    }
                };
                draggingHandle   = new GUIStyle("RL DragHandle");
                headerBackground = new GUIStyle("RL Header");
                footerBackground = new GUIStyle("RL Footer");
                paginationHeader = new GUIStyle("RL Element")
                {
                    border = new RectOffset(2, 3, 2, 3)
                };
                elementBackground = new GUIStyle("RL Element")
                {
                    border = new RectOffset(2, 3, 2, 3)
                };
                verticalLabel = new GUIStyle(EditorStyles.label)
                {
                    alignment     = TextAnchor.UpperLeft,
                    contentOffset = new Vector2(10f, 3f)
                };
                boxBackground = new GUIStyle("RL Background")
                {
                    border = new RectOffset(6, 3, 3, 6)
                };
                preButton = new GUIStyle("RL FooterButton");

                expandButton         = EditorGUIUtility.TrIconContent(GetIconName(nameof(expandButton)));
                expandButton.tooltip = "Expand All Elements";

                collapseButton         = EditorGUIUtility.TrIconContent(GetIconName(nameof(collapseButton)));
                collapseButton.tooltip = "Collapse All Elements";

                sortAscending          = EditorGUIUtility.TrIconContent("align_vertically_bottom");
                sortAscending.tooltip  = "Sort Ascending";
                sortDescending         = EditorGUIUtility.TrIconContent("align_vertically_top");
                sortDescending.tooltip = "Sort Descending";
                listIcon               = EditorGUIUtility.TrIconContent("align_horizontally_right");
            }
        }

        private static readonly Dictionary<string, Dictionary<int, string>> iconNames =
            new Dictionary<string, Dictionary<int, string>>
            {
                {
                    "expandButton", new Dictionary<int, string>
                    {
                        { 2023, "d_winbtn_mac_inact" },
                        { 2020, "d_winbtn_win_max" },
                        { 0, "winbtn_win_max" }
                    }
                },
                {
                    "collapseButton", new Dictionary<int, string>
                    {
                        { 2023, "d_winbtn_win_min" },
                        { 2020, "d_winbtn_win_restore" },
                        { 0, "winbtn_win_max" }
                    }
                },
            };

        private struct DragList
        {
            private DragElement[] elements;

            internal int StartIndex { get; private set; }

            internal int Length { get; private set; }

            internal DragElement[] Elements
            {
                get => elements;
                set => elements = value;
            }

            internal DragElement this[int index]
            {
                get => elements[index];
                set => elements[index] = value;
            }

            internal DragList(int length)
            {
                Length     = length;
                StartIndex = 0;
                elements   = new DragElement[length];
            }

            internal void Resize(int start, int length)
            {
                StartIndex = start;
                Length     = length;
                if (elements.Length != length)
                {
                    Array.Resize(ref elements, length);
                }
            }

            internal void SortByIndex()
            {
                Array.Sort(elements, delegate(DragElement a, DragElement b)
                {
                    if (b.selected) return !a.selected ? 1 : a.startIndex.CompareTo(b.startIndex);
                    return a.selected ? -1 : a.startIndex.CompareTo(b.startIndex);
                });
            }

            internal void RecordState()
            {
                for (var i = 0; i < Length; i++)
                {
                    elements[i].RecordState();
                }
            }

            internal void RestoreState(SerializedProperty list)
            {
                for (var i = 0; i < Length; i++)
                {
                    elements[i].RestoreState(list.GetArrayElementAtIndex(i + StartIndex));
                }
            }

            internal void SortByPosition() { Array.Sort(elements, (a, b) => a.desiredRect.center.y.CompareTo(b.desiredRect.center.y)); }

            internal int GetIndexFromSelection(int index) { return Array.FindIndex(elements, t => t.startIndex == index); }
        }

        private struct DragElement
        {
            internal SerializedProperty property;

            internal int startIndex;

            internal float dragOffset;

            internal bool selected;

            internal Rect rect;

            internal Rect desiredRect;

            private bool isExpanded;

            private Dictionary<int, bool> states;

            internal bool Overlaps(Rect value, int index, int direction)
            {
                if (direction < 0 && index < startIndex)
                {
                    return desiredRect.yMin < value.center.y;
                }

                if (direction > 0 && index > startIndex)
                {
                    return desiredRect.yMax > value.center.y;
                }

                return false;
            }

            internal void RecordState()
            {
                states     = new Dictionary<int, bool>();
                isExpanded = property.isExpanded;
                Iterate(this, property,
                        delegate(DragElement e, SerializedProperty p, int index) { e.states[index] = p.isExpanded; });
            }

            internal void RestoreState(SerializedProperty value)
            {
                value.isExpanded = isExpanded;
                Iterate(this, value,
                        delegate(DragElement e, SerializedProperty p, int index) { p.isExpanded = e.states[index]; });
            }

            private static void Iterate(DragElement                                  element,
                                        SerializedProperty                           property,
                                        Action<DragElement, SerializedProperty, int> action)
            {
                var serializedProperty = property.Copy();
                var endProperty        = serializedProperty.GetEndProperty();
                var num                = 0;
                while (serializedProperty.NextVisible(enterChildren: true) &&
                       !SerializedProperty.EqualContents(serializedProperty, endProperty))
                {
                    if (!serializedProperty.hasVisibleChildren) continue;
                    action(element, serializedProperty, num);
                    num++;
                }
            }
        }

        private class SlideGroup
        {
            private Dictionary<int, Rect> animIDs;

            public SlideGroup() { animIDs = new Dictionary<int, Rect>(); }

            public Rect GetRect(int id, Rect r, float easing)
            {
                if (Event.current.type != EventType.Repaint)
                {
                    return r;
                }

                if (!animIDs.ContainsKey(id))
                {
                    animIDs.Add(id, r);
                    return r;
                }

                var rect = animIDs[id];
                if (rect.y == r.y) return r;

                var num  = r.y - rect.y;
                var num2 = Mathf.Abs(num);
                if (num2 > rect.height * 2f)
                {
                    r.y = num > 0f ? r.y - rect.height : r.y + rect.height;
                }
                else if (num2 > 0.5)
                {
                    r.y = Mathf.Lerp(rect.y, r.y, easing);
                }

                animIDs[id] = r;
                HandleUtility.Repaint();

                return r;
            }

            public Rect SetRect(int id, Rect rect)
            {
                animIDs[id] = rect;
                return rect;
            }
        }

        private struct Pagination
        {
            internal bool enabled;

            internal int pageSize;

            internal int page;

            internal bool usePagination
            {
                get
                {
                    if (enabled)
                    {
                        return pageSize > 0;
                    }

                    return false;
                }
            }

            internal int GetVisibleLength(int total)
            {
                if (GetVisibleRange(total, out var start, out var end))
                {
                    return end - start;
                }

                return total;
            }

            internal int GetPageForIndex(int index) { return !usePagination ? 0 : Mathf.FloorToInt(index / (float)pageSize); }

            internal int GetPageCount(int total) { return !usePagination ? 1 : Mathf.CeilToInt(total / (float)pageSize); }

            internal bool GetVisibleRange(int total, out int start, out int end)
            {
                if (usePagination)
                {
                    var num = pageSize;
                    start = Mathf.Clamp(page * num, 0, total - 1);
                    end   = Mathf.Min(start + num, total);
                    return true;
                }

                start = 0;
                end   = total;
                return false;
            }
        }

        private class ListSelection : IEnumerable<int>
        {
            private List<int> indexes;

            internal int? firstSelected;

            public int First
            {
                get
                {
                    if (indexes.Count <= 0)
                    {
                        return -1;
                    }

                    return indexes[0];
                }
            }

            public int Last
            {
                get
                {
                    if (indexes.Count <= 0)
                    {
                        return -1;
                    }

                    return indexes[indexes.Count - 1];
                }
            }

            public int Length => indexes.Count;

            public int this[int index]
            {
                get => indexes[index];
                set
                {
                    var num = indexes[index];
                    indexes[index] = value;
                    if (num == firstSelected)
                    {
                        firstSelected = value;
                    }
                }
            }

            public ListSelection() { indexes = new List<int>(); }

            public ListSelection(IEnumerable<int> indexes) { this.indexes = new List<int>(indexes); }

            public bool Contains(int index) { return indexes.Contains(index); }

            public void Clear()
            {
                indexes.Clear();
                firstSelected = null;
            }

            public void SelectWhenNoAction(int index, Event evt)
            {
                if (!EditorGUI.actionKey && !evt.shift)
                {
                    Select(index);
                }
            }

            public void Select(int index)
            {
                indexes.Clear();
                indexes.Add(index);
                firstSelected = index;
            }

            public void Remove(int index)
            {
                if (indexes.Contains(index))
                {
                    indexes.Remove(index);
                }
            }

            public void AppendWithAction(int index, Event evt)
            {
                if (EditorGUI.actionKey)
                {
                    if (Contains(index))
                    {
                        Remove(index);
                        return;
                    }

                    Append(index);
                    firstSelected = index;
                }
                else if (evt.shift && indexes.Count > 0 && firstSelected.HasValue)
                {
                    indexes.Clear();
                    AppendRange(firstSelected.Value, index);
                }
                else if (!Contains(index))
                {
                    Select(index);
                }
            }

            public void Sort()
            {
                if (indexes.Count > 0)
                {
                    indexes.Sort();
                }
            }

            public void Sort(Comparison<int> comparison)
            {
                if (indexes.Count > 0)
                {
                    indexes.Sort(comparison);
                }
            }

            public int[] ToArray() { return indexes.ToArray(); }

            public ListSelection Clone()
            {
                return new ListSelection(ToArray())
                {
                    firstSelected = firstSelected
                };
            }

            internal void Trim(int min, int max)
            {
                var num = indexes.Count;
                while (--num > -1)
                {
                    var num2 = indexes[num];
                    if (num2 >= min && num2 < max) continue;
                    if (num2 == firstSelected && num > 0)
                        firstSelected = indexes[num - 1];
                    indexes.RemoveAt(num);
                }
            }

            internal bool CanRevert(SerializedProperty list)
            {
                if (list.serializedObject.targetObjects.Length != 1) return false;
                for (var i = 0; i < Length; i++)
                {
                    if (list.GetArrayElementAtIndex(this[i]).isInstantiatedPrefab)
                    {
                        return true;
                    }
                }

                return false;
            }

            internal void RevertValues(object userData)
            {
                if (!(userData is SerializedProperty serializedProperty)) return;
                for (var i = 0; i < Length; i++)
                {
                    var arrayElementAtIndex = serializedProperty.GetArrayElementAtIndex(this[i]);
                    if (arrayElementAtIndex.isInstantiatedPrefab)
                    {
                        arrayElementAtIndex.prefabOverride = false;
                    }
                }

                serializedProperty.serializedObject.ApplyModifiedProperties();
                serializedProperty.serializedObject.Update();
                HandleUtility.Repaint();
            }

            internal void Duplicate(SerializedProperty list)
            {
                var num = 0;
                for (var i = 0; i < Length; i++)
                {
                    this[i] += num;
                    list.GetArrayElementAtIndex(this[i]).DuplicateCommand();
                    list.serializedObject.ApplyModifiedProperties();
                    list.serializedObject.Update();
                    num++;
                }

                HandleUtility.Repaint();
            }

            internal void Delete(SerializedProperty list)
            {
                Sort();
                var num = Length;
                while (--num > -1)
                {
                    list.GetArrayElementAtIndex(this[num]).DeleteCommand();
                }

                Clear();
                list.serializedObject.ApplyModifiedProperties();
                list.serializedObject.Update();
                HandleUtility.Repaint();
            }

            private void Append(int index)
            {
                if (index >= 0 && !indexes.Contains(index))
                {
                    indexes.Add(index);
                }
            }

            private void AppendRange(int from, int to)
            {
                int num = (int)Mathf.Sign(to - from);
                if (num != 0)
                {
                    for (int i = from; i != to; i += num)
                    {
                        Append(i);
                    }
                }

                Append(to);
            }

            public IEnumerator<int> GetEnumerator() { return ((IEnumerable<int>)indexes).GetEnumerator(); }

            IEnumerator IEnumerable.GetEnumerator() { return ((IEnumerable<int>)indexes).GetEnumerator(); }
        }

        private static class ListSort
        {
            private delegate int SortComparision(SerializedProperty p1, SerializedProperty p2);

            internal static void SortOnProperty(SerializedProperty list,
                                                int                length,
                                                bool               descending,
                                                string             propertyName)
            {
                BubbleSort(list, length, delegate(SerializedProperty p1, SerializedProperty p2)
                {
                    SerializedProperty serializedProperty  = p1.FindPropertyRelative(propertyName);
                    SerializedProperty serializedProperty2 = p2.FindPropertyRelative(propertyName);
                    if (serializedProperty != null && serializedProperty2 != null &&
                        serializedProperty.propertyType == serializedProperty2.propertyType)
                    {
                        int num = Compare(serializedProperty, serializedProperty2, descending,
                                          serializedProperty.propertyType);
                        if (!descending)
                        {
                            return num;
                        }

                        return -num;
                    }

                    return 0;
                });
            }

            internal static void SortOnType(SerializedProperty     list,
                                            int                    length,
                                            bool                   descending,
                                            SerializedPropertyType type)
            {
                BubbleSort(list, length, delegate(SerializedProperty p1, SerializedProperty p2)
                {
                    int num = Compare(p1, p2, descending, type);
                    return (!descending) ? num : (-num);
                });
            }

            private static void BubbleSort(SerializedProperty list, int length, SortComparision comparision)
            {
                for (int i = 0; i < length; i++)
                {
                    SerializedProperty arrayElementAtIndex = list.GetArrayElementAtIndex(i);
                    for (int j = i + 1; j < length; j++)
                    {
                        SerializedProperty arrayElementAtIndex2 = list.GetArrayElementAtIndex(j);
                        if (comparision(arrayElementAtIndex, arrayElementAtIndex2) > 0)
                        {
                            list.MoveArrayElement(j, i);
                        }
                    }
                }
            }

            private static int Compare(SerializedProperty     p1,
                                       SerializedProperty     p2,
                                       bool                   descending,
                                       SerializedPropertyType type)
            {
                if (p1 == null || p2 == null)
                {
                    return 0;
                }

                switch (type)
                {
                    case SerializedPropertyType.Boolean:
                        return p1.boolValue.CompareTo(p2.boolValue);
                    case SerializedPropertyType.Integer:
                    case SerializedPropertyType.LayerMask:
                    case SerializedPropertyType.Enum:
                    case SerializedPropertyType.Character:
                        return p1.longValue.CompareTo(p2.longValue);
                    case SerializedPropertyType.Color:
                        return p1.colorValue.grayscale.CompareTo(p2.colorValue.grayscale);
                    case SerializedPropertyType.ExposedReference:
                        return CompareObjects(p1.exposedReferenceValue, p2.exposedReferenceValue, descending);
                    case SerializedPropertyType.Float:
                        return p1.doubleValue.CompareTo(p2.doubleValue);
                    case SerializedPropertyType.ObjectReference:
                        return CompareObjects(p1.objectReferenceValue, p2.objectReferenceValue, descending);
                    case SerializedPropertyType.String:
                        return p1.stringValue.CompareTo(p2.stringValue);
                    default:
                        return 0;
                }
            }

            private static int CompareObjects(Object obj1, Object obj2, bool descending)
            {
                if ((bool)obj1 && (bool)obj2)
                {
                    return obj1.name.CompareTo(obj2.name);
                }

                if ((bool)obj1)
                {
                    if (!descending)
                    {
                        return -1;
                    }

                    return 1;
                }

                if (!descending)
                {
                    return 1;
                }

                return -1;
            }
        }

        public struct Surrogate
        {
            public Type type;

            public bool exactType;

            public SurrogateCallback callback;

            internal bool enabled;

            public bool HasType
            {
                get
                {
                    if (enabled)
                    {
                        return type != null;
                    }

                    return false;
                }
            }

            public Surrogate(Type type)
                : this(type, null) { }

            public Surrogate(Type type, SurrogateCallback callback)
            {
                this.type     = type;
                this.callback = callback;
                enabled       = true;
                exactType     = false;
            }

            public void Invoke(SerializedProperty element, Object objectReference, ReorderableList list)
            {
                if (element != null && callback != null)
                {
                    callback(element, objectReference, list);
                }
            }
        }

        private class InvalidListException : InvalidOperationException
        {
            public InvalidListException()
                : base("ReorderableList serializedProperty must be an array") { }
        }

        private class MissingListException : ArgumentNullException
        {
            public MissingListException() : base($"ReorderableList serializedProperty is null") { }
        }

        private static class Internals
        {
            private static MethodInfo dragDropValidation;

            private static object[] dragDropValidationParams;

            private static MethodInfo appendDragDrop;

            private static object[] appendDragDropParams;

            static Internals()
            {
                dragDropValidation = Type.GetType("UnityEditor.EditorGUI, UnityEditor")
                                         ?
                                         .GetMethod("ValidateObjectFieldAssignment", BindingFlags.Static | BindingFlags.NonPublic);
                appendDragDrop = typeof(SerializedProperty).GetMethod("AppendFoldoutPPtrValue",
                                                                      BindingFlags.Instance | BindingFlags.NonPublic);
            }

            internal static Object ValidateObjectDragAndDrop(Object[]           references,
                                                             SerializedProperty property,
                                                             Type               type,
                                                             bool               exactType)
            {
                dragDropValidationParams    = GetParams(ref dragDropValidationParams, 3);
                dragDropValidationParams[0] = references;
                dragDropValidationParams[1] = type;
                dragDropValidationParams[2] = property;
                return dragDropValidation.Invoke(null, dragDropValidationParams) as Object;
            }

            internal static void AppendDragAndDropValue(Object obj, SerializedProperty list)
            {
                appendDragDropParams    = GetParams(ref appendDragDropParams, 1);
                appendDragDropParams[0] = obj;
                appendDragDrop.Invoke(list, appendDragDropParams);
            }

            private static object[] GetParams(ref object[] parameters, int count)
            {
                if (parameters == null)
                {
                    parameters = new object[count];
                }

                return parameters;
            }
        }

        private const float ELEMENT_EDGE_TOP = 1f;

        private const float ELEMENT_EDGE_BOT = 3f;

        private const float ELEMENT_HEIGHT_OFFSET = 4f;

        private static int selectionHash = "ReorderableListSelection".GetHashCode();

        private static int dragAndDropHash = "ReorderableListDragAndDrop".GetHashCode();

        private const string EMPTY_LABEL = "List is Empty";

        private const string ARRAY_ERROR = "{0} is not an Array!";

        public bool canAdd;

        public bool canRemove;

        public bool draggable;

        public bool sortable;

        public bool expandable;

        public bool multipleSelection;

        public GUIContent label;

        public float headerHeight;

        public float paginationHeight;

        public float footerHeight;

        public float slideEasing;

        public float verticalSpacing;

        public bool showDefaultBackground;

        public ElementDisplayType elementDisplayType;

        public string elementNameProperty;

        public string elementNameOverride;

        public bool elementLabels;

        public Texture elementIcon;

        public Surrogate surrogate;

        internal readonly int id;

        private SerializedProperty list;

        private int controlID = -1;

        private Rect[] elementRects;

        private GUIContent elementLabel;

        private GUIContent pageInfoContent;

        private GUIContent pageSizeContent;

        private ListSelection selection;

        private SlideGroup slideGroup;

        private int pressIndex;

        private bool dragging;

        private float pressPosition;

        private float dragPosition;

        private int dragDirection;

        private DragList dragList;

        private ListSelection beforeDragSelection;

        private Pagination pagination;

        private int dragDropControlID = -1;

        public bool paginate
        {
            get { return pagination.enabled; }
            set { pagination.enabled = value; }
        }

        public int pageSize
        {
            get { return pagination.pageSize; }
            set { pagination.pageSize = value; }
        }

        private bool doPagination
        {
            get
            {
                if (pagination.enabled)
                {
                    return !list.serializedObject.isEditingMultipleObjects;
                }

                return false;
            }
        }

        private float elementSpacing => Mathf.Max(0f, verticalSpacing - 2f);

        public SerializedProperty List
        {
            get { return list; }
            internal set { list = value; }
        }

        public bool HasList
        {
            get
            {
                if (list != null)
                {
                    return list.isArray;
                }

                return false;
            }
        }

        public int Length
        {
            get
            {
                if (!HasList)
                {
                    return 0;
                }

                if (!list.hasMultipleDifferentValues)
                {
                    return list.arraySize;
                }

                int      num           = list.arraySize;
                Object[] targetObjects = list.serializedObject.targetObjects;
                for (int i = 0; i < targetObjects.Length; i++)
                {
                    num = Mathf.Min(new SerializedObject(targetObjects[i]).FindProperty(list.propertyPath).arraySize,
                                    num);
                }

                return num;
            }
        }

        public int VisibleLength => pagination.GetVisibleLength(Length);

        public int[] Selected
        {
            get { return selection.ToArray(); }
            set { selection = new ListSelection(value); }
        }

        public int Index
        {
            get { return selection.First; }
            set { selection.Select(value); }
        }

        public bool IsDragging => dragging;

        public event DrawHeaderDelegate drawHeaderCallback;

        public event DrawFooterDelegate drawFooterCallback;

        public event DrawElementDelegate drawElementCallback;

        public event DrawElementDelegate drawElementBackgroundCallback;

        public event GetElementHeightDelegate getElementHeightCallback;

        public event GetElementsHeightDelegate getElementsHeightCallback;

        public event GetElementNameDelegate getElementNameCallback;

        public event GetElementLabelDelegate getElementLabelCallback;

        public event DragDropReferenceDelegate onValidateDragAndDropCallback;

        public event DragDropAppendDelegate onAppendDragDropCallback;

        public event ActionDelegate onReorderCallback;

        public event ActionDelegate onSelectCallback;

        public event ActionDelegate onAddCallback;

        public event AddDropdownDelegate onAddDropdownCallback;

        public event ActionDelegate onRemoveCallback;

        public event ActionDelegate onMouseUpCallback;

        public event ActionBoolDelegate onCanRemoveCallback;

        public event ActionDelegate onChangedCallback;

        public ReorderableList(SerializedProperty list)
            : this(list, canAdd: true, canRemove: true, draggable: true) { }

        public ReorderableList(SerializedProperty list, bool canAdd, bool canRemove, bool draggable)
            : this(list, canAdd, canRemove, draggable, ElementDisplayType.Auto, null, null, null) { }

        public ReorderableList(SerializedProperty list,
                               bool               canAdd,
                               bool               canRemove,
                               bool               draggable,
                               ElementDisplayType elementDisplayType,
                               string             elementNameProperty,
                               Texture            elementIcon)
            : this(list, canAdd, canRemove, draggable, elementDisplayType, elementNameProperty, null, elementIcon) { }

        public ReorderableList(SerializedProperty list,
                               bool               canAdd,
                               bool               canRemove,
                               bool               draggable,
                               ElementDisplayType elementDisplayType,
                               string             elementNameProperty,
                               string             elementNameOverride,
                               Texture            elementIcon)
        {
            if (list == null)
            {
                throw new MissingListException();
            }

            if (!list.isArray)
            {
                SerializedProperty serializedProperty = list.FindPropertyRelative("array");
                if (serializedProperty == null || !serializedProperty.isArray)
                {
                    throw new InvalidListException();
                }

                this.list = serializedProperty;
            }
            else
            {
                this.list = list;
            }

            this.canAdd              = canAdd;
            this.canRemove           = canRemove;
            this.draggable           = draggable;
            this.elementDisplayType  = elementDisplayType;
            this.elementNameProperty = elementNameProperty;
            this.elementNameOverride = elementNameOverride;
            this.elementIcon         = elementIcon;
            id                       = GetHashCode();
            list.isExpanded          = true;
            label                    = new GUIContent(list.displayName);
            pageInfoContent          = new GUIContent();
            pageSizeContent          = new GUIContent();
            verticalSpacing          = EditorGUIUtility.standardVerticalSpacing;
            headerHeight             = 18f;
            paginationHeight         = 18f;
            footerHeight             = 13f;
            slideEasing              = 0.15f;
            expandable               = true;
            elementLabels            = true;
            showDefaultBackground    = true;
            multipleSelection        = true;
            pagination               = default(Pagination);
            elementLabel             = new GUIContent();
            dragList                 = new DragList(0);
            selection                = new ListSelection();
            slideGroup               = new SlideGroup();
            elementRects             = new Rect[0];
        }

        public float GetHeight()
        {
            if (!HasList) return EditorGUIUtility.singleLineHeight;
            var num = doPagination ? headerHeight + paginationHeight : headerHeight;
            if (!list.isExpanded) return headerHeight;
            return num + GetElementsHeight() + footerHeight + 15;
        }

        public void DoLayoutList()
        {
            var controlRect = EditorGUILayout.GetControlRect(false, GetHeight(), EditorStyles.largeLabel);
            DoList(EditorGUI.IndentedRect(controlRect), label);
        }

        public void DoList(Rect rect, GUIContent titleLabel)
        {
            var indentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            var rect2 = rect;
            rect2.height = headerHeight;
            if (!HasList)
            {
                DrawEmpty(rect2, $"{titleLabel.text} is not an Array!", GUIStyle.none, EditorStyles.helpBox);
            }
            else
            {
                controlID         = GUIUtility.GetControlID(selectionHash, FocusType.Keyboard, rect);
                dragDropControlID = GUIUtility.GetControlID(dragAndDropHash, FocusType.Passive, rect);
                DrawHeader(rect2, titleLabel);
                if (list.isExpanded)
                {
                    if (doPagination)
                    {
                        var rect3 = rect2;
                        rect3.y      += rect2.height + 16f;
                        rect3.height =  paginationHeight;
                        rect3.width  -= 1;
                        rect3.xMin   += 1;
                        DrawPaginationHeader(rect3);
                        rect2.yMax = rect3.yMax - 1f;
                    }

                    var rect4 = rect;
                    rect4.yMin = rect2.yMax;
                    rect4.yMax = rect.yMax - footerHeight;
                    var current = Event.current;
                    if (selection.Length > 1 && current.type == EventType.ContextClick &&
                        CanSelect(current.mousePosition))
                    {
                        HandleMultipleContextClick(current);
                    }

                    if (Length > 0)
                    {
                        if (!dragging)
                        {
                            UpdateElementRects(rect4, current);
                        }

                        if (elementRects.Length != 0)
                        {
                            pagination.GetVisibleRange(elementRects.Length, out var start, out var end);
                            var rect5 = rect4;
                            rect5.yMin = elementRects[start].yMin;
                            rect5.yMax = elementRects[end - 1].yMax;
                            HandlePreSelection(rect5, current);
                            DrawElements(rect4, current);
                            HandlePostSelection(rect5, current);
                        }
                    }
                    else
                    {
                        DrawEmpty(rect4, "List is Empty", Style.boxBackground, Style.verticalLabel);
                    }
                }

                var rect6 = rect;
                rect6.yMin = rect.yMax - footerHeight;
                rect6.xMin = rect.xMax - 58f;
                DrawFooter(rect6);
            }

            EditorGUI.indentLevel = indentLevel;
        }

        public SerializedProperty AddItem<T>(T item)
        where T : Object
        {
            SerializedProperty serializedProperty = AddItem();
            if (serializedProperty != null)
            {
                serializedProperty.objectReferenceValue = item;
            }

            return serializedProperty;
        }

        public SerializedProperty AddItem()
        {
            if (HasList)
            {
                list.arraySize++;
                selection.Select(list.arraySize - 1);
                SetPageByIndex(list.arraySize - 1);
                DispatchChange();
                return list.GetArrayElementAtIndex(selection.Last);
            }

            throw new InvalidListException();
        }

        public void Remove(int[] selection)
        {
            Array.Sort(selection);
            int num = selection.Length;
            while (--num > -1)
            {
                RemoveItem(selection[num]);
            }
        }

        public void RemoveItem(int index)
        {
            if (index >= 0 && index < Length)
            {
                SerializedProperty arrayElementAtIndex = list.GetArrayElementAtIndex(index);
                if (arrayElementAtIndex.propertyType == SerializedPropertyType.ObjectReference &&
                    (bool)arrayElementAtIndex.objectReferenceValue)
                {
                    arrayElementAtIndex.objectReferenceValue = null;
                }

                list.DeleteArrayElementAtIndex(index);
                selection.Remove(index);
                if (Length > 0)
                {
                    selection.Select(Mathf.Max(0, index - 1));
                }

                DispatchChange();
            }
        }

        public SerializedProperty GetItem(int index)
        {
            if (index >= 0 && index < Length)
            {
                return list.GetArrayElementAtIndex(index);
            }

            return null;
        }

        public int IndexOf(SerializedProperty element)
        {
            if (element != null)
            {
                int num = Length;
                while (--num > -1)
                {
                    if (SerializedProperty.EqualContents(element, list.GetArrayElementAtIndex(num)))
                    {
                        return num;
                    }
                }
            }

            return -1;
        }

        public void GrabKeyboardFocus() { GUIUtility.keyboardControl = id; }

        public bool HasKeyboardControl() { return GUIUtility.keyboardControl == id; }

        public void ReleaseKeyboardFocus()
        {
            if (GUIUtility.keyboardControl == id)
            {
                GUIUtility.keyboardControl = 0;
            }
        }

        public void SetPage(int page)
        {
            if (doPagination)
            {
                pagination.page = page;
            }
        }

        public void SetPageByIndex(int index)
        {
            if (doPagination)
            {
                pagination.page = pagination.GetPageForIndex(index);
            }
        }

        public int GetPage(int index)
        {
            if (!doPagination)
            {
                return 0;
            }

            return pagination.page;
        }

        public int GetPageByIndex(int index)
        {
            if (!doPagination)
            {
                return 0;
            }

            return pagination.GetPageForIndex(index);
        }

        private float GetElementsHeight()
        {
            if (this.getElementsHeightCallback != null)
            {
                return this.getElementsHeightCallback(this);
            }

            int length = Length;
            if (length == 0)
            {
                return 28f;
            }

            float num  = 0f;
            float num2 = elementSpacing;
            pagination.GetVisibleRange(length, out var start, out var end);
            for (int i = start; i < end; i++)
            {
                num += GetElementHeight(list.GetArrayElementAtIndex(i)) + num2;
            }

            return num + 7f - num2;
        }

        private float GetElementHeight(SerializedProperty element)
        {
            if (this.getElementHeightCallback != null)
            {
                return this.getElementHeightCallback(element) + 4f;
            }

            float propertyHeight = EditorGUI.GetPropertyHeight(element, GetElementLabel(element, elementLabels),
                                                               IsElementExpandable(element));
            return (propertyHeight > 0f) ? (propertyHeight + 4f) : propertyHeight;
        }

        private Rect GetElementDrawRect(int index, Rect desiredRect)
        {
            if (slideEasing <= 0f)
            {
                return desiredRect;
            }

            if (!dragging)
            {
                return slideGroup.SetRect(index, desiredRect);
            }

            return slideGroup.GetRect(dragList[index].startIndex, desiredRect, slideEasing);
        }

        private Rect GetElementRenderRect(SerializedProperty element, Rect elementRect)
        {
            float num    = (draggable ? 20 : 5);
            Rect  result = elementRect;
            result.xMin += (IsElementExpandable(element) ? (num + 10f) : num);
            result.xMax -= 6f;
            result.yMin += 1f;
            result.yMax -= 3f;
            return result;
        }

        private void DrawHeader(Rect rect, GUIContent titleLabel)
        {
            if (showDefaultBackground && Event.current.type == EventType.Repaint)
            {
                Style.headerBackground.Draw(rect, isHover: false, isActive: false, on: false, hasKeyboardFocus: false);
            }

            HandleDragAndDrop(rect, Event.current);
            var flag  = elementDisplayType != ElementDisplayType.SingleLine;
            var rect2 = rect;
            rect2.xMin   += 6f;
            rect2.xMax   -= flag ? 95f : 55f;
            rect2.height =  15f;
            rect2.y++;
            titleLabel = EditorGUI.BeginProperty(rect2, titleLabel, list);
            if (this.drawHeaderCallback != null)
            {
                drawHeaderCallback(rect2, titleLabel);
            }
            else if (expandable)
            {
                rect2.xMin += 10f;
                EditorGUI.BeginChangeCheck();
                var isExpanded = EditorGUI.Foldout(rect2, list.isExpanded, titleLabel, toggleOnLabelClick: true);
                if (EditorGUI.EndChangeCheck())
                {
                    list.isExpanded = isExpanded;
                }
            }
            else
            {
                GUI.Label(rect2, titleLabel, EditorStyles.label);
            }

            EditorGUI.EndProperty();
            if (flag)
            {
                var position = rect;
                position.xMin = rect.xMax - 25f;
                position.xMax = rect.xMax - 5f;
                if (GUI.Button(position, Style.expandButton, Style.preButton))
                {
                    ExpandElements(expand: true);
                }

                var position2 = rect;
                position2.xMin = position.xMin - 20f;
                position2.xMax = position.xMin;
                if (GUI.Button(position2, Style.collapseButton, Style.preButton))
                {
                    ExpandElements(expand: false);
                }

                rect.xMax = position2.xMin + 5f;
            }

            if (!sortable) return;
            var rect3 = rect;
            rect3.xMin = rect.xMax - 25f;
            rect3.xMax = rect.xMax;
            var rect4 = rect;
            rect4.xMin = rect3.xMin - 20f;
            rect4.xMax = rect3.xMin;
            if (EditorGUI.DropdownButton(rect3, Style.sortAscending, FocusType.Passive, Style.preButton))
            {
                SortElements(rect3, descending: false);
            }

            if (EditorGUI.DropdownButton(rect4, Style.sortDescending, FocusType.Passive, Style.preButton))
            {
                SortElements(rect4, descending: true);
            }
        }

        private void ExpandElements(bool expand)
        {
            if (!list.isExpanded && expand)
            {
                list.isExpanded = true;
            }

            int length = Length;
            for (int i = 0; i < length; i++)
            {
                list.GetArrayElementAtIndex(i).isExpanded = expand;
            }
        }

        private void SortElements(Rect rect, bool descending)
        {
            int total = Length;
            if (total <= 1)
            {
                return;
            }

            SerializedProperty arrayElementAtIndex = list.GetArrayElementAtIndex(0);
            if (arrayElementAtIndex.propertyType == SerializedPropertyType.Generic)
            {
                GenericMenu        genericMenu        = new GenericMenu();
                SerializedProperty serializedProperty = arrayElementAtIndex.Copy();
                SerializedProperty endProperty        = serializedProperty.GetEndProperty();
                bool               enterChildren      = true;
                while (serializedProperty.NextVisible(enterChildren) &&
                       !SerializedProperty.EqualContents(serializedProperty, endProperty))
                {
                    genericMenu.AddItem(new GUIContent(serializedProperty.name), on: false, delegate(object userData)
                    {
                        ListSort.SortOnProperty(list, total, descending, (string)userData);
                        ApplyReorder();
                        HandleUtility.Repaint();
                    }, serializedProperty.name);
                    enterChildren = false;
                }

                genericMenu.DropDown(rect);
            }
            else
            {
                ListSort.SortOnType(list, total, descending, arrayElementAtIndex.propertyType);
                ApplyReorder();
            }
        }

        private void DrawEmpty(Rect rect, string label, GUIStyle backgroundStyle, GUIStyle labelStyle)
        {
            if (showDefaultBackground && Event.current.type == EventType.Repaint)
            {
                backgroundStyle.Draw(rect, isHover: false, isActive: false, on: false, hasKeyboardFocus: false);
            }

            EditorGUI.LabelField(rect, label, labelStyle);
        }

        private void UpdateElementRects(Rect rect, Event evt)
        {
            int length = Length;
            if (length != elementRects.Length)
            {
                Array.Resize(ref elementRects, length);
            }

            if (evt.type == EventType.Repaint)
            {
                Rect  rect2 = rect;
                float num3  = (rect2.yMin = (rect2.yMax = rect.yMin + 2f));
                float num4  = elementSpacing;
                pagination.GetVisibleRange(length, out var start, out var end);
                for (int i = start; i < end; i++)
                {
                    SerializedProperty arrayElementAtIndex = list.GetArrayElementAtIndex(i);
                    rect2.y         =  rect2.yMax;
                    rect2.height    =  GetElementHeight(arrayElementAtIndex);
                    elementRects[i] =  rect2;
                    rect2.yMax      += num4;
                }
            }
        }

        private void DrawElements(Rect rect, Event evt)
        {
            if (showDefaultBackground && evt.type == EventType.Repaint)
            {
                Style.boxBackground.Draw(rect, isHover: false, isActive: false, on: false, hasKeyboardFocus: false);
            }

            if (!dragging)
            {
                pagination.GetVisibleRange(Length, out var start, out var end);
                for (int i = start; i < end; i++)
                {
                    bool flag = selection.Contains(i);
                    DrawElement(list.GetArrayElementAtIndex(i), GetElementDrawRect(i, elementRects[i]), flag,
                                flag && GUIUtility.keyboardControl == controlID);
                }
            }
            else
            {
                if (evt.type != EventType.Repaint)
                {
                    return;
                }

                int length  = dragList.Length;
                int length2 = selection.Length;
                int j;
                for (j = 0; j < length2; j++)
                {
                    DragElement value = dragList[j];
                    value.desiredRect.y = dragPosition - value.dragOffset;
                    dragList[j]         = value;
                }

                j = length;
                while (--j > -1)
                {
                    DragElement value2 = dragList[j];
                    if (value2.selected)
                    {
                        DrawElement(value2.property, value2.desiredRect, selected: true, focused: true);
                        continue;
                    }

                    Rect rect2 = value2.rect;
                    int  num   = value2.startIndex;
                    int  num2  = ((dragDirection > 0) ? (length2 - 1) : 0);
                    int  num3  = ((dragDirection > 0) ? (-1) : length2);
                    for (int num4 = num2; num4 != num3; num4 -= dragDirection)
                    {
                        DragElement dragElement = dragList[num4];
                        if (dragElement.Overlaps(rect2, num, dragDirection))
                        {
                            rect2.y -= dragElement.rect.height * (float)dragDirection;
                            num     += dragDirection;
                        }
                    }

                    DrawElement(value2.property, GetElementDrawRect(j, rect2), selected: false, focused: false);
                    value2.desiredRect = rect2;
                    dragList[j]        = value2;
                }
            }
        }

        private void DrawElement(SerializedProperty element, Rect rect, bool selected, bool focused)
        {
            if (!(rect.height < 1f))
            {
                Event current = Event.current;
                if (this.drawElementBackgroundCallback != null)
                {
                    this.drawElementBackgroundCallback(rect, element, null, selected, focused);
                }
                else if (current.type == EventType.Repaint)
                {
                    Style.elementBackground.Draw(rect, isHover: false, selected, selected, focused);
                    EditorGUI.DrawRect(new Rect(rect.x + 1f, rect.y + rect.height - 2f, rect.width - 3f, 1f),
                                       new Color(0f, 0f, 0f, 0.2f));
                }

                if (current.type == EventType.Repaint && draggable)
                {
                    Style.draggingHandle.Draw(
                                              new Rect(rect.x + 5f, rect.y + 14f, 10f, rect.height - (rect.height - 6f)), isHover: false,
                                              isActive: false, on: false, hasKeyboardFocus: false);
                }

                GUIContent contents          = GetElementLabel(element, elementLabels);
                Rect       elementRenderRect = GetElementRenderRect(element, rect);
                if (this.drawElementCallback != null)
                {
                    this.drawElementCallback(elementRenderRect, element, contents, selected, focused);
                }
                else
                {
                    EditorGUI.PropertyField(elementRenderRect, element, contents, includeChildren: true);
                }

                int num = GUIUtility.GetControlID(contents, FocusType.Passive, rect);
                if (current.GetTypeForControl(num) == EventType.ContextClick && rect.Contains(current.mousePosition))
                {
                    HandleSingleContextClick(current, element);
                }
            }
        }

        private GUIContent GetElementLabel(SerializedProperty element, bool allowElementLabel)
        {
            if (!allowElementLabel)
            {
                return GUIContent.none;
            }

            if (this.getElementLabelCallback != null)
            {
                return this.getElementLabelCallback(element);
            }

            string text = ((this.getElementNameCallback == null)
                ? GetElementName(element, elementNameProperty, elementNameOverride)
                : this.getElementNameCallback(element));
            elementLabel.text    = ((!string.IsNullOrEmpty(text)) ? text : element.displayName);
            elementLabel.tooltip = element.tooltip;
            elementLabel.image   = elementIcon;
            return elementLabel;
        }

        private static string GetElementName(SerializedProperty element, string nameProperty, string nameOverride)
        {
            if (!string.IsNullOrEmpty(nameOverride))
            {
                string propertyPath = element.propertyPath;
                if (propertyPath.EndsWith("]"))
                {
                    int num = propertyPath.LastIndexOf('[') + 1;
                    return $"{nameOverride} {propertyPath.Substring(num, propertyPath.Length - num - 1)}";
                }

                return nameOverride;
            }

            if (string.IsNullOrEmpty(nameProperty))
            {
                return null;
            }

            if (element.propertyType == SerializedPropertyType.ObjectReference && nameProperty == "name")
            {
                if (!element.objectReferenceValue)
                {
                    return null;
                }

                return element.objectReferenceValue.name;
            }

            SerializedProperty serializedProperty = element.FindPropertyRelative(nameProperty);
            if (serializedProperty != null)
            {
                switch (serializedProperty.propertyType)
                {
                    case SerializedPropertyType.ObjectReference:
                        if (!serializedProperty.objectReferenceValue)
                        {
                            return null;
                        }

                        return serializedProperty.objectReferenceValue.name;
                    case SerializedPropertyType.Enum:
                        return serializedProperty.enumDisplayNames[serializedProperty.enumValueIndex];
                    case SerializedPropertyType.Integer:
                    case SerializedPropertyType.Character:
                        return serializedProperty.intValue.ToString();
                    case SerializedPropertyType.LayerMask:
                        return GetLayerMaskName(serializedProperty.intValue);
                    case SerializedPropertyType.String:
                        return serializedProperty.stringValue;
                    case SerializedPropertyType.Float:
                        return serializedProperty.floatValue.ToString();
                    default:
                        return serializedProperty.displayName;
                }
            }

            return null;
        }

        private static string GetLayerMaskName(int mask)
        {
            if (mask == 0)
            {
                return "Nothing";
            }

            if (mask < 0)
            {
                return "Everything";
            }

            string text = string.Empty;
            int    num  = 0;
            for (int i = 0; i < 32; i++)
            {
                if (((1 << i) & mask) != 0)
                {
                    if (num == 4)
                    {
                        return "Mixed ...";
                    }

                    text = string.Concat(text, (num > 0) ? ", " : string.Empty, LayerMask.LayerToName(i));
                    num++;
                }
            }

            return text;
        }

        private void DrawFooter(Rect rect)
        {
            if (this.drawFooterCallback != null)
            {
                this.drawFooterCallback(rect);
                return;
            }

            if (Event.current.type == EventType.Repaint)
            {
                Style.footerBackground.Draw(rect, isHover: false, isActive: false, on: false, hasKeyboardFocus: false);
            }

            var rect2    = new Rect(rect.xMin + 4f, rect.y - 3f, 25f, 13f);
            var position = new Rect(rect.xMax - 29f, rect.y - 3f, 25f, 13f);
            EditorGUI.BeginDisabledGroup(!canAdd);
            if (GUI.Button(rect2,
                           onAddDropdownCallback != null ? Style.iconToolbarPlusMore : Style.iconToolbarPlus,
                           Style.preButton))
            {
                if (this.onAddDropdownCallback != null)
                {
                    this.onAddDropdownCallback(rect2, this);
                }
                else if (this.onAddCallback != null)
                {
                    this.onAddCallback(this);
                }
                else
                {
                    AddItem();
                }
            }

            EditorGUI.EndDisabledGroup();
            EditorGUI.BeginDisabledGroup(!CanSelect(selection) || !canRemove ||
                                         (this.onCanRemoveCallback != null && !this.onCanRemoveCallback(this)));
            if (GUI.Button(position, Style.iconToolbarMinus, Style.preButton))
            {
                if (this.onRemoveCallback != null)
                {
                    this.onRemoveCallback(this);
                }
                else
                {
                    Remove(selection.ToArray());
                    list.serializedObject.ApplyModifiedProperties();
                    list.serializedObject.Update();
                }
            }

            EditorGUI.EndDisabledGroup();
        }

        private void DrawPaginationHeader(Rect rect)
        {
            var length    = Length;
            var pageCount = pagination.GetPageCount(length);
            var num       = Mathf.Clamp(pagination.page, 0, pageCount - 1);
            if (num != pagination.page)
            {
                pagination.page = num;
                HandleUtility.Repaint();
            }

            var position  = new Rect(rect.xMin + 4f, rect.y - 1f, 17f, 14f);
            var position2 = new Rect(position.xMax, rect.y - 1f, 17f, 14f);
            var position3 = new Rect(position2.xMax, rect.y - 1f, 17f, 14f);
            if (Event.current.type == EventType.Repaint)
            {
                Style.paginationHeader.Draw(rect, isHover: false, isActive: true, on: true, hasKeyboardFocus: false);
            }

            pageInfoContent.text = $"{pagination.page + 1} / {pageCount}";
            var position4 = rect;
            position4.width =  Style.paginationText.CalcSize(pageInfoContent).x;
            position4.x     =  rect.xMax - position4.width - 7f;
            position4.y     += 2f;
            GUI.Label(position4, pageInfoContent, Style.paginationText);
            if (GUI.Button(position, Style.iconPagePrev, Style.preButton))
            {
                pagination.page = Mathf.Max(0, pagination.page - 1);
            }

            if (EditorGUI.DropdownButton(position2, Style.iconPagePopup, FocusType.Passive, Style.preButton))
            {
                GenericMenu genericMenu = new GenericMenu();
                for (int i = 0; i < pageCount; i++)
                {
                    int num2 = i;
                    genericMenu.AddItem(new GUIContent($"Page {i + 1}"), i == pagination.page, OnPageDropDownSelect,
                                        num2);
                }

                genericMenu.DropDown(position2);
            }

            if (GUI.Button(position3, Style.iconPageNext, Style.preButton))
            {
                pagination.page = Mathf.Min(pageCount - 1, pagination.page + 1);
            }

            pageSizeContent.text = length.ToString();
            GUIStyle pageSizeTextField = Style.pageSizeTextField;
            Texture  image             = Style.listIcon.image;
            float    num3              = position3.xMax + 5f;
            float    num4              = position4.xMin - 5f - num3;
            float    num5              = image.width + 2;
            float    num6              = pageSizeTextField.CalcSize(pageSizeContent).x + 50f + num5;
            Rect     position5         = rect;
            position5.x     = num3 + (num4 - num6) / 2f;
            position5.width = num6 - num5;
            EditorGUI.BeginChangeCheck();
            EditorGUIUtility.labelWidth = num5;
            EditorGUIUtility.SetIconSize(new Vector2(image.width, image.height));
            int value = EditorGUI.DelayedIntField(position5, Style.listIcon, pagination.pageSize, pageSizeTextField);
            EditorGUIUtility.labelWidth = 0f;
            EditorGUIUtility.SetIconSize(Vector2.zero);
            if (EditorGUI.EndChangeCheck())
            {
                pagination.pageSize = Mathf.Clamp(value, 0, length);
                pagination.page     = Mathf.Min(pagination.GetPageCount(length) - 1, pagination.page);
            }
        }

        private void OnPageDropDownSelect(object userData) { pagination.page = (int)userData; }

        private void DispatchChange()
        {
            if (this.onChangedCallback != null)
            {
                this.onChangedCallback(this);
            }
        }

        private void HandleSingleContextClick(Event evt, SerializedProperty element)
        {
            selection.Select(IndexOf(element));
            GenericMenu genericMenu = new GenericMenu();
            if (element.isInstantiatedPrefab)
            {
                genericMenu.AddItem(
                                    new GUIContent(string.Concat("Revert ", GetElementLabel(element, allowElementLabel: true).text,
                                                                 " to Prefab")), on: false, selection.RevertValues, list);
                genericMenu.AddSeparator(string.Empty);
            }

            HandleSharedContextClick(evt, genericMenu, "Duplicate Array Element", "Delete Array Element",
                                     "Move Array Element");
        }

        private void HandleMultipleContextClick(Event evt)
        {
            GenericMenu genericMenu = new GenericMenu();
            if (selection.CanRevert(list))
            {
                genericMenu.AddItem(new GUIContent("Revert Values to Prefab"), on: false, selection.RevertValues, list);
                genericMenu.AddSeparator(string.Empty);
            }

            HandleSharedContextClick(evt, genericMenu, "Duplicate Array Elements", "Delete Array Elements",
                                     "Move Array Elements");
        }

        private void HandleSharedContextClick(Event       evt,
                                              GenericMenu menu,
                                              string      duplicateLabel,
                                              string      deleteLabel,
                                              string      moveLabel)
        {
            menu.AddItem(new GUIContent(duplicateLabel), on: false, HandleDuplicate, list);
            menu.AddItem(new GUIContent(deleteLabel), on: false, HandleDelete, list);
            if (doPagination)
            {
                int pageCount = pagination.GetPageCount(Length);
                if (pageCount > 1)
                {
                    for (int i = 0; i < pageCount; i++)
                    {
                        string text = $"{moveLabel}/Page {i + 1}";
                        menu.AddItem(new GUIContent(text), i == pagination.page, HandleMoveElement, i);
                    }
                }
            }

            menu.ShowAsContext();
            evt.Use();
        }

        private void HandleMoveElement(object userData)
        {
            int num    = (int)userData;
            int page   = pagination.page;
            int num2   = pagination.pageSize;
            int num3   = num * num2 - page * num2;
            int num4   = ((num3 > 0) ? 1 : (-1));
            int length = Length;
            int num5   = 0;
            for (int i = 0; i < selection.Length; i++)
            {
                int num6 = selection[i] + num3;
                num5 = ((num4 < 0) ? Mathf.Min(num5, num6) : Mathf.Max(num5, num6 - length));
            }

            num3 -= num5;
            UpdateDragList(0f, 0, length);
            var elements = new List<DragElement>(dragList.Elements.Where(t => !selection.Contains(t.startIndex)));
            selection.Sort();
            for (int j = 0; j < selection.Length; j++)
            {
                int num7               = selection[j];
                int indexFromSelection = dragList.GetIndexFromSelection(num7);
                int index              = Mathf.Clamp(num7 + num3, 0, elements.Count);
                elements.Insert(index, dragList[indexFromSelection]);
            }

            dragList.Elements = elements.ToArray();
            ReorderDraggedElements(num4, 0, null);
            pagination.page = num;
            HandleUtility.Repaint();
        }

        private void HandleDelete(object userData)
        {
            selection.Delete(userData as SerializedProperty);
            DispatchChange();
        }

        private void HandleDuplicate(object userData)
        {
            selection.Duplicate(userData as SerializedProperty);
            DispatchChange();
        }

        private void HandleDragAndDrop(Rect rect, Event evt)
        {
            switch (evt.GetTypeForControl(dragDropControlID))
            {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                {
                    if (!GUI.enabled || !rect.Contains(evt.mousePosition))
                    {
                        break;
                    }

                    Object[] objectReferences = DragAndDrop.objectReferences;
                    Object[] array            = new Object[1];
                    bool     flag             = false;
                    Object[] array2           = objectReferences;
                    for (int i = 0; i < array2.Length; i++)
                    {
                        Object @object = (array[0] = array2[i]);
                        Object object2 = ValidateObjectDragAndDrop(array);
                        if (object2 != null)
                        {
                            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                            if (evt.type == EventType.DragPerform)
                            {
                                AppendDragAndDropValue(object2);
                                flag                        = true;
                                DragAndDrop.activeControlID = 0;
                            }
                            else
                            {
                                DragAndDrop.activeControlID = dragDropControlID;
                            }
                        }
                    }

                    if (flag)
                    {
                        GUI.changed = true;
                        DragAndDrop.AcceptDrag();
                    }

                    break;
                }
                case EventType.DragExited:
                    if (GUI.enabled)
                    {
                        HandleUtility.Repaint();
                    }

                    break;
            }
        }

        private Object ValidateObjectDragAndDrop(Object[] references)
        {
            if (this.onValidateDragAndDropCallback != null)
            {
                return this.onValidateDragAndDropCallback(references, this);
            }

            if (surrogate.HasType)
            {
                return Internals.ValidateObjectDragAndDrop(references, null, surrogate.type, surrogate.exactType);
            }

            return Internals.ValidateObjectDragAndDrop(references, list, null, exactType: false);
        }

        private void AppendDragAndDropValue(Object obj)
        {
            if (this.onAppendDragDropCallback != null)
            {
                this.onAppendDragDropCallback(obj, this);
            }
            else if (surrogate.HasType)
            {
                surrogate.Invoke(AddItem(), obj, this);
            }
            else
            {
                Internals.AppendDragAndDropValue(obj, list);
            }

            DispatchChange();
        }

        private void HandlePreSelection(Rect rect, Event evt)
        {
            if (evt.type == EventType.MouseDrag && draggable && GUIUtility.hotControl == controlID)
            {
                if (selection.Length > 0 && UpdateDragPosition(evt.mousePosition, rect, dragList))
                {
                    GUIUtility.keyboardControl = controlID;
                    dragging                   = true;
                }

                evt.Use();
            }
        }

        private void HandlePostSelection(Rect rect, Event evt)
        {
            switch (evt.GetTypeForControl(controlID))
            {
                case EventType.MouseDown:
                    if (rect.Contains(evt.mousePosition) && IsSelectionButton(evt))
                    {
                        int selectionIndex = GetSelectionIndex(evt.mousePosition);
                        if (CanSelect(selectionIndex))
                        {
                            DoSelection(selectionIndex,
                                        GUIUtility.keyboardControl == 0 || GUIUtility.keyboardControl == controlID ||
                                        evt.button == 2, evt);
                        }
                        else
                        {
                            selection.Clear();
                        }

                        HandleUtility.Repaint();
                    }

                    break;
                case EventType.MouseUp:
                    if (!draggable)
                    {
                        selection.SelectWhenNoAction(pressIndex, evt);
                        if (this.onMouseUpCallback != null &&
                            IsPositionWithinElement(evt.mousePosition, selection.Last))
                        {
                            this.onMouseUpCallback(this);
                        }
                    }
                    else if (GUIUtility.hotControl == controlID)
                    {
                        evt.Use();
                        if (dragging)
                        {
                            dragging = false;
                            ReorderDraggedElements(dragDirection, dragList.StartIndex,
                                                   delegate { dragList.SortByPosition(); });
                        }
                        else
                        {
                            selection.SelectWhenNoAction(pressIndex, evt);
                            if (this.onMouseUpCallback != null)
                            {
                                this.onMouseUpCallback(this);
                            }
                        }

                        GUIUtility.hotControl = 0;
                    }

                    HandleUtility.Repaint();
                    break;
                case EventType.KeyDown:
                    if (GUIUtility.keyboardControl != controlID)
                    {
                        break;
                    }

                    if (evt.keyCode == KeyCode.DownArrow && !dragging)
                    {
                        selection.Select(Mathf.Min(selection.Last + 1, Length - 1));
                        evt.Use();
                    }
                    else if (evt.keyCode == KeyCode.UpArrow && !dragging)
                    {
                        selection.Select(Mathf.Max(selection.Last - 1, 0));
                        evt.Use();
                    }
                    else if (evt.keyCode == KeyCode.Escape && GUIUtility.hotControl == controlID)
                    {
                        GUIUtility.hotControl = 0;
                        if (dragging)
                        {
                            dragging  = false;
                            selection = beforeDragSelection;
                        }

                        evt.Use();
                    }

                    break;
                case EventType.MouseMove:
                case EventType.MouseDrag:
                    break;
            }
        }

        private bool IsSelectionButton(Event evt)
        {
            if (evt.button != 0)
            {
                return evt.button == 2;
            }

            return true;
        }

        private void DoSelection(int index, bool setKeyboardControl, Event evt)
        {
            if (multipleSelection)
            {
                selection.AppendWithAction(pressIndex = index, evt);
            }
            else
            {
                selection.Select(pressIndex = index);
            }

            if (this.onSelectCallback != null)
            {
                this.onSelectCallback(this);
            }

            if (draggable)
            {
                dragging     = false;
                dragPosition = (pressPosition = evt.mousePosition.y);
                pagination.GetVisibleRange(Length, out var start, out var end);
                UpdateDragList(dragPosition, start, end);
                selection.Trim(start, end);
                beforeDragSelection   = selection.Clone();
                GUIUtility.hotControl = controlID;
            }

            if (setKeyboardControl)
            {
                GUIUtility.keyboardControl = controlID;
            }

            evt.Use();
        }

        private void UpdateDragList(float dragPosition, int start, int end)
        {
            dragList.Resize(start, end - start);
            for (int i = start; i < end; i++)
            {
                SerializedProperty arrayElementAtIndex = list.GetArrayElementAtIndex(i);
                Rect               rect                = elementRects[i];
                DragElement        dragElement         = default(DragElement);
                dragElement.property    = arrayElementAtIndex;
                dragElement.dragOffset  = dragPosition - rect.y;
                dragElement.rect        = rect;
                dragElement.desiredRect = rect;
                dragElement.selected    = selection.Contains(i);
                dragElement.startIndex  = i;
                DragElement value = dragElement;
                dragList[i - start] = value;
            }

            dragList.SortByIndex();
        }

        private bool UpdateDragPosition(Vector2 position, Rect bounds, DragList dragList)
        {
            int   index      = 0;
            int   index2     = selection.Length - 1;
            float dragOffset = dragList[index].dragOffset;
            float num        = dragList[index2].rect.height - dragList[index2].dragOffset;
            dragPosition = Mathf.Clamp(position.y, bounds.yMin + dragOffset, bounds.yMax - num);
            if (Mathf.Abs(dragPosition - pressPosition) > 1f)
            {
                dragDirection = (int)Mathf.Sign(dragPosition - pressPosition);
                return true;
            }

            return false;
        }

        private void ReorderDraggedElements(int direction, int offset, Action sortList)
        {
            dragList.RecordState();
            sortList?.Invoke();
            selection.Sort(delegate(int a, int b)
            {
                int indexFromSelection2 = dragList.GetIndexFromSelection(a);
                int indexFromSelection3 = dragList.GetIndexFromSelection(b);
                return (direction <= 0)
                    ? indexFromSelection3.CompareTo(indexFromSelection2)
                    : indexFromSelection2.CompareTo(indexFromSelection3);
            });
            int num = selection.Length;
            while (--num > -1)
            {
                int indexFromSelection = dragList.GetIndexFromSelection(selection[num]);
                int num2               = indexFromSelection + offset;
                selection[num] = num2;
                list.MoveArrayElement(dragList[indexFromSelection].startIndex, num2);
            }

            dragList.RestoreState(list);
            ApplyReorder();
        }

        private void ApplyReorder()
        {
            list.serializedObject.ApplyModifiedProperties();
            list.serializedObject.Update();
            if (this.onReorderCallback != null)
            {
                this.onReorderCallback(this);
            }

            DispatchChange();
        }

        private int GetSelectionIndex(Vector2 position)
        {
            pagination.GetVisibleRange(elementRects.Length, out var start, out var end);
            for (int i = start; i < end; i++)
            {
                Rect rect = elementRects[i];
                if (rect.Contains(position) || (i == 0 && position.y <= rect.yMin) ||
                    (i == end - 1 && position.y >= rect.yMax))
                {
                    return i;
                }
            }

            return -1;
        }

        private bool CanSelect(ListSelection selection)
        {
            if (selection.Length <= 0)
            {
                return false;
            }

            return selection.All((int s) => CanSelect(s));
        }

        private bool CanSelect(int index)
        {
            if (index >= 0)
            {
                return index < Length;
            }

            return false;
        }

        private bool CanSelect(Vector2 position)
        {
            if (selection.Length <= 0)
            {
                return false;
            }

            return selection.Any((int s) => IsPositionWithinElement(position, s));
        }

        private bool IsPositionWithinElement(Vector2 position, int index) { return CanSelect(index) && elementRects[index].Contains(position); }

        private bool IsElementExpandable(SerializedProperty element)
        {
            switch (elementDisplayType)
            {
                case ElementDisplayType.Auto:
                    if (element.hasVisibleChildren)
                    {
                        return IsTypeExpandable(element.propertyType);
                    }

                    return false;
                case ElementDisplayType.Expandable:
                    return true;
                case ElementDisplayType.SingleLine:
                    return false;
                default:
                    return false;
            }
        }

        private bool IsTypeExpandable(SerializedPropertyType type)
        {
            switch (type)
            {
                case SerializedPropertyType.Generic:
                case SerializedPropertyType.Vector4:
                case SerializedPropertyType.ArraySize:
                case SerializedPropertyType.Quaternion:
                    return true;
                default:
                    return false;
            }
        }
    }
}