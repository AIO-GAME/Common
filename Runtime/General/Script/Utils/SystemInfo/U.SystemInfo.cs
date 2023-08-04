using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace AIO
{
    public static partial class UtilsEngine
    {
        /// <summary>
        /// 设备信息 Unity API 2019_or_new
        /// </summary>
        public static partial class SystemInfo
        {
#if UNITY_2019_1_OR_NEWER
            /// <summary>
            /// 是否支持镶嵌着色器
            /// Are tessellation shaders supported
            /// </summary>
            public static bool IsSupportsTessellationShaders()
            {
                return UnityEngine.SystemInfo.supportsTessellationShaders;
            }

            /// <summary>
            /// 检查当前配置是否支持射线跟踪。
            /// Checks if ray tracing is supported by the current configuration.
            /// </summary>
            public static bool IsSupportsRayTracing()
            {
                return UnityEngine.SystemInfo.supportsRayTracing;
            }

            /// <summary>
            /// 是否 Mip达到最高等级
            /// </summary>
            public static bool IsMipMaxLevel()
            {
                return UnityEngine.SystemInfo.hasMipMaxLevel;
            }

            /// <summary>
            /// 确定Unity在一个计算着色器中同时支持多少计算缓冲读取
            /// </summary>
            public static int MaxComputeBufferInputsCompute()
            {
                return UnityEngine.SystemInfo.maxComputeBufferInputsCompute;
            }

            /// <summary>
            /// 确定Unity在一个域着色器中同时支持多少计算缓冲读取
            /// </summary>
            public static int MaxComputeBufferInputsDomain()
            {
                return UnityEngine.SystemInfo.maxComputeBufferInputsDomain;
            }

            /// <summary>
            /// 确定Unity在碎片着色器中同时支持多少计算缓冲读取
            /// </summary>
            public static int MaxComputeBufferInputsFragment()
            {
                return UnityEngine.SystemInfo.maxComputeBufferInputsFragment;
            }

            /// <summary>
            /// 确定Unity在一个几何着色器中同时支持多少计算缓冲读取。
            /// </summary>
            public static int MaxComputeBufferInputsGeometry()
            {
                return UnityEngine.SystemInfo.maxComputeBufferInputsGeometry;
            }

            /// <summary>
            /// 确定Unity在一个赫尔着色器中同时支持多少计算缓冲读取。
            /// </summary>
            public static int MaxComputeBufferInputsHull()
            {
                return UnityEngine.SystemInfo.maxComputeBufferInputsHull;
            }

            /// <summary>
            /// 确定Unity在一个顶点着色器中同时支持多少计算缓冲读取。
            /// </summary>
            public static int MaxComputeBufferInputsVertex()
            {
                return UnityEngine.SystemInfo.maxComputeBufferInputsVertex;
            }

            /// <summary>
            /// 在单个本地工作组中可以分派到计算着色器的最大调用总数
            /// </summary>
            public static int MaxComputeWorkGroupSize()
            {
                return UnityEngine.SystemInfo.maxComputeWorkGroupSize;
            }

            /// <summary>
            /// 在X维度中，计算着色器可以使用的最大工作组数
            /// </summary>
            public static int MaxComputeWorkGroupSizeX()
            {
                return UnityEngine.SystemInfo.maxComputeWorkGroupSizeX;
            }

            /// <summary>
            /// 在Y维度中，计算着色器可以使用的最大工作组数
            /// </summary>
            public static int MaxComputeWorkGroupSizeY()
            {
                return UnityEngine.SystemInfo.maxComputeWorkGroupSizeY;
            }

            /// <summary>
            /// 在Z维度中，计算着色器可以使用的最大工作组数
            /// </summary>
            public static int MaxComputeWorkGroupSizeZ()
            {
                return UnityEngine.SystemInfo.maxComputeWorkGroupSizeZ;
            }
#endif

            /// <summary>
            /// 最大立方地图纹理大小
            /// </summary>
            public static int MaxCubemapSize()
            {
                return UnityEngine.SystemInfo.maxCubemapSize;
            }

            /// <summary>
            /// 最大的纹理尺寸
            /// </summary>
            public static int MaxTextureSize()
            {
                return UnityEngine.SystemInfo.maxTextureSize;
            }

#if UNITY_2020_1_OR_NEWER
            /// <summary>
            /// 是否 使用Shader绑定常量缓冲区时的最小缓冲区偏移量(以字节为单位)。 SetConstantBuffer或Material.SetConstantBuffer。
            /// </summary>
            public static bool IsMinConstantBufferOffsetAlignment()
            {
                return UnityEngine.SystemInfo.constantBufferOffsetAlignment == 0;
            }
#endif

#if UNITY_2019_1_OR_NEWER
            /// <summary>
            /// 指示此设备是否支持给定的顶点属性格式和维度组合。
            /// Indicates whether the given combination of a vertex attribute format and dimension is supported on this device.
            /// </summary>
            public static bool IsSupportsVertexAttributeFormat(VertexAttributeFormat format, int dimension)
            {
                return UnityEngine.SystemInfo.SupportsVertexAttributeFormat(format, dimension);
            }

            /// <summary>
            /// 如果图形API考虑了RenderBufferLoadAction和RenderBufferStoreAction，则为True，否则为false。
            /// True if the Graphics API takes RenderBufferLoadAction and RenderBufferStoreAction into account, false if otherwise.
            /// </summary>
            public static bool IsUsesLoadStoreActions()
            {
                return UnityEngine.SystemInfo.usesLoadStoreActions;
            }

            /// <summary>
            /// 获取兼容的图像格式
            /// </summary>
            public static GraphicsFormat GetCompatibleFormat(GraphicsFormat format, FormatUsage usage)
            {
                return UnityEngine.SystemInfo.GetCompatibleFormat(format, usage);
            }

            /// <summary>
            /// 获取图形格式
            /// </summary>
            public static GraphicsFormat GetGraphicsFormat(DefaultFormat format)
            {
                return UnityEngine.SystemInfo.GetGraphicsFormat(format);
            }
#endif

            /// <summary>
            /// 是否 支持指定格式
            /// </summary>
            public static bool IsFormatSupported(GraphicsFormat format, FormatUsage usage)
            {
                return UnityEngine.SystemInfo.IsFormatSupported(format, usage);
            }

#if UNITY_2019_1_OR_NEWER
            /// <summary>
            /// 应用程序的实际渲染线程模式
            /// Application's actual rendering threading mode
            /// </summary>
            public static RenderingThreadingMode GetRenderingThreadingMode()
            {
                return UnityEngine.SystemInfo.renderingThreadingMode;
            }

            /// <summary>
            /// Unity同时支持的随机写目标(UAV)的最大数量。
            /// The maximum number of random write targets (UAV) that Unity supports simultaneously.
            /// </summary>
            public static int GetSupportedRandomWriteTargetCount()
            {
                return UnityEngine.SystemInfo.supportedRandomWriteTargetCount;
            }

            /// <summary>
            /// 是否支持几何着色
            /// Are geometry shaders supported
            /// </summary>
            public static bool IsSupportsGeometryShaders()
            {
                return UnityEngine.SystemInfo.supportsGeometryShaders;
            }

            /// <summary>
            /// 当平台支持GraphicsFences时返回true，否则返回false。
            /// Returns true when the platform supports GraphicsFences, and false if otherwise.
            /// </summary>
            public static bool IsSupportsGraphicsFence()
            {
                return UnityEngine.SystemInfo.supportsGraphicsFence;
            }
#endif
        }
    }

    public static partial class UtilsEngine
    {
        /// <summary>
        /// 设备信息 Unity API
        /// </summary>
        public static partial class SystemInfo
        {

#if UNITY_2019_1_OR_NEWER
            /// <summary>
            /// 当前渲染器是否直接支持绑定常量缓冲区
            /// Does the current renderer support binding constant buffers directly
            /// </summary>
            public static bool IsSupportsSetConstantBuffer()
            {
                return UnityEngine.SystemInfo.supportsSetConstantBuffer;
            }
#endif

            /// <summary>
            /// SystemInfo字符串属性返回的值，该属性在当前平台上不受支持。
            /// Value returned by UnityEngine. SystemInfo string properties which are not supported on the current platform.
            /// </summary>
            public const string UnsupportedIdentifier = UnityEngine.SystemInfo.unsupportedIdentifier;

            /// <summary>
            /// 获取设备电池状态
            /// </summary>
            /// <!--
            /// Unknown     : 如果电池状态不可用 无法确定设备的电池状态。
            /// Charging    : 设备已插入并正在充电。
            /// Discharging : 设备被拔出并正在放电。
            /// NotCharging : 设备已插入，但未充电。
            /// Full        : 设备已插入，电池已满。
            /// -->
            public static BatteryStatus GetBatteryStatus()
            {
                return UnityEngine.SystemInfo.batteryStatus;
            }

            /// <summary>
            /// 支持多种复制纹理功能的情况。
            /// </summary>
            /// <!--
            /// None           : No support for Graphics.CopyTexture.                                    : 不支持Graphics.CopyTexture。
            /// Basic          : Basic Graphics.CopyTexture support.                                    : 基本图形CopyTexture支持
            /// Copy3D         : Support for Texture3D in Graphics.CopyTexture.                         : 在Graphics.CopyTexture中支持Texture3D
            /// DifferentTypes : Support for Graphics.CopyTexture between different texture types.      : 支持图形 不同纹理类型之间的CopyTexture
            /// TextureToRT    : Support for Texture to RenderTexture copies in Graphics.CopyTexture.   : 在Graphics.CopyTexture中支持纹理到渲染纹理副本。
            /// RTToTexture    : Support for RenderTexture to Texture copies in Graphics.CopyTexture.   : 在Graphics.CopyTexture中支持渲染纹理到纹理拷贝。
            /// -->
            public static CopyTextureSupport GetTextureSupport()
            {
                return UnityEngine.SystemInfo.copyTextureSupport;
            }

            /// <summary>
            /// 获取设备模型
            /// </summary>
            public static string GetDeviceModel()
            {
                return UnityEngine.SystemInfo.deviceModel;
            }

            /// <summary>
            /// 获取用户设备自定义名称
            /// </summary>
            public static string GetDeviceName()
            {
                return UnityEngine.SystemInfo.deviceName;
            }

            /// <summary>
            /// 返回程序运行所在的设备类型 PC电脑、掌上型等
            /// </summary>
            /// <!--
            ///  Unknown = 0  : Device type is unknown.You should never see this in practice. : 设备类型未知。 你不应该在实践中看到这种情况
            ///  Handheld = 1 : A handheld device like mobile phone or a tablet.              : 像移动电话或平板电脑这样的手持设备。
            ///  Console = 2  : A stationary gaming console.                                  : 一个固定的游戏机。
            ///  Desktop = 3  : Desktop or laptop computer.                                   : 台式或笔记本电脑。
            /// -->
            public static DeviceType GetDeviceType()
            {
                return UnityEngine.SystemInfo.deviceType;
            }

            /// <summary>
            /// 设备的唯一标识符。每一台设备都有唯一的标识符。
            /// </summary>
            public static string GetDeviceUniqueIdentifier()
            {
                return UnityEngine.SystemInfo.deviceUniqueIdentifier;
            }

            #region Graphics Device

            /// <summary>
            /// 获取图形设备ID 显卡的唯一标识符ID。
            /// </summary>
            public static int GetGraphicsDeviceID()
            {
                return UnityEngine.SystemInfo.graphicsDeviceID;
            }

            /// <summary>
            /// 显卡的名称
            /// </summary>
            public static string GetGraphicsDeviceName()
            {
                return UnityEngine.SystemInfo.graphicsDeviceName;
            }

            /// <summary>
            /// 图形设备类型 显卡的类型
            /// </summary>
            public static GraphicsDeviceType GetGraphicsDeviceType()
            {
                return UnityEngine.SystemInfo.graphicsDeviceType;
            }

            /// <summary>
            /// 图形设备供应商 显卡的供应商
            /// </summary>
            public static string GetGraphicsDeviceVendor()
            {
                return UnityEngine.SystemInfo.graphicsDeviceVendor;
            }

            /// <summary>
            /// 设备厂商ID 显卡供应商的唯一识别码ID
            /// </summary>
            public static int GetGraphicsDeviceVendorID()
            {
                return UnityEngine.SystemInfo.graphicsDeviceVendorID;
            }

            /// <summary>
            /// 图形设备版本 显卡的类型和版本
            /// </summary>
            public static string GetGraphicsDeviceVersion()
            {
                return UnityEngine.SystemInfo.graphicsDeviceVersion;
            }

            /// <summary>
            /// 图形内存大小 显存大小
            /// </summary>
            public static int GetGraphicsMemorySize()
            {
                return UnityEngine.SystemInfo.graphicsMemorySize;
            }

            /// <summary>
            /// 是否支持多线程渲染
            /// </summary>
            public static bool IsGraphicsMultiThreaded()
            {
                return UnityEngine.SystemInfo.graphicsMultiThreaded;
            }

            /// <summary>
            /// 图形着色器水平 显卡着色器的级别。
            /// </summary>
            public static int GetGraphicsShaderLevel()
            {
                return UnityEngine.SystemInfo.graphicsShaderLevel;
            }

            /// <summary>
            /// 图形UV是否从顶部开始
            /// </summary>
            public static bool IsGraphicsUVStartsAtTop()
            {
                return UnityEngine.SystemInfo.graphicsUVStartsAtTop;
            }

            /// <summary>
            /// 是否 在碎片着色器中 有动态统一数组索引
            /// </summary>
            public static bool IsDynamicUniformArrayIndexingInFragmentShaders()
            {
                return UnityEngine.SystemInfo.hasDynamicUniformArrayIndexingInFragmentShaders;
            }

            /// <summary>
            /// 是否 GPU上有隐藏表面去除
            /// </summary>
            public static bool IsHiddenSurfaceRemovalOnGPU()
            {
                return UnityEngine.SystemInfo.hasHiddenSurfaceRemovalOnGPU;
            }

            /// <summary>
            /// GPU提供了什么NPOT(两种尺寸的非电源)纹理支持
            /// </summary>
            public static NPOTSupport GetNpotSupport()
            {
                return UnityEngine.SystemInfo.npotSupport;
            }

            #endregion

            /// <summary>
            /// 返回运行游戏的操作系统家族
            /// </summary>
            public static string GetOperatingSystem()
            {
                return UnityEngine.SystemInfo.operatingSystem;
            }

            /// <summary>
            /// 返回运行游戏的操作系统家族
            /// </summary>
            public static OperatingSystemFamily GetOperatingSystemFamily()
            {
                return UnityEngine.SystemInfo.operatingSystemFamily;
            }

            /// <summary>
            /// 处理器存在的数量
            /// </summary>
            public static int GetProcessorCount()
            {
                return UnityEngine.SystemInfo.processorCount;
            }

            /// <summary>
            /// 处理器频率(MHz)
            /// </summary>
            public static int GetProcessorFrequency()
            {
                return UnityEngine.SystemInfo.processorFrequency;
            }

            /// <summary>
            /// 处理器的名字
            /// </summary>
            public static string GetProcessorType()
            {
                return UnityEngine.SystemInfo.processorType;
            }

            /// <summary>
            /// 返回运行游戏的操作系统系列
            /// </summary>
            public static OperatingSystemFamily OperatingSystemFamily()
            {
                return UnityEngine.SystemInfo.operatingSystemFamily;
            }

            /// <summary>
            /// 支持多少同时呈现目标(mrt)
            /// How many simultaneous render targets (MRTs) are supported
            /// </summary>
            public static float GetSupportedRenderTargetCount()
            {
                return UnityEngine.SystemInfo.supportedRenderTargetCount;
            }

            /// <summary>
            /// 是否支持2D数组纹理
            /// Are 2D Array textures supported
            /// </summary>
            public static bool IsSupports2DArrayTextures()
            {
                return UnityEngine.SystemInfo.supports2DArrayTextures;
            }

            /// <summary>
            /// 是否支持32位索引缓冲区
            /// Are 32-bit index buffers supported
            /// </summary>
            public static bool IsSupports32bitsIndexBuffer()
            {
                return UnityEngine.SystemInfo.supports32bitsIndexBuffer;
            }

            /// <summary>
            /// 是否支持3D(体积)渲染纹理
            /// Are 3D (volume) RenderTextures supported
            /// </summary>
            public static bool IsSupports3DRenderTextures()
            {
                return UnityEngine.SystemInfo.supports3DRenderTextures;
            }

            /// <summary>
            /// 是否支持3D(体积)纹理
            /// Are 3D (volume) textures supported
            /// </summary>
            public static bool IsSupports3DTextures()
            {
                return UnityEngine.SystemInfo.supports3DTextures;
            }

            /// <summary>
            /// 是否设备上有加速度计
            /// Is an accelerometer available on the device
            /// </summary>
            public static bool IsSupportsAccelerometer()
            {
                return UnityEngine.SystemInfo.supportsAccelerometer;
            }

            /// <summary>
            /// 当平台支持异步计算队列时返回true，否则返回false。
            /// Returns true when the platform supports asynchronous compute queues and false if otherwise.
            /// </summary>
            public static bool IsSupportsAsyncCompute()
            {
                return UnityEngine.SystemInfo.supportsAsyncCompute;
            }

            /// <summary>
            /// 如果此设备可以异步读回GPU数据，则返回true，否则返回false。
            /// Returns true if asynchronous readback of GPU data is available for this device and false otherwise.
            /// </summary>
            public static bool IsSupportsAsyncGPUReadback()
            {
                return UnityEngine.SystemInfo.supportsAsyncGPUReadback;
            }

            /// <summary>
            /// 是否有音频设备可用于回放
            /// Is there an Audio device available for playback
            /// </summary>
            public static bool IsSupportsAudio()
            {
                return UnityEngine.SystemInfo.supportsAudio;
            }

            /// <summary>
            /// 渲染纹理格式是否支持混合
            /// Is blending supported on render texture format
            /// </summary>
            public static bool IsSupportsBlendingOnRenderTextureFormat(RenderTextureFormat format)
            {
                return UnityEngine.SystemInfo.SupportsBlendingOnRenderTextureFormat(format);
            }

            /// <summary>
            /// 是否支持计算着色器
            /// Are compute shaders supported
            /// </summary>
            public static bool IsSupportsComputeShaders()
            {
                return UnityEngine.SystemInfo.supportsComputeShaders;
            }

            /// <summary>
            /// 是否支持Cubemap数组纹理
            /// Are Cubemap Array textures supported
            /// </summary>
            public static bool IsSupportsCubemapArrayTextures()
            {
                return UnityEngine.SystemInfo.supportsCubemapArrayTextures;
            }

            /// <summary>
            /// 是否设备上有陀螺仪
            /// Is a gyroscope available on the device
            /// </summary>
            public static bool IsSupportsGyroscope()
            {
                return UnityEngine.SystemInfo.supportsGyroscope;
            }

            /// <summary>
            /// 是否硬件是否支持四元拓扑
            /// Does the hardware support quad topology
            /// </summary>
            public static bool IsSupportsHardwareQuadTopology()
            {
                return UnityEngine.SystemInfo.supportsHardwareQuadTopology;
            }

            /// <summary>
            /// 是否支持GPU绘制调用实例化
            /// Is GPU draw call instancing supported
            /// </summary>
            public static bool IsSupportsInstancing()
            {
                return UnityEngine.SystemInfo.supportsInstancing;
            }

            /// <summary>
            /// 设备是否能够报告其位置
            /// Is the device capable of reporting its location
            /// </summary>
            public static bool IsSupportsLocationService()
            {
                return UnityEngine.SystemInfo.supportsLocationService;
            }

            /// <summary>
            /// 是否支持纹理mip贴图流
            /// Is streaming of texture mip maps supported
            /// </summary>
            public static bool IsSupportsMipStreaming()
            {
                return UnityEngine.SystemInfo.supportsMipStreaming;
            }

            /// <summary>
            /// 该平台是否支持运动向量。
            /// Whether motion vectors are supported on this platform.
            /// </summary>
            public static bool IsSupportsMotionVectors()
            {
                return UnityEngine.SystemInfo.supportsMotionVectors;
            }

            /// <summary>
            /// 如果多重采样纹理被自动解析，则返回true
            /// Returns true if multisampled textures are resolved automatically
            /// </summary>
            public static bool IsSupportsMultisampleAutoResolve()
            {
                return UnityEngine.SystemInfo.supportsMultisampleAutoResolve;
            }

            /// <summary>
            /// 是否支持多采样纹理
            /// Are multisampled textures supported
            /// </summary>
            public static int GetSsupportsMultisampledTextures()
            {
                return UnityEngine.SystemInfo.supportsMultisampledTextures;
            }

            /// <summary>
            /// 是否支持从阴影贴图中采样原始深度
            /// Is sampling raw depth from shadowmaps supported
            /// </summary>
            public static bool IsSupportsRawShadowDepthSampling()
            {
                return UnityEngine.SystemInfo.supportsRawShadowDepthSampling;
            }

            /// <summary>
            /// 是否支持渲染纹理格式
            /// Is render texture format supported
            /// </summary>
            public static bool IsSupportsRenderTextureFormat(RenderTextureFormat format)
            {
                return UnityEngine.SystemInfo.SupportsRenderTextureFormat(format);
            }

            /// <summary>
            /// 当平台在渲染多个渲染目标时支持不同的混合模式时返回true，否则返回false。
            /// Returns true when the platform supports different blend modes when rendering to multiple render targets, or false otherwise.
            /// </summary>
            public static bool IsSupportsSeparatedRenderTargetsBlend()
            {
                return UnityEngine.SystemInfo.supportsSeparatedRenderTargetsBlend;
            }

            /// <summary>
            /// 是否支持内置阴影
            /// Are built-in shadows supported
            /// </summary>
            public static bool IsSupportsShadows()
            {
                return UnityEngine.SystemInfo.supportsShadows;
            }

            /// <summary>
            /// 是否支持稀疏纹理
            /// Are sparse textures supported
            /// </summary>
            public static bool IsSupportsSparseTextures()
            {
                return UnityEngine.SystemInfo.supportsSparseTextures;
            }

            /// <summary>
            /// 这个设备是否支持纹理格式
            /// Is texture format supported on this device
            /// </summary>
            public static bool IsSupportsTextureFormat(TextureFormat format)
            {
                return UnityEngine.SystemInfo.SupportsTextureFormat(format);
            }

            /// <summary>
            /// 如果支持“Mirror Once”纹理缠绕模式，返回true
            /// Returns true if the 'Mirror Once' texture wrap mode is supported
            /// </summary>
            public static int GetSupportsTextureWrapMirrorOnce()
            {
                return UnityEngine.SystemInfo.supportsTextureWrapMirrorOnce;
            }

            /// <summary>
            /// 设备是否能够通过振动向用户提供触觉反馈
            /// Is the device capable of providing the user haptic feedback by vibration
            /// </summary>
            public static bool IsSupportsVibration()
            {
                return UnityEngine.SystemInfo.supportsVibration;
            }

            /// <summary>
            /// 当前系统内存的数量
            /// Amount of system memory present
            /// </summary>
            public static int GetSystemMemorySize()
            {
                return UnityEngine.SystemInfo.systemMemorySize;
            }

            /// <summary>
            /// 如果当前平台使用了反向深度缓冲区(近平面的值范围为1，远平面的值范围为0)，则该属性为true，如果深度缓冲区为normal(0为near, 1为far)，则为false。
            /// This property is true if the current platform uses a reversed depth buffer (where values range from 1 at the near plane and 0 at far plane), and false if the depth buffer is normal (0 is near, 1 is far).
            /// </summary>
            public static bool IsUsesReversedZBuffer()
            {
                return UnityEngine.SystemInfo.usesReversedZBuffer;
            }
        }
    }
}
