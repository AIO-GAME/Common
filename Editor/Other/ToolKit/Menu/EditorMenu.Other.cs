/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-08-03
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling.Memory.Experimental;
using YamlDotNet.Serialization;

namespace AIO.UEditor
{
    public class ScriptIDViewer : EditorWindow
    {
        [MenuItem("Tools/Debug/ScriptIDViewer")]
        public static void Open()
        {
            var wnd = GetWindow<ScriptIDViewer>("ScriptIDViewer");
            wnd.Show();
            wnd.minSize = new Vector2(600, 400);
            wnd.maxSize = new Vector2(600, 400);
        }

        private void Awake()
        {
            m_material = new Material(Shader.Find("Hidden/Internal-Colored"));
            m_material.hideFlags = HideFlags.HideAndDontSave;
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space();
            GetId();
            DrawLine();
            GetScriptId();
            DrawLine();
            GetAssetFromId();
            DrawLine();
            GetDllScriptId();
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
        }

        private MonoBehaviour _mono;

        void GetId()
        {
            _mono = EditorGUILayout.ObjectField(_mono, typeof(MonoBehaviour), true) as MonoBehaviour;
            if (_mono)
            {
                string guid;
                long fid;
                var script = MonoScript.FromMonoBehaviour(_mono);
                var path = AssetDatabase.TryGetGUIDAndLocalFileIdentifier(script, out guid, out fid);
                EditorGUILayout.TextField("fileID", fid.ToString());
                EditorGUILayout.TextField("guid", guid);
            }
        }

        private MonoScript _script;

        void GetScriptId()
        {
            _script = EditorGUILayout.ObjectField(_script, typeof(MonoScript), true) as MonoScript;
            if (_script)
            {
                string guid;
                long fid;
                var path = AssetDatabase.TryGetGUIDAndLocalFileIdentifier(_script, out guid, out fid);
                EditorGUILayout.TextField("fileID", fid.ToString());
                EditorGUILayout.TextField("guid", guid);
            }
        }

        string _guid, _fid;
        string _result;

        void GetAssetFromId()
        {
            EditorGUILayout.BeginHorizontal();
            _fid = EditorGUILayout.TextField("fileID", _fid);
            _guid = EditorGUILayout.TextField("guid", _guid);
            if (GUILayout.Button("find"))
            {
                _result = "未找到";
                var path = AssetDatabase.GUIDToAssetPath(_guid);
                var assets = AssetDatabase.LoadAllAssetsAtPath(path);
                string guid;
                long fid;

                for (int i = 0; i < assets.Length; i++)
                {
                    var asset = assets[i];
                    var s = AssetDatabase.TryGetGUIDAndLocalFileIdentifier(asset, out guid, out fid);
                    if (s && _fid == fid.ToString())
                    {
                        var script = asset as MonoScript;
                        string name;
                        if (script != null)
                            name = script.GetClass().FullName;
                        else
                            name = asset.name;
                        _result = string.Format("{0}->{1}", path, name);
                        break;
                    }
                }
            }

            EditorGUILayout.EndHorizontal();
            GUILayout.TextField(_result);
        }


        private DefaultAsset _dll;
        private string _dll_guid;
        private long _dll_fid;
        private UnityEngine.Object[] _dllAssets;
        private float _scrollValueX = 0;
        private float _scrollValueY = 0;
        private Vector2 _scroll;

