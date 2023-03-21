/*
Copyright (c) 2020 Omar Duarte
Unauthorized copying of this file, via any medium is strictly prohibited.
Writen by Omar Duarte, 2020.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace PluginMaster
{
    public static class TransformTools
    {
        #region BOUNDS

        public enum Bound
        {
            MIN,
            CENTER,
            MAX
        }

        public enum RelativeTo
        {
            LAST_SELECTED,
            FIRST_SELECTED,
            BIGGEST_OBJECT,
            SMALLEST_OBJECT,
            SELECTION,
            CANVAS
        }

        public enum Axis
        {
            X,
            Y,
            Z
        }

        private static Vector3 GetBound(Bounds bounds, Bound bound)
        {
            switch (bound)
            {
                case Bound.MIN:
                    return bounds.min;
                case Bound.CENTER:
                    return bounds.center;
                case Bound.MAX:
                    return bounds.max;
            }

            return bounds.center;
        }

        private static GameObject GetAnchorObject(IReadOnlyList<GameObject> selection, RelativeTo relativeTo, Axis axis, bool recursive = true)
        {
            if (selection.Count == 0) return null;
            switch (relativeTo)
            {
                case RelativeTo.LAST_SELECTED:
                    return selection.Last();
                case RelativeTo.FIRST_SELECTED:
                    return selection[0];
                case RelativeTo.BIGGEST_OBJECT:
                    GameObject biggestObject = null;
                    var maxSize = float.MinValue;
                    foreach (var obj in selection)
                    {
                        var bounds = BoundsUtils.GetBoundsRecursive(obj.transform, recursive);
                        switch (axis)
                        {
                            case Axis.X:
                                if (bounds.size.x > maxSize)
                                {
                                    maxSize = bounds.size.x;
                                    biggestObject = obj;
                                }

                                break;
                            case Axis.Y:
                                if (bounds.size.y > maxSize)
                                {
                                    maxSize = bounds.size.y;
                                    biggestObject = obj;
                                }

                                break;
                            case Axis.Z:
                                if (bounds.size.z > maxSize)
                                {
                                    maxSize = bounds.size.z;
                                    biggestObject = obj;
                                }

                                break;
                        }
                    }

                    return biggestObject;
                case RelativeTo.SMALLEST_OBJECT:
                    GameObject smallestObject = null;
                    var minSize = float.MaxValue;
                    foreach (var obj in selection)
                    {
                        var bounds = BoundsUtils.GetBoundsRecursive(obj.transform, recursive);
                        switch (axis)
                        {
                            case Axis.X:
                                if (bounds.size.x < minSize)
                                {
                                    minSize = bounds.size.x;
                                    smallestObject = obj;
                                }

                                break;
                            case Axis.Y:
                                if (bounds.size.y < minSize)
                                {
                                    minSize = bounds.size.y;
                                    smallestObject = obj;
                                }

                                break;
                            case Axis.Z:
                                if (bounds.size.z < minSize)
                                {
                                    minSize = bounds.size.z;
                                    smallestObject = obj;
                                }

                                break;
                        }
                    }

                    return smallestObject;
                default:
                    return null;
            }
        }

        private static Bounds GetSelectionBounds(GameObject[] selection, bool recursive = true, BoundsUtils.ObjectProperty property = BoundsUtils.ObjectProperty.BOUNDING_BOX)
        {
            var max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            var min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            foreach (var obj in selection)
            {
                var bounds = BoundsUtils.GetBoundsRecursive(obj.transform, recursive, property);
                max = Vector3.Max(bounds.max, max);
                min = Vector3.Min(bounds.min, min);
            }

            var size = max - min;
            var center = min + size / 2f;
            return new Bounds(center, size);
        }

        private static Tuple<GameObject, Bounds> GetSelectionBounds(GameObject[] selection, RelativeTo relativeTo, Axis axis, bool recursive = true,
            BoundsUtils.ObjectProperty property = BoundsUtils.ObjectProperty.BOUNDING_BOX)
        {
            if (selection.Length == 0) return new Tuple<GameObject, Bounds>(null, new Bounds());
            var anchor = GetAnchorObject(selection, relativeTo, axis);
            if (anchor != null) return new Tuple<GameObject, Bounds>(anchor, BoundsUtils.GetBoundsRecursive(anchor.transform, recursive, property));
            if (relativeTo == RelativeTo.CANVAS)
            {
                var canvasBounds = GetCanvasBounds(selection);
                if (canvasBounds.size != Vector3.zero) return new Tuple<GameObject, Bounds>(null, GetCanvasBounds(selection));
            }

            return new Tuple<GameObject, Bounds>(null, GetSelectionBounds(selection, recursive, property));
        }

        private static Bounds GetCanvasBounds(IReadOnlyCollection<GameObject> selection)
        {
            if (selection.Count == 0) return new Bounds();
            var max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            var min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            var noCanvasFound = true;
            foreach (var obj in selection)
            {
                var canvas = GetTopmostCanvas(obj);
                if (canvas == null) continue;
                noCanvasFound = false;
                var rectTransform = canvas.GetComponent<RectTransform>();
                var halfSize = rectTransform.sizeDelta / 2;
                var position = rectTransform.position;
                max = Vector3.Max(max, position + (Vector3)halfSize);
                min = Vector3.Min(min, position - (Vector3)halfSize);
            }

            if (noCanvasFound) return new Bounds();
            var size = max - min;
            var center = min + size / 2f;
            return new Bounds(center, size);
        }

        private static Bounds GetCanvasBounds(Component canvas)
        {
            var rectTransform = canvas.GetComponent<RectTransform>();
            return new Bounds(rectTransform.position, rectTransform.sizeDelta);
        }

        private static Canvas GetTopmostCanvas(GameObject obj)
        {
            var canvasesInParent = obj.GetComponentsInParent<Canvas>();
            switch (canvasesInParent.Length)
            {
                case 0: return null;
                case 1: return canvasesInParent[0];
            }

            foreach (var canvasInParent in canvasesInParent)
            {
                var canvasCount = canvasInParent.GetComponentsInParent<Canvas>().Length;
                if (canvasCount == 1) return canvasInParent;
            }

            return null;
        }

        #endregion

        #region ALIGN

        public static void Align(GameObject[] selection, RelativeTo relativeTo, Axis axis, Bound bound, bool AlignToAnchor,
            bool filterByTopLevel = true, BoundsUtils.ObjectProperty property = BoundsUtils.ObjectProperty.BOUNDING_BOX, bool recordAction = true)
        {
            if (selection.Length == 0) return;
            if (bound == Bound.CENTER && AlignToAnchor) return;

            const string COMMAND_NAME = "Align";

            var selectionBoundsTuple = GetSelectionBounds(selection, relativeTo, axis, filterByTopLevel);
            var selectionBound = GetBound(selectionBoundsTuple.Item2, AlignToAnchor ? (bound == Bound.MAX ? Bound.MIN : Bound.MAX) : bound);
            var anchor = selectionBoundsTuple.Item1;

            foreach (var obj in selection)
            {
                if (obj == anchor && relativeTo != RelativeTo.SELECTION) continue;

                if (recordAction) Undo.RecordObject(obj.transform, COMMAND_NAME);

                var objBound = GetBound(BoundsUtils.GetBoundsRecursive(obj.transform, filterByTopLevel, property), bound);
                var alignedPosition = obj.transform.position;

                switch (axis)
                {
                    case Axis.X:
                        alignedPosition.x = obj.transform.position.x + selectionBound.x - objBound.x;
                        break;
                    case Axis.Y:
                        alignedPosition.y = obj.transform.position.y + selectionBound.y - objBound.y;
                        break;
                    case Axis.Z:
                        alignedPosition.z = obj.transform.position.z + selectionBound.z - objBound.z;
                        break;
                }

                var delta = alignedPosition - obj.transform.position;
                obj.transform.position = alignedPosition;
                if (anchor != null && anchor.transform.parent == obj.transform)
                {
                    Undo.RecordObject(anchor.transform, COMMAND_NAME);
                    anchor.transform.position -= delta;
                }
            }
        }

        #endregion

        #region DISTRIBUTE

        public static void Distribute(GameObject[] selection, Axis axis, Bound bound)
        {
            if (selection.Length < 2) return;
            const string COMMAND_NAME = "Distribute";
            var sortedList = new List<GameObject>(selection);
            switch (axis)
            {
                case Axis.X:
                    sortedList.Sort((obj1, obj2) => GetBound(BoundsUtils.GetBoundsRecursive(obj1.transform), bound).x.CompareTo(GetBound(BoundsUtils.GetBoundsRecursive(obj2.transform), bound).x));
                    break;
                case Axis.Y:
                    sortedList.Sort((obj1, obj2) => GetBound(BoundsUtils.GetBoundsRecursive(obj1.transform), bound).y.CompareTo(GetBound(BoundsUtils.GetBoundsRecursive(obj2.transform), bound).y));
                    break;
                case Axis.Z:
                    sortedList.Sort((obj1, obj2) => GetBound(BoundsUtils.GetBoundsRecursive(obj1.transform), bound).z.CompareTo(GetBound(BoundsUtils.GetBoundsRecursive(obj2.transform), bound).z));
                    break;
            }

            var min = GetBound(BoundsUtils.GetBoundsRecursive(sortedList.First().transform), bound);
            var max = GetBound(BoundsUtils.GetBoundsRecursive(sortedList.Last().transform), bound);

            var objDistance = 0f;
            switch (axis)
            {
                case Axis.X:
                    objDistance = (max.x - min.x) / (selection.Length - 1);
                    break;
                case Axis.Y:
                    objDistance = (max.y - min.y) / (selection.Length - 1);
                    break;
                case Axis.Z:
                    objDistance = (max.z - min.z) / (selection.Length - 1);
                    break;
            }

            for (var i = 0; i < sortedList.Count; ++i)
            {
                var transform = sortedList[i].transform;
                Undo.RecordObject(transform, COMMAND_NAME);
                var distributedPosition = transform.position;
                var objBound = GetBound(BoundsUtils.GetBoundsRecursive(transform), bound);
                switch (axis)
                {
                    case Axis.X:
                        distributedPosition.x += min.x - objBound.x + objDistance * i;
                        break;
                    case Axis.Y:
                        distributedPosition.y += min.y - objBound.y + objDistance * i;
                        break;
                    case Axis.Z:
                        distributedPosition.z += min.z - objBound.z + objDistance * i;
                        break;
                }

                transform.position = distributedPosition;
            }
        }

        public static void DistributeGaps(GameObject[] selection, Axis axis, float strength = 1f, bool recordAction = true)
        {
            if (selection.Length < 2) return;
            const string COMMAND_NAME = "Distribute Gaps";
            var selectionBounds = GetSelectionBounds(selection, RelativeTo.SELECTION, axis).Item2;
            var gapSize = selectionBounds.size;
            foreach (var obj in selection) gapSize -= BoundsUtils.GetBoundsRecursive(obj.transform).size;
            gapSize /= selection.Length - 1;

            var sortedList = new List<GameObject>(selection);
            switch (axis)
            {
                case Axis.X:
                    sortedList.Sort((obj1, obj2) => BoundsUtils.GetBoundsRecursive(obj1.transform).center.x.CompareTo(BoundsUtils.GetBoundsRecursive(obj2.transform).center.x));
                    break;
                case Axis.Y:
                    sortedList.Sort((obj1, obj2) => BoundsUtils.GetBoundsRecursive(obj1.transform).center.y.CompareTo(BoundsUtils.GetBoundsRecursive(obj2.transform).center.y));
                    break;
                case Axis.Z:
                    sortedList.Sort((obj1, obj2) => BoundsUtils.GetBoundsRecursive(obj1.transform).center.z.CompareTo(BoundsUtils.GetBoundsRecursive(obj2.transform).center.z));
                    break;
            }

            var prevMax = BoundsUtils.GetBoundsRecursive(sortedList.First().transform).min - gapSize;

            foreach (var obj in sortedList)
            {
                if (recordAction) Undo.RecordObject(obj.transform, COMMAND_NAME);
                var transform = obj.transform;
                var distributedPosition = transform.position;
                var objBounds = BoundsUtils.GetBoundsRecursive(transform);
                var objMin = objBounds.min;
                switch (axis)
                {
                    case Axis.X:
                        distributedPosition.x += prevMax.x + gapSize.x - objMin.x;
                        break;
                    case Axis.Y:
                        distributedPosition.y += prevMax.y + gapSize.y - objMin.y;
                        break;
                    case Axis.Z:
                        distributedPosition.z += prevMax.z + gapSize.z - objMin.z;
                        break;
                }

                transform.position = Vector3.Lerp(transform.position, distributedPosition, strength);
                prevMax = BoundsUtils.GetBoundsRecursive(transform).max;
            }
        }

        #endregion

        #region GRID ARRANGE

        public class ArrangeAxisData
        {
            private bool _overwrite = true;
            private float _cellSize = 0f;

            public ArrangeAxisData(int priority) => this.priority = priority;

            public int direction { get; set; } = 1;

            public int priority { get; set; } = 0;

            public int cells { get; set; } = 1;

            public Bound aligment { get; set; } = Bound.CENTER;

            public float spacing { get; set; } = 0f;

            public CellSizeType cellSizeType { get; set; } = CellSizeType.BIGGEST_OBJECT;

            public float cellSize
            {
                get => _cellSize;
                set
                {
                    if (value < 0 || Math.Abs(_cellSize - value) < 0) return;
                    _cellSize = value;
                }
            }

            public bool overwrite
            {
                get => _overwrite;
                set
                {
                    if (_overwrite == value) return;
                    _overwrite = value;
                    if (!_overwrite)
                    {
                        cells = 1;
                        priority = 2;
                    }
                }
            }
        }

        public enum SortBy
        {
            SELECTION,
            POSITION,
            HIERARCHY
        }

        public enum CellSizeType
        {
            BIGGEST_OBJECT_PER_GROUP,
            BIGGEST_OBJECT,
            CUSTOM
        }

        public enum ArrangeRelativeTo
        {
            SELECTION,
            FIRST_OBJECT
        }

        public class ArrangeData
        {
            private SortBy _sortBy = SortBy.POSITION;
            private List<Axis> _priorityList = new List<Axis> { Axis.X, Axis.Y, Axis.Z };

            public ArrangeAxisData x { get; set; } = new ArrangeAxisData(0);

            public ArrangeAxisData y { get; set; } = new ArrangeAxisData(1);

            public ArrangeAxisData z { get; set; } = new ArrangeAxisData(2);

            public SortBy sortBy
            {
                get => _sortBy;
                set
                {
                    if (_sortBy == value) return;
                    _sortBy = value;
                    if (_sortBy == SortBy.POSITION)
                    {
                        x.priority = 0;
                        y.priority = 1;
                        z.priority = 2;
                        z.direction = y.direction = x.direction = +1;
                    }
                }
            }

            public BoundsUtils.ObjectProperty alignProperty { get; set; } = BoundsUtils.ObjectProperty.BOUNDING_BOX;

            public ArrangeRelativeTo arrangeRelativeTo { get; set; } = ArrangeRelativeTo.SELECTION;

            public ArrangeAxisData GetData(Axis axis)
            {
                return axis == Axis.X ? x : axis == Axis.Y ? y : z;
            }

            public void UpdatePriorities(Axis axis)
            {
                var activeAxes = Convert.ToInt32(x.overwrite) + Convert.ToInt32(y.overwrite) + Convert.ToInt32(z.overwrite);
                if (activeAxes > 0)
                {
                    if (x.overwrite) x.priority = Mathf.Min(x.priority, activeAxes - 1);
                    if (y.overwrite) y.priority = Mathf.Min(y.priority, activeAxes - 1);
                    if (z.overwrite) z.priority = Mathf.Min(z.priority, activeAxes - 1);
                }

                _priorityList.Remove(axis);
                _priorityList.Insert(GetData(axis).priority, axis);


                for (int priority = 0; priority < 3; ++priority)
                {
                    switch (_priorityList[priority])
                    {
                        case Axis.X:
                            x.priority = priority;
                            break;
                        case Axis.Y:
                            y.priority = priority;
                            break;
                        case Axis.Z:
                            z.priority = priority;
                            break;
                    }
                }
            }

            public Axis GetAxisByPriority(int priority) => _priorityList[priority];
            public ArrangeAxisData GetAxisDataByPriority(int priority) => GetData(_priorityList[priority]);
        }

        private static Dictionary<(int i, int j, int k), GameObject> SortBySelectionOrder(GameObject[] selection, ArrangeData data)
        {
            int i = 0;
            int j = 0;
            int k = 0;

            var dataList = new List<ArrangeAxisData>() { data.x, data.y, data.z };
            dataList.Sort((data1, data2) => data1.priority.CompareTo(data2.priority));

            var p0 = dataList[0] == data.x ? i : dataList[0] == data.y ? j : k;
            var p1 = dataList[1] == data.x ? i : dataList[1] == data.y ? j : k;
            var p2 = dataList[2] == data.x ? i : dataList[2] == data.y ? j : k;

            var objDictionary = new Dictionary<(int i, int j, int k), GameObject>();

            int GetNextCellIndex(int currentIndex, int cellCount) => IsLastCell(currentIndex, cellCount) ? 0 : currentIndex + 1;
            bool IsFirstCell(int currentIndex) => currentIndex == 0;
            bool IsLastCell(int currentIndex, int cellCount) => currentIndex == cellCount - 1;

            foreach (var obj in selection)
            {
                objDictionary.Add((
                    dataList[0] == data.x ? p0 : dataList[1] == data.x ? p1 : p2,
                    dataList[0] == data.y ? p0 : dataList[1] == data.y ? p1 : p2,
                    dataList[0] == data.z ? p0 : dataList[1] == data.z ? p1 : p2), obj);

                p0 = GetNextCellIndex(p0, dataList[0].cells);
                if (!IsFirstCell(p0)) continue;
                p1 = GetNextCellIndex(p1, dataList[1].cells);
                if (!IsFirstCell(p1)) continue;
                p2 = GetNextCellIndex(p2, dataList[2].cells);
            }

            return objDictionary;
        }

        private static Dictionary<(int i, int j, int k), GameObject> SortByPosition(GameObject[] selection, ArrangeData data, Bounds selectionBounds)
        {
            var maxSize = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            var averageSize = Vector3.zero;
            foreach (var obj in selection)
            {
                var objBounds = BoundsUtils.GetBoundsRecursive(obj.transform, true, data.alignProperty);
                maxSize = Vector3.Max(maxSize, objBounds.size);
                averageSize += objBounds.size;
            }

            averageSize /= selection.Length;
            var cellSize = new Vector3(
                data.x.cellSizeType == CellSizeType.BIGGEST_OBJECT ? maxSize.x : data.x.cellSizeType == CellSizeType.BIGGEST_OBJECT_PER_GROUP ? averageSize.x : data.x.cellSize,
                data.y.cellSizeType == CellSizeType.BIGGEST_OBJECT ? maxSize.y : data.y.cellSizeType == CellSizeType.BIGGEST_OBJECT_PER_GROUP ? averageSize.y : data.y.cellSize,
                data.z.cellSizeType == CellSizeType.BIGGEST_OBJECT ? maxSize.z : data.z.cellSizeType == CellSizeType.BIGGEST_OBJECT_PER_GROUP ? averageSize.z : data.z.cellSize);

            var firstCellCenter = selectionBounds.min + cellSize / 2f;

            var cellDict = new Dictionary<(int i, int j, int k), Bounds>();

            for (int k = 0; k < data.z.cells; ++k)
            {
                for (int j = 0; j < data.y.cells; ++j)
                {
                    for (int i = 0; i < data.x.cells; ++i)
                    {
                        var cellCenter = firstCellCenter + new Vector3(cellSize.x * i, cellSize.y * j, cellSize.z * k);
                        var cellBounds = new Bounds(cellCenter, cellSize);
                        cellDict.Add((i, j, k), cellBounds);
                    }
                }
            }

            var unsorted = new List<GameObject>(selection);
            var objDict = new Dictionary<(int i, int j, int k), GameObject>();

            while (unsorted.Count > 0)
            {
                var cellObjectsDict = new Dictionary<(int i, int j, int k), List<(GameObject obj, float sqrDistanceToCorner, float sqrDistanceToCenter)>>();
                foreach (var obj in unsorted)
                {
                    var objBounds = BoundsUtils.GetBoundsRecursive(obj.transform, true, data.alignProperty);
                    var minSqrDistanceToCorner = float.MaxValue;
                    var minSqrDistanceToCenter = float.MaxValue;
                    var closestCell = new KeyValuePair<(int i, int j, int k), Bounds>();
                    foreach (var cell in cellDict)
                    {
                        var objToCorner = new Vector3(
                            objBounds.min.x - cell.Value.min.x,
                            objBounds.min.y - cell.Value.min.y,
                            objBounds.min.z - cell.Value.min.z);
                        var sqrDistanceToCorner = Vector3.SqrMagnitude(objToCorner);
                        var sqrDistanceToCenter = Vector3.SqrMagnitude(objBounds.center - cell.Value.center);
                        if (sqrDistanceToCorner < minSqrDistanceToCorner)
                        {
                            minSqrDistanceToCorner = sqrDistanceToCorner;
                            minSqrDistanceToCenter = sqrDistanceToCenter;
                            closestCell = cell;
                        }
                        else if (Math.Abs(minSqrDistanceToCorner - sqrDistanceToCorner) < 0 && sqrDistanceToCenter < minSqrDistanceToCenter)
                        {
                            minSqrDistanceToCenter = sqrDistanceToCenter;
                            closestCell = cell;
                        }
                    }

                    if (cellObjectsDict.ContainsKey((closestCell.Key))) cellObjectsDict[closestCell.Key].Add((obj, minSqrDistanceToCorner, minSqrDistanceToCenter));
                    else
                    {
                        cellObjectsDict.Add(closestCell.Key, new List<(GameObject obj, float sqrDistanceToCorner, float sqrDistanceToCenter)>());
                        cellObjectsDict[closestCell.Key].Add((obj, minSqrDistanceToCorner, minSqrDistanceToCenter));
                    }
                }

                int GetKeyValue((int i, int j, int k) key) => key.i * (int)Mathf.Pow(10, data.x.priority + 1)
                                                              + key.j * (int)Mathf.Pow(10, data.y.priority + 1)
                                                              + key.k * (int)Mathf.Pow(10, data.z.priority + 1);

                foreach (var cellObjs in cellObjectsDict)
                {
                    var minSqrDistanceToCorner = cellObjs.Value[0].sqrDistanceToCorner;
                    var minSqrDistanceToCenter = cellObjs.Value[0].sqrDistanceToCenter;
                    var minKeyValue = 0;
                    var closestObj = cellObjs.Value[0].obj;
                    for (var i = 1; i < cellObjs.Value.Count; ++i)
                    {
                        var (gameObject, sqrDistanceToCorner, sqrDistanceToCenter) = cellObjs.Value[i];
                        var keyValue = GetKeyValue(cellObjs.Key);
                        if (sqrDistanceToCorner < minSqrDistanceToCorner || (Math.Abs(sqrDistanceToCorner - minSqrDistanceToCorner) < 0 && keyValue < minKeyValue))
                        {
                            minKeyValue = keyValue;
                            minSqrDistanceToCorner = sqrDistanceToCorner;
                            minSqrDistanceToCenter = sqrDistanceToCenter;
                            closestObj = gameObject;
                        }
                        else if (Math.Abs(minSqrDistanceToCorner - sqrDistanceToCorner) < 0 && sqrDistanceToCenter < minSqrDistanceToCenter)
                        {
                            minSqrDistanceToCenter = sqrDistanceToCenter;
                            closestObj = gameObject;
                        }
                    }

                    objDict.Add(cellObjs.Key, closestObj);
                    unsorted.Remove(closestObj);
                    cellDict.Remove(cellObjs.Key);
                }
            }

            return objDict;
        }

        public static bool Arrange(GameObject[] selection, ArrangeData data)
        {
            var cellCount = data.x.cells * data.y.cells * data.z.cells;
            if (selection.Length < 2 || selection.Length > cellCount) return false;


            if (data.sortBy == SortBy.HIERARCHY) selection = SortByHierarchy(selection);
            var firstPosition = selection[0].transform.position;
            Vector3 selectionCenter;
            var selectionBounds = new Bounds();
            if (data.alignProperty == BoundsUtils.ObjectProperty.PIVOT)
            {
                selectionBounds.size = new Vector3(
                    data.x.cellSize * data.x.cells + data.x.spacing * (data.x.cells - 1),
                    data.y.cellSize * data.y.cells + data.y.spacing * (data.y.cells - 1),
                    data.z.cellSize * data.z.cells + data.z.spacing * (data.z.cells - 1));
                selectionBounds.center = Vector3.zero;
                foreach (var obj in selection) selectionBounds.center += obj.transform.position;
                selectionBounds.center /= selection.Length;
            }
            else selectionBounds = GetSelectionBounds(selection, true, data.alignProperty);

            var originalSelectionCenter = selectionCenter = selectionBounds.center;
            Dictionary<(int i, int j, int k), GameObject> objDictionary;
            if (data.sortBy == SortBy.POSITION)
            {
                if (data.alignProperty == BoundsUtils.ObjectProperty.BOUNDING_BOX)
                {
                    var centerBounds = GetSelectionBounds(selection, true, BoundsUtils.ObjectProperty.CENTER);
                    if (data.x.cellSizeType == CellSizeType.CUSTOM) selectionCenter.x = centerBounds.center.x;
                    if (data.y.cellSizeType == CellSizeType.CUSTOM) selectionCenter.y = centerBounds.center.y;
                    if (data.z.cellSizeType == CellSizeType.CUSTOM) selectionCenter.z = centerBounds.center.z;
                }

                objDictionary = SortByPosition(selection, data, selectionBounds);
            }
            else objDictionary = SortBySelectionOrder(selection, data);

            if (selection.Length < cellCount)
            {
                var usedCells = Vector3Int.zero;
                foreach (var key in objDictionary.Keys)
                {
                    usedCells.x = Mathf.Max(usedCells.x, key.i);
                    usedCells.y = Mathf.Max(usedCells.y, key.j);
                    usedCells.z = Mathf.Max(usedCells.z, key.k);
                }

                data.x.cells = usedCells.x + 1;
                data.y.cells = usedCells.y + 1;
                data.z.cells = usedCells.z + 1;
            }

            float[] GetCellSizes(Axis mainAxis, out float totalSize)
            {
                totalSize = 0f;
                var mainAxisData = data.GetData(mainAxis);
                var cellSizes = new float[mainAxisData.cells];
                for (int a = 0; a < mainAxisData.cells; ++a)
                {
                    cellSizes[a] = mainAxisData.cellSize;
                    if (mainAxisData.cellSizeType == CellSizeType.CUSTOM)
                    {
                        totalSize += cellSizes[a];
                        continue;
                    }

                    Axis secondaryAxis1 = Axis.Y;
                    Axis secondaryAxis2 = Axis.Z;
                    if (mainAxis == Axis.Y)
                    {
                        secondaryAxis1 = Axis.X;
                        secondaryAxis2 = Axis.Z;
                    }
                    else if (mainAxis == Axis.Z)
                    {
                        secondaryAxis1 = Axis.X;
                        secondaryAxis2 = Axis.Y;
                    }

                    var seondaryAxisData1 = data.GetData(secondaryAxis1);
                    var seondaryAxisData2 = data.GetData(secondaryAxis2);

                    List<GameObject> objList = new List<GameObject>();
                    for (int b = 0; b < seondaryAxisData1.cells; ++b)
                    for (int c = 0; c < seondaryAxisData2.cells; ++c)
                    {
                        var i = mainAxis == Axis.X ? a : secondaryAxis1 == Axis.X ? b : c;
                        var j = mainAxis == Axis.Y ? a : secondaryAxis1 == Axis.Y ? b : c;
                        var k = mainAxis == Axis.Z ? a : secondaryAxis1 == Axis.Z ? b : c;
                        if (objDictionary.ContainsKey((i, j, k))) objList.Add(objDictionary[(i, j, k)]);
                    }

                    Align(objList.ToArray(), RelativeTo.SELECTION, mainAxis, mainAxisData.aligment, false, true, data.alignProperty);
                    var size = GetSelectionBounds(objList.ToArray(), RelativeTo.SELECTION, mainAxis, true, data.alignProperty).Item2.size;
                    cellSizes[a] = mainAxis == Axis.X ? size.x : mainAxis == Axis.Y ? size.y : size.z;
                    totalSize += cellSizes[a];
                }

                totalSize += mainAxisData.spacing * (mainAxisData.cells - 1);
                return cellSizes;
            }

            var ArrangementSize = Vector3.zero;
            var cellSizesX = GetCellSizes(Axis.X, out ArrangementSize.x);
            var cellSizesY = GetCellSizes(Axis.Y, out ArrangementSize.y);
            var cellSizesZ = GetCellSizes(Axis.Z, out ArrangementSize.z);

            var firstCellCenter = data.arrangeRelativeTo == ArrangeRelativeTo.FIRST_OBJECT
                ? firstPosition
                : selectionCenter - Vector3.Scale(ArrangementSize, new Vector3(data.x.direction, data.y.direction, data.z.direction)) / 2f
                  + new Vector3(cellSizesX[0] * data.x.direction, cellSizesY[0] * data.y.direction, cellSizesZ[0] * data.z.direction) / 2f;
            if (data.x.cellSizeType == CellSizeType.CUSTOM)
            {
                if (data.x.aligment == Bound.MIN) firstCellCenter.x += cellSizesX[0] / 2f;
                else if (data.x.aligment == Bound.MAX) firstCellCenter.x -= cellSizesX[0] / 2f;
            }

            if (data.y.cellSizeType == CellSizeType.CUSTOM)
            {
                if (data.y.aligment == Bound.MIN) firstCellCenter.y += cellSizesY[0] / 2f;
                else if (data.y.aligment == Bound.MAX) firstCellCenter.y -= cellSizesY[0] / 2f;
            }

            if (data.z.cellSizeType == CellSizeType.CUSTOM)
            {
                if (data.z.aligment == Bound.MIN) firstCellCenter.z += cellSizesZ[0] / 2f;
                else if (data.z.aligment == Bound.MAX) firstCellCenter.z -= cellSizesZ[0] / 2f;
            }

            var cells = new Dictionary<(int i, int j, int k), Bounds>();
            var cellCenter = firstCellCenter;
            var cellSize = Vector3.zero;
            for (int i = 0; i < data.x.cells; ++i)
            {
                cellSize.x = cellSizesX[i];
                if (i > 0) cellCenter.x += (cellSizesX[i - 1] / 2f + data.x.spacing + cellSize.x / 2f) * data.x.direction;
                cellCenter.y = firstCellCenter.y;
                for (int j = 0; j < data.y.cells; ++j)
                {
                    cellSize.y = cellSizesY[j];
                    if (j > 0) cellCenter.y += (cellSizesY[j - 1] / 2f + data.y.spacing + cellSize.y / 2) * data.y.direction;
                    cellCenter.z = firstCellCenter.z;
                    for (int k = 0; k < data.z.cells; ++k)
                    {
                        cellSize.z = cellSizesZ[k];
                        if (k > 0) cellCenter.z += (cellSizesZ[k - 1] / 2f + data.z.spacing + cellSize.z / 2) * data.z.direction;
                        cells.Add((i, j, k), new Bounds(cellCenter, cellSize));
                    }
                }
            }

            void AlignObjectInCell(GameObject obj, Bounds cellBounds)
            {
                var objBounds = BoundsUtils.GetBoundsRecursive(obj.transform, true, data.alignProperty);
                var alignedPosition = obj.transform.position;
                alignedPosition.x += GetBound(cellBounds, data.x.aligment).x - GetBound(objBounds, data.x.aligment).x;
                alignedPosition.y += GetBound(cellBounds, data.y.aligment).y - GetBound(objBounds, data.y.aligment).y;
                alignedPosition.z += GetBound(cellBounds, data.z.aligment).z - GetBound(objBounds, data.z.aligment).z;
                obj.transform.position = alignedPosition;
            }

            const string COMMAND_NAME = "Grid Arrange";
            foreach (var key in objDictionary.Keys)
            {
                var obj = objDictionary[key];
                Undo.RecordObject(obj.transform, COMMAND_NAME);
                AlignObjectInCell(obj, cells[key]);
            }

            if (data.alignProperty == BoundsUtils.ObjectProperty.BOUNDING_BOX
                && (data.x.cellSizeType == CellSizeType.CUSTOM || data.z.cellSizeType == CellSizeType.CUSTOM || data.z.cellSizeType == CellSizeType.CUSTOM))
            {
                var newBounds = GetSelectionBounds(selection, true, data.alignProperty);
                var centerDelta = newBounds.center - originalSelectionCenter;
                foreach (var item in selection) item.transform.position -= centerDelta;
            }

            return true;
        }

        #endregion

        #region REARRANGE

        public static void Rearrange(GameObject[] selection, ArrangeBy arrangeBy)
        {
            const string COMMAND_NAME = "Rearrange";
            if (selection.Length < 2) return;
            if (arrangeBy == ArrangeBy.HIERARCHY_ORDER) selection = SortByHierarchy(selection);
            var firstPosition = selection[0].transform.position;
            for (int i = 0; i < selection.Length - 1; ++i)
            {
                Undo.RecordObject(selection[i].transform, COMMAND_NAME);
                selection[i].transform.position = selection[i + 1].transform.position;
            }

            Undo.RecordObject(selection[selection.Length - 1].transform, COMMAND_NAME);
            selection[selection.Length - 1].transform.position = firstPosition;
        }

        #endregion

        #region RADIAL ARRANGE

        public enum RotateAround
        {
            SELECTION_CENTER,
            TRANSFORM_POSITION,
            OBJECT_BOUNDS_CENTER,
            CUSTOM_POSITION
        }

        public enum Shape
        {
            CIRCLE,
            CIRCULAR_SPIRAL,
            ELLIPSE,
            ELLIPTICAL_SPIRAL
        }


        public class RadialArrangeData
        {
            private RotateAround _rotateAround = RotateAround.SELECTION_CENTER;
            private Transform _centerTransform = null;

            public ArrangeBy arrangeBy { get; set; } = ArrangeBy.SELECTION_ORDER;

            public Vector3 axis { get; set; } = Vector3.forward;

            public Shape shape { get; set; } = Shape.CIRCLE;

            public Vector2 startEllipseAxes { get; set; } = Vector2.one;

            public Vector2 endEllipseAxes { get; set; } = Vector2.one;

            public float startAngle { get; set; } = 0f;

            public float maxArcAngle { get; set; } = 360f;

            public bool orientToRadius { get; set; } = false;

            public Vector3 center { get; set; } = Vector3.zero;

            public Vector3 orientDirection { get; set; } = Vector3.right;

            public Vector3 parallelDirection { get; set; } = Vector3.up;

            public Transform centerTransform
            {
                get => _centerTransform;
                set
                {
                    if (_centerTransform == value) return;
                    _centerTransform = value;
                    UpdateCenter();
                }
            }

            public RotateAround rotateAround
            {
                get => _rotateAround;
                set
                {
                    if (_rotateAround == value) return;
                    _rotateAround = value;
                    UpdateCenter();
                }
            }

            public bool overwriteX { get; set; } = true;

            public bool overwriteY { get; set; } = true;

            public bool overwriteZ { get; set; } = true;

            public bool lastSpotEmpty { get; set; } = false;

            public float spacing { get; set; } = 0f;

            public void UpdateCenter()
            {
                if (_centerTransform == null &&
                    (_rotateAround == RotateAround.TRANSFORM_POSITION
                     || _rotateAround == RotateAround.OBJECT_BOUNDS_CENTER)) center = Vector3.zero;
                else if (_rotateAround == RotateAround.TRANSFORM_POSITION) center = _centerTransform.transform.position;
                else if (_rotateAround == RotateAround.OBJECT_BOUNDS_CENTER) center = BoundsUtils.GetBoundsRecursive(_centerTransform).center;
            }

            public void UpdateCenter(GameObject[] selection)
            {
                if (_rotateAround != RotateAround.SELECTION_CENTER) return;
                if (selection.Length == 0) center = Vector3.zero;
                else center = GetSelectionBounds(selection).center;
            }

            public void UpdateCircleSpacing(int selectionCount)
            {
                if (selectionCount == 0)
                {
                    spacing = 0f;
                    return;
                }

                var perimeter = Mathf.PI * startEllipseAxes.x * Mathf.Abs(maxArcAngle) / 180f;
                spacing = perimeter / (selectionCount - (lastSpotEmpty ? 0f : 1f));
            }

            public void UpdateCircleRadius(int selectionCount)
            {
                if (selectionCount == 0)
                {
                    startEllipseAxes = endEllipseAxes = Vector2.zero;
                    return;
                }

                var perimeter = spacing * (selectionCount - (lastSpotEmpty ? 0f : 1f));
                startEllipseAxes = endEllipseAxes = Vector2.one * (perimeter / Mathf.PI / Mathf.Abs(maxArcAngle) * 180f);
            }
        }

        private static float GetEllipseRadius(Vector2 ellipseAxes, float angle)
        {
            if (Math.Abs(ellipseAxes.x - ellipseAxes.y) < 0) return ellipseAxes.x;
            var a = ellipseAxes.x;
            var b = ellipseAxes.y;
            var sin = Mathf.Sin(angle * Mathf.Deg2Rad);
            var cos = Mathf.Cos(angle * Mathf.Deg2Rad);
            return a * b / Mathf.Sqrt(a * a * sin * sin + b * b * cos * cos);
        }

        private static Vector3 GetRadialPosition(Vector3 center, Vector3 axis, float radius, float angle)
        {
            var radiusDirection = Vector3.right;
            if (axis.x > 0 || axis.y < 0) radiusDirection = Vector3.forward;
            else if (axis.x < 0 || axis.z > 0) radiusDirection = Vector3.up;
            return center + Quaternion.AngleAxis(angle, axis) * radiusDirection * radius;
        }

        public static void RadialArrange(GameObject[] selection, RadialArrangeData data)
        {
            const string COMMAND_NAME = "Radial Arrange";
            if (data.arrangeBy == ArrangeBy.HIERARCHY_ORDER) selection = SortByHierarchy(selection);
            data.UpdateCenter();
            var angle = data.startAngle;

            var deltaAngle = data.maxArcAngle / (selection.Length - (data.lastSpotEmpty ? 0f : 1f));
            var ellipseAxes = data.startEllipseAxes;
            var deltaEllipseAxes = (data.endEllipseAxes - data.startEllipseAxes) / ((float)selection.Length - 1);
            foreach (var obj in selection)
            {
                Undo.RecordObject(obj.transform, COMMAND_NAME);
                var radius = GetEllipseRadius(ellipseAxes, angle);
                var position = GetRadialPosition(data.center, data.axis, radius, angle);
                obj.transform.position = new Vector3(
                    data.overwriteX ? position.x : obj.transform.position.x,
                    data.overwriteY ? position.y : obj.transform.position.y,
                    data.overwriteZ ? position.z : obj.transform.position.z);
                if (data.orientToRadius)
                {
                    obj.transform.rotation = Quaternion.identity;
                    LookAtCenter(obj.transform, data.center, data.axis, data.orientDirection, data.parallelDirection);
                }

                angle += deltaAngle;
                ellipseAxes += deltaEllipseAxes;
            }
        }

        #endregion

        #region PROGRESSION

        public enum IncrementalDataType
        {
            CONSTANT_DELTA,
            CURVE,
            OBJECT_SIZE
        }

        public enum ArrangeBy
        {
            SELECTION_ORDER,
            HIERARCHY_ORDER
        }

        public class ProgressiveAxisData
        {
            public float constantDelta { get; set; } = 0f;

            public AnimationCurve curve { get; set; } = AnimationCurve.Constant(0, 1, 0);

            public float curveRangeMin { get; set; } = 0f;

            public float curveRangeSize { get; set; } = 0f;

            public Rect curveRange { get; set; } = new Rect(0, 0, 1, 1);

            public bool overwrite { get; set; } = true;
        }

        public class ProgressionData
        {
            public ArrangeBy arrangeOrder { get; set; } = ArrangeBy.HIERARCHY_ORDER;

            public IncrementalDataType type { get; set; } = IncrementalDataType.CONSTANT_DELTA;

            public Vector3 constantDelta
            {
                get => new Vector3(x.constantDelta, y.constantDelta, z.constantDelta);
                set
                {
                    x.constantDelta = value.x;
                    y.constantDelta = value.y;
                    z.constantDelta = value.z;
                }
            }

            public Vector3 curveRangeMin
            {
                get => new Vector3(x.curveRangeMin, y.curveRangeMin, z.curveRangeMin);
                set
                {
                    if (new Vector3(x.curveRangeMin, y.curveRangeMin, z.curveRangeMin) == value) return;
                    var rangeX = x.curveRange;
                    rangeX.yMin = x.curveRangeMin = value.x;
                    x.curveRange = rangeX;
                    var rangeY = y.curveRange;
                    rangeY.yMin = y.curveRangeMin = value.y;
                    y.curveRange = rangeY;
                    var rangeZ = z.curveRange;
                    rangeZ.yMin = z.curveRangeMin = value.z;
                    z.curveRange = rangeZ;
                    UpdateRanges();
                }
            }

            public Vector3 curveRangeSize
            {
                get => new Vector3(x.curveRangeSize, y.curveRangeSize, z.curveRangeSize);
                set
                {
                    if (new Vector3(x.curveRangeSize, y.curveRangeSize, z.curveRangeSize) == value) return;
                    x.curveRangeSize = value.x;
                    y.curveRangeSize = value.y;
                    z.curveRangeSize = value.z;
                    UpdateRanges();
                }
            }

            public ProgressiveAxisData x { get; set; } = new ProgressiveAxisData();

            public ProgressiveAxisData y { get; set; } = new ProgressiveAxisData();

            public ProgressiveAxisData z { get; set; } = new ProgressiveAxisData();

            private void UpdateRanges()
            {
                var rangeX = x.curveRange;
                rangeX.yMax = x.curveRangeMin + x.curveRangeSize;
                x.curveRange = rangeX;
                var rangeY = y.curveRange;
                rangeY.yMax = y.curveRangeMin + y.curveRangeSize;
                y.curveRange = rangeY;
                var rangeZ = z.curveRange;
                rangeZ.yMax = z.curveRangeMin + z.curveRangeSize;
                z.curveRange = rangeZ;
            }

            public Vector3 EvaluateCurve(float t)
            {
                return new Vector3(
                    x.overwrite ? x.curve.Evaluate(t) : 0f,
                    y.overwrite ? y.curve.Evaluate(t) : 0f,
                    z.overwrite ? z.curve.Evaluate(t) : 0f);
            }

            public Rect GetRect(Axis axis)
            {
                switch (axis)
                {
                    case Axis.X:
                        return x.curveRange;
                    case Axis.Y:
                        return y.curveRange;
                    default:
                        return z.curveRange;
                }
            }
        }

        private static int[] GetHierarchyIndex(GameObject obj)
        {
            var idxList = new List<int>();
            var parent = obj.transform;
            do
            {
                idxList.Insert(0, parent.transform.GetSiblingIndex());
                parent = parent.transform.parent;
            } while (parent != null);

            return idxList.ToArray();
        }

        public static void IncrementalPosition(GameObject[] selection, ProgressionData data, bool orientToPath, Vector3 orientation)
        {
            if (selection.Length < 2) return;
            const string COMMAND_NAME = "Position Progression";
            if (data.arrangeOrder == ArrangeBy.HIERARCHY_ORDER) selection = SortByHierarchy(selection);
            var position = selection[0].transform.position;
            var t = 0f;
            var delta = 1f / (selection.Length - 1f);
            var i = 0;
            GameObject prevObj = null;
            foreach (var obj in selection)
            {
                var bounds = BoundsUtils.GetBoundsRecursive(obj.transform);
                var centerLocalPos = obj.transform.TransformVector(obj.transform.InverseTransformPoint(bounds.center));
                if (i > 0 && data.type == IncrementalDataType.OBJECT_SIZE) position += bounds.size / 2f - centerLocalPos;
                ++i;
                if (!orientToPath || (orientToPath && data.type != IncrementalDataType.OBJECT_SIZE)) Undo.RecordObject(obj.transform, COMMAND_NAME);
                obj.transform.position = new Vector3(
                    data.x.overwrite ? position.x : obj.transform.position.x,
                    data.y.overwrite ? position.y : obj.transform.position.y,
                    data.z.overwrite ? position.z : obj.transform.position.z);
                t += delta;

                position = data.type == IncrementalDataType.CONSTANT_DELTA
                    ? position + data.constantDelta
                    : data.type == IncrementalDataType.CURVE
                        ? selection[0].transform.position + data.EvaluateCurve(t)
                        : position + centerLocalPos + bounds.size / 2f;

                if (!orientToPath) continue;
                if (data.type != IncrementalDataType.OBJECT_SIZE) LookAtNext(obj.transform, position, orientation);
                else if (i > 1)
                {
                    Undo.RecordObject(prevObj.transform, COMMAND_NAME);
                    LookAtNext(prevObj.transform, obj.transform.position, orientation);
                }

                if (data.type == IncrementalDataType.OBJECT_SIZE && i == selection.Length)
                {
                    Undo.RecordObject(obj.transform, COMMAND_NAME);
                    obj.transform.eulerAngles = prevObj.transform.eulerAngles;
                }

                prevObj = obj;
            }
        }

        private static void LookAtNext(Transform transform, Vector3 next, Vector3 orientation)
        {
            var objToCenter = next - transform.position;
            transform.rotation = Quaternion.FromToRotation(orientation, objToCenter);
        }

        public static void IncrementalRotation(GameObject[] selection, ProgressionData data)
        {
            if (selection.Length < 2) return;
            const string COMMAND_NAME = "Rotation Progression";
            if (data.arrangeOrder == ArrangeBy.HIERARCHY_ORDER)
            {
                selection = SortByHierarchy(selection);
            }

            var eulerAngles = selection[0].transform.rotation.eulerAngles;
            var firstObjEulerAngles = eulerAngles;
            var t = 0f;
            foreach (var obj in selection)
            {
                Undo.RecordObject(obj.transform, COMMAND_NAME);
                if (data.type == IncrementalDataType.CURVE)
                {
                    eulerAngles = firstObjEulerAngles + data.EvaluateCurve(t);
                    t += 1f / (selection.Length - 1f);
                }

                obj.transform.rotation = Quaternion.Euler(
                    data.x.overwrite ? eulerAngles.x : obj.transform.rotation.eulerAngles.x,
                    data.y.overwrite ? eulerAngles.y : obj.transform.rotation.eulerAngles.y,
                    data.z.overwrite ? eulerAngles.z : obj.transform.rotation.eulerAngles.z);
                if (data.type == IncrementalDataType.CONSTANT_DELTA) eulerAngles += data.constantDelta;
            }
        }

        public static void IncrementalScale(GameObject[] selection, ProgressionData data)
        {
            if (selection.Length < 2) return;
            const string COMMAND_NAME = "Scale Progression";
            if (data.arrangeOrder == ArrangeBy.HIERARCHY_ORDER) selection = SortByHierarchy(selection);
            var scale = selection[0].transform.localScale;
            var firstObjScale = scale;
            var t = 0f;
            foreach (var obj in selection)
            {
                Undo.RecordObject(obj.transform, COMMAND_NAME);

                if (data.type == IncrementalDataType.CURVE)
                {
                    scale = firstObjScale + data.EvaluateCurve(t);
                    t += 1f / (selection.Length - 1f);
                }

                obj.transform.localScale = new Vector3(
                    data.x.overwrite ? scale.x : obj.transform.localScale.x,
                    data.y.overwrite ? scale.y : obj.transform.localScale.y,
                    data.z.overwrite ? scale.z : obj.transform.localScale.z);

                if (data.type == IncrementalDataType.CONSTANT_DELTA) scale += data.constantDelta;
            }
        }

        #endregion

        #region RANDOMIZE

        public class RandomizeAxisData
        {
            public bool randomizeAxis { get; set; } = true;

            public RandomUtils.Range offset { get; set; } = new RandomUtils.Range();
        }

        public class RandomizeData
        {
            public RandomizeAxisData x { get; set; } = new RandomizeAxisData();

            public RandomizeAxisData y { get; set; } = new RandomizeAxisData();

            public RandomizeAxisData z { get; set; } = new RandomizeAxisData();

            public float multiplier { get; set; } = 1f;
        }

        public static void RandomizePositions(IEnumerable<GameObject> selection, RandomizeData data)
        {
            const string COMMAND_NAME = "Randomize Position";
            foreach (var obj in selection)
            {
                Undo.RecordObject(obj.transform, COMMAND_NAME);
                obj.transform.position += new Vector3(
                    data.x.randomizeAxis ? data.x.offset.randomValue * data.multiplier : 0f,
                    data.y.randomizeAxis ? data.y.offset.randomValue * data.multiplier : 0f,
                    data.z.randomizeAxis ? data.z.offset.randomValue * data.multiplier : 0f);
            }
        }

        public static void RandomizeRotations(IEnumerable<GameObject> selection, RandomizeData data)
        {
            const string COMMAND_NAME = "Randomize Rotation";
            foreach (var obj in selection)
            {
                Undo.RecordObject(obj.transform, COMMAND_NAME);
                obj.transform.Rotate(
                    data.x.randomizeAxis ? data.x.offset.randomValue * data.multiplier : 0f,
                    data.y.randomizeAxis ? data.y.offset.randomValue * data.multiplier : 0f,
                    data.z.randomizeAxis ? data.z.offset.randomValue * data.multiplier : 0f);
            }
        }

        public static void RandomizeScales(IEnumerable<GameObject> selection, RandomizeData data, bool separateAxes)
        {
            const string COMMAND_NAME = "Randomize Scale";
            foreach (var obj in selection)
            {
                Undo.RecordObject(obj.transform, COMMAND_NAME);
                if (separateAxes)
                {
                    obj.transform.localScale += new Vector3(
                        data.x.randomizeAxis ? data.x.offset.randomValue * data.multiplier : obj.transform.localScale.x,
                        data.y.randomizeAxis ? data.y.offset.randomValue * data.multiplier : obj.transform.localScale.y,
                        data.z.randomizeAxis ? data.z.offset.randomValue * data.multiplier : obj.transform.localScale.z);
                }
                else
                {
                    var value = data.x.offset.randomValue * data.multiplier;
                    obj.transform.localScale += new Vector3(value, value, value);
                }
            }
        }

        #endregion

        #region HOMOGENIZE

        public class HomogenizeAxis
        {
            public bool homogenize { get; set; } = true;

            public float strength { get; set; } = 0.1f;
        }

        public class HomogenizeData
        {
            public HomogenizeAxis x { get; set; } = new HomogenizeAxis();

            public HomogenizeAxis y { get; set; } = new HomogenizeAxis();

            public HomogenizeAxis z { get; set; } = new HomogenizeAxis();
        }

        public static void HomogenizeSpacing(GameObject[] selection, HomogenizeData data)
        {
            const string COMMAND_NAME = "Homogenize Spacing";
            foreach (var obj in selection) Undo.RecordObject(obj.transform, COMMAND_NAME);
            if (data.x.homogenize) DistributeGaps(selection, Axis.X, data.x.strength, false);
            if (data.y.homogenize) DistributeGaps(selection, Axis.Y, data.y.strength, false);
            if (data.z.homogenize) DistributeGaps(selection, Axis.Z, data.z.strength, false);
        }

        public static void HomogenizeRotation(GameObject[] selection, HomogenizeData data)
        {
            const string COMMAND_NAME = "Homogenize Rotation";
            var sum = Vector3.zero;
            foreach (var obj in selection)
            {
                var euler = obj.transform.eulerAngles;
                if (euler.x < 0) euler.x = 360f + euler.x;
                if (euler.y < 0) euler.y = 360f + euler.y;
                if (euler.z < 0) euler.z = 360f + euler.z;
                sum += euler;
            }

            var average = sum / selection.Length;
            foreach (var obj in selection)
            {
                Undo.RecordObject(obj.transform, COMMAND_NAME);
                var newEulerAngles = obj.transform.eulerAngles;
                if (data.x.homogenize) newEulerAngles.x = Mathf.LerpAngle(obj.transform.eulerAngles.x, average.x, data.x.strength);
                if (data.y.homogenize) newEulerAngles.y = Mathf.LerpAngle(obj.transform.eulerAngles.y, average.y, data.y.strength);
                if (data.z.homogenize) newEulerAngles.z = Mathf.LerpAngle(obj.transform.eulerAngles.z, average.z, data.z.strength);
                obj.transform.eulerAngles = newEulerAngles;
            }
        }

        public static void HomogenizeScale(GameObject[] selection, HomogenizeData data)
        {
            const string COMMAND_NAME = "Homogenize Scale";
            var sum = Vector3.zero;
            foreach (var obj in selection) sum += obj.transform.localScale;
            var average = sum / selection.Length;
            foreach (var obj in selection)
            {
                Undo.RecordObject(obj.transform, COMMAND_NAME);
                var newScale = obj.transform.localScale;
                if (data.x.homogenize) newScale.x = Mathf.Lerp(obj.transform.localScale.x, average.x, data.x.strength);
                if (data.y.homogenize) newScale.y = Mathf.Lerp(obj.transform.localScale.y, average.y, data.y.strength);
                if (data.z.homogenize) newScale.z = Mathf.Lerp(obj.transform.localScale.z, average.z, data.z.strength);
                obj.transform.localScale = newScale;
            }
        }

        #endregion

        #region UNOVERLAP

        public class UnoverlapAxisData
        {
            public bool unoverlap { get; set; } = true;

            public float minDistance { get; set; } = 0f;

            public UnoverlapAxisData(bool unoverlap = true, float minDistance = 0f)
            {
                this.unoverlap = unoverlap;
                this.minDistance = minDistance;
            }
        }

        public class UnoverlapData
        {
            public UnoverlapAxisData x { get; set; } = new UnoverlapAxisData();

            public UnoverlapAxisData y { get; set; } = new UnoverlapAxisData();

            public UnoverlapAxisData z { get; set; } = new UnoverlapAxisData();

            public UnoverlapAxisData GetData(in Axis axis)
            {
                return axis == Axis.X ? x : axis == Axis.Y ? y : z;
            }
        }

        private class OverlapData
        {
            public Vector3 size = Vector3.zero;
            public float volume = 0f;
            public Vector3 solution = Vector3.zero;
            public float solutionVolume = 0f;
        }

        private class OverlapedObject
        {
            public Bounds bounds;
            public int objId = -1;
            public Vector3 transformPosition = Vector3.zero;
            public List<OverlapData> _dataList = new List<OverlapData>();
            public int moveCount = 0;

            public bool isOverlaped
            {
                get
                {
                    foreach (var data in _dataList)
                    {
                        if (data.volume != 0) return true;
                    }

                    return false;
                }
            }

            public float overlapedVolume
            {
                get
                {
                    var retVal = 0f;
                    foreach (var data in _dataList) retVal += data.volume;
                    return retVal;
                }
            }

            public float solutionVolume
            {
                get { return _dataList.Sum(data => data.solutionVolume); }
            }

            public List<Vector3> solutions
            {
                get
                {
                    var retVal = _dataList.Select(data => data.solution).ToList();
                    retVal.Sort((s1, s2) => s1.magnitude.CompareTo(s2.magnitude));
                    return retVal;
                }
            }

            private void MoveTest(Axis axis, bool unoverlap, Vector3 solution, ref float minVol, ref Vector3 bestMove)
            {
                var moveSize = GetVectorComponent(solution, axis);
                if (!unoverlap || moveSize == 0) return;
                var move = GetMoveVector(moveSize, axis);
                var obj = new OverlapedObject();
                obj.bounds = bounds;
                obj.bounds.center += move;
                var vol = obj.solutionVolume;
                if (vol < minVol && Mathf.Abs(moveSize) < bestMove.magnitude)
                {
                    minVol = vol;
                    bestMove = move;
                }
            }

            public bool ExecuteBestSolution(OverlapedObject[] selection, UnoverlapData unoverlapData)
            {
                var bestMove = new Vector3(100000, 100000, 100000);
                var minVol = overlapedVolume;
                foreach (var solution in solutions)
                {
                    MoveTest(Axis.X, unoverlapData.x.unoverlap, solution, ref minVol, ref bestMove);
                    MoveTest(Axis.Y, unoverlapData.y.unoverlap, solution, ref minVol, ref bestMove);
                    MoveTest(Axis.Z, unoverlapData.z.unoverlap, solution, ref minVol, ref bestMove);
                }

                if (overlapedVolume > minVol)
                {
                    bounds.center += bestMove;
                    transformPosition = transformPosition + bestMove;
                    ++moveCount;
                    return true;
                }

                return false;
            }
        }

        private static float GetVectorComponent(Vector3 v, Axis axis)
        {
            return axis == Axis.X ? v.x : axis == Axis.Y ? v.y : v.z;
        }

        private static Vector3 GetMoveVector(float move, Axis axis)
        {
            return new Vector3(axis == Axis.X ? move : 0f, axis == Axis.Y ? move : 0f, axis == Axis.Z ? move : 0f);
        }

        private static void GetOverlapedDataAxis(Axis axis, UnoverlapData unoverlapData, OverlapedObject[] selection, int index, Bounds b1, Bounds b2,
            bool getSolutionVolumen, ref OverlapData retVal, ref float minVol, ref Vector3 bestMove)
        {
            var retValSize = GetVectorComponent(retVal.size, axis);
            if (!unoverlapData.GetData(axis).unoverlap || retValSize <= 0) return;
            var tempObj = new OverlapedObject();
            var tempSelection = selection.ToList();
            tempSelection.RemoveAt(index);
            tempSelection.Insert(0, tempObj);

            var b1Min = GetVectorComponent(b1.min, axis);
            var b1Max = GetVectorComponent(b1.max, axis);
            var b2Min = GetVectorComponent(b2.min, axis);
            var b2Max = GetVectorComponent(b2.max, axis);

            var pSol = b2Max - b1Min;
            var nSol = b2Min - b1Max;

            var moveSize = pSol < -nSol ? pSol : nSol;
            var move = GetMoveVector(moveSize, axis);
            if (getSolutionVolumen)
            {
                tempObj.bounds = b1;
                tempObj.bounds.center += move;

                tempObj._dataList = GetOverlapedData(tempSelection.ToArray(), 0, unoverlapData, false);
                var vol = tempObj.overlapedVolume;
                if (vol < minVol || (Math.Abs(vol - minVol) < 0 && Mathf.Abs(moveSize) < bestMove.magnitude))
                {
                    minVol = vol;
                    bestMove = move;
                    retVal.solution = bestMove;
                    retVal.solutionVolume = tempObj.overlapedVolume;
                }
            }
            else
            {
                retVal.solution = move;
                retVal.solutionVolume = retVal.volume;
            }
        }

        private static OverlapData GetOverlapedData(OverlapedObject[] selection, int index, Bounds b2, UnoverlapData unoverlapData, bool getSolutionVolumen)
        {
            var b1 = selection[index].bounds;
            var min = Vector3.Max(b1.min, b2.min);
            var max = Vector3.Min(b1.max, b2.max);

            var retVal = new OverlapData();
            retVal.size = Vector3.Max(max - min, Vector3.zero);

            retVal.volume = (unoverlapData.x.unoverlap ? retVal.size.x : 1f) * (unoverlapData.y.unoverlap ? retVal.size.y : 1f) * (unoverlapData.z.unoverlap ? retVal.size.z : 1f);

            if (retVal.volume > 0)
            {
                var bestMove = new Vector3(100000, 100000, 100000);
                var minVol = float.MaxValue;
                GetOverlapedDataAxis(Axis.X, unoverlapData, selection, index, b1, b2, getSolutionVolumen, ref retVal, ref minVol, ref bestMove);
                GetOverlapedDataAxis(Axis.Y, unoverlapData, selection, index, b1, b2, getSolutionVolumen, ref retVal, ref minVol, ref bestMove);
                GetOverlapedDataAxis(Axis.Z, unoverlapData, selection, index, b1, b2, getSolutionVolumen, ref retVal, ref minVol, ref bestMove);
            }

            return retVal;
        }

        private static List<OverlapData> GetOverlapedData(OverlapedObject[] selection, int index, UnoverlapData unoverlapData, bool getSolutionVolumen)
        {
            var retVal = new List<OverlapData>();
            var target = selection[index];
            foreach (var obj in selection)
            {
                if (obj == target) continue;
                var data = GetOverlapedData(selection, index, obj.bounds, unoverlapData, getSolutionVolumen);
                if (data.size != Vector3.zero && data.solution != Vector3.zero)
                {
                    retVal.Add(data);
                    if (retVal.Count >= 3) break;
                }
            }

            return retVal;
        }

        private static float GetBoundsVolume(Bounds bounds)
        {
            var size = Vector3.Max(bounds.size, new Vector3(0.001f, 0.001f, 0.001f));
            return size.x * size.y * size.z;
        }

        private static int CompareOverlapedObjects(OverlapedObject obj1, OverlapedObject obj2)
        {
            if (obj1.moveCount < obj2.moveCount) return -1;
            else if (obj1.moveCount > obj2.moveCount) return 1;
            else
            {
                var obj1Vol = GetBoundsVolume(obj1.bounds);
                var obj2Vol = GetBoundsVolume(obj2.bounds);
                var v1 = obj1.overlapedVolume / obj1Vol;
                var v2 = obj2.overlapedVolume / obj2Vol;
                if (Math.Abs(v1 - v2) < 0)
                {
                    var r = obj1Vol.CompareTo(obj2Vol);
                    if (r != 0) return r;
                    v1 = obj1.solutionVolume / obj1Vol;
                    v2 = obj2.solutionVolume / obj2Vol;
                    return v1.CompareTo(v2);
                }

                if (v1 == 0) return 1;
                if (v2 == 0) return -1;

                {
                    var r = v1.CompareTo(v2);
                    if (r != 0) return r;
                    return obj1Vol.CompareTo(obj2Vol);
                }
            }
        }

        public class Unoverlapper
        {
            private readonly (int objId, Bounds bounds)[] _selection;
            private readonly UnoverlapData _unoverlapData;
            private bool _cancel = false;

            public Unoverlapper((int objId, Bounds bounds)[] selection, UnoverlapData unoverlapData)
            {
                _selection = selection;
                _unoverlapData = unoverlapData;
            }

            public event Action<float> progressChanged;
            public event Action<(int objId, Vector3 offset)[]> OnDone;

            public void RemoveOverlaps()
            {
                if (!_unoverlapData.x.unoverlap && !_unoverlapData.y.unoverlap && !_unoverlapData.z.unoverlap) return;

                var minSize = new Vector3(0.001f, 0.001f, 0.001f);

                var overlapedList = new List<OverlapedObject>();

                foreach (var obj in _selection)
                {
                    var overlapedObj = new OverlapedObject();
                    overlapedObj.bounds = obj.bounds;
                    overlapedObj.objId = obj.objId;
                    overlapedObj.bounds.center = new Vector3
                    (_unoverlapData.x.unoverlap ? overlapedObj.bounds.center.x : 0f,
                        _unoverlapData.y.unoverlap ? overlapedObj.bounds.center.y : 0f,
                        _unoverlapData.z.unoverlap ? overlapedObj.bounds.center.z : 0f);
                    overlapedObj.bounds.size = Vector3.Max(overlapedObj.bounds.size, minSize) + new Vector3(_unoverlapData.x.minDistance, _unoverlapData.y.minDistance, _unoverlapData.z.minDistance);
                    overlapedList.Add(overlapedObj);
                }

                var i = 0;
                foreach (var obj in overlapedList)
                {
                    obj._dataList = GetOverlapedData(overlapedList.ToArray(), i, _unoverlapData, true);
                    ++i;
                }

                overlapedList.Sort(CompareOverlapedObjects);
                var prevProgress = 0f;
                var overlapedObjects = 0;
                do
                {
                    if (_cancel) return;
                    var first = overlapedList[0];
                    if (!first.isOverlaped)
                    {
                        overlapedList.RemoveAt(0);
                        overlapedList.Add(first);
                        continue;
                    }
                    else
                    {
                        var executed = first.ExecuteBestSolution(overlapedList.ToArray(), _unoverlapData);
                        if (!executed)
                        {
                            overlapedList.RemoveAt(0);
                            overlapedList.Add(first);
                            continue;
                        }
                        else
                        {
                            overlapedList.Sort(CompareOverlapedObjects);
                            if (overlapedList[0] == first)
                            {
                                overlapedList.RemoveAt(0);
                                overlapedList.Add(first);
                            }
                        }
                    }

                    overlapedObjects = 0;
                    i = 0;
                    foreach (var obj in overlapedList)
                    {
                        obj._dataList = GetOverlapedData(overlapedList.ToArray(), i, _unoverlapData, true);
                        ++i;
                        if (obj.isOverlaped) ++overlapedObjects;
                    }

                    var progress = Mathf.Max(1f - overlapedObjects / (float)_selection.Length, prevProgress);
                    if (Math.Abs(prevProgress - progress) > 0) progressChanged(progress);
                    prevProgress = progress;
                } while (overlapedObjects > 0);

                var boundsArray = overlapedList.Select(obj => (obj.objId, obj.transformPosition)).ToArray();
                OnDone(boundsArray);
            }

            public void Cancel()
            {
                _cancel = true;
            }
        }

        #endregion

        #region EDIT PIVOT

        private const string EDIT_PIVOT_COMMAND_NAME = "Edit Pivot";

        private static UnityEngine.Material _pivotMaterial = null;

        private static UnityEngine.Material pivotMaterial
        {
            get
            {
                if (_pivotMaterial == null) _pivotMaterial = new UnityEngine.Material(UnityEngine.Shader.Find("PluginMaster/Pivot"));
                return _pivotMaterial;
            }
        }

        private static Mesh _pivotMesh = null;

        private static Mesh pivotMesh
        {
            get
            {
                if (_pivotMesh == null) _pivotMesh = Resources.Load<Mesh>("Meshes/Sphere");
                return _pivotMesh;
            }
        }

        private static GameObject _pivot = null;

        public static GameObject StartEditingPivot(GameObject target)
        {
            if (target == null || target.scene.rootCount == 0) return null;
            var pivot = new GameObject("Pivot");
            Tools.current = UnityEditor.Tool.Move;
            pivot.transform.parent = target.transform;
            pivot.transform.localPosition = Vector3.zero;
            pivot.transform.localRotation = Quaternion.identity;
            pivot.transform.localScale = Vector3.one;
            Selection.activeGameObject = pivot;
            _pivot = pivot;
            return pivot;
        }

        public static void SaveMesh(MeshFilter meshFilter, string savePath, Transform pivot)
        {
            Mesh mesh = meshFilter.sharedMesh;
            var otherFilters = new List<MeshFilter>();
            if (!string.IsNullOrEmpty(AssetDatabase.GetAssetPath(mesh)))
            {
                mesh = Object.Instantiate(mesh);
                var allFilters = Object.FindObjectsOfType<MeshFilter>();
                foreach (var filter in allFilters)
                {
                    if (filter == meshFilter) continue;
                    var filterPath = AssetDatabase.GetAssetPath(filter.sharedMesh);
                    if (filterPath != savePath) continue;
                    otherFilters.Add(filter);
                }
            }

            AssetDatabase.CreateAsset(mesh, savePath);
            AssetDatabase.SaveAssets();
            Undo.RecordObject(meshFilter, EDIT_PIVOT_COMMAND_NAME);
            meshFilter.sharedMesh = mesh;
            ApplyPivot(pivot);
            foreach (var filter in otherFilters)
            {
                Undo.RecordObject(filter, EDIT_PIVOT_COMMAND_NAME);
                filter.mesh = null;
                filter.mesh = mesh;
                var target = filter.transform;
                EditColliders(target, pivot);
                EditNavMeshObject(target, pivot);
                EditPivotPositionAndRotation(target, pivot);
            }
        }

        private static void EditSprite(SpriteRenderer renderer, Transform pivot)
        {
            var sprite = renderer.sprite;
            var rect = sprite.rect;
            var pixelsPerUnit = sprite.pixelsPerUnit;
            var min = renderer.transform.InverseTransformPoint(renderer.bounds.min);
            var localPosition = pivot.localPosition;
            var pivot2D = new Vector2((localPosition.x - min.x) * pixelsPerUnit / rect.width, (localPosition.y - min.y) * pixelsPerUnit / rect.height);

            var path = AssetDatabase.GetAssetPath(renderer.sprite);
            var textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
            Undo.RecordObject(textureImporter, EDIT_PIVOT_COMMAND_NAME);
            var settings = new TextureImporterSettings();
            textureImporter.ReadTextureSettings(settings);
            settings.spriteAlignment = (int)SpriteAlignment.Custom;
            settings.spritePivot = pivot2D;
            textureImporter.SetTextureSettings(settings);
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);

            var allRenderers = Object.FindObjectsOfType<SpriteRenderer>();
            foreach (var r in allRenderers)
            {
                if (r == renderer) continue;
                if (r.sprite != renderer.sprite) continue;
                EditColliders(r.transform, pivot);
                EditPivotPositionAndRotation(r.transform, pivot);
            }
        }

        private static void EditRectTransform(RectTransform transform, Transform pivot)
        {
            var localPivot = transform.InverseTransformPoint(pivot.position);
            var rect = transform.rect;
            transform.pivot = new Vector2((localPivot.x - rect.min.x) / rect.width, (localPivot.y - rect.min.y) / rect.height);
        }

        private static void EditMesh(Mesh mesh, Transform pivot)
        {
            var vertices = mesh.vertices;
            var normals = mesh.normals;
            var tangents = mesh.tangents;

            if (pivot.localPosition != Vector3.zero)
            {
                for (int i = 0; i < vertices.Length; ++i) vertices[i] -= pivot.localPosition;
            }

            if (pivot.localEulerAngles != Vector3.zero)
            {
                var invRot = Quaternion.Inverse(pivot.localRotation);
                for (int i = 0; i < vertices.Length; ++i)
                {
                    vertices[i] = invRot * vertices[i];
                    normals[i] = invRot * normals[i];
                    var tanDir = invRot * tangents[i];
                    tangents[i] = new Vector4(tanDir.x, tanDir.y, tanDir.z, tangents[i].w);
                }
            }

            mesh.vertices = vertices;
            mesh.normals = normals;
            mesh.tangents = tangents;
            mesh.RecalculateBounds();
        }

        private static void EditColliders(Transform target, Transform pivot)
        {
            var meshFilter = pivot.parent.GetComponent<MeshFilter>();
            var meshColliders = target.GetComponents<MeshCollider>();
            foreach (var collider in meshColliders)
            {
                if (collider.sharedMesh == null) continue;
                Undo.RecordObject(collider, EDIT_PIVOT_COMMAND_NAME);
                collider.sharedMesh = null;
                collider.sharedMesh = meshFilter.sharedMesh;
            }

            var boxColliders = target.GetComponents<BoxCollider>();
            foreach (var collider in boxColliders)
            {
                Undo.RecordObject(collider, EDIT_PIVOT_COMMAND_NAME);
                collider.center -= pivot.localPosition;
            }

            var capsuleColliders = target.GetComponents<CapsuleCollider>();
            foreach (var collider in capsuleColliders)
            {
                Undo.RecordObject(collider, EDIT_PIVOT_COMMAND_NAME);
                collider.center -= pivot.localPosition;
            }

            var sphereColliders = target.GetComponents<SphereCollider>();
            foreach (var collider in sphereColliders)
            {
                Undo.RecordObject(collider, EDIT_PIVOT_COMMAND_NAME);
                collider.center -= pivot.localPosition;
            }

            var wheelColliders = target.GetComponents<WheelCollider>();
            foreach (var collider in wheelColliders)
            {
                Undo.RecordObject(collider, EDIT_PIVOT_COMMAND_NAME);
                collider.center -= pivot.localPosition;
            }

            var colliders2D = target.GetComponents<Collider2D>();
            foreach (var collider in colliders2D)
            {
                Undo.RecordObject(collider, EDIT_PIVOT_COMMAND_NAME);
                collider.offset -= (Vector2)pivot.localPosition;
            }
        }

        private static void EditPivotPositionAndRotation(Transform target, Transform pivot)
        {
            var children = target.GetComponentsInChildren<Transform>();
            var childrenPosAndRot = children.Select(child => (child, child.position, child.rotation)).ToArray();
            Undo.RecordObject(target, EDIT_PIVOT_COMMAND_NAME);
            target.position += target.TransformVector(pivot.localPosition);
            target.eulerAngles += pivot.localEulerAngles;
            for (int i = 0; i < childrenPosAndRot.Length; ++i)
            {
                var child = childrenPosAndRot[i].child;
                if (child == target || child == pivot) continue;
                Undo.RecordObject(child, EDIT_PIVOT_COMMAND_NAME);
                child.position = childrenPosAndRot[i].position;
                child.rotation = childrenPosAndRot[i].rotation;
            }
        }

        private static void EditNavMeshObject(Transform target, Transform pivot)
        {
            var obstacle = target.GetComponent<NavMeshObstacle>();
            if (obstacle == null) return;
            Undo.RecordObject(obstacle, EDIT_PIVOT_COMMAND_NAME);
            obstacle.center -= pivot.localPosition;
        }

        public static void ApplyPivot(Transform pivot)
        {
            var target = pivot.parent;

            var meshFilter = target.GetComponent<MeshFilter>();
            if (meshFilter != null && meshFilter.sharedMesh != null)
            {
                Undo.RecordObject(meshFilter.sharedMesh, EDIT_PIVOT_COMMAND_NAME);
                EditMesh(meshFilter.sharedMesh, pivot);
            }

            var spriteRenderer = target.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null && spriteRenderer.sprite != null) EditSprite(spriteRenderer, pivot);

            var rectTransform = target.GetComponent<RectTransform>();
            if (rectTransform != null) EditRectTransform(rectTransform, pivot);

            EditColliders(target, pivot);
            EditNavMeshObject(target, pivot);
            EditPivotPositionAndRotation(target, pivot);
        }

        public static GameObject CreateCenteredPivot(Transform target)
        {
            if (target == null || target.gameObject.scene.rootCount == 0) return null;
            var bounds = BoundsUtils.GetBoundsRecursive(target);
            var pivot = new GameObject("Pivot");
            pivot.transform.parent = target.transform;
            pivot.transform.localRotation = Quaternion.identity;
            pivot.transform.localScale = Vector3.one;
            pivot.transform.position = bounds.center;
            _pivot = pivot;
            return pivot;
        }

        public static void CenterPivot(Transform target, string savePath)
        {
            if (target == null || target.gameObject.scene.rootCount == 0) return;
            var pivot = CreateCenteredPivot(target);
            var meshFilter = target.GetComponent<MeshFilter>();
            SaveMesh(meshFilter, savePath, pivot.transform);
            Object.DestroyImmediate(pivot);
            Selection.activeObject = target.gameObject;
        }

        public static void CenterPivot(Transform target)
        {
            if (target == null || target.gameObject.scene.rootCount == 0) return;
            var pivot = CreateCenteredPivot(target);
            ApplyPivot(pivot.transform);
            Object.DestroyImmediate(pivot);
            Selection.activeObject = target.gameObject;
        }

        public static void DuringSceneGUI(SceneView sceneView)
        {
            if (_pivot == null) return;
            var position = _pivot.transform.position;
            var scale = Vector3.one * (HandleUtility.GetHandleSize(position) * 0.3f);
            var matrix = Matrix4x4.TRS(position, Quaternion.identity, scale);
            Graphics.DrawMesh(pivotMesh, matrix, pivotMaterial, 0, sceneView.camera);
        }

        #endregion

        #region PLACE ON SURFACE

        public class PlaceOnSurfaceData
        {
            public bool rotateToSurface { get; set; } = true;

            public Vector3 objectOrientation { get; set; } = Vector3.down;

            public float surfaceDistance { get; set; } = 0f;

            public Vector3 projectionDirection { get; set; } = Vector3.down;

            public Space projectionDirectionSpace { get; set; } = Space.Self;

            public LayerMask mask { get; set; } = ~0;

            public bool placeOnColliders { get; set; } = true;
        }

        private static (Vector3 vertex, Transform transform)[] GetDirectionVertices(Transform target, Vector3 worldProjDir)
        {
            var children = Array.FindAll(target.GetComponentsInChildren<MeshFilter>(), filter => filter != null && filter.sharedMesh != null).Select(filter => (filter.transform, filter.sharedMesh))
                .ToArray();
            var maxSqrDistance = float.MinValue;
            var bounds = BoundsUtils.GetBoundsRecursive(target);
            var vertices = new List<(Vector3 vertex, Transform transform)>() { (bounds.center, target) };
            foreach (var child in children)
            {
                foreach (var vertex in child.sharedMesh.vertices)
                {
                    var centerToVertex = child.transform.TransformPoint(vertex) - bounds.center;
                    var projection = Vector3.Project(centerToVertex, worldProjDir);
                    var sqrDistance = projection.sqrMagnitude * (projection.normalized != worldProjDir.normalized ? -1 : 1);
                    var vertexTrans = (vertex, child.transform);
                    if (sqrDistance > maxSqrDistance)
                    {
                        vertices.Clear();
                        maxSqrDistance = sqrDistance;
                        vertices.Add(vertexTrans);
                    }
                    else if (sqrDistance + 0.001 >= maxSqrDistance)
                    {
                        if (vertices.Exists(item => item.vertex == vertexTrans.vertex)) continue;
                        vertices.Add(vertexTrans);
                    }
                }
            }

            return vertices.ToArray();
        }

        private static void PlaceOnSurface(Transform target, PlaceOnSurfaceData data, MeshFilter[] filters)
        {
            var worldProjDir = (data.projectionDirectionSpace == Space.World
                ? data.projectionDirection
                : target.TransformDirection(data.projectionDirection)).normalized;

            var originalPosition = target.position;
            var originalRotation = target.rotation;
            const string COMMAND_NAME = "Place On Surface";
            Undo.RecordObject(target, COMMAND_NAME);
            if (data.rotateToSurface)
            {
                var worldOrientDir = target.TransformDirection(data.objectOrientation);
                var orientAngle = Vector3.Angle(worldOrientDir, worldProjDir);
                var cross = Vector3.Cross(worldOrientDir, worldProjDir);
                if (cross == Vector3.zero)
                {
                    cross = target.TransformDirection(data.objectOrientation.y != 0 ? Vector3.forward : data.objectOrientation.z != 0 ? Vector3.right : Vector3.up);
                    orientAngle = worldOrientDir == worldProjDir ? 0 : 180;
                }

                target.Rotate(cross, orientAngle);
            }

            bool Raycast(Vector3 rayOrigin, out RaycastHit hitInfo)
            {
                if (data.placeOnColliders) return Physics.Raycast(rayOrigin, worldProjDir, out hitInfo, float.MaxValue, data.mask);
                hitInfo = new RaycastHit();
                var meshHit = RaycastMesh.Raycast(rayOrigin, worldProjDir, out var meshHitInfo, out var colliderFilter, filters, float.MaxValue);
                var colliderHit = Physics.Raycast(rayOrigin, worldProjDir, out var colliderHitInfo, float.MaxValue, data.mask);
                if (colliderHit && meshHit)
                {
                    hitInfo = colliderHitInfo.distance < meshHitInfo.distance ? colliderHitInfo : meshHitInfo;
                    return true;
                }

                if (colliderHit)
                {
                    hitInfo = colliderHitInfo;
                    return true;
                }

                if (meshHit)
                {
                    hitInfo = meshHitInfo;
                    return true;
                }

                return false;
            }

            if (!target.GetComponentsInChildren<MeshFilter>().Any())
            {
                if (Raycast(target.position, out var hitInfo)) target.position = hitInfo.point;
                return;
            }

            var dirVert = GetDirectionVertices(target, worldProjDir);
            var minDistance = float.MaxValue;
            var closestVertexInfoList = new List<((Vector3 vertex, Transform transform), RaycastHit hitInfo)>();
            foreach (var vertexTransform in dirVert)
            {
                var rayOrigin = vertexTransform.transform.TransformPoint(vertexTransform.vertex);
                if (!Raycast(rayOrigin, out var hitInfo)) continue;
                if (hitInfo.distance < minDistance)
                {
                    minDistance = hitInfo.distance;
                    closestVertexInfoList.Clear();
                    closestVertexInfoList.Add((vertexTransform, hitInfo));
                }
                else if (hitInfo.distance - 0.001 <= minDistance)
                {
                    closestVertexInfoList.Add((vertexTransform, hitInfo));
                }
            }

            if (closestVertexInfoList.Count == 0)
            {
                target.SetPositionAndRotation(originalPosition, originalRotation);
                return;
            }

            var averageWorldVertex = Vector3.zero;
            var averageHitPoint = Vector3.zero;
            var averageNormal = Vector3.zero;
            foreach (var vertInfo in closestVertexInfoList)
            {
                averageWorldVertex += vertInfo.Item1.transform.TransformPoint(vertInfo.Item1.vertex);
                averageHitPoint += vertInfo.hitInfo.point;
                averageNormal += vertInfo.hitInfo.normal;
            }

            averageWorldVertex /= closestVertexInfoList.Count;
            var averageVertex = target.InverseTransformPoint(averageWorldVertex);
            averageHitPoint /= closestVertexInfoList.Count;
            averageNormal /= closestVertexInfoList.Count;

            if (data.rotateToSurface)
            {
                var worldOrientDir = target.TransformDirection(-data.objectOrientation);
                var angle = Vector3.Angle(worldOrientDir, averageNormal);
                var cross = Vector3.Cross(worldOrientDir, averageNormal);
                if (cross != Vector3.zero)
                {
                    target.RotateAround(target.TransformPoint(averageVertex), cross, angle);
                }
            }

            target.position = averageHitPoint - target.TransformVector(averageVertex) - worldProjDir * data.surfaceDistance;
        }

        public static void PlaceOnSurface(GameObject[] selection, PlaceOnSurfaceData data)
        {
            var ignoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
            var layerDictionary = new Dictionary<GameObject, int>();
            foreach (var obj in selection)
            {
                var children = obj.transform.GetComponentsInChildren<Transform>(true);
                foreach (var child in children)
                {
                    var gameObject = child.gameObject;
                    layerDictionary.Add(gameObject, gameObject.layer);
                    child.gameObject.layer = ignoreRaycast;
                }
            }

            var filters = data.placeOnColliders ? null : RaycastMesh.FindMeshFilters(data.mask, selection);
            foreach (var obj in selection) PlaceOnSurface(obj.transform, data, filters);
            foreach (var item in layerDictionary) item.Key.layer = item.Value;
        }

        #endregion

        #region UTILS

        private static int CompareHierarchyIndex(GameObject obj1, GameObject obj2)
        {
            var idx1 = GetHierarchyIndex(obj1);
            var idx2 = GetHierarchyIndex(obj2);
            var depth = 0;
            do
            {
                if (idx1.Length <= depth) return -1;
                if (idx2.Length <= depth) return 1;
                var result = idx1[depth].CompareTo(idx2[depth]);
                if (result != 0) return result;
                ++depth;
            } while (true);
        }

        private static GameObject[] SortByHierarchy(GameObject[] selection)
        {
            var sortedList = selection.ToList();
            sortedList.Sort(CompareHierarchyIndex);
            return sortedList.ToArray();
        }

        private static void LookAtCenter(Transform transform, Vector3 center, Vector3 axis, Vector3 orientation, Vector3 parallelAxis)
        {
            transform.rotation = Quaternion.FromToRotation(parallelAxis, axis);
            var worldOrientation = transform.TransformDirection(orientation);
            var objToCenter = center - transform.position;
            var angle = Vector3.Angle(worldOrientation, objToCenter);
            var cross = Vector3.Cross(worldOrientation, objToCenter);
            if (cross == Vector3.zero) cross = axis;
            transform.Rotate(cross, angle, Space.World);
        }

        #endregion
    }
}