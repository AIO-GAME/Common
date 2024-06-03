/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

namespace AIO
{
    public partial class PrWin
    {
        /// <summary>
        /// 适用于 复制文件
        /// HELP => 
        /// /A          : [仅复制有存档属性集的文件 但不更改属性]
        /// /M          : [仅复制有存档属性集的文件 并关闭存档属性]
        /// /D:         : [允许解密要创建的目标文件]
        /// /V          : [验证新文件写入是否正确]
        /// /N          : [复制带有非 8dot3 名称的文件时]
        /// /Y          : [取消提示以确认要覆盖 现有目标文件]
        /// /-Y         : [要提示以确认要覆盖现有目标文件]
        /// /Z          : [用可重新启动模式复制已联网的文件]
        /// /L          : [如果源是符号链接，请将链接复制到目标而不是源链接指向的实际文件]
        /// source      : [指定要复制的文件]
        /// destination : [为新文件指定目录和/或文件名]
        /// </summary>
        public const string CMD_Copy = "copy";

        /// <summary>
        /// 适用于删除文件
        /// HELP => 
        /// /P      : [删除每一个文件之前提示确认]
        /// /F      : [强制删除只读文件]
        /// /S      : [删除所有子目录中的指定的文件]
        /// /Q      : [安静模式。删除全局通配符时，不要求确认]
        /// /A      : [根据属性选择要删除的文件]
        /// [R:只读文件] [L:重新分析点]
        /// [H:隐藏文件] [A:准备存档的文件]
        /// [O:脱机文件] [I:无内容索引文件]
        /// [S:系统文件] [-:表示“否”的前缀]
        /// </summary>
        public const string CMD_Del = "del";

        /// <summary>
        /// 移动或重命名目录
        /// HELP => 
        /// [/Y]    :[关闭 移动文件时的确认]
        /// [/-Y]   :[打开 移动文件时的确认]
        /// </summary>
        public const string CMD_Move = "move";

        /// <summary>
        /// HELP => 
        /// COMP [data1] [data2] [/D] [/A] [/L] [/N=number] [/C] [/OFF[LINE]] [/M]
        /// /D         : [以十进制格式显示差异]
        /// /A         : [以 ASCII 字符显示差异]
        /// /L         : [显示不同的行数]
        /// /N=number  : [只比较每个文件中第一个指定的行数]
        /// /C         : [比较文件时 ASCII 字母不区分大小写]
        /// /OFF[LINE] : [不要跳过带有脱机属性集的文件]
        /// /M         : [不提示比较更多文件]
        /// </summary>
        public const string CMD_Comp = "comp";

        /// <summary>
        /// HELP => 
        /// </summary>
        public const string CMD_Shrpubw = "shrpubw";

        /// <summary>
        /// HELP => 
        /// </summary>
        public const string CMD_Mkdir = "mkdir";

        /// <summary>
        /// 适用于 删除文件夹
        /// HELP => 
        /// RMDIR[/ S][/ Q][drive:] path
        /// RD[/ S][/ Q][drive:] path
        ///    /S 除目录本身外，还将删除指定目录下的所有子目录和文件。用于删除目录树。
        ///    /Q 安静模式，带 /S 删除目录树时不要求确认
        /// </summary>
        public const string CMD_Rmdir = "rmdir";

        /// <summary>
        /// HELP => 
        /// MKLINK [[/D] | [/H] | [/J]] Link Target
        /// /D Creates a directory symbolic link.Default is a file symbolic link.
        /// /H Creates a hard link instead of a symbolic link.
        /// /J Creates a Directory Junction.
        ///    Link Specifies the new symbolic link name.
        ///    Target Specifies the path(relative or absolute) that the new linkrefers to.
        /// </summary>
        public const string CMD_Mklink = "mklink";

