/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-26
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.IO;

namespace AIO.UEditor
{
    public class PackageData
    {
        public List<string> URL;

        [NonSerialized] public List<string> Names;

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
            {
                if (Path.GetFileName(item).Replace(".git", "") == key)
                    return item;
            }

            throw new Exception("packagedata not find key");
        }
    }
}