        void GetDllScriptId()
        {
            _dll = EditorGUILayout.ObjectField(".dll", _dll, typeof(DefaultAsset), true) as DefaultAsset;
            GUILayout.BeginHorizontal();
            EditorGUILayout.TextField("fileID", _dll_fid.ToString());
            EditorGUILayout.TextField("guid", _dll_guid);
            GUILayout.EndHorizontal();
            if (GUILayout.Button("Open") && _dll != null)
            {
                var path = AssetDatabase.GetAssetPath(_dll);
                if (path.EndsWith(".dll"))
                    _dllAssets = AssetDatabase.LoadAllAssetsAtPath(path);
                else
                    _dllAssets = null;
            }

            if (_dllAssets != null && _dllAssets.Length > 0)
            {
                _scroll = EditorGUILayout.BeginScrollView(_scroll, GUILayout.Width(600));
                for (int i = 0; i < _dllAssets.Length; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.TextField(_dllAssets[i].name);
                    if (GUILayout.Button("view", GUILayout.Width(100)))
                    {
                        var s = AssetDatabase.TryGetGUIDAndLocalFileIdentifier(_dllAssets[i], out _dll_guid, out _dll_fid);
                    }

                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndScrollView();
            }
        }

        Material m_material;

        void DrawLine()
        {
            EditorGUILayout.Space(20);
            if (Event.current.type == EventType.Repaint)
            {
                var lastRect = GUILayoutUtility.GetLastRect();
                var rect = new Rect(0, lastRect.y, 600, 20);
                GL.PushMatrix();
                m_material.SetPass(0);
                GL.LoadPixelMatrix();
                GL.Begin(GL.QUADS);
                GL.Color(new Color32(78, 201, 176, 255));
                GL.Vertex3(rect.x, rect.y, 0);
                GL.Vertex3(rect.x + rect.width, rect.y, 0);
                GL.Vertex3(rect.x + rect.width, rect.y + rect.height, 0);
                GL.Vertex3(rect.x, rect.y + rect.height, 0);
                GL.End();
                GL.PopMatrix();
            }
        }
    }

    public class AssetModify
    {
        delegate void AssetHandler(string file);

        public static string[] AsssetExtension = { ".asset", ".prefab", ".unity", ".anim" };

        List<Assembly> _assemblies;
        DirectoryInfo _metadir;
        DirectoryInfo _assetdir;

        //all .cs.meta files Key:filename(has extension) Value:fullpath
        ConcurrentDictionary<string, string> _csmetafiles = new ConcurrentDictionary<string, string>();

        //has any AsssetExtension files
        ConcurrentBag<string> _assetfiles = new ConcurrentBag<string>();


        //Assembly corresponding guid
        Dictionary<Assembly, string> _assembliesguid = new Dictionary<Assembly, string>();

        //C# Type <=> FilePath
        //对于某种类他总是应该存在对应的文件[文件名与类名应该一致]
        ConcurrentDictionary<Type, string> _type2path = new ConcurrentDictionary<Type, string>();
        ConcurrentDictionary<string, Type> _path2type = new ConcurrentDictionary<string, Type>();

        ConcurrentDictionary<Type, string> _type2fileid = new ConcurrentDictionary<Type, string>();

        ConcurrentDictionary<string, string> _guid2path = new ConcurrentDictionary<string, string>();

        ConcurrentDictionary<string, AssetHandler> _extension_handler = new ConcurrentDictionary<string, AssetHandler>();

        //note assembly should be in assetdir[has corresponding .meta]
        public AssetModify(List<Assembly> assemblies, DirectoryInfo metadir, DirectoryInfo assetdir)
        {
            if (assemblies == null)
                throw new ArgumentNullException("assembly");
            if (metadir == null)
                throw new ArgumentNullException("metadir");
            if (assetdir == null)
                assetdir = metadir;

            _assemblies = assemblies;
            _metadir = metadir;
            _assetdir = assetdir;
        }

        public AssetModify(Assembly assemblies, DirectoryInfo metadir, DirectoryInfo assetdir)
        {
            if (assemblies == null)
                throw new ArgumentNullException("assembly");
            if (metadir == null)
                throw new ArgumentNullException("metadir");
            if (assetdir == null)
                assetdir = metadir;

            _assemblies = new List<Assembly> { assemblies };
            _metadir = metadir;
            _assetdir = assetdir;
        }

        public void RegisterExtensionHandler(string extension, Action<string> action)
        {
            if (_extension_handler.ContainsKey(extension))
                _extension_handler[extension] += new AssetHandler(action);
            else
                _extension_handler[extension] = new AssetHandler(action);
        }

        string ShortPathName(DirectoryInfo parent, string fullpath)
        {
            return fullpath.Replace(parent.FullName, "");
        }

