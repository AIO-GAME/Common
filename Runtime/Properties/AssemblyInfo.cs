﻿#region

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Scripting;

#endregion

[assembly: UnityAPICompatibilityVersion("2019.4.0", true)]
[assembly: Preserve]
#if UNITY_2018_3_OR_NEWER
[assembly: AlwaysLinkAssembly]
#endif

[assembly: ComVisible(false)]
[assembly: InternalsVisibleTo("AIO.Unity.Editor")]
[assembly: InternalsVisibleTo("AIO.Core.Editor")]
[assembly: InternalsVisibleTo("AIO.Core.Runtime")]
