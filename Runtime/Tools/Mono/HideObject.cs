/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2025-10-13
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using UnityEngine;

namespace AIO
{
    [HelpURL("https://gist.github.com/yasirkula/6add4dd2a392524fe3bd7c9882c839e4")]
    [DisallowMultipleComponent]
    public class HideObject : MonoBehaviour
    {
        private void Awake() { gameObject.SetActive(false); }
    }
}