        public void BuildAssemblyInfo(string pattern = ".*")
        {
            var DefaultAssetModfiy = new AssetHandler(MakeStringReplaceAssetModify("m_Script"));
            foreach (var extension in AsssetExtension)
            {
                if (!_extension_handler.ContainsKey(extension))
                    _extension_handler.TryAdd(extension, DefaultAssetModfiy);
            }

            if (_extension_handler.ContainsKey(".anim"))
            {
                _extension_handler[".anim"] = new AssetHandler(MakeStringReplaceAssetModify("script"));
            }

            var assembliesname = _assemblies.Select((assembly) => assembly.GetName().Name + ".dll.meta");
            var assembliesmeta = new ConcurrentDictionary<string, string>();

            //find assemblyname.meta build _assetfiles
            var assetdirfileinfos = _assetdir.GetFiles("*.*", SearchOption.AllDirectories);
            Parallel.ForEach(assetdirfileinfos, file =>
            {
                if (file.Extension == ".meta" && file.Name.Contains(".dll"))
                {
                    var assemblyname = assembliesname.FirstOrDefault(name => file.Name == name);
                    if (assemblyname != null)
                        assembliesmeta.TryAdd(assemblyname, file.FullName);
                }

                var regex = new Regex(pattern);
                if (!regex.IsMatch(file.FullName))
                    return;

                if (AsssetExtension.Contains(file.Extension))
                {
                    _assetfiles.Add(file.FullName);
                    return;
                }
            });


            //build _assembliesguid;
            foreach (var assembly in _assemblies)
            {
                var assemblyname = assembly.GetName().Name + ".dll.meta";
                var assemblymeta = string.Empty;
                if (!assembliesmeta.TryGetValue(assemblyname, out assemblymeta))
                {
                    Console.WriteLine(string.Format("build assmebliesguid: Try Get {0} 'meta failed", assemblyname));
                    continue;
                }

                try
                {
                    var yaml = new Deserializer().Deserialize(File.OpenText(assemblymeta));
                    //var json = new YamlDotNet.Serialization.SerializerBuilder().JsonCompatible().Build().Serialize(yaml);

                    var pairs = (Dictionary<object, object>)yaml;
                    _assembliesguid[assembly] = pairs["guid"].ToString();
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("build assembliesguid parse {0} failed Exception: {1}", assemblymeta, e));
                }
            }

            //build _metafiles
            var metadirfileinfos = _metadir.GetFiles("*.*", SearchOption.AllDirectories);
            Parallel.ForEach(metadirfileinfos, file =>
                {
                    if (file.Extension != ".meta")
                        return;
                    if (!file.Name.Contains(".cs"))
                        return;
                    _csmetafiles.AddOrUpdate(file.Name, file.FullName, (key, value) =>
                    {
                        Console.WriteLine(string.Format("{0} has exist please change filename&classname.warning replace {1} by {2}",
                            file.Name,
                            ShortPathName(_metadir, value),
                            ShortPathName(_metadir, file.FullName)));
                        return file.FullName;
                    });
                }
            );

            //parse metafiles build _guid2path
            Parallel.ForEach(_csmetafiles, pair =>
            {
                try
                {
                    var yaml = new Deserializer().Deserialize(File.OpenText(pair.Value));
                    var pairs = (Dictionary<object, object>)yaml;
                    if (!_guid2path.TryAdd(pairs["guid"].ToString(), pair.Value))
                        Console.WriteLine(string.Format("build guidpath add {0},{1} failed", pairs["guid"], pair.Value));
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("build guidpath parse {0} failed Exception: {1}", pair.Value, e));
                }
            });

