using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using AIO;

/// <summary>
/// [时间单位]
/// </summary>
[Conditional(Strings.UnityEditor)]
[Description("时间单位转化")]
public sealed class UTimeAttribute : UnitAttribute
{
    public UTimeAttribute(IReadOnlyList<double> values, IReadOnlyList<string> names) : base(values, names)
    {
    }

    public UTimeAttribute(IDictionary<double, string> data) : base(data)
    {
    }

    public UTimeAttribute(UTime conver, int unitIndex = 0)
    {
        var list = new List<(double, string)>();
        foreach (Enum value in Enum.GetValues(typeof(UTime)))
        {
            if (conver.HasFlag(value))
            {
                var equit = value.GetType().GetField(value.ToString()).GetCustomAttribute<UDefaultAttribute>().Unit;
                list.Add((equit, value.ToString()));
            }
        }

        if (list.Count == 0) return;
        if (list.Count >= 2)
        {
            list.Sort((a, b) =>
            {
                if (a.Item1 > b.Item1) return 1;
                if (a.Item1 < b.Item1) return -1;
                return 0;
            });
        }


        var multipliers = new double[list.Count];
        var suffixes = new string[list.Count];
        var min = list[0];
        for (var j = list.Count - 1; j >= 0; j--)
        {
            var i = list.Count - 1 - j;
            multipliers[i] = list[j].Item1 / min.Item1;
            suffixes[i] = string.Concat(" ", list[j].Item2);
        }

        SetUnits(multipliers, new CompactUnitConversionCache[suffixes.Length], unitIndex);
        for (var i = 0; i < suffixes.Length; i++) DisplayConverters[i] = new CompactUnitConversionCache(suffixes[i]);
    }
}