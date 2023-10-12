using System.Collections.Generic;
using UnityEditor;

namespace AIO.UEditor
{
    public class GraphicSettingsProvider : SettingsProvider
    {
        public GraphicSettingsProvider(
            string path,
            SettingsScope scopes,
            IEnumerable<string> keywords = null
        ) : base(path, scopes, keywords)
        {
        }
    }
}