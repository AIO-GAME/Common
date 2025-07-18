﻿#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

#endregion

namespace AIO
{
    /// <summary>
    /// File cache is used to cache files in memory with optional timeouts.
    /// FileSystemWatcher is used to monitor file system changes in cached
    /// directories.
    /// </summary>
    /// <remarks>Thread-safe.</remarks>
    public class FileCache : IDisposable
    {
        #region Delegates

        /// <summary>
        /// 
        /// </summary>
        public delegate bool InsertHandler(FileCache cache, string key, byte[] value, TimeSpan timeout);

        #endregion

        private readonly ReaderWriterLockSlim                _lockEx;
        private          Dictionary<string, MemCacheEntry>   EntriesByKey;
        private          Dictionary<string, HashSet<string>> EntriesByPath;
        private          Dictionary<string, FileCacheEntry>  PathsByKey;

        /// <summary>
        /// 
        /// </summary>
        public FileCache()
        {
            _lockEx       = new ReaderWriterLockSlim();
            EntriesByKey  = new Dictionary<string, MemCacheEntry>();
            EntriesByPath = new Dictionary<string, HashSet<string>>();
            PathsByKey    = new Dictionary<string, FileCacheEntry>();
        }

        /// <summary>
        /// Is the file cache empty?
        /// </summary>
        public bool Empty => EntriesByKey.Count == 0;

        /// <summary>
        /// Get the file cache size
        /// </summary>
        public int Size => EntriesByKey.Count;

        /// <summary>
        /// Add a new cache value with the given timeout into the file cache
        /// </summary>
        /// <param name="key">Key to add</param>
        /// <param name="value">Value to add</param>
        /// <param name="timeout">Cache timeout (default is 0 - no timeout)</param>
        /// <returns>'true' if the cache value was added, 'false' if the given key was not added</returns>
        public bool Add(string key, byte[] value, TimeSpan timeout = new TimeSpan())
        {
            using (new LockWrite(_lockEx))
            {
                // Try to find and remove the previous key
                EntriesByKey.Remove(key);

                // Update the cache entry
                EntriesByKey.Add(key, new MemCacheEntry(value, timeout));

                return true;
            }
        }

        /// <summary>
        /// Add a new cache value with the given timeout into the file cache
        /// </summary>
        /// <param name="key">Key to add</param>
        /// <param name="value">Value to add</param>
        /// <param name="timeout">Cache timeout (default is 0 - no timeout)</param>
        /// <returns>'true' if the cache value was added, 'false' if the given key was not added</returns>
        public bool Add<T>(string key, T value, TimeSpan timeout = new TimeSpan())
        {
            using (new LockWrite(_lockEx))
            {
                // Try to find and remove the previous key
                EntriesByKey.Remove(key);

                // Update the cache entry
                EntriesByKey.Add(key, new MemCacheEntry<T>(value, timeout));

                return true;
            }
        }

        /// <summary>
        /// Try to find the cache value by the given key
        /// </summary>
        /// <param name="key">Key to find</param>
        /// <returns>'true' and cache value if the cache value was found, 'false' if the given key was not found</returns>
        public Tuple<bool, byte[]> Get(string key)
        {
            using (new LockRead(_lockEx))
            {
                // Try to find the given key
                return !EntriesByKey.TryGetValue(key, out var cacheValue)
                    ? Tuple.Create(false, Array.Empty<byte>())
                    : Tuple.Create(true, cacheValue.Value);
            }
        }

        /// <summary>
        /// Try to find the cache value by the given key
        /// </summary>
        /// <param name="key">Key to find</param>
        /// <returns>'true' and cache value if the cache value was found, 'false' if the given key was not found</returns>
        public Tuple<bool, T> Get<T>(string key)
        {
            using (new LockRead(_lockEx))
            {
                // Try to find the given key
                return !EntriesByKey.TryGetValue(key, out var cacheValue)
                    ? Tuple.Create(false, default(T))
                    : Tuple.Create(true, AHelper.Binary.Deserialize<T>(cacheValue.Value));
            }
        }

        /// <summary>
        /// Remove the cache value with the given key from the file cache
        /// </summary>
        /// <param name="key">Key to remove</param>
        /// <returns>'true' if the cache value was removed, 'false' if the given key was not found</returns>
        public bool Remove(string key)
        {
            using (new LockWrite(_lockEx))
            {
                return EntriesByKey.Remove(key);
            }
        }

        #region Nested type: FileCacheEntry

        private class FileCacheEntry
        {
            private readonly InsertHandler     _handler;
            private readonly string            _path;
            private readonly string            _prefix;
            private readonly TimeSpan          _timespan;
            private readonly FileSystemWatcher _watcher;

            public FileCacheEntry(FileCache     cache,
                                  string        prefix,
                                  string        path,
                                  string        filter,
                                  InsertHandler handler,
                                  TimeSpan      timespan)
            {
                _prefix   = prefix;
                _path     = path;
                _handler  = handler;
                _timespan = timespan;
                _watcher  = new FileSystemWatcher();

                // Start the filesystem watcher
                StartWatcher(cache, path, filter);
            }

