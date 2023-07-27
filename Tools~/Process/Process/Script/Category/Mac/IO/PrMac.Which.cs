namespace AIO
{
    public sealed partial class PrMac
    {
        /// <summary>
        /// 查询软件
        /// </summary>
        public static IExecutor Which(string package)
        {
            return Create(CMD_Which, package);
        }
    }
}