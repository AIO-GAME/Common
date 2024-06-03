//namespace AIO.Core.Runtime.Extend
//{
//    using System.Globalization;
//    using System.Text;
//    using System.Threading;

//    /*
//        2.2.1 线程的标识符
//        ManagedThreadId是确认线程的唯一标识符，程序在大部分情况下都是通过Thread.ManagedThreadId来辨别线程的。
//        而Name是一个可变值，在默认时候，Name为一个空值 Null，开发人员可以通过程序设置线程的名称，但这只是一个辅助功能。

//        2.2.2 线程的优先级别
//        当线程之间争夺CPU时间时，CPU按照线程的优先级给予服务。
//        高优先级的线程可以完全阻止低优先级的线程执行。
//        .NET为线程设置了Priority属性来定义线程执行的优先级别，
//        里面包含5个选项，其中Normal是默认值。
//        除非系统有特殊要求，否则不应该随便设置线程的优先级别。

//        Lowest	可以将 Thread 安排在具有任何其他优先级的线程之后。
//        BelowNormal	可以将 Thread 安排在具有 Normal 优先级的线程之后，在具有 Lowest 优先级的线程之前。
//        Normal	默认选择。可以将 Thread 安排在具有 AboveNormal 优先级的线程之后，在具有 BelowNormal 优先级的线程之前。
//        AboveNormal	可以将 Thread 安排在具有 Highest 优先级的线程之后，在具有 Normal 优先级的线程之前。
//        Highest	可以将 Thread 安排在具有任何其他优先级的线程之前。

//        2.2.3 线程的状态
//        通过ThreadState可以检测线程是处于Unstarted、Sleeping、Running 等等状态，它比 IsAlive 属性能提供更多的特定信息。
//        前面说过，一个应用程序域中可能包括多个上下文，而通过CurrentContext可以获取线程当前的上下文。
//        CurrentThread是最常用的一个属性，它是用于获取当前运行的线程

//        2.2.4 System.Threading.Thread的方法
//        Thread 中包括了多个方法来控制线程的创建、挂起、停止、销毁，以后来的例子中会经常使用。
//        Abort()　　　　	终止本线程。
//        GetDomain()	    返回当前线程正在其中运行的当前域。
//        GetDomainId()	返回当前线程正在其中运行的当前域Id。
//        Interrupt()	    中断处于 WaitSleepJoin 线程状态的线程。
//        Join()	        已重载。 阻塞调用线程，直到某个线程终止时为止。
//        Resume()	    继续运行已挂起的线程。
//        Start()　    　	执行本线程。  Start方法标记这个线程就绪了，可以随时被执行，具体什么时候执行这个线程，由CPU决定
//        Suspend()	    挂起当前线程，如果当前线程已属于挂起状态则此不起作用
//        Sleep()　　	    把正在运行的线程挂起一段时间。

//        2.3 前台线程和后台线程
//        前台线程：只有所有的前台线程都结束，应用程序才能结束。默认情况下创建的线程都是前台线程
//        后台线程：只要所有的前台线程结束，后台线程自动结束。通过Thread.IsBackground设置后台线程。必须在调用Start方法之前设置线程的类型，否则一旦线程运行，将无法改变其类型。
//        通过BeginXXX方法运行的线程都是后台线程。

//        后台线程一般用于处理不重要的事情，应用程序结束时，后台线程是否执行完成对整个应用程序没有影响。如果要执行的事情很重要，需要将线程设置为前台线程。

//        2.4 线程同步
//        所谓同步：是指在某一时刻只有一个线程可以访问变量。如果不能确保对变量的访问是同步的，就会产生错误。

//        c#为同步访问变量提供了一个非常简单的方式，即使用c#语言的关键字Lock，
//        它可以把一段代码定义为互斥段，互斥段在一个时刻内只允许一个线程进入执行，
//        而其他线程必须等待。在c#中，关键字Lock定义如下：
//        Lock(expression)
//        {
//           statement_block
//        }

//        expression代表你希望跟踪的对象：
//                   如果你想保护一个类的实例，一般地，你可以使用this；
//                   如果你想保护一个静态变量（如互斥代码段在一个静态方法内部），一般使用类名就可以了
//        而statement_block就算互斥段的代码，这段代码在一个时刻内只可能被一个线程执行。