            //parse assembly build _type2path & _path2type
            var types = _assemblies
                .Select(assembly => assembly.GetTypes())
                .SelectMany(t => t)
                //必须继承ScriptableObject才能序列化
                //必须继承MonoBehaviour才能被挂接
                .Where(type => type.IsSubclassOf(typeof(ScriptableObject)) || type.IsSubclassOf(typeof(MonoBehaviour))).ToList();
            Parallel.ForEach(types, type =>
            {
                var fullpath = string.Empty;
                if (!_csmetafiles.TryGetValue(type.Name + ".cs.meta", out fullpath))
                {
                    Console.WriteLine(string.Format("warning :find {0}.cs failed", type.Name));
                    return;
                }

                if (!_type2path.TryAdd(type, fullpath))
                {
                    Console.WriteLine(string.Format("warning :type {0} has existed", type.Name));
                    return;
                }

                if (!_path2type.TryAdd(fullpath, type))
                    Console.WriteLine(string.Format("error:type {0} path {1} existed!", type.Name, ShortPathName(_metadir, fullpath)));

                if (!_type2fileid.TryAdd(type, UtilsGen.FileID.Compute(type).ToString()))
                    Console.WriteLine(string.Format("error:type {0} add fileid failed", type.Name));
            });
        }

        public void ModifyAsset()
        {
            Parallel.ForEach(_assetfiles, file =>
            {
                var extension = Path.GetExtension(file);
                AssetHandler handler = null;
                if (_extension_handler.TryGetValue(extension, out handler))
                    handler.Invoke(file);
            });
        }

        public void ModfiyAsset(string file)
        {
            var extension = Path.GetExtension(file);
            AssetHandler handler = null;
            if (_extension_handler.TryGetValue(extension, out handler))
                return;
            Parallel.ForEach(_assetfiles, asset =>
            {
                var filename = Path.GetFileName(asset);
                if (filename != file)
                    return;
                handler.Invoke(file);
            });
        }

        Action<string> MakeStringReplaceAssetModify(string prefix)
        {
            return file => DefaultAssetModfiy(file, prefix);
        }

