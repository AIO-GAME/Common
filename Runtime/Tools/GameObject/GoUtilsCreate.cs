#region

using UnityEngine;
using Object = UnityEngine.Object;

#endregion

namespace AIO.UEngine
{
    partial class GoUtils
    {
        #region Create Game Object

        public static GameObject CreateGameObject()
        {
            return new GameObject();
        }

        public static GameObject CreateGameObject(in string name)
        {
            return new GameObject(name);
        }

        public static GameObject CreateGameObject(in string name, in Transform parent)
        {
            var obj = new GameObject(name);
            obj.transform.SetParent(parent, false);
            return obj;
        }


        public static GameObject CreateGameObject(in string name, in Transform parent, in Vector3 localPos)
        {
            var obj = new GameObject(name);
            obj.transform.SetParent(parent, false);
            obj.transform.localPosition = localPos;
            return obj;
        }


        public static GameObject CreateGameObject(in string  name, in Transform parent, in Vector3 localPos,
                                                  in Vector3 localEulerAngles)
        {
            var obj = new GameObject(name);
            obj.transform.SetParent(parent, false);
            obj.transform.localPosition    = localPos;
            obj.transform.localEulerAngles = localEulerAngles;
            return obj;
        }


        public static GameObject CreateGameObject(in string  name,             in Transform parent, in Vector3 localPos,
                                                  in Vector3 localEulerAngles, in Vector3   localScale)
        {
            var obj = new GameObject(name);
            obj.transform.SetParent(parent, false);
            obj.transform.localPosition    = localPos;
            obj.transform.localEulerAngles = localEulerAngles;
            obj.transform.localScale       = localScale;
            return obj;
        }


        public static GameObject CreateGameObject(in Transform parent)
        {
            var obj = new GameObject();
            obj.transform.SetParent(parent, false);
            return obj;
        }


        public static GameObject CreateGameObject(in Transform parent, in Vector3 vector)
        {
            var obj = new GameObject();
            obj.SetParent(parent, false);
            obj.transform.localPosition = vector;
            return obj;
        }


        public static GameObject CreateGameObject(in Transform parent, in Vector3 localPos, in Vector3 localEulerAngles)
        {
            var obj = new GameObject();
            obj.SetParent(parent, false);
            obj.transform.localPosition    = localPos;
            obj.transform.localEulerAngles = localEulerAngles;
            return obj;
        }


        public static GameObject CreateGameObject(in Transform parent, in Vector3 localPos, in Vector3 localEulerAngles,
                                                  in Vector3   localScale)
        {
            var obj = new GameObject();
            obj.SetParent(parent, false);
            obj.transform.localPosition    = localPos;
            obj.transform.localEulerAngles = localEulerAngles;
            obj.transform.localScale       = localScale;
            return obj;
        }


        public static GameObject CreateGameObject(in Vector3 vector)
        {
            var obj = new GameObject
            {
                transform =
                {
                    localPosition = vector
                }
            };
            return obj;
        }


        public static GameObject CreateGameObject(in Vector3 localPos, in Vector3 localEulerAngles)
        {
            var obj = new GameObject
            {
                transform =
                {
                    localPosition    = localPos,
                    localEulerAngles = localEulerAngles
                }
            };
            return obj;
        }


        public static GameObject CreateGameObject(in Vector3 localPos, in Vector3 localEulerAngles,
                                                  in Vector3 localScale)
        {
            var obj = new GameObject
            {
                transform =
                {
                    localPosition    = localPos,
                    localEulerAngles = localEulerAngles,
                    localScale       = localScale
                }
            };
            return obj;
        }


        public static GameObject CreateGameObject(in string name, in Vector3 localPos)
        {
            var obj = new GameObject(name)
            {
                transform =
                {
                    localPosition = localPos
                }
            };
            return obj;
        }


        public static GameObject CreateGameObject(in string name, in Vector3 localPos, in Vector3 localEulerAngles)
        {
            var obj = new GameObject(name)
            {
                transform =
                {
                    localPosition    = localPos,
                    localEulerAngles = localEulerAngles
                }
            };
            return obj;
        }


        public static GameObject CreateGameObject(in string  name, in Vector3 localPos, in Vector3 localEulerAngles,
                                                  in Vector3 localScale)
        {
            var obj = new GameObject(name)
            {
                transform =
                {
                    localPosition    = localPos,
                    localEulerAngles = localEulerAngles,
                    localScale       = localScale
                }
            };
            return obj;
        }

        #endregion

        #region Create Add Component

        public static T1 CreateAddComponentWithGet<T1>(string name, Transform parent)
        where T1 : Component
        {
            var obj = new GameObject(name);
            obj.SetParent(parent, false);
            return obj.AddComponent<T1>();
        }

        public static T CreateAddComponent<T>(string name)
        where T : Component
        {
            var obj = new GameObject(name);
            return obj.AddComponent<T>();
        }

        public static GameObject CreateAddComponent<T1, T2>(string name)
        where T1 : Component where T2 : Component
        {
            var obj = new GameObject(name);
            obj.AddComponent<T1>();
            obj.AddComponent<T2>();
            return obj;
        }

        public static GameObject CreateAddComponent<T1, T2, T3>(string name)
        where T1 : Component where T2 : Component where T3 : Component
        {
            var obj = new GameObject(name);
            obj.AddComponent<T1>();
            obj.AddComponent<T2>();
            obj.AddComponent<T3>();
            return obj;
        }

        public static GameObject CreateAddComponent<T1>(string name, params T1[] components)
        where T1 : Component
        {
            var obj = new GameObject(name);
            foreach (var item in components) obj.AddComponent(item.GetType());
            return obj;
        }

