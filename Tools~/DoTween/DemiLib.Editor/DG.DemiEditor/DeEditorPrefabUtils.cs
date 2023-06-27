using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

namespace DG.DemiEditor
{
    /// <summary>
    /// Prefab utilities
    /// </summary>
    public static class DeEditorPrefabUtils
    {
        /// <summary>
        /// Behaves as the Inspector's Apply button, applying any modification of this instance to the prefab parent
        /// </summary>
        /// <param name="instance"></param>
        public static void ApplyPrefabInstanceModifications(GameObject instance)
        {
            PrefabUtility.ReplacePrefab(instance, DeEditorCompatibilityUtils.GetPrefabParent(instance), ReplacePrefabOptions.ConnectToPrefab);
        }

        /// <summary>
        /// Returns TRUe if a prefab instance has unapplied modifications, ignoring any modifications applied to the transform.<para />
        /// NOTE: this a somehow costly operation (since it generates GC)
        /// </summary>
        public static bool InstanceHasUnappliedModifications(GameObject instance)
        {
            PropertyModification[] propertyModifications = PrefabUtility.GetPropertyModifications(instance);
            for (int i = 0; i < propertyModifications.Length; i++)
            {
                string propertyPath = propertyModifications[i].propertyPath;
                int length = propertyPath.Length;
                if ((length <= 7 || !(propertyPath.Substring(0, 7) == "m_Local")) && (length != 11 || !(propertyPath == "m_RootOrder")))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Completely removes any prefab connection from the given prefab instances, by desotroing the original object and recreating it.<para />
        /// Returns a list with all the new elements created.
        /// <para>
        /// Based on RodGreen's method (http://forum.unity3d.com/threads/82883-Breaking-connection-from-gameObject-to-prefab-for-good.?p=726602&amp;viewfull=1#post726602)
        /// </para>
        /// </summary>
        public static List<GameObject> BreakPrefabInstances(List<GameObject> prefabInstances, bool keepOriginals = false)
        {
            List<GameObject> list = new List<GameObject>();
            for (int i = 0; i < prefabInstances.Count; i++)
            {
                GameObject item = BreakPrefabInstance(prefabInstances[i], keepOriginals);
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// Completely removes any prefab connection from the given prefab instance, by desotroing the original object and recreating it.
        /// <para>
        /// Based on RodGreen's method (http://forum.unity3d.com/threads/82883-Breaking-connection-from-gameObject-to-prefab-for-good.?p=726602&amp;viewfull=1#post726602)
        /// </para>
        /// </summary>
        public static GameObject BreakPrefabInstance(GameObject prefabInstance, bool keepOriginal = false)
        {
            string name = prefabInstance.name;
            Transform transform = prefabInstance.transform;
            Transform parent = transform.parent;
            int siblingIndex = transform.GetSiblingIndex();
            transform.SetParent(null);
            GameObject gameObject = Object.Instantiate(prefabInstance);
            gameObject.name = name;
            gameObject.SetActive(prefabInstance.activeSelf);
            gameObject.transform.SetParent(parent);
            gameObject.transform.SetSiblingIndex(siblingIndex);
            if (!keepOriginal)
            {
                Object.DestroyImmediate(prefabInstance, allowDestroyingAssets: false);
            }
            return gameObject;
        }
    }
}
