using System.ComponentModel;
using System;

namespace AIO
{
    /// <summary>
    ///   <para>The platform application is running. Returned by Application.platform.</para>
    /// </summary>
    public enum ERuntimePlatform
    {
        [Obsolete("GameCoreScarlett is deprecated, please use GameCoreXboxSeries (UnityUpgradable) -> GameCoreXboxSeries", false)] GameCoreScarlett = -1, // 0xFFFFFFFF
        /// <summary>
        ///   <para>In the Unity editor on macOS.</para>
        /// </summary>
        OSXEditor = 0,
        /// <summary>
        ///   <para>In the player on macOS.</para>
        /// </summary>
        OSXPlayer = 1,
        /// <summary>
        ///   <para>In the player on Windows.</para>
        /// </summary>
        WindowsPlayer = 2,
        /// <summary>
        ///   <para>In the web player on macOS.</para>
        /// </summary>
        [Obsolete("WebPlayer export is no longer supported in Unity 5.4+.", true)] OSXWebPlayer = 3,
        /// <summary>
        ///   <para>In the Dashboard widget on macOS.</para>
        /// </summary>
        [Obsolete("Dashboard widget on Mac OS X export is no longer supported in Unity 5.4+.", true)] OSXDashboardPlayer = 4,
        /// <summary>
        ///   <para>In the web player on Windows.</para>
        /// </summary>
        [Obsolete("WebPlayer export is no longer supported in Unity 5.4+.", true)] WindowsWebPlayer = 5,
        /// <summary>
        ///   <para>In the Unity editor on Windows.</para>
        /// </summary>
        WindowsEditor = 7,
        /// <summary>
        ///   <para>In the player on the iPhone.</para>
        /// </summary>
        IPhonePlayer = 8,
        [Obsolete("PS3 export is no longer supported in Unity >=5.5.")] PS3 = 9,
        [Obsolete("Xbox360 export is no longer supported in Unity 5.5+.")] XBOX360 = 10, // 0x0000000A
        /// <summary>
        ///   <para>In the player on Android devices.</para>
        /// </summary>
        Android = 11, // 0x0000000B
        [Obsolete("NaCl export is no longer supported in Unity 5.0+.")] NaCl = 12, // 0x0000000C
        /// <summary>
        ///   <para>In the player on Linux.</para>
        /// </summary>
        LinuxPlayer = 13, // 0x0000000D
        [Obsolete("FlashPlayer export is no longer supported in Unity 5.0+.")] FlashPlayer = 15, // 0x0000000F
        /// <summary>
        ///   <para>In the Unity editor on Linux.</para>
        /// </summary>
        LinuxEditor = 16, // 0x00000010
        /// <summary>
        ///   <para>In the player on WebGL</para>
        /// </summary>
        WebGLPlayer = 17, // 0x00000011
        [Obsolete("Use WSAPlayerX86 instead")] MetroPlayerX86 = 18, // 0x00000012
        /// <summary>
        ///   <para>In the player on Windows Store Apps when CPU architecture is X86.</para>
        /// </summary>
        WSAPlayerX86 = 18, // 0x00000012
        [Obsolete("Use WSAPlayerX64 instead")] MetroPlayerX64 = 19, // 0x00000013
        /// <summary>
        ///   <para>In the player on Windows Store Apps when CPU architecture is X64.</para>
        /// </summary>
        WSAPlayerX64 = 19, // 0x00000013
        [Obsolete("Use WSAPlayerARM instead")] MetroPlayerARM = 20, // 0x00000014
        /// <summary>
        ///   <para>In the player on Windows Store Apps when CPU architecture is ARM.</para>
        /// </summary>
        WSAPlayerARM = 20, // 0x00000014
        [Obsolete("Windows Phone 8 was removed in 5.3")] WP8Player = 21, // 0x00000015
        [Obsolete("BB10Player export is no longer supported in Unity 5.4+."), EditorBrowsable(EditorBrowsableState.Never)] BB10Player = 22, // 0x00000016
        [Obsolete("BlackBerryPlayer export is no longer supported in Unity 5.4+.")] BlackBerryPlayer = 22, // 0x00000016
        [Obsolete("TizenPlayer export is no longer supported in Unity 2017.3+.")] TizenPlayer = 23, // 0x00000017
        [Obsolete("PSP2 is no longer supported as of Unity 2018.3")] PSP2 = 24, // 0x00000018
        /// <summary>
        ///   <para>In the player on the Playstation 4.</para>
        /// </summary>
        PS4 = 25, // 0x00000019
        [Obsolete("PSM export is no longer supported in Unity >= 5.3")] PSM = 26, // 0x0000001A
        /// <summary>
        ///   <para>In the player on Xbox One.</para>
        /// </summary>
        XboxOne = 27, // 0x0000001B
        [Obsolete("SamsungTVPlayer export is no longer supported in Unity 2017.3+.")] SamsungTVPlayer = 28, // 0x0000001C
        [Obsolete("Wii U is no longer supported in Unity 2018.1+.")] WiiU = 30, // 0x0000001E
        /// <summary>
        ///   <para>In the player on the Apple's tvOS.</para>
        /// </summary>
        tvOS = 31, // 0x0000001F
        /// <summary>
        ///   <para>In the player on Nintendo Switch.</para>
        /// </summary>
        Switch = 32, // 0x00000020
        Lumin = 33, // 0x00000021
        /// <summary>
        ///   <para>In the player on Stadia.</para>
        /// </summary>
        Stadia = 34, // 0x00000022
        /// <summary>
        ///   <para>In the player on CloudRendering.</para>
        /// </summary>
        CloudRendering = 35, // 0x00000023
        GameCoreXboxSeries = 36, // 0x00000024
        GameCoreXboxOne = 37, // 0x00000025
        /// <summary>
        ///   <para>In the player on the Playstation 5.</para>
        /// </summary>
        PS5 = 38, // 0x00000026
    }
}
