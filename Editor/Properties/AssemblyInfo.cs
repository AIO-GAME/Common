using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;
using UnityEditor.PackageManager;

[assembly: ComVisible(false)]
[assembly: InternalsVisibleTo("AIO.Unity.Editor")]

namespace AIO.UEditor
{
    internal static class Setting
    {
        /// <summary>
        /// 名称
        /// </summary>
        public const string Name = "AIO.Common";

        /// <summary>
        /// 版本
        /// </summary>
        public static string Version { get; private set; }

        [AInit(mode: EInitAttrMode.Both, int.MaxValue)]
        public static void Initialize()
        {
            var package = PackageInfo.FindForAssembly(typeof(Setting).Assembly);
            var packageJson = AHelper.IO.ReadJsonUTF8<JObject>(string.Concat(package.resolvedPath, "/package.json"));
            Version = packageJson.Value<string>("version");
        }
    }
}