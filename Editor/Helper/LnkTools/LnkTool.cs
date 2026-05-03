namespace AIO.UEditor
{
    public static class LnkTool
    {
        /// <summary>
        /// 创建快捷工具
        /// </summary>
        /// <param name="data"> 数据 </param>
        public static void Add(LnkToolData data)
        {
            ToolbarExtend.AddData.Add(new LnkToolDataInternal(data));
            data.Dispose();
        }
    }
}