#region

using System;
using System.Collections.Generic;
using System.IO;

#endregion

namespace AIO.UEditor
{
    public class PackageData
    {
        [NonSerialized]
        public List<string> Names;

        public List<string> URL;

        public PackageData()
        {
            Names = new List<string>();
        }

        public void GetNames()
        {
            Names.Clear();
            foreach (var item in URL)
                Names.Add(Path.GetFileName(item).Replace(".git", ""));
        }

        public string GetURL(string key)
        {
            foreach (var item in URL)
                if (Path.GetFileName(item).Replace(".git", "") == key)
                    return item;

            throw new Exception("packagedata not find key");
        }
    }
}