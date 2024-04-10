#region

using System;
using System.Collections.Generic;
using System.Text;

#endregion

/// <summary>
/// 命令结构
/// </summary>
public class GCommandSearchDatabase : IDisposable
{
    private GCommandSearchDatabase()
    {
        Commands = new Dictionary<string, List<GCommandAttribute>>();
        Child    = new Dictionary<string, GCommandSearchDatabase>();
    }

    /// <summary>
    /// 命令结构
    /// </summary>
    internal GCommandSearchDatabase(string title) : this()
    {
        Title = title;
    }

    /// <summary>
    /// 命令结构
    /// </summary>
    internal GCommandSearchDatabase(string title, GCommandSearchDatabase parent) : this()
    {
        Title  = title;
        Parent = parent;
    }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    /// 命令
    /// </summary>
    private Dictionary<string, List<GCommandAttribute>> Commands { get; set; }

    /// <summary>
    /// 父标题
    /// </summary>
    public GCommandSearchDatabase Parent { get; private set; }

    /// <summary>
    /// 父标题
    /// </summary>
    public Dictionary<string, GCommandSearchDatabase> Child { get; private set; }

    #region IDisposable Members

    /// <summary>
    /// 释放
    /// </summary>
    public void Dispose()
    {
        Commands.Clear();
        Commands = null;
    }

    #endregion

    /// <summary>
    /// 添加
    /// </summary>
    internal void Add(GCommandAttribute attribute)
    {
        if (!Commands.ContainsKey(attribute.Title)) Commands.Add(attribute.Title, new List<GCommandAttribute>());
        Commands[attribute.Title].Add(attribute);
    }

    /// <summary>
    /// 添加
    /// </summary>
    private void Add(string label, GCommandAttribute attribute)
    {
        if (!Commands.ContainsKey(label)) Commands.Add(label, new List<GCommandAttribute>());
        Commands[label].Add(attribute);
    }

    /// <summary>
    /// 添加
    /// </summary>
    internal void Add(IList<string> lables, GCommandAttribute attribute)
    {
        if (lables is null || lables.Count < 1) return;
        var label = lables[0];
        if (!Child.ContainsKey(label)) Child.Add(label, new GCommandSearchDatabase(label, this));

        if (lables.Count == 1)
        {
            Child[label].Add(lables[0], attribute);
        }
        else
        {
            lables.RemoveAt(0);
            Child[label].Add(lables, attribute);
        }
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        var parent = Parent;
        var str = new StringBuilder();
        var all = new StringBuilder();

        str.Append(Title);
        while (parent != null)
        {
            str.Insert(0, string.Concat(parent.Title, '/'));
            parent = parent.Parent;
        }

        all.Append("Title -> ").Append(str).Append('\n');
        str.Clear();

        foreach (var item in Commands)
        foreach (var attribute in item.Value)
        {
            str.Append("[\n");
            str.Append(attribute).Append("\n]");
        }

        if (str.Length != 0)
        {
            all.Append("Command -> \n").Append(str).Append('\n');
            str.Clear();
        }

        foreach (var item in Child) str.Append(item.Value).Append('\n');
        if (str.Length != 0)
        {
            var info = str.Replace("\n", "\n ").ToString();
            all.Append("Child -> \n[\n ").Append(info.Substring(0, info.Length - 3)).Append("]\n");
            str.Clear();
        }

        return all.ToString();
    }
}