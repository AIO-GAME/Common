/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-07
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System.Collections;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// nameof(ToConvertExtend)
    /// </summary>
    public static class ToConvertExtend
    {
        #region 数学工具

        /// <summary>
        /// Vector2转换为标准Copy字符串
        /// </summary>
        /// <param name="value">Vector2值</param>
        /// <param name="format">格式</param>
        /// <returns>Copy字符串</returns>
        public static string ToCopyString(this Vector2 value, string format)
        {
            return $"Vector2({value.x.ToString(format)}f,{value.y.ToString(format)}f)";
        }

        /// <summary>
        /// Vector3转换为标准Copy字符串
        /// </summary>
        /// <param name="value">Vector3值</param>
        /// <param name="format">格式</param>
        /// <returns>Copy字符串</returns>
        public static string ToCopyString(this Vector3 value, string format)
        {
            return $"Vector3({value.x.ToString(format)}f,{value.y.ToString(format)}f,{value.z.ToString(format)}f)";
        }

        /// <summary>
        /// Vector4转换为标准Copy字符串
        /// </summary>
        /// <param name="value">Vector4值</param>
        /// <param name="format">格式</param>
        /// <returns>Copy字符串</returns>
        public static string ToCopyString(this Vector4 value, string format)
        {
            return
                $"Vector4({value.x.ToString(format)}f,{value.y.ToString(format)}f,{value.z.ToString(format)}f,{value.w.ToString(format)}f)";
        }

        /// <summary>
        /// Vector2Int转换为标准Copy字符串
        /// </summary>
        /// <param name="value">Vector2Int值</param>
        /// <returns>Copy字符串</returns>
        public static string ToCopyString(this Vector2Int value)
        {
            return $"Vector2Int({value.x},{value.y})";
        }

        /// <summary>
        /// Vector3Int转换为标准Copy字符串
        /// </summary>
        /// <param name="value">Vector3Int值</param>
        /// <returns>Copy字符串</returns>
        public static string ToCopyString(this Vector3Int value)
        {
            return $"Vector3Int({value.x},{value.y},{value.z})";
        }

        /// <summary>
        /// Quaternion转换为标准Copy字符串
        /// </summary>
        /// <param name="value">Quaternion值</param>
        /// <param name="format">格式</param>
        /// <returns>Copy字符串</returns>
        public static string ToCopyString(this Quaternion value, string format)
        {
            return
                $"Quaternion({value.x.ToString(format)}f,{value.y.ToString(format)}f,{value.z.ToString(format)}f,{value.w.ToString(format)}f)";
        }

        /// <summary>
        /// Bounds转换为标准Copy字符串
        /// </summary>
        /// <param name="value">Bounds值</param>
        /// <param name="format">格式</param>
        /// <returns>Copy字符串</returns>
        public static string ToCopyString(this Bounds value, string format)
        {
            return string.Format("Bounds({0}f,{1}f,{2}f,{3}f,{4}f,{5}f)"
                , value.center.x.ToString(format), value.center.y.ToString(format), value.center.z.ToString(format)
                , value.size.x.ToString(format), value.size.y.ToString(format), value.size.z.ToString(format));
        }

        /// <summary>
        /// BoundsInt转换为标准Copy字符串
        /// </summary>
        /// <param name="value">BoundsInt值</param>
        /// <returns>Copy字符串</returns>
        public static string ToCopyString(this BoundsInt value)
        {
            return string.Format("BoundsInt({0},{1},{2},{3},{4},{5})"
                , value.position.x, value.position.y, value.position.z
                , value.size.x, value.size.y, value.size.z);
        }

        /// <summary>
        /// 标准Paste字符串转换为Vector2
        /// </summary>
        /// <param name="value">Paste字符串</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>Vector2值</returns>
        public static Vector2 ToPasteVector2(this string value, Vector2 defaultValue = default)
        {
            if (!value.StartsWith("Vector2(")) return defaultValue;
            value = value.Replace("Vector2(", "");
            value = value.Replace(")", "");
            value = value.Replace("f", "");

            var vector2 = value.Split(',');
            if (vector2.Length != 2) return defaultValue;

            if (float.TryParse(vector2[0], out var x) &&
                float.TryParse(vector2[1], out var y))
            {
                return new Vector2(x, y);
            }

            return defaultValue;
        }

        /// <summary>
        /// 标准Paste字符串转换为Vector3
        /// </summary>
        /// <param name="value">Paste字符串</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>Vector3值</returns>
        public static Vector3 ToPasteVector3(this string value, Vector3 defaultValue = default)
        {
            if (!value.StartsWith("Vector3(")) return defaultValue;
            value = value.Replace("Vector3(", "");
            value = value.Replace(")", "");
            value = value.Replace("f", "");

            var vector3 = value.Split(',');
            if (vector3.Length != 3) return defaultValue;
            if (float.TryParse(vector3[0], out var x) &&
                float.TryParse(vector3[1], out var y) &&
                float.TryParse(vector3[2], out var z)
               ) return new Vector3(x, y, z);

            return defaultValue;
        }

        /// <summary>
        /// 标准Paste字符串转换为Vector4
        /// </summary>
        /// <param name="value">Paste字符串</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>Vector4值</returns>
        public static Vector4 ToPasteVector4(this string value, Vector4 defaultValue = default)
        {
            if (!value.StartsWith("Vector4(")) return defaultValue;
            value = value.Replace("Vector4(", "");
            value = value.Replace(")", "");
            value = value.Replace("f", "");

            var vector4 = value.Split(',');
            if (vector4.Length != 4) return defaultValue;
            if (float.TryParse(vector4[0], out var x) &&
                float.TryParse(vector4[1], out var y) &&
                float.TryParse(vector4[2], out var z) &&
                float.TryParse(vector4[3], out var w)
               ) return new Vector4(x, y, z, w);

            return defaultValue;
        }

        /// <summary>
        /// 标准Paste字符串转换为Vector2Int
        /// </summary>
        /// <param name="value">Paste字符串</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>Vector2Int值</returns>
        public static Vector2Int ToPasteVector2Int(this string value, Vector2Int defaultValue = default)
        {
            if (!value.StartsWith("Vector2Int(")) return defaultValue;
            value = value.Replace("Vector2Int(", "");
            value = value.Replace(")", "");

            var vector2 = value.Split(',');
            if (vector2.Length != 2) return defaultValue;
            if (int.TryParse(vector2[0], out var x) &&
                int.TryParse(vector2[1], out var y)
               ) return new Vector2Int(x, y);

            return defaultValue;
        }

        /// <summary>
        /// 标准Paste字符串转换为Vector3Int
        /// </summary>
        /// <param name="value">Paste字符串</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>Vector3Int值</returns>
        public static Vector3Int ToPasteVector3Int(this string value, Vector3Int defaultValue = default)
        {
            if (!value.StartsWith("Vector3Int(")) return defaultValue;
            value = value.Replace("Vector3Int(", "");
            value = value.Replace(")", "");

            var vector3 = value.Split(',');
            if (vector3.Length != 3) return defaultValue;
            if (int.TryParse(vector3[0], out var x) &&
                int.TryParse(vector3[1], out var y) &&
                int.TryParse(vector3[2], out var z)
               ) return new Vector3Int(x, y, z);

            return defaultValue;
        }

        /// <summary>
        /// 标准Paste字符串转换为Quaternion
        /// </summary>
        /// <param name="value">Paste字符串</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>Quaternion值</returns>
        public static Quaternion ToPasteQuaternion(this string value, Quaternion defaultValue = default)
        {
            if (!value.StartsWith("Quaternion(")) return defaultValue;
            value = value.Replace("Quaternion(", "");
            value = value.Replace(")", "");
            value = value.Replace("f", "");

            var quaternion = value.Split(',');
            if (quaternion.Length != 4) return defaultValue;
            if (float.TryParse(quaternion[0], out var x) &&
                float.TryParse(quaternion[1], out var y) &&
                float.TryParse(quaternion[2], out var z) &&
                float.TryParse(quaternion[3], out var w)
               ) return new Quaternion(x, y, z, w);

            return defaultValue;
        }

        /// <summary>
        /// 标准Paste字符串转换为Bounds
        /// </summary>
        /// <param name="value">Paste字符串</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>Bounds值</returns>
        public static Bounds ToPasteBounds(this string value, Bounds defaultValue = default)
        {
            if (!value.StartsWith("Bounds(")) return defaultValue;
            value = value.Replace("Bounds(", "");
            value = value.Replace(")", "");
            value = value.Replace("f", "");

            var bounds = value.Split(',');
            if (bounds.Length != 6) return defaultValue;
            if (float.TryParse(bounds[0], out var centerX) &&
                float.TryParse(bounds[1], out var centerY) &&
                float.TryParse(bounds[2], out var centerZ) &&
                float.TryParse(bounds[3], out var sizeX) &&
                float.TryParse(bounds[4], out var sizeY) &&
                float.TryParse(bounds[5], out var sizeZ)
               ) return new Bounds(new Vector3(centerX, centerY, centerZ), new Vector3(sizeX, sizeY, sizeZ));

            return defaultValue;
        }

        /// <summary>
        /// 标准Paste字符串转换为BoundsInt
        /// </summary>
        /// <param name="value">Paste字符串</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>BoundsInt值</returns>
        public static BoundsInt ToPasteBoundsInt(this string value, BoundsInt defaultValue = default)
        {
            if (!value.StartsWith("BoundsInt(")) return defaultValue;
            value = value.Replace("BoundsInt(", "");
            value = value.Replace(")", "");

            var bounds = value.Split(',');
            if (bounds.Length != 6) return defaultValue;
            if (int.TryParse(bounds[0], out var centerX) &&
                int.TryParse(bounds[1], out var centerY) &&
                int.TryParse(bounds[2], out var centerZ) &&
                int.TryParse(bounds[3], out var sizeX) &&
                int.TryParse(bounds[4], out var sizeY) &&
                int.TryParse(bounds[5], out var sizeZ)
               ) return new BoundsInt(new Vector3Int(centerX, centerY, centerZ), new Vector3Int(sizeX, sizeY, sizeZ));

            return defaultValue;
        }

        /// <summary>
        /// 将Location转换为Json字符串
        /// </summary>
        /// <param name="location">Location对象</param>
        /// <returns>Json字符串</returns>
        public static string LocationToJson(this Location location)
        {
            if (location.IsNull) return null;
            return AHelper.Json.Serialize(new Hashtable
            {
                ["type"] = "Location",
                ["Position"] = location.Position.ToCopyString("F4"),
                ["Rotation"] = location.Rotation.ToCopyString("F4"),
                ["Scale"] = location.Scale.ToCopyString("F4")
            });
        }

        /// <summary>
        /// 将Json字符串转换为Location
        /// </summary>
        /// <param name="json">Json字符串</param>
        /// <returns>Location对象</returns>
        public static Location JsonToLocation(this string json)
        {
            var jsonData = AHelper.Json.Deserialize<Hashtable>(json);
            if (jsonData == null || jsonData.GetOrDefault<string>("type", string.Empty) != "Location")
                return Location.Null;

            var location = new Location
            {
                Position = jsonData["Position"].ToString().ToPasteVector3(Vector3.zero),
                Rotation = jsonData["Rotation"].ToString().ToPasteVector3(Vector3.zero),
                Scale = jsonData["Scale"].ToString().ToPasteVector3(Vector3.zero)
            };
            return location;
        }

        #endregion
    }
}