/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-08-03
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
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
using YamlDotNet.Serialization;

namespace AIO.Unity.Editor
{
    public class MD4 : HashAlgorithm
    {
        private uint _a;
        private uint _b;
        private uint _c;
        private uint _d;
        private uint[] _x;
        private int _bytesProcessed;

        public MD4()
        {
            _x = new uint[16];
            Initialize();
        }

        public override void Initialize()
        {
            _a = 0x67452301;
            _b = 0xefcdab89;
            _c = 0x98badcfe;
            _d = 0x10325476;
            _bytesProcessed = 0;
        }

        protected override void HashCore(byte[] array, int offset, int length)
        {
            ProcessMessage(Bytes(array, offset, length));
        }

        protected override byte[] HashFinal()
        {
            try
            {
                ProcessMessage(Padding());
                return new[] { _a, _b, _c, _d }.SelectMany(word => Bytes(word)).ToArray();
            }
            finally
            {
                Initialize();
            }
        }

        private void ProcessMessage(IEnumerable<byte> bytes)
        {
            foreach (byte b in bytes)
            {
                int c = _bytesProcessed & 63;
                int i = c >> 2;
                int s = (c & 3) << 3;
                _x[i] = (_x[i] & ~((uint)255 << s)) | ((uint)b << s);
                if (c == 63)
                {
                    Process16WordBlock();
                }

                _bytesProcessed++;
            }
        }

        private static IEnumerable<byte> Bytes(byte[] bytes, int offset, int length)
        {
            for (int i = offset; i < length; i++)
            {
                yield return bytes[i];
            }
        }

        private IEnumerable<byte> Bytes(uint word)
        {
            yield return (byte)(word & 255);
            yield return (byte)((word >> 8) & 255);
            yield return (byte)((word >> 16) & 255);
            yield return (byte)((word >> 24) & 255);
        }

        private IEnumerable<byte> Repeat(byte value, int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return value;
            }
        }

        private IEnumerable<byte> Padding()
        {
            return Repeat(128, 1)
                .Concat(Repeat(0, ((_bytesProcessed + 8) & 0x7fffffc0) + 55 - _bytesProcessed))
                .Concat(Bytes((uint)_bytesProcessed << 3))
                .Concat(Repeat(0, 4));
        }

        private void Process16WordBlock()
        {
            uint aa = _a;
            uint bb = _b;
            uint cc = _c;
            uint dd = _d;
            foreach (int k in new[] { 0, 4, 8, 12 })
            {
                aa = Round1Operation(aa, bb, cc, dd, _x[k], 3);
                dd = Round1Operation(dd, aa, bb, cc, _x[k + 1], 7);
                cc = Round1Operation(cc, dd, aa, bb, _x[k + 2], 11);
                bb = Round1Operation(bb, cc, dd, aa, _x[k + 3], 19);
            }

            foreach (int k in new[] { 0, 1, 2, 3 })
            {
                aa = Round2Operation(aa, bb, cc, dd, _x[k], 3);
                dd = Round2Operation(dd, aa, bb, cc, _x[k + 4], 5);
                cc = Round2Operation(cc, dd, aa, bb, _x[k + 8], 9);
                bb = Round2Operation(bb, cc, dd, aa, _x[k + 12], 13);
            }

            foreach (int k in new[] { 0, 2, 1, 3 })
            {
                aa = Round3Operation(aa, bb, cc, dd, _x[k], 3);
                dd = Round3Operation(dd, aa, bb, cc, _x[k + 8], 9);
                cc = Round3Operation(cc, dd, aa, bb, _x[k + 4], 11);
                bb = Round3Operation(bb, cc, dd, aa, _x[k + 12], 15);
            }

            unchecked
            {
                _a += aa;
                _b += bb;
                _c += cc;
                _d += dd;
            }
        }

        private static uint ROL(uint value, int numberOfBits)
        {
            return (value << numberOfBits) | (value >> (32 - numberOfBits));
        }

        private static uint Round1Operation(uint a, uint b, uint c, uint d, uint xk, int s)
        {
            unchecked
            {
                return ROL(a + ((b & c) | (~b & d)) + xk, s);
            }
        }

        private static uint Round2Operation(uint a, uint b, uint c, uint d, uint xk, int s)
        {
            unchecked
            {
                return ROL(a + ((b & c) | (b & d) | (c & d)) + xk + 0x5a827999, s);
            }
        }

        private static uint Round3Operation(uint a, uint b, uint c, uint d, uint xk, int s)
        {
            unchecked
            {
                return ROL(a + (b ^ c ^ d) + xk + 0x6ed9eba1, s);
            }
        }
    }

    public static class FileIDUtil
    {
        public static int Compute(Type t)
        {
            string toBeHashed = "s\0\0\0" + t.Namespace + t.Name;
            using (HashAlgorithm hash = new MD4())
            {
                byte[] hashed = hash.ComputeHash(Encoding.UTF8.GetBytes(toBeHashed));
                int result = 0;
                for (int i = 3; i >= 0; --i)
                {
                    result <<= 8;
                    result |= hashed[i];
                }

                return result;
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

                if (!_type2fileid.TryAdd(type, FileIDUtil.Compute(type).ToString()))
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
            };
            Test2(assemblies);
        }

        private static void Test2(ICollection<Assembly> assemblies)
        {
            var fileidDic = new Dictionary<string, long>();

            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsAbstract) continue;
                    if (!type.IsSubclassOf(typeof(UnityEngine.Object))) continue;

                    var fileid = UtilsGen.FileID.Compute(type);
                    fileidDic.Add(type.FullName, fileid);
                    Console.WriteLine("{0} [ fileid : {1} ]", type.FullName, fileid);
                }
            }
        }
    }
}