        /// <summary>
        /// HELP => 
        /// /A          : [仅复制有存档属性集的文件 但不更改属性]
        /// /M          : [仅复制有存档属性集的文件 并关闭存档属性]
        /// /D:m-d-y    : [复制在指定日期或指定日期以后更改的文件。 如果没有提供日期，只复制那些源时间比目标时间新的文件]
        /// /P          : [创建每个目标文件之前提示你]
        /// /S          : [复制目录和子目录，不包括空目录]
        /// /E          : [复制目录和子目录，包括空目录。与 /S /E 相同。可以用来修改 /T]
        /// /V          : [验证每个新文件的大小]
        /// /W          : [提示你在复制前按键]
        /// /C          : [即使有错误，也继续复制]
        /// /I          : [如果目标不存在，且要复制多个文件，则假定目标必须是目录]
        /// /Q          : [复制时不显示文件名]
        /// /F          : [复制时显示完整的源文件名和目标文件名]
        /// /L          : [显示要复制的文件]
        /// /G          : [允许将加密文件复制到不支持加密的目标]
        /// /H          : [也复制隐藏文件和系统文件]
        /// /R          : [覆盖只读文件]
        /// /T          : [创建目录结构，但不复制文件。不包括空目录或子目录。/T /E 包括空目录和子目录]
        /// /U          : [只复制已经存在于目标中的文件]
        /// /K          : [复制属性。一般的 Xcopy 会重置只读属性]
        /// /N          : [用生成的短名称复制]
        /// /O          : [复制文件所有权和 ACL 信息]
        /// /X          : [复制文件审核设置(隐含 /O)]
        /// /Y          : [取消提示以确认要覆盖 现有目标文件]
        /// /-Y         : [要提示以确认要覆盖现有目标文件]
        /// /Z          : [在可重新启动模式下复制网络文件]
        /// /B          : [复制符号链接本身与链接目标]
        /// /J          : [复制时不使用缓冲的 I/O。推荐复制大文件时使用]
        /// source      : [指定要复制的文件]
        /// destination : [为新文件指定目录和/或文件名]
        /// </summary>
        public const string CMD_Xcopy = "xcopy";

        /// <summary>
        /// HELP =>
        /// shutdown [/i | /l | /s | /sg | /r | /g | /a | /p | /h | /e | /o] [/hybrid] [/soft] [/fw] [/f] [/m \\computer][/t xxx][/d [p|u:]xx:yy [/c "comment"]]
        /// No args    Display help. This is the same as typing /?.
        /// /?         Display help. This is the same as not typing any options.
        /// /i         Display the graphical user interface (GUI). This must be the first option. 
        ///  
        /// /l         Log off. This cannot be used with /m or /d options.
        /// /s         Shutdown the computer.
        /// /sg        Shutdown the computer. On the next boot, if Automatic Restart Sign-On is enabled,
        ///            automatically sign in and lock last interactive user. After sign in, restart any registered applications.     
        /// /r         Full shutdown and restart the computer.
        /// /g         Full shutdown and restart the computer. After the system is rebooted, if Automatic Restart Sign-On is enabled,
        ///            automatically sign in and lock last interactive user. After sign in, restart any registered applications.    
        ///  /a        Abort a system shutdown. This can only be used during the time-out period.
        ///            Combine with /fw to clear any pending boots to firmware. 
        ///  /p        Turn off the local computer with no time-out or warning. Can be used with /d and /f options.      
        ///  /h        Hibernate the local computer. Can be used with the /f option.    
        ///  /hybrid   Performs a shutdown of the computer and prepares it for fast startup. Must be used with /s option.   
        ///  /fw       Combine with a shutdown option to cause the next boot to go to the firmware user interface.         
        ///  /e        Document the reason for an unexpected shutdown of a computer.
        ///  /o        Go to the advanced boot options menu and restart the computer. Must be used with /r option.
        ///  /m \\computer Specify the target computer.
        ///  /t xxx    Set the time-out period before shutdown to xxx seconds.
        ///            The valid range is 0-315360000 (10 years), with a default of 30.
        ///            If the timeout period is greater than 0, the /f parameter is implied.   
        ///  /c "comment" Comment on the reason for the restart or shutdown. Maximum of 512 characters allowed.
        ///  /f         Force running applications to close without forewarning users.
        ///             The /f parameter is implied when a value greater than 0 is specified for the /t parameter.      
        ///  /d [p|u:]xx:yy  Provide the reason for the restart or shutdown.
        ///             p indicates that the restart or shutdown is planned.
        ///             u indicates that the reason is user defined.
        ///             If neither p nor u is specified the restart or shutdown is unplanned.   
        ///             xx is the major reason number (positive integer less than 256).
        ///             yy is the minor reason number (positive integer less than 65536).
        /// </summary>
        public const string Cmd_Shutdown = "shutdown";
    }
}