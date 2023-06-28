/// <summary>
/// 输出类型
/// </summary>
public enum EPrint
{
    /// <summary>
    /// 日志
    /// </summary>
    Log = 1,

    /// <summary>
    /// 警告
    /// </summary>
    Warn = 2,

    /// <summary>
    /// 错误
    /// </summary>
    Error = 4,

    /// <summary>
    /// 异常
    /// </summary>
    Exception = 8,

    /// <summary>
    /// 日志 警告                 
    /// </summary>
    Log_Warn = Log | Warn,

    /// <summary>
    /// 日志 警告 错误                 
    /// </summary>
    Log_Warn_Error = Log | Warn | Error,

    /// <summary>
    /// 日志 警告 异常                 
    /// </summary>
    Log_Warn_Exception = Log | Warn | Exception,

    /// <summary>
    /// 日志 错误                 
    /// </summary>
    Log_Error = Log | Error,

    /// <summary>
    /// 日志 错误 异常                 
    /// </summary>
    Log_Error_Exception = Log | Error | Exception,

    /// <summary>
    /// 日志 异常       
    /// </summary>
    Log_Exception = Log | Exception,

    /// <summary>
    /// 日志 警告 错误 异常
    /// </summary>
    ALL = Log | Warn | Error | Exception,
}