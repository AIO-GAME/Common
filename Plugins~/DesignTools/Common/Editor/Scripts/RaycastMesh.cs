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

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace PluginMaster
{
    public static class RaycastMesh
    {
        public static MeshFilter[] FindMeshFilters(LayerMask mask, GameObject[] exclude = null, bool excludeColliders = true)
        {
            var filterArray = GameObject.FindObjectsOfType<MeshFilter>();
            var filterList = new List<MeshFilter>();
            var excludeList = new List<MeshFilter>();
            if(exclude != null)
            {
                foreach(var obj in exclude)
                {
                    if (obj == null) continue;
                    var children = obj.GetComponentsInChildren<MeshFilter>();
                    foreach(var child in children)
                    {
                        if (excludeList.Contains(child)) continue;
                        if (excludeColliders)
                        {
                            var collider = child.GetComponent<Collider>();
                            if (collider != null && collider.enabled) continue;
                        }
                        excludeList.Add(child);
                    }
                }
            }

            foreach(var filter in filterArray)
            {
                var flag = 1 << filter.gameObject.layer;
                if ((mask.value & flag) == 0) continue;
                if (exclude != null && excludeList.Contains(filter)) continue;
                if (excludeColliders)
                {
                    var collider = filter.GetComponent<Collider>();
                    if (collider != null && collider.enabled) continue;
                }
                filterList.Add(filter);
            }
            return filterList.ToArray();
        }


        private static MethodInfo intersectRayMesh = null;
        public static bool Raycast(Ray ray, out RaycastHit hitInfo, out MeshFilter colliderFilter, MeshFilter[] filters, float maxDistance)
        {
            colliderFilter = null;
            hitInfo = new RaycastHit();
            if (intersectRayMesh == null)
            {
                var editorTypes = typeof(Editor).Assembly.GetTypes();
                var type_HandleUtility = editorTypes.FirstOrDefault(t => t.Name == "HandleUtility");
                intersectRayMesh = type_HandleUtility.GetMethod("IntersectRayMesh", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            }
            var minDistance = float.MaxValue;
            var result = false;
            foreach (var filter in filters)
            {
                if (filter == null) continue;
                if (filter.sharedMesh == null) continue;
                var parameters = new object[] { ray, filter.sharedMesh, filter.transform.localToWorldMatrix, null };
                if ((bool)intersectRayMesh.Invoke(null, parameters))
                {
                    if (hitInfo.distance > maxDistance) continue;
                    result = true;
                    var hit = (RaycastHit)parameters[3];
                    if (hit.distance < minDistance)
                    {
                        colliderFilter = filter;
                        minDistance = hit.distance;
                        hitInfo = hit;
                    }
                }
            }
            if(result)
            {
                hitInfo.normal = hitInfo.normal.normalized;
            }
            return result;
        }

        public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, out MeshFilter colliderFilter, MeshFilter[]  filters, float maxDistance)
        {
            var ray = new Ray(origin, direction);
            return Raycast(ray, out hitInfo, out colliderFilter, filters, maxDistance);
        }
    }
}