        public void DefaultAssetModfiy(string file, string prefix)
        {
            var shortfilename = ShortPathName(_assetdir, file);
            try
            {
                var lines = File.ReadAllLines(file);
                lines = lines.Select(line =>
                {
                    if (!line.Contains(prefix))
                        return line;
                    var begin = line.IndexOf("fileID: ") + 8;
                    if (begin == -1)
                        return line;
                    var end = line.IndexOf(',', begin);
                    if (end == -1)
                        return line;

                    var fileID = line.Substring(begin, end - begin);
                    begin = line.IndexOf("guid: ") + 6;
                    if (begin == -1)
                        return line;
                    end = begin + 32;
                    var guid = line.Substring(begin, end - begin);

                    if (_assembliesguid.ContainsValue(guid))
                    {
                        Console.WriteLine(string.Format("{0}.MonoBehavior.m_Script Had Modfiy Skip it", shortfilename));
                        return line;
                    }

                    //guid->path
                    string cspath = null;
                    if (!_guid2path.TryGetValue(guid, out cspath))
                    {
                        Console.WriteLine(string.Format("AssetModfiy.Map GUID {0} To Path Failed", shortfilename));
                        return line;
                    }

                    Console.WriteLine(string.Format("Old {0}.MonoBehavior.m_Script {{fileID:{1} guid:{2}}}", shortfilename, fileID, guid));

                    //path->type
                    Type type = null;
                    if (!_path2type.TryGetValue(cspath, out type))
                    {
                        Console.WriteLine(string.Format("AssetModfiy.Map(GUID:{1}) Path {0} To Type Failed", shortfilename, guid));
                        return line;
                    }

                    string newguid = null;
                    //type->assembly->guid
                    if (!_assembliesguid.TryGetValue(type.Assembly, out newguid))
                    {
                        Console.WriteLine(string.Format("Error: AssetModfiy.Map Type {0} To GUID", type));
                        return line;
                    }

                    string newfileID = null;
                    //type->fileID
                    if (!_type2fileid.TryGetValue(type, out newfileID))
                    {
                        Console.WriteLine(string.Format("Error: AssetModfiy.Map Type {0} To fileID", type));
                        return line;
                    }

                    Console.WriteLine(string.Format("New {0}.MonoBehavior.m_Script {{fileID:{1} guid:{2}}}", shortfilename, newfileID, newguid));
                    return line.Replace(fileID, newfileID).Replace(guid, newguid);
                }).ToArray();

                var modfiy = file + ".modfiy";
                if (!File.Exists(modfiy))
                    File.Copy(file, modfiy);

                File.SetAttributes(file, File.GetAttributes(file) & ~FileAttributes.ReadOnly);

                //行拼接
                //并干掉: ''

                //强制覆盖
                File.Delete(file);
                File.WriteAllLines(file, lines);
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("AssetModfiy.ModfiyAsset {0} Failed Exception:{1}", shortfilename, e));
            }
        }

        public void AssetBundleModfiy(string file)
        {
        }


        public void RevertAsset()
        {
            var assetdirfileinfos = _assetdir.GetFiles("*.modfiy", SearchOption.AllDirectories);
            Parallel.ForEach(assetdirfileinfos, file =>
            {
                var targetfile = file.FullName.Replace(file.Extension, "");
                if (File.Exists(targetfile))
                {
                    File.SetAttributes(targetfile, File.GetAttributes(targetfile) & ~FileAttributes.ReadOnly);
                    File.Delete(targetfile);
                }

                File.Move(file.FullName, targetfile);
            });
        }
    }

    // var assemblies = Assembly.LoadFile(@"G:\UnityProject\G108-Win-2020\Packages\com.blz.config\DBVC\DBVC.dll");
    //
    // var asset_modify = new AssetModify(
    //     assemblies,
    //     new DirectoryInfo(@"G:\UnityProject\G108-Win-2020\Packages\com.blz.config\DBVC"),
    //     new DirectoryInfo(@"G:\Test\DBVC")
    // );
    //
    // asset_modify.BuildAssemblyInfo();
    // asset_modify.ModifyAsset();

    public partial class EditorMenu
    {
        [MenuItem("Tools/AssetModfiy/Revert")]
        public static void Test()
        {
            var assemblies = new List<Assembly>
            {
                Assembly.LoadFile(@"G:\UnityProject\G108-Win-2020\Packages\com.blz.config\DBVC\DBVC.dll"),
                Assembly.LoadFile(@"G:\UnityProject\G108-Win-2020\Packages\com.blz.config\DBVC\ClientCore.dll"),
                Assembly.LoadFile(@"G:\UnityProject\G201\proj\third-plugins-back\Demigiant\Source\DOTweenPro\DOTweenPro.dll"),
            };
            var dirs = new List<DirectoryInfo>
            {
                new DirectoryInfo(@"G:\UnityProject\G201\proj\third-plugins\client-core\ClientCore"),
                new DirectoryInfo(@"G:\UnityProject\G201\proj\third-plugins\client-core\DBVC"),
                new DirectoryInfo(@"G:\UnityProject\G201\proj\third-plugins\Demigiant\Runtime"),
            };
            var md5 = new Dictionary<string, string>
            {
                { "356a8f05a6726e645ade74e1e74b6523", "ClientCore" },
                { "53d0d244ae5b5d343b19aced455b29ca", "DBVC" },
                { "543bd8adf2811e447b9b5dc5b8c7feb1", "DOTweenPro" },
            };
            Test2(assemblies, dirs, md5);
        }

        private struct ScriptDataInfo
        {
            public string GUID;

            public long FileID;

            public string RealPath;

            public string FileName;

            public string NameSpace;
        }

        private static void Test2(
            ICollection<Assembly> assemblies,
            ICollection<DirectoryInfo> dirs,
            IDictionary<string, string> md5)
        {
            //
            var fileidDic = new Dictionary<long, string>();


            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsAbstract) continue;
                    if (!type.IsSubclassOf(typeof(UnityEngine.Object))) continue;

                    var fileid = UtilsGen.FileID.Compute(type);
                    fileidDic.Add(fileid, type.FullName);
                    Console.WriteLine("{0} [ fileid : {1} ]", type.FullName, fileid);
                }
            }

            var guidDic = new Dictionary<string, string>();
            foreach (var directory in dirs)
            {
                foreach (var file in directory.GetFiles("*.cs", SearchOption.AllDirectories))
                {
                    if (file.Name.StartsWith("AssemblyInfo")) continue;
                    if (file.Extension.Contains(".meta")) continue;
                    if (!file.Extension.Contains(".cs")) continue;
                    var meta = string.Concat(file.FullName, ".meta");
                    if (!File.Exists(string.Concat(file.FullName, ".meta"))) continue;
                    var metaData = UtilsGen.Yaml.Deserialize<Hashtable>(File.ReadAllText(meta));


                    var namespacename = "";
                    foreach (var line in File.ReadLines(file.FullName))
                    {
                        if (line.StartsWith("namespace"))
                        {
                            namespacename = line.Replace("namespace ", "").Replace("{", "").Trim();
                            namespacename = string.Concat(namespacename, ".");
                            break;
                        }
                    }

                    namespacename = string.Concat(namespacename, file.Name.Replace(file.Extension, ""));

                    if (guidDic.ContainsKey(namespacename))
                    {
                        Debug.LogError(string.Format("Error: {0} {1} {2}", namespacename, guidDic[namespacename], metaData["guid"]));
                    }
                    else guidDic.Add(namespacename, metaData["guid"].ToString());

                    Console.WriteLine("{0} [ fileid : {1} ]", namespacename, metaData["guid"]);
                }
            }

            var path = Application.dataPath.Replace("Assets", "");

            var assetList = new List<string>();
            foreach (var file in AssetDatabase.GetAllAssetPaths())
            {
                if (file.Contains("SRDebugger")) continue;
                if (file.Contains("Sirenix")) continue;

                var full = Path.Combine(path, file);
                if (!File.Exists(full)) continue;

                var Extension = Path.GetExtension(full).ToLower();
                if (string.IsNullOrEmpty(Extension)) continue;
                if (Extension.Contains("cs")) continue;
                if (Extension.Contains("dll")) continue;
                if (Extension.Contains("txt")) continue;
                if (Extension.Contains("json")) continue;
                if (Extension.Contains("lua")) continue;
                if (Extension.Contains("bytes")) continue;

                if (Extension.Contains("png")) continue;
                if (Extension.Contains("jpg")) continue;
                if (Extension.Contains("mat")) continue;
                if (Extension.Contains("shader")) continue;
                if (Extension.Contains("mp3")) continue;
                if (Extension.Contains("fbx")) continue;
                if (Extension.Contains("font")) continue;
                if (Extension.Contains("otf")) continue;
                if (Extension.Contains("ttf")) continue;
                if (Extension.Contains("unity")) continue;
                if (Extension.Contains("so")) continue;
                if (Extension.Contains("asmdef")) continue;
                if (Extension.Contains("uss")) continue;
                if (Extension.Contains("xml")) continue;
                if (Extension.Contains("prefs")) continue;


                assetList.Add(full);
            }


            var builder = new StringBuilder();
            foreach (var file in assetList)
            {
                var lines = File.ReadLines(file);
                var changed = false;
                builder.Clear();
                foreach (var line in lines)
                {
                    if (!(line.Contains("m_Script") && line.Contains("fileID") && line.Contains("guid")))
                    {
                        builder.AppendLine(line);
                        continue;
                    }

                    var arr = line.Split(':').Trim();
                    var fileid = long.Parse(arr[2].Split(',')[0]);
                    if (fileid == 11500000)
                    {
                        builder.AppendLine(line);
                        continue;
                    }

                    var guid = arr[3].Split(',')[0];
                    if (!md5.ContainsKey(guid))
                    {
                        builder.AppendLine(line);
                        continue;
                    }


                    if (!fileidDic.TryGetValue(fileid, out var newguid))
                    {
                        builder.AppendLine(line);
                        continue;
                    }

                    if (!guidDic.TryGetValue(newguid, out newguid))
                    {
                        builder.AppendLine(line);
                        continue;
                    }

                    builder.AppendLine(line.Replace(fileid.ToString(), "11500000").Replace(guid, newguid));
                    changed = true;
                }

                if (changed)
                {
                    Console.WriteLine(file);
                    File.WriteAllText(file, builder.ToString());
                }
            }
        }
    }
}
