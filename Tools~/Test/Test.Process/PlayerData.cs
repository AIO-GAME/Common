/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2025-07-14
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System;
using AIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Models
{
    public enum EPlayerComponent
    {
        /// <summary>
        /// 角色属性组件
        /// </summary>
        Attributes,

        /// <summary>
        /// 角色技能组件
        /// </summary>
        Skills,

        /// <summary>
        /// 角色装备组件
        /// </summary>
        Equipment,

        /// <summary>
        /// 角色任务组件
        /// </summary>
        Quests,

        /// <summary>
        /// 角色成就组件
        /// </summary>
        Achievements,

        /// <summary>
        /// 角色背包组件
        /// </summary>
        Bag
    }

    /// <summary>
    /// 角色数据
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    [JsonConverter(typeof(Converter))]
    public class PlayerData
    {
        #region Constants

        private class Converter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                if (value is PlayerData player)
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("name");
                    writer.WriteValue(player.Name);
                    writer.WritePropertyName("pid");
                    writer.WriteValue(player.PID);
                    writer.WritePropertyName("head");
                    writer.WriteValue(player.Head);
                    writer.WritePropertyName("race");
                    writer.WriteValue(player.Race);
                    writer.WritePropertyName("sex");
                    writer.WriteValue(player.Sex);
                    writer.WritePropertyName("job");
                    writer.WriteValue(player.Job);
                    writer.WritePropertyName("age");
                    writer.WriteValue(player.Age);
                    writer.WritePropertyName("skin");
                    writer.WriteValue(player.SkinColor);
                    writer.WritePropertyName("birthPlace");
                    writer.WriteValue(player.BirthPlace);

                    writer.WriteEndObject();
                }
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                var player = new PlayerData();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.EndObject)
                        break; // 增加对象结束判断
                    if (reader.TokenType == JsonToken.PropertyName)
                    {
                        var propertyName = reader.Value.ToString();
                        reader.Read();

                        switch (propertyName)
                        {
                            case "name":
                                player.Name = reader.Value.ToString();
                                break;
                            case "pid":
                                player.PID = Convert.ToInt32(reader.Value);
                                break;
                            case "head":
                                player.Head = Convert.ToInt32(reader.Value);
                                break;
                            case "race":
                                player.Race = Convert.ToInt32(reader.Value);
                                break;
                            case "sex":
                                player.Sex = Convert.ToInt32(reader.Value);
                                break;
                            case "age":
                                player.Age = Convert.ToInt32(reader.Value);
                                break;
                            case "skin":
                                player.SkinColor = Convert.ToInt32(reader.Value);
                                break;
                            case "birthplace":
                                player.BirthPlace = Convert.ToInt32(reader.Value);
                                break;
                            case "job":
                                player.Job = Convert.ToInt32(reader.Value);
                                break;
                        }
                    }
                }

                return player;
            }

            public override bool CanConvert(Type objectType) => objectType == typeof(PlayerData);
        }

        #endregion

        /// <summary>
        /// 角色名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; private set; }

        /// <summary>
        /// 角色唯一ID
        /// </summary>
        [JsonProperty("pid")]
        public int PID { get; private set; }

        /// <summary>
        /// 头像ID
        /// </summary>
        [JsonProperty("head", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Head { get; set; }

        /// <summary>
        /// 种族
        /// </summary>
        [JsonProperty("race", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Race { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [JsonProperty("sex", NullValueHandling = NullValueHandling.Ignore)]
        public int Sex { get; set; } = 0; // 0: 男性, 1: 女性

        /// <summary>
        /// 年龄
        /// </summary>
        [JsonProperty("age", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Age { get; set; } = 18; // 年龄

        /// <summary>
        /// 皮肤颜色
        /// </summary>
        [JsonProperty("skin", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int SkinColor { get; set; } = 0;

        /// <summary>
        /// 出生地
        /// </summary>
        [JsonProperty("birthPlace", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int BirthPlace { get; set; } = 0;

        /// <summary>
        /// 职业
        /// </summary>
        [JsonProperty("job", NullValueHandling = NullValueHandling.Ignore)]
        public int Job { get; set; } = 0;

        public PlayerData(string name) : this()
        {
            Name = name;
            PID  = AHelper.Random.RandInt32(1, int.MaxValue);
        }

        public PlayerData(string name, int pid) : this()
        {
            Name = name;
            PID  = pid;
        }

        [JsonConstructor]
        private PlayerData() { }

        /// <summary>
        ///  随机生成角色数据
        /// </summary>
        public void Random()
        {
            Age  = AHelper.Random.RandInt16(18, 36);
            Sex  = AHelper.Random.RandInt16(0, 2);
            Race = 0;
            Head = AHelper.Random.RandInt16(1, 16);
            Name = AHelper.Random.RandomChineseName
                (Sex == 0,
                 AHelper.Random.RandBool(),
                 AHelper.Random.RandBool(),
                 AHelper.Random.RandInt16(2, 5)
                );
        }

        /// <summary>
        /// 设置角色名称
        /// </summary>
        /// <param name="name">角色名称</param>
        /// <exception cref="ArgumentException"> 角色名称不能为空或超过20个字符 </exception>
        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("角色名称不能为空。");
            if (name.Length > 20)
                throw new ArgumentException("角色名称不能超过20个字符。");
            Name = name;
        }

        public override string ToString() { return $"角色信息 : {Name} ({PID})"; }

        #region Ext

        public string GetHeadPath() => GetHeadPath(Race, Sex, Head);

        public static string GetHeadPath(int type, int sex, int index)
        {
            //actor/human-girl/gril-6
            switch (type)
            {
                case 0:
                case 1:
                    var name = (sex == 0 ? "boy" : "girl");
                    return $"actor/human-{name}/{name}-{index}";
            }

            return string.Empty;
        }

        #endregion
    }
}