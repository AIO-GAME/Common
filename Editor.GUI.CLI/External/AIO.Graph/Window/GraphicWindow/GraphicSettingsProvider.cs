#region

using System.Collections.Generic;
using UnityEditor;

#endregion

namespace AIO.UEditor
{
    public class GraphicSettingsProvider : SettingsProvider
    {
        public GraphicSettingsProvider(
            string              path,
            SettingsScope       scopes,
            IEnumerable<string> keywords = null
        ) : base(path, scopes, keywords) { }
    }
}