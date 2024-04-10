#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using AIO;

#endregion

/// <summary>
/// [单位:长度换算]
/// </summary>
[Conditional(Strings.UnityEditor), Description("长度单位转化")]
public sealed class ULengthAttribute : UnitAttribute
{
    public ULengthAttribute(IReadOnlyList<double> values, IReadOnlyList<string> names) : base(values, names) { }

    public ULengthAttribute(IDictionary<double, string> data) : base(data) { }

    public ULengthAttribute(ULength conver, int unitIndex = 0)
    {
        var list = new List<(double, string)>();
        foreach (Enum value in Enum.GetValues(typeof(ULength)))
            if (conver.HasFlag(value))
            {
                var equit = value.GetType().GetField(value.ToString()).GetCustomAttribute<UDefaultAttribute>().Unit;
                list.Add((equit, value.ToString()));
            }

        if (list.Count == 0) return;

        if (list.Count > 2)
            list.Sort((a, b) =>
            {
                if (a.Item1 < b.Item1) return 1;
                if (a.Item1 > b.Item1) return -1;
                return 0;
            });


        var multipliers = new double[list.Count];
        var suffixes = new string[list.Count];
        var min = list[list.Count - 1];
        for (var i = list.Count - 1; i >= 0; i--)
        {
            multipliers[i] = Math.Floor(list[i].Item1 / min.Item1);
            suffixes[i]    = string.Concat(" ", list[i].Item2);
        }

        SetUnits(multipliers, new CompactUnitConversionCache[suffixes.Length], unitIndex);
        for (var i = 0; i < suffixes.Length; i++) DisplayConverters[i] = new CompactUnitConversionCache(suffixes[i]);
    }
}