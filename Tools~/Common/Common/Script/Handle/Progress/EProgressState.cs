/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-17
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/


/// <summary>
/// 进度状态
/// </summary>
public enum EProgressState : byte
{
    /// <summary>
    /// 准备
    /// </summary>
    Ready = 0,

    /// <summary>
    /// 运行
    /// </summary>
    Running,

    /// <summary>
    /// 取消
    /// </summary>
    Cancel,

    /// <summary>
    /// 暂停
    /// </summary>
    Pause,

    /// <summary>
    /// 完成
    /// </summary>
    Finish,

    /// <summary>
    /// 失败
    /// </summary>
    Fail,
}