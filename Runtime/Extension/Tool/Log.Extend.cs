using UnityEngine;

namespace AIO
{
    /// <summary>
    /// 日志工具箱
    /// </summary>
    public static class ExtendLogTool
    {
#if UNITY_EDITOR
        private const string InfoPrefix = "<b><color=cyan>[AF.Info]</color></b> ";
        private const string WarningPrefix = "<b><color=yellow>[AF.Warning]</color></b> ";
        private const string ErrorPrefix = "<b><color=red>[AF.Error]</color></b> ";
#endif

        /// <summary>
        /// 转换为超链接文本（仅在编辑器中控制台生效）
        /// </summary>
        /// <param name="content">原文本</param>
        /// <param name="href">超链接，可为网络地址或资源路径</param>
        /// <param name="line">超链接为资源路径时，链接的行数</param>
        /// <returns>超链接文本</returns>
        public static string Hyperlink(this string content, string href, int line = 0)
        {
#if UNITY_EDITOR
            return $"<a href=\"{href}\" line=\"{line}\">{content}</a>";
#else
            return content;
#endif
        }

        /// <summary>
        /// 生成链接到此对象脚本文件的超链接文本（仅在编辑器中控制台生效）
        /// </summary>
        /// <param name="behaviour">脚本对象</param>
        /// <param name="line">链接的行数</param>
        /// <returns>超链接文本</returns>
        public static string HyperlinkFile(this MonoBehaviour behaviour, int line = 0)
        {
            if (behaviour == null)
                return null;

            var name = behaviour.GetType().FullName;
#if UNITY_EDITOR
            var monoScript = UnityEditor.MonoScript.FromMonoBehaviour(behaviour);
            var path = UnityEditor.AssetDatabase.GetAssetPath(monoScript);
            return $"<a href=\"{path}\" line=\"{line}\">{name}</a>";
#else
            return name;
#endif
        }

        /// <summary>
        /// 生成链接到此对象脚本文件的超链接文本（仅在编辑器中控制台生效）
        /// </summary>
        /// <param name="scriptableObject">脚本对象</param>
        /// <param name="line">链接的行数</param>
        /// <returns>超链接文本</returns>
        public static string HyperlinkFile(this ScriptableObject scriptableObject, int line = 0)
        {
            if (scriptableObject == null)
                return null;

            var name = scriptableObject.GetType().FullName;
#if UNITY_EDITOR
            var monoScript = UnityEditor.MonoScript.FromScriptableObject(scriptableObject);
            var path = UnityEditor.AssetDatabase.GetAssetPath(monoScript);
            return $"<a href=\"{path}\" line=\"{line}\">{name}</a>";
#else
            return name;
#endif
        }

        /// <summary>
        /// 打印信息日志
        /// </summary>
        /// <param name="content">日志内容</param>
        public static void Info(this string content)
        {
            Info(content, null);
        }

        /// <summary>
        /// 打印警告日志
        /// </summary>
        /// <param name="content">日志内容</param>
        public static void Warning(this string content)
        {
            Warning(content, null);
        }

        /// <summary>
        /// 打印错误日志
        /// </summary>
        /// <param name="content">日志内容</param>
        public static void Error(this string content)
        {
            Error(content, null);
        }

        /// <summary>
        /// 打印信息日志
        /// </summary>
        /// <param name="content">日志内容</param>
        /// <param name="context">上下文目标</param>
        public static void Info(this string content, Object context)
        {
#if UNITY_EDITOR
            Debug.Log(InfoPrefix + content, context);
#else
            Debug.Log(content, context);
#endif
        }

        /// <summary>
        /// 打印警告日志
        /// </summary>
        /// <param name="content">日志内容</param>
        /// <param name="context">上下文目标</param>
        public static void Warning(this string content, Object context)
        {
#if UNITY_EDITOR
            Debug.LogWarning(WarningPrefix + content, context);
#else
            Debug.LogWarning(content, context);
#endif
        }

        /// <summary>
        /// 打印错误日志
        /// </summary>
        /// <param name="content">日志内容</param>
        /// <param name="context">上下文目标</param>
        public static void Error(this string content, Object context)
        {
#if UNITY_EDITOR
            Debug.LogError(ErrorPrefix + content, context);
#else
            Debug.LogWarning(content, context);
#endif
        }
    }
}