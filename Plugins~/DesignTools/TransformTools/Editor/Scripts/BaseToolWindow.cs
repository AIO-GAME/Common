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
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace PluginMaster
{
    public class BaseToolWindow : EditorWindow
    {
        protected GUISkin _skin = null;
        protected List<GameObject> _selectionOrderedTopLevel = new List<GameObject>();
        protected List<GameObject> _selectionOrdered = new List<GameObject>();

        protected virtual void OnEnable()
        {
            _skin = Resources.Load<GUISkin>("TransformToolsSkin");
            Assert.IsNotNull(_skin);
            OnSelectionChange();
        }
        protected virtual void OnGUI() 
        {
            if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                GUI.FocusControl(null);
                Repaint();
            }
        }

        protected virtual void OnSelectionChange()
        {
            UpdateSelection(_selectionOrderedTopLevel, true);
            UpdateSelection(_selectionOrdered, false);
        }

        private void UpdateSelection(List<GameObject> list,  bool _filteredByTopLevel)
        {
            var newSet = new HashSet<GameObject>(Selection.GetFiltered<GameObject>(SelectionMode.Editable | SelectionMode.ExcludePrefab | (_filteredByTopLevel ?  SelectionMode.TopLevel : SelectionMode.Unfiltered)));

            if (newSet.Count == 0)
            {
                list.Clear();
                return;
            }

            var unselectedSet = new HashSet<GameObject>(list);
            unselectedSet.ExceptWith(newSet);
            foreach (var obj in unselectedSet) list.Remove(obj);

            newSet.ExceptWith(list);
            foreach (var obj in newSet) list.Add(obj);
        }

        protected List<GameObject> GetSelection(bool filteredByTopLevel)
        {
            return filteredByTopLevel ? _selectionOrderedTopLevel : _selectionOrdered;
        }
    }
}