            private void StartWatcher(FileCache cache, string path, string filter)
            {
                var entry = this;

                // Initialize a new filesystem watcher
                _watcher.Created               += (sender, e) => OnCreated(sender, e, cache, entry);
                _watcher.Changed               += (sender, e) => OnChanged(sender, e, cache, entry);
                _watcher.Deleted               += (sender, e) => OnDeleted(sender, e, cache, entry);
                _watcher.Renamed               += (sender, e) => OnRenamed(sender, e, cache, entry);
                _watcher.Path                  =  path;
                _watcher.IncludeSubdirectories =  true;
                _watcher.Filter                =  filter;
                _watcher.NotifyFilter          =  NotifyFilters.FileName | NotifyFilters.LastWrite;
                _watcher.EnableRaisingEvents   =  true;
            }

            public void StopWatcher() { _watcher.Dispose(); }

            private static bool IsDirectory(string path)
            {
                try
                {
                    // Skip directory updates
                    if (File.GetAttributes(path).HasFlag(FileAttributes.Directory))
                        return true;
                }
                catch (Exception)
                {
                    // 
                }

                return false;
            }

            private static void OnCreated(object sender, FileSystemEventArgs e, FileCache cache, FileCacheEntry entry)
            {
                var key  = e.FullPath.Replace('\\', Path.PathSeparator).Replace(string.Concat(entry._path, "/"), entry._prefix);
                var file = e.FullPath.Replace('\\', Path.PathSeparator);

                // Skip missing files
                if (!File.Exists(file))
                    return;
                // Skip directory updates
                if (IsDirectory(file))
                    return;

                cache.InsertFileInternal(entry._path, file, key, entry._timespan, entry._handler);
            }

            private static void OnChanged(object sender, FileSystemEventArgs e, FileCache cache, FileCacheEntry entry)
            {
                if (e.ChangeType != WatcherChangeTypes.Changed)
                    return;

                var key  = e.FullPath.Replace('\\', Path.PathSeparator).Replace(string.Concat(entry._path, "/"), entry._prefix);
                var file = e.FullPath.Replace('\\', Path.PathSeparator);

                // Skip missing files
                if (!File.Exists(file))
                    return;
                // Skip directory updates
                if (IsDirectory(file))
                    return;

                cache.InsertFileInternal(entry._path, file, key, entry._timespan, entry._handler);
            }

            private static void OnDeleted(object sender, FileSystemEventArgs e, FileCache cache, FileCacheEntry entry)
            {
                var key  = e.FullPath.Replace('\\', Path.PathSeparator).Replace(string.Concat(entry._path, "/"), entry._prefix);
                var file = e.FullPath.Replace('\\', Path.PathSeparator);

                cache.RemoveFileInternal(entry._path, key);
            }

            private static void OnRenamed(object sender, RenamedEventArgs e, FileCache cache, FileCacheEntry entry)
            {
                var oldKey  = e.OldFullPath.Replace('\\', Path.PathSeparator).Replace(string.Concat(entry._path, "/"), entry._prefix);
                var oldFile = e.OldFullPath.Replace('\\', Path.PathSeparator);
                var newKey  = e.FullPath.Replace('\\', Path.PathSeparator).Replace(string.Concat(entry._path, "/"), entry._prefix);
                var newFile = e.FullPath.Replace('\\', Path.PathSeparator);

                // Skip missing files
                if (!File.Exists(newFile))
                    return;
                // Skip directory updates
                if (IsDirectory(newFile))
                    return;

                cache.RemoveFileInternal(entry._path, oldKey);
                cache.InsertFileInternal(entry._path, newFile, newKey, entry._timespan, entry._handler);
            }
        }

        #endregion

        #region Nested type: MemCacheEntry

        private class MemCacheEntry
        {
            public MemCacheEntry(byte[] value, TimeSpan timespan = new TimeSpan())
            {
                Value    = value;
                Timespan = timespan;
            }

            public byte[] Value { get; set; }

            public TimeSpan Timespan { get; set; }
        }

        private class MemCacheEntry<T> : MemCacheEntry
        {
            public MemCacheEntry(T value, TimeSpan timespan = new TimeSpan()) :
                base(AHelper.Binary.Serialize(value), timespan) { }
        }

        #endregion

        #region Cache management methods

