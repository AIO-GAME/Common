using System;

public partial class Pool
{
    /// <summary>
    /// 引用释放
    /// </summary>
    public static IDisposable Using<T>(out T value) where T : class, IDisposable, new()
    {
        var disposable = new Disposable<T>(Activator.CreateInstance<T>(),
            release => { release.Dispose(); });
        value = disposable.Item;
        return disposable;
    }

    /// <summary>
    /// 引用释放
    /// </summary>
    public static IDisposable Using<T>(out T value, Action<T> onRelease)
    {
        var disposable = new Disposable<T>(Activator.CreateInstance<T>(), onRelease);
        value = disposable.Item;
        return disposable;
    }

    /// <summary>
    /// 引用释放
    /// </summary>
    public static IDisposable Using<T>(T value, Action<T> onRelease)
    {
        return new Disposable<T>(value, onRelease);
    }

    /// <summary>
    /// 引用释放
    /// </summary>
    public static IDisposable Using<T>(out System.Collections.Generic.List<T> value)
    {
        var disposable = new Disposable<System.Collections.Generic.List<T>>(
            List<T>.New(),
            release => { release.Free(); });
        value = disposable.Item;
        return disposable;
    }

    /// <summary>
    /// 引用释放
    /// </summary>
    public static IDisposable Using<T>(out System.Collections.Generic.HashSet<T> value)
    {
        var disposable = new Disposable<System.Collections.Generic.HashSet<T>>(
            HashSet<T>.New(),
            release => { release.Free(); });
        value = disposable.Item;
        return disposable;
    }

    /// <summary>
    /// 引用释放
    /// </summary>
    public static IDisposable Using<T>(out System.Collections.Generic.Stack<T> value)
    {
        var disposable = new Disposable<System.Collections.Generic.Stack<T>>(
            Stack<T>.New(),
            release => { release.Free(); });
        value = disposable.Item;
        return disposable;
    }

    /// <summary>
    /// 引用释放
    /// </summary>
    public static IDisposable Using<T>(out System.Collections.Generic.Queue<T> value)
    {
        var disposable = new Disposable<System.Collections.Generic.Queue<T>>(
            Queue<T>.New(),
            release => { release.Free(); });
        value = disposable.Item;
        return disposable;
    }

    /// <summary>
    /// 引用释放
    /// </summary>
    public static IDisposable Using<K, V>(out System.Collections.Generic.Dictionary<K, V> value)
    {
        var disposable = new Disposable<System.Collections.Generic.Dictionary<K, V>>(
            Dictionary<K, V>.New(),
            release => { release.Free(); });
        value = disposable.Item;
        return disposable;
    }
}