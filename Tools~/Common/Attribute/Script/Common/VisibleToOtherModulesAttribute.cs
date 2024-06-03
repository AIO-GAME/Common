//namespace AIO
//{
//    using System;
//    using System.Data.SqlTypes;
//    using System.Diagnostics;
//    using System.Linq;
//    using System.Runtime.ExceptionServices;
//    using System.Threading;
//    using System.Threading.Tasks;

//    /// <summary>
//    /// 
//    /// </summary>
//    [AttributeUsage(AttributeTargets.Class
//        | AttributeTargets.Struct
//        | AttributeTargets.Enum
//        | AttributeTargets.Constructor
//        | AttributeTargets.Method
//        | AttributeTargets.Property
//        | AttributeTargets.Field
//        | AttributeTargets.Interface
//        | AttributeTargets.Delegate
//         , Inherited = false
//        )]
//    [VisibleToOtherModules]
//    internal class VisibleToOtherModulesAttribute : Attribute
//    {
//        private string[] modules;


//        /// <summary>
//        /// 构造函数
//        /// </summary>
//        public VisibleToOtherModulesAttribute()
//        {
//            //Environment.FailFast("VisibleToOtherModulesAttribute");
//            //Debug.WriteLineIf(true, "VisibleToOtherModulesAttribute");
//            //Debug.Indent();
//            //Parallel.For(0, 10, (i) =>
//            //{
//            //    Debug.WriteLineIf(true, i);
//            //});
//            //int[] nums = Enumerable.Range(0, 1000000).ToArray();
//            //long total = 0;

//            //Parallel.For<long>(0, nums.Length, () => 0, (j, loop, subtotal) =>
//            //{
//            //    subtotal += nums[j];
//            //    return subtotal;
//            //}, (x) => Interlocked.Add(ref total, x)
//            //);

//        }

//        //WeakReference
//        //public VisibleToOtherModulesAttribute(Exception ex)
//        //{
//        //    var possibleexception = ExceptionDispatchInfo.Capture(ex);
//        //    possibleexception.Throw();
//        //}

//        //__reftype
//        //__refvalue
//        //__refanytype
//        //__refanyval
//        //__arglist
//        //__makeref
//        public VisibleToOtherModulesAttribute(__arglist)
//        {
//            var b = __refvalue;
//            //var ai = new ArgIterator(__arglist);
//            //while (ai.GetRemainingCount() > 0)
//            //{
//            //    TypedReference tr = ai.GetNextArg();
//            //    Console.WriteLine(TypedReference.ToObject(tr));
//            //}
//        }

//        /// <summary>
//        /// 构造函数
//        /// </summary>
//        /// <param name="modules">模块</param>
//        public VisibleToOtherModulesAttribute(params string[] modules)
//        {
//            modules = modules ?? new string[0];
//        }
//    }
//}

