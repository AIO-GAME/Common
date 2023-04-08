using System.ComponentModel;
using System;

namespace AIO
{
    /// <summary>
    /// The platform application is running. Returned by Application.platform.
    /// </summary>
    public enum ERuntimePlatform
    {
        /// <summary>
        ///  In the Unity editor on GameCoreScarlett.
        /// </summary>
        [Obsolete("GameCoreScarlett is deprecated, please use GameCoreXboxSeries (UnityUpgradable) -> GameCoreXboxSeries", false)]
        GameCoreScarlett = -1, // 0xFFFFFFFF

        /// <summary>
        /// In the Unity editor on macOS.
        /// </summary>
        OSXEditor = 0,

        /// <summary>
        /// In the player on macOS.
        /// </summary>
        OSXPlayer = 1,

        /// <summary>
        /// In the player on Windows.
        /// </summary>
        WindowsPlayer = 2,

        /// <summary>
        /// In the web player on macOS.
        /// </summary>
        [Obsolete("WebPlayer export is no longer supported in Unity 5.4+.", true)]
        OSXWebPlayer = 3,

        /// <summary>
        /// In the Dashboard widget on macOS.
        /// </summary>
        [Obsolete("Dashboard widget on Mac OS X export is no longer supported in Unity 5.4+.", true)]
        OSXDashboardPlayer = 4,

        /// <summary>
        /// In the web player on Windows.
        /// </summary>
        [Obsolete("WebPlayer export is no longer supported in Unity 5.4+.", true)]
        WindowsWebPlayer = 5,

        /// <summary>
        /// In the Unity editor on Windows.
        /// </summary>
        WindowsEditor = 7,

        /// <summary>
        /// In the player on the iPhone.
        /// </summary>
        IPhonePlayer = 8,

        /// <summary>
        /// In the Unity editor on PS3.
        /// </summary>
        [Obsolete("PS3 export is no longer supported in Unity >=5.5.")]
        PS3 = 9,

        /// <summary>
        /// In the Unity editor on XBOX360.
        /// </summary>
        [Obsolete("Xbox360 export is no longer supported in Unity 5.5+.")]
        XBOX360 = 10, // 0x0000000A

        /// <summary>
        /// In the player on Android devices.
        /// </summary>
        Android = 11, // 0x0000000B

        /// <summary>
        /// In the Unity editor on NaCl.
        /// </summary>
        [Obsolete("NaCl export is no longer supported in Unity 5.0+.")]
        NaCl = 12, // 0x0000000C

        /// <summary>
        /// In the player on Linux.
        /// </summary>
        LinuxPlayer = 13, // 0x0000000D

        /// <summary>
        /// In the player on FlashPlayer.
        /// </summary>
        [Obsolete("FlashPlayer export is no longer supported in Unity 5.0+.")]
        FlashPlayer = 15, // 0x0000000F

        /// <summary>
        /// In the Unity editor on Linux.
        /// </summary>
        LinuxEditor = 16, // 0x00000010

        /// <summary>
        /// In the player on WebGL
        /// </summary>
        WebGLPlayer = 17, // 0x00000011

        /// <summary>
        /// In the player on Windows Store Apps when CPU architecture is X86.
        /// </summary>
        WSAPlayerX86 = 18, // 0x00000012

        /// <summary>
        /// In the player on Windows Store Apps when CPU architecture is X64.
        /// </summary>
        WSAPlayerX64 = 19, // 0x00000013 

        /// <summary>
        /// In the player on Windows Store Apps when CPU architecture is ARM.
        /// </summary>
        WSAPlayerARM = 20, // 0x00000014

        /// <summary>
        /// In the web player on WP8Player.
        /// </summary>
        [Obsolete("Windows Phone 8 was removed in 5.3")]
        WP8Player = 21, // 0x00000015

        /// <summary>
        /// In the web player on BB10Player.
        /// </summary>
        [Obsolete("BB10Player export is no longer supported in Unity 5.4+."), EditorBrowsable(EditorBrowsableState.Never)]
        BB10Player = 22, // 0x00000016

        /// <summary>
        /// In the web player on BlackBerryPlayer.
        /// </summary>
        [Obsolete("BlackBerryPlayer export is no longer supported in Unity 5.4+.")]
        BlackBerryPlayer = 22, // 0x00000016

        /// <summary>
        /// In the web player on TizenPlayer.
        /// </summary>
        [Obsolete("TizenPlayer export is no longer supported in Unity 2017.3+.")]
        TizenPlayer = 23, // 0x00000017

        /// <summary>
        /// In the web player on PSP2.
        /// </summary>
        [Obsolete("PSP2 is no longer supported as of Unity 2018.3")]
        PSP2 = 24, // 0x00000018

        /// <summary>
        /// In the player on the Playstation 4.
        /// </summary>
        PS4 = 25, // 0x00000019

        /// <summary>
        /// In the web player on PSM.
        /// </summary>
        [Obsolete("PSM export is no longer supported in Unity >= 5.3")]
        PSM = 26, // 0x0000001A

        /// <summary>
        /// In the player on Xbox One.
        /// </summary>
        XboxOne = 27, // 0x0000001B

        /// <summary>
        /// In the web player on SamsungTVPlayer.
        /// </summary>
        [Obsolete("SamsungTVPlayer export is no longer supported in Unity 2017.3+.")]
        SamsungTVPlayer = 28, // 0x0000001C

        /// <summary>
        /// In the web player on WiiU.
        /// </summary>
        [Obsolete("Wii U is no longer supported in Unity 2018.1+.")]
        WiiU = 30, // 0x0000001E

        /// <summary>
        /// In the player on the Apple's tvOS.
        /// </summary>
        tvOS = 31, // 0x0000001F

        /// <summary>
        /// In the player on Nintendo Switch.
        /// </summary>
        Switch = 32, // 0x00000020

        /// <summary>
        /// In the web player on Lumin.
        /// </summary>
        Lumin = 33, // 0x00000021

        /// <summary>
        /// In the player on Stadia.
        /// </summary>
        Stadia = 34, // 0x00000022

        /// <summary>
        /// In the player on CloudRendering.
        /// </summary>
        CloudRendering = 35, // 0x00000023

        /// <summary>
        /// In the web player on GameCoreXboxSeries.
        /// </summary>
        GameCoreXboxSeries = 36, // 0x00000024      

        /// <summary>
        /// In the web player on GameCoreXboxOne.
        /// </summary>
        GameCoreXboxOne = 37, // 0x00000025

        /// <summary>
        /// In the player on the Playstation 5.
        /// </summary>
        PS5 = 38, // 0x00000026
    }
}