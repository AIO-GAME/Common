/*=================================================================================================|*|
↓  Copyright(C) 2022 by DefaultCompany            |*| ╭╩╮╮╮╔════╗╔═══════╗╔════╗╔═══════╗╔═══════╗  ↩
↓  All Rights Reserved By Author lihongliu.       |*|╭╯L ╭╠╣ No ╠╣ Pains ╠╣ No ╠╣ Gains ╠╣ XNSKY ╟  ↩
↓  Author:      |*| XiNan                         |*|╰◎═◎╯╯╚◎══◎╝╚◎═════◎╝╚◎══◎╝╚◎═════◎╝╚◎═════◎╝  ↩
↓  Email:       |*| 1398581458@qq.com                                                               ↩
↓  Version:     |*| 1.0                           |*| ╭╩╮╮╮╔════╗╔═══╗╔═══╗╔═══════╗╔════╗╔══════╗  ↩
↓  UnityVersion:|*| 2021.2.13f1c1                 |*|╭╯H ╭╠╣Only╠╣You╠╣Can╠╣Cantrol╠╣Your╠╣Future╟  ↩
↓  Date:        |*| 2022-03-08                    |*|╰◎═◎╯╯╚◎══◎╝╚◎═◎╝╚◎═◎╝╚◎═════◎╝╚◎══◎╝╚◎════◎╝  ↩
↓  URL:         |*| www.XiNansky.com                                                                ↩
↓  Nowtime:     |*| 15:06:55                      |*| ╭╩╮╮╮╔═════╗╔════╗╔══════╗╔═══╗╔══════╗╔═══╗  ↩
↓  Description: |*| |U_U|                         |*|╭╯L ╭╠╣There╠╣ Is ╠╣Always╠╣ A ╠╣Better╠╣Way╟  ↩
↓  History:     |*| |>"<|                         |*|╰◎═◎╯╯╚◎═══◎╝╚◎══◎╝╚◎════◎╝╚◎═◎╝╚◎════◎╝╚◎═◎╝  ↩
↓===================================================================================================*/

//      编辑器的相关特性

//      %	                                        Ctr/Command
//      #	                                        Shift
//      &	                                        Alt
//      LEFT/Right/UP/DOWN                          方向键
//      F1-F2                                       F功能键
//      _g                                          字母g

//      常用的属性特性
//      [Range(0,100)]                              //限制数值范围
//      [Multiline(3)]                              //字符串多行显示
//      [TextArea(2,4)]                             //文本输入框
//      [SerializeField]                            //序列化字段，主要用于序列化私有字段
//      [NonSerialized]                             //反序列化一个变量，并且在Inspector上隐藏
//      [HideInInspector]                           //public变量在Inspector面板隐藏
//      [FormerlySerializedAs(“Value1”)]            //当变量名发生改变时，可以保存原来Value1的值
//      [ContextMenu(“TestBtn”)]                    //组件右键菜单按钮
//      [ContextMenuItem(“Reset Value”,“Reset”)]    //定义属性的右键菜单
//      [Header(“Header Name”)]                     //加粗效果的标题
//      [Space(10)]                                 //表示间隔空间，数字越大，间隔越大
//      [Tooltip(“Tips”)]                           //显示字段的提示信息
//      [ColorUsage(true)]                          //显示颜色面板

//      常用的方法特性
//      [DrawGizmo]                                 //用于Gizmos渲染，将逻辑与调试代码分离
//      [MenuItem]                                  //添加菜单项

//      常用的类的特性
//      [Serializable]                              //序列化一个类，作为一个子属性显示在监视面板
//      [RequireComponent(typeof(Animator))]        //挂载该类的对象，必须要有Animator组件
//      [DisallowMultipleComponent]                 //不允许挂载多个该类或其子类
//      [ExecuteInEditMode]                         //允许脚本在编辑器未运行的情况下运行
//      [CanEditMultipleObjects]                    //允许当选择多个挂有该脚本的对象时，统一修改值
//      [AddComponentMenu]                          //可以在菜单栏Component内添加组件按钮
//      [CustomEditor]                              //要自定义编辑器就要加这个特性
//      [CustomPropertyDrawer]                      //用于绘制自定义PropertyDrawer的特性
//      [SelectionBase]                             //选择在场景视图中使用此属性的组件对象，即不会误选中子物体
