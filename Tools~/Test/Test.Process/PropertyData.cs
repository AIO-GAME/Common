/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2025-07-16
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System;
using System.Collections.Generic;
using System.Security.Permissions;
using AIO;
using Newtonsoft.Json;

/// <summary>
/// 道具KEY
/// </summary>
public enum PropertyType
{
    Quality = 1,   //品质
    Level   = 2,   //等级
    Name    = 3,   //名称
    Exp     = 4,   //经验
    HP      = 100, //血量
    Defense,       //防御
    Attack,        //攻击力
    // ... 等等，
    // 如血量，防御等词条性质的其实不能简单放在这里，因为存在百分比和直接加成等，当然也可以直接用两个Key分别表示百分比值和加成值。
}

[Serializable, SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
public class PropertyData1 : PropertyData
{
    [JsonConstructor]
    private PropertyData1()
    {
        id    = 0;
        type  = 0;
        value = null;
        uid   = AHelper.Random.RandInt16();
    }

    /// <inheritdoc />
    public PropertyData1(PropertyType id, string value) : base(id, value) { uid = AHelper.Random.RandInt16(); }

    /// <inheritdoc />
    public PropertyData1(PropertyType id, int value) : base(id, value) { uid = AHelper.Random.RandInt16(); }

    /// <inheritdoc />
    public PropertyData1(PropertyType id, float value) : base(id, value) { uid = AHelper.Random.RandInt16(); }

    public string Description1;

    // public string Description2 { get; set; }

    private int uid { get; set; }

    // public override string ToString() { return $"{id}:{data} - [{Description1} - {Description2} ] - {string.Join(", ", Slots)} | {uid}"; }

    public override string ToString() { return $"{id}:{value} - [{Description1}  ] - {string.Join(", ", Slots)} | type={type} | uid={uid}"; }

    //
    // /// <inheritdoc />
    // public void GetObjectData(SerializationInfo info, StreamingContext context) { info.AddValue("props", Description1, typeof(string)); }
    //
    // [OnSerializing]
    // private void OnSerializing(StreamingContext context)
    // {
    //     // 在序列化前执行的操作
    //     Description1 = "描述1";
    //     Description2 = "描述2";
    // }
    //
    // [OnSerialized]
    // private void OnSerialized(StreamingContext context)
    // {
    //     // 在序列化后执行的操作
    //     Console.WriteLine("序列化完成");
    // }
}

[Serializable]
public partial class PropertyData
{
    /// <summary>
    /// 属性类型
    /// </summary>
    [JsonProperty("id")]
    public PropertyType id { get; protected set; }

    /// <summary>
    /// 属性值类型 1:字符串 2:int 3:浮点数
    /// </summary>
    [JsonProperty("type")]
    public byte type { get; protected set; }

    /// <summary>
    /// 属性
    /// </summary>
    [JsonProperty("value")]
    public object value { get; protected set; }

    public string Name => $"{GetType().FullName}";

    [JsonProperty("slots")]
    public List<int> Slots { get; protected set; } = new List<int>();

    public int Capacity
    {
        get => Slots.Capacity;
        set
        {
            if (Slots.Count < value) Slots.Capacity = value;
        }
    }

    /// <summary>
    /// 设置属性
    /// </summary>
    /// <param name="val"> 值 </param>
    /// <exception cref="System.Exception"> 类型不匹配 </exception>
    public void Set(int val)
    {
        if (type != 2) throw new Exception("类型不匹配");
        this.value = val;
    }

    /// <summary>
    /// 设置属性
    /// </summary>
    /// <param name="value"> 值 </param>
    /// <exception cref="System.Exception"> 类型不匹配 </exception>
    public void Set(string value)
    {
        if (type != 1) throw new Exception("类型不匹配");
        this.value = value;
    }

    /// <summary>
    /// 设置属性
    /// </summary>
    /// <param name="value"> 值 </param>
    /// <exception cref="System.Exception"> 类型不匹配 </exception>
    public void Set(float value)
    {
        if (type != 3) throw new Exception("类型不匹配");
        this.value = value;
    }

    public override string ToString() { return $"{id}:{value} - {string.Join(", ", Slots)} | type={type}"; }

    [JsonConstructor]
    protected PropertyData()
    {
        id    = 0;
        type  = 0;
        value = null;
    }

    public PropertyData(PropertyType id, string value)
    {
        this.id    = id;
        this.type  = 1;
        this.value = value;
    }

    public PropertyData(PropertyType id, int value)
    {
        this.id    = id;
        this.type  = 2;
        this.value = value;
    }

    public PropertyData(PropertyType id, float value)
    {
        this.id    = id;
        this.type  = 3;
        this.value = value;
    }
}