        public static GameObject CreateAddComponent<T1>(string name, Transform parent)
        where T1 : Component
        {
            var obj = new GameObject(name);
            obj.AddComponent<T1>();
            obj.SetParent(parent, false);
            return obj;
        }

        public static GameObject CreateAddComponent<T1, T2>(string name, Transform parent)
        where T1 : Component where T2 : Component
        {
            var obj = new GameObject(name);
            obj.AddComponent<T1>();
            obj.AddComponent<T2>();
            obj.SetParent(parent, false);
            return obj;
        }

        public static GameObject CreateAddComponent<T1, T2, T3>(string name, Transform parent)
        where T1 : Component where T2 : Component where T3 : Component
        {
            var obj = new GameObject(name);
            obj.AddComponent<T1>();
            obj.AddComponent<T2>();
            obj.AddComponent<T3>();
            obj.SetParent(parent, false);
            return obj;
        }

        public static GameObject CreateAddComponent<T1>(string name, Transform parent, params T1[] components)
        where T1 : Component
        {
            var obj = new GameObject(name);
            foreach (var item in components) obj.AddComponent(item.GetType());
            obj.SetParent(parent, false);
            return obj;
        }


        public static GameObject CreateAddComponent<T1>(string name, Transform parent, Vector3 vector)
        where T1 : Component
        {
            var obj = new GameObject(name);
            obj.AddComponent<T1>();
            obj.SetParent(parent, false);
            obj.transform.localPosition = vector;
            return obj;
        }


        public static GameObject CreateAddComponent<T1, T2>(string name, Transform parent, Vector3 vector)
        where T1 : Component where T2 : Component
        {
            var obj = new GameObject(name);
            obj.AddComponent<T1>();
            obj.AddComponent<T2>();
            obj.SetParent(parent, false);
            obj.transform.localPosition = vector;
            return obj;
        }


        public static GameObject CreateAddComponent<T1, T2, T3>(string name, Transform parent, Vector3 vector)
        where T1 : Component where T2 : Component where T3 : Component
        {
            var obj = new GameObject(name);
            obj.AddComponent<T1>();
            obj.AddComponent<T2>();
            obj.AddComponent<T3>();
            obj.SetParent(parent, false);
            obj.transform.localPosition = vector;
            return obj;
        }


        public static GameObject CreateAddComponent<T1>(string      name, Transform parent, Vector3 vector,
                                                        params T1[] components)
        where T1 : Component
        {
            var obj = new GameObject(name);
            foreach (var item in components) obj.AddComponent(item.GetType());
            obj.SetParent(parent, false);
            obj.transform.localPosition = vector;
            return obj;
        }

        #endregion

        #region Instantiate

        public static GameObject Instantiate(in GameObject original, in Transform parent)
        {
            if (original.Equals(null)) return null;
            var go = Object.Instantiate(original, parent, false);
            go.name = original.name;
            return go;
        }


        public static GameObject Instantiate(in GameObject original, in Transform parent, in Vector3 localPos)
        {
            if (original.Equals(null)) return null;
            var go = Object.Instantiate(original, parent, false);
            go.name                    = original.name;
            go.transform.localPosition = localPos;
            return go;
        }

        public static GameObject Instantiate(in GameObject original, in string name, in Transform parent,
                                             in Vector3    localPos)
        {
            if (original.Equals(null)) return null;
            var go = Object.Instantiate(original, parent, true);
            go.name                    = name;
            go.transform.localPosition = localPos;
            return go;
        }


        public static GameObject Instantiate(in GameObject original, in Transform parent, in Vector3 localPos,
                                             in Quaternion localRot)
        {
            if (original.Equals(null)) return null;
            var go = Object.Instantiate(original, parent, false);
            go.name                    = original.name;
            go.transform.localPosition = localPos;
            go.transform.localRotation = localRot;
            return go;
        }


        public static GameObject Instantiate(in GameObject original, in Transform parent, in Vector3 localPos,
                                             in Quaternion localRot, in Vector3   localScale)
        {
            if (original.Equals(null)) return null;
            var go = Object.Instantiate(original, parent, false);
            go.name                    = original.name;
            go.transform.localPosition = localPos;
            go.transform.localRotation = localRot;
            go.transform.localScale    = localScale;
            return go;
        }


        public static T Instantiate<T>(in GameObject original, in Transform parent)
        where T : Component
        {
            if (original.Equals(null)) return null;
            var go = Object.Instantiate(original, parent);
            go.name = original.name;
            var r = go.GetComponent<T>();
            if (r == null) r = go.AddComponent<T>();
            return r;
        }

        public static T Instantiate<T>(in GameObject original, in Transform parent, in Vector3 localPos)
        where T : Component
        {
            if (original.Equals(null)) return null;
            var r = Instantiate<T>(original, parent);
            r.transform.localPosition = localPos;
            return r;
        }

        public static T Instantiate<T>(in GameObject original, in Transform parent, in Vector3 localPos,
                                       in Quaternion localRot)
        where T : Component
        {
            if (original.Equals(null)) return null;
            var r = Instantiate<T>(original, parent);
            var t = r.transform;
            t.localPosition = localPos;
            t.localRotation = localRot;
            return r;
        }

        public static T Instantiate<T>(in GameObject original, in Transform parent, in Vector3 localPos,
                                       in Quaternion localRot, in Vector3   scale)
        where T : Component
        {
            if (original.Equals(null)) return null;
            var r = Instantiate<T>(original, parent);
            var t = r.transform;
            t.localPosition = localPos;
            t.localRotation = localRot;
            t.localScale    = scale;
            return r;
        }

        #endregion
    }
}