        /// <summary>
        /// Insert a new cache path with the given timeout into the file cache
        /// </summary>
        /// <param name="path">Path to insert</param>
        /// <param name="prefix">Cache prefix (default is "/")</param>
        /// <param name="filter">Cache filter (default is "*.*")</param>
        /// <param name="timeout">Cache timeout (default is 0 - no timeout)</param>
        /// <param name="handler">Cache insert handler (default is 'return cache.Add(key, value, timeout)')</param>
        /// <returns>'true' if the cache path was setup, 'false' if failed to setup the cache path</returns>
        public bool InsertPath(string        path,
                               string        prefix  = "/",
                               string        filter  = "*.*",
                               TimeSpan      timeout = new TimeSpan(),
                               InsertHandler handler = null)
        {
            handler ??= (cache, key, value, timespan) => cache.Add(key, value, timespan);

            // Try to find and remove the previous path
            using (new LockWrite(_lockEx))
            {
                // Try to find the given path
                if (PathsByKey.TryGetValue(path, out var cacheValue))
                {
                    // Stop the file system watcher
                    cacheValue.StopWatcher();

                    // Remove path entries
                    foreach (var entryKey in EntriesByPath[path]) EntriesByKey.Remove(entryKey);
                    EntriesByPath.Remove(path);

                    // Remove cache path
                    PathsByKey.Remove(path);
                }

                // Add the given path to the cache
                PathsByKey.Add(path, new FileCacheEntry(this, prefix, path, filter, handler, timeout));
                // Create entries by path map
                EntriesByPath[path] = new HashSet<string>();
            }

            // Insert the cache path
            return InsertPathInternal(path, path, prefix, timeout, handler);
        }

        /// <summary>
        /// Try to find the cache path
        /// </summary>
        /// <param name="path">Path to find</param>
        /// <returns>'true' if the cache path was found, 'false' if the given path was not found</returns>
        public bool ContainPath(string path)
        {
            using (new LockRead(_lockEx))
            {
                // Try to find the given key
                return PathsByKey.ContainsKey(path);
            }
        }

        /// <summary>
        /// Remove the cache path from the file cache
        /// </summary>
        /// <param name="path">Path to remove</param>
        /// <returns>'true' if the cache path was removed, 'false' if the given path was not found</returns>
        public bool RemovePath(string path) { return RemovePathInternal(path); }

        /// <summary>
        /// Clear the memory cache
        /// </summary>
        public void Clear()
        {
            using (new LockWrite(_lockEx))
            {
                // Stop all file system watchers
                foreach (var fileCacheEntry in PathsByKey)
                    fileCacheEntry.Value.StopWatcher();

                // Clear all cache entries
                EntriesByKey.Clear();
                EntriesByPath.Clear();
                PathsByKey.Clear();
            }
        }

        #endregion

        #region Internal

        private bool InsertFileInternal(string path, string file, string key, TimeSpan timeout, InsertHandler handler)
        {
            try
            {
                // Load the cache file content
                var content = File.ReadAllBytes(file);
                if (!handler(this, key, content, timeout))
                    return false;
                using (new LockWrite(_lockEx))
                {
                    // Update entries by path map
                    EntriesByPath[path].Add(key);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool RemoveFileInternal(string path, string key)
        {
            try
            {
                using (new LockWrite(_lockEx))
                {
                    // Update entries by path map
                    EntriesByPath[path].Remove(key);
                }

                return Remove(key);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool InsertPathInternal(string        root,
                                        string        path,
                                        string        prefix,
                                        TimeSpan      timeout,
                                        InsertHandler handler)
        {
            try
            {
                var keyPrefix = string.IsNullOrEmpty(prefix) || prefix == "/" ? "/" : prefix + "/";

                // Iterate through all directory entries
                foreach (var item in Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly))
                {
                    var key = string.Concat(keyPrefix, Path.GetDirectoryName(item));

                    // Recursively insert sub-directory
                    if (!InsertPathInternal(root, item, key, timeout, handler))
                        return false;
                }

                foreach (var item in Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly))
                {
                    var key = string.Concat(keyPrefix, Path.GetFileName(item));

                    // Insert file into the cache
                    if (!InsertFileInternal(root, item, key, timeout, handler))
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                CS.WriteLine(e);
                return false;
            }
        }

        private bool RemovePathInternal(string path)
        {
            using (new LockWrite(_lockEx))
            {
                // Try to find the given path
                if (!PathsByKey.TryGetValue(path, out var cacheValue)) return false;

                // Stop the file system watcher
                cacheValue.StopWatcher();

                // Remove path entries
                foreach (var entryKey in EntriesByPath[path]) EntriesByKey.Remove(entryKey);
                EntriesByPath.Remove(path);

                // Remove cache path
                PathsByKey.Remove(path);

                return true;
            }
        }

        #endregion

        #region IDisposable implementation

        // Disposed flag.
        private bool _disposed;

        // Implement IDisposable.
        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposingManagedResources"></param>
        protected virtual void Dispose(bool disposingManagedResources)
        {
            // The idea here is that Dispose(Boolean) knows whether it is
            // being called to do explicit cleanup (the Boolean is true)
            // versus being called due to a garbage collection (the Boolean
            // is false). This distinction is useful because, when being
            // disposed explicitly, the Dispose(Boolean) method can safely
            // execute code using reference type fields that refer to other
            // objects knowing for sure that these other objects have not been
            // finalized or disposed of yet. When the Boolean is false,
            // the Dispose(Boolean) method should not execute code that
            // refer to reference type fields because those objects may
            // have already been finalized."

            if (!_disposed)
            {
                if (disposingManagedResources)
                    // Dispose managed resources here...
                    Clear();

                // Dispose unmanaged resources here...

                // Set large fields to null here...

                // Mark as disposed.
                _disposed = true;
            }
        }

        #endregion
    }
}