/*|============|*|
|*|Author:     |*| xinan                
|*|Date:       |*| 2023-10-07               
|*|E-Mail:     |*| 1398581458@qq.com     
|*|============|*/

using System;

namespace AIO
{
    /// <summary>
    /// Version
    /// </summary>
    public sealed class Version
    {
        /// <summary>
        /// 比较版本号
        /// </summary>
        /// <param name="version">版本</param>
        /// <param name="ignoreBuildNumber">忽略构件号</param>
        /// <returns>0:相等 1:大于目标版本 -1:小余目标版本</returns>
        /// <exception cref="Exception"></exception>
        public int CompareTo(object version, bool ignoreBuildNumber = false)
        {
            if (version == null) return 1;
            var v = version as Version;
            if (v == null) throw new Exception("error type");
            if (major != v.major) return major > v.major ? 1 : -1;
            if (minor != v.minor) return minor > v.minor ? 1 : -1;
            if (revision != v.revision) return revision > v.revision ? 1 : -1;
            if (ignoreBuildNumber) return 0;
            if (build != v.build) return build > v.build ? 1 : -1;
            return 0;
        }

        /// <summary>
        /// 主版本号
        /// </summary>
        public int major { get; }

        /// <summary>
        /// 小版本号
        /// </summary>
        public int minor { get; }

        /// <summary>
        /// 补丁修正号
        /// </summary>
        public int revision { get; }

        /// <summary>
        /// 构建号,可选
        /// </summary>
        public int build { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Version(string version)
        {
            var arr = version.Split('.');
            if (arr.Length < 2 || arr.Length > 4) throw new Exception("版本文件格式错误 major.minor[.revision[.build]]");
            var len = arr.Length;
            switch (len)
            {
                case 2:
                    major = int.Parse(arr[0]);
                    minor = int.Parse(arr[1]);
                    revision = -1;
                    build = -1;
                    break;
                case 3:
                    major = int.Parse(arr[0]);
                    minor = int.Parse(arr[1]);
                    revision = int.Parse(arr[2]);
                    build = -1;
                    break;
                default:
                    major = int.Parse(arr[0]);
                    minor = int.Parse(arr[1]);
                    revision = int.Parse(arr[2]);
                    build = int.Parse(arr[3]);
                    break;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="major">主版本号</param>
        /// <param name="minor">小版本号</param>
        /// <param name="revision">补丁修正号</param>
        public Version(int major, int minor, int revision) : this(major, minor, revision, -1)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="major">主版本号</param>
        /// <param name="minor">小版本号</param>
        /// <param name="revision">补丁修正号</param>
        /// <param name="build">构建号,可选</param>
        public Version(int major, int minor, int revision, int build)
        {
            if (major < 0) throw new Exception("Major should not be < 0");
            if (minor < 0) throw new Exception("Minor should not be < 0");
            if (revision < 0) throw new Exception("Revision should not be < 0");
            this.major = major;
            this.minor = minor;
            this.revision = revision;
            this.build = build;
        }


        /// <summary>
        /// 转换为字符串
        /// </summary>
        public override string ToString()
        {
            if (revision < 0) return string.Format("{0}.{1}", major, minor);
            if (build < 0) return string.Format("{0}.{1}.{2}", major, minor, revision);
            return string.Format("{0}.{1}.{2}.{3}", major, minor, revision, build);
        }

        /// <summary>
        /// 转换为字符串 不包含构建号
        /// </summary>
        public string ToStringWithoutBuildNumber()
        {
            return $"{major}.{minor}.{revision}";
        }

        /// <summary>
        /// 获取哈希值
        /// </summary>
        public override int GetHashCode()
        {
            var accumulator = 0;
            accumulator |= (major & 0x0000000F) << 28;
            accumulator |= (minor & 0x000000FF) << 20;
            accumulator |= (revision & 0x000000FF) << 12;
            accumulator |= (build & 0x00000FFF);
            return accumulator;
        }

        /// <summary>
        /// 比较版本号
        /// </summary>
        public override bool Equals(object obj)
        {
            var version = obj as Version;
            if (version == null) return false;
            return
                version.major == major &&
                version.minor == major &&
                version.revision == revision &&
                version.build == build;
        }

        /// <summary>
        /// ==
        /// </summary>
        /// <param name="v1"><see cref="Version"/></param>
        /// <param name="v2"><see cref="Version"/></param>
        /// <returns></returns>
        public static bool operator ==(Version v1, Version v2)
        {
            if (ReferenceEquals(v1, null)) return ReferenceEquals(v2, null);
            return v1.Equals(v2);
        }

        /// <summary>
        /// !=
        /// </summary>
        /// <param name="v1"><see cref="Version"/></param>
        /// <param name="v2"><see cref="Version"/></param>
        /// <returns></returns>
        public static bool operator !=(Version v1, Version v2)
        {
            return !(v1 == v2);
        }

        /// <summary>
        /// <
        /// </summary>
        /// <param name="v1"><see cref="Version"/></param>
        /// <param name="v2"><see cref="Version"/></param>
        /// <returns></returns>
        public static bool operator <(Version v1, Version v2)
        {
            if (ReferenceEquals(v1, null) || ReferenceEquals(v2, null)) throw new ArgumentNullException($"{v1}/{v2}");
            return (v1.CompareTo(v2) < 0);
        }

        /// <summary>
        /// &lt;=
        /// </summary>
        /// <param name="v1"><see cref="Version"/></param>
        /// <param name="v2"><see cref="Version"/></param>
        /// <returns></returns>
        public static bool operator <=(Version v1, Version v2)
        {
            if (ReferenceEquals(v1, null) || ReferenceEquals(v2, null)) throw new ArgumentNullException($"{v1}/{v2}");
            return v1.CompareTo(v2) <= 0;
        }

        /// <summary>
        /// >
        /// </summary>
        /// <param name="v1"><see cref="Version"/></param>
        /// <param name="v2"><see cref="Version"/></param>
        /// <returns></returns>
        public static bool operator >(Version v1, Version v2)
        {
            return v2 < v1;
        }

        /// <summary>
        /// >=
        /// </summary>
        /// <param name="v1"><see cref="Version"/></param>
        /// <param name="v2"><see cref="Version"/></param>
        /// <returns></returns>
        public static bool operator >=(Version v1, Version v2)
        {
            return v2 <= v1;
        }
    }
}