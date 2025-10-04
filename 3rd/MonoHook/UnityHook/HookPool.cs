using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AIO;

namespace MonoHook
{
    /// <summary>
    /// Hook 池，防止重复 Hook
    /// </summary>
    public static class HookPool
    {
        private static readonly Dictionary<MethodBase, MethodHook> _hooks = new Dictionary<MethodBase, MethodHook>();

        public static void AddHook(MethodBase method, MethodHook hook)
        {
            if (_hooks.TryGetValue(method, out var preHook))
            {
                preHook.Uninstall();
                _hooks[method] = hook;
            }
            else
                _hooks.Add(method, hook);
        }

        public static MethodHook GetHook(MethodBase method)
        {
            if (method == null) return null;
            return _hooks.GetValue(method);
        }

        public static void RemoveHooker(MethodBase method)
        {
            if (method == null) return;

            _hooks.Remove(method);
        }

        public static void UninstallAll()
        {
            var list = _hooks.Values.ToList();
            foreach (var hook in list)
                hook.Uninstall();

            _hooks.Clear();
        }

        public static void UninstallByTag(string tag)
        {
            foreach (var hook in _hooks.Values.ToList().Where(hook => hook.tag == tag)) hook.Uninstall();
        }

        public static List<MethodHook> GetAllHooks() { return _hooks.Values.ToList(); }
    }
}