//        三、同步和异步
//        同步和异步是对方法执行顺序的描述。
//        同步：等待上一行完成计算之后，才会进入下一行。
//        例如：请同事吃饭，同事说很忙，然后就等着同事忙完，然后一起去吃饭。
//        异步：不会等待方法的完成，会直接进入下一行，是非阻塞的。
//        例如：请同事吃饭，同事说很忙，那同事先忙，自己去吃饭，同事忙完了他自己去吃饭。
//    */

//    public static class ThreadExtend
//    {
//        /// <summary>
//        /// 获取线程信息
//        /// </summary>
//        public static string GetAliveInfo(this Thread thread)
//        {
//            StringBuilder builder = new StringBuilder();
//            builder.Append("线程名 : ").AppendLine(thread.Name);
//            builder.Append("存活 : ").AppendLine(thread.IsAlive.ToString());
//            builder.Append("托管线程 ID : ").AppendLine(thread.ManagedThreadId.ToString());
//            builder.Append("线程状态 : ").AppendLine(thread.ThreadState.ToString());
//            builder.Append("线程池线程 : ").AppendLine(thread.IsThreadPoolThread.ToString());

//            if (thread.IsAlive)
//            {
//                builder.Append("后台线程 : ").AppendLine(thread.IsBackground.ToString());
//                builder.Append("优先级 : ").AppendLine(thread.Priority.ToString());
//                if (thread.ExecutionContext != null)
//                {
//                    builder.Append("获取一个 ExecutionContext 对象，该对象包含有关当前线程的各种上下文的信息。 : ").AppendLine(thread.ExecutionContext.ToString());
//                    builder.Append("获取线程正在其中执行的当前上下文。 : ").AppendLine(thread.CurrentCulture.GetCultureInfo());
//                    builder.Append("当前上文 : ").AppendLine(thread.CurrentUICulture.GetCultureInfo());
//                }
//            }

//            return builder.ToString();
//        }
//    }


//    /// <summary>
//    /// 多线程上下文信息
//    /// </summary>
//    public static class CultureInfoExtend
//    {
//        /// <summary>
//        /// 获取线程信息
//        /// </summary>
//        public static string GetCultureInfo(this CultureInfo culture)
//        {
//            StringBuilder builder = new StringBuilder();
//            builder.Append("Name : ").AppendLine(culture.Name.ToStringDetial());
//            builder.Append("DisplayName : ").AppendLine(culture.DisplayName.ToStringDetial());
//            builder.Append("EnglishName : ").AppendLine(culture.EnglishName.ToStringDetial());
//            builder.Append("NativeName : ").AppendLine(culture.NativeName.ToStringDetial());
//            builder.Append("ThreeLetterISOLanguageName : ").AppendLine(culture.ThreeLetterISOLanguageName.ToStringDetial());
//            builder.Append("ThreeLetterWindowsLanguageName : ").AppendLine(culture.ThreeLetterWindowsLanguageName.ToStringDetial());
//            builder.Append("TwoLetterISOLanguageName : ").AppendLine(culture.TwoLetterISOLanguageName.ToStringDetial());
//            builder.Append("CultureTypes : ").AppendLine(culture.CultureTypes.ToStringDetial());
//            builder.Append("IetfLanguageTag : ").AppendLine(culture.IetfLanguageTag.ToStringDetial());
//            builder.Append("IsNeutralCulture : ").AppendLine(culture.IsNeutralCulture.ToStringDetial());
//            builder.Append("IsReadOnly : ").AppendLine(culture.IsReadOnly.ToStringDetial());
//            builder.Append("KeyboardLayoutId : ").AppendLine(culture.KeyboardLayoutId.ToStringDetial());
//            builder.Append("LCID : ").AppendLine(culture.LCID.ToStringDetial());
//            builder.Append("TextInfo : ").AppendLine(culture.TextInfo.ToStringDetial());

//            return builder.ToString();
//        }

//    }

//    /// <summary>
//    ///
//    /// </summary>
//    public static class TextInfoExtend
//    {
//        /// <summary>
//        /// 获取线程信息
//        /// </summary>
//        public static string GetCultureInfo(this TextInfo info)
//        {
//            StringBuilder builder = new StringBuilder();


//            return builder.ToString();
//        }
//    }
//}

