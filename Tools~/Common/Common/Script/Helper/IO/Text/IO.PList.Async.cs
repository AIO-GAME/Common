using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using AIO.PList;

public partial class AHelper
{
    public partial class IO
    {
        /// <summary>
        /// 写入 Plist XML
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task WritePListBinaryAsync(string path, PListRoot value)
        {
            await Task.Factory.StartNew(() => { value.Save(path, PListFormat.Binary); });
        }

        /// <summary>
        /// 写入 Plist XML
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task WritePListBinaryAsync(string path, PListDict value)
        {
            await Task.Factory.StartNew(() =>
            {
                var root = new PListRoot { Root = value };
                root.Save(path, PListFormat.Binary);
            });
        }


        /// <summary>
        /// 写入 Plist XML
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task WritePListAsync(string path, PListRoot value)
        {
            await Task.Factory.StartNew(() => { value.Save(path, PListFormat.Xml); });
        }

        /// <summary>
        /// 写入 Plist XML
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task WritePListAsync(string path, PListDict value)
        {
            await Task.Factory.StartNew(() =>
            {
                var root = new PListRoot { Root = value };
                root.Save(path, PListFormat.Xml);
            });
        }
    }
}
