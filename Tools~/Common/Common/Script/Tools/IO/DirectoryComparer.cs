/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2025-10-13
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

#define COMPARE_FILE_CONTENTS
#if COMPARE_FILE_CONTENTS
#define USE_THREADS
#endif

#if USE_THREADS
using System.Threading;
#endif
using System;
using System.Collections.Generic;
using System.IO;

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

namespace AIO.IO
{
    /// <summary>
    /// Compares two directories and finds differences (new, modified, deleted files/folders).
    /// </summary>
    /// <example>https://gist.github.com/yasirkula/5c5f407a7b90450c8804af67113eba70</example>
    public static class DirectoryComparer
    {
        #region Diff Entries

        public abstract class DiffEntry
        {
            public readonly string filename;

            protected DiffEntry(string filename) { this.filename = filename; }

            public abstract          void   Apply(string sourceDirectory, string targetDirectory);
            public abstract override string ToString();
        }

        public class NewFileEntry : DiffEntry
        {
            public NewFileEntry(string filename) : base(filename) { }
            public override string ToString() { return "New file: " + filename; }

            public override void Apply(string sourceDirectory, string targetDirectory) { File.Copy(sourceDirectory + filename, targetDirectory + filename, true); }
        }

        public class ChangedFileEntry : NewFileEntry
        {
            public ChangedFileEntry(string filename) : base(filename) { }
            public override string ToString() { return "Modified file: " + filename; }
        }

        public class NewFolderEntry : DiffEntry
        {
            public NewFolderEntry(string filename) : base(filename) { }
            public override string ToString() { return "New folder: " + filename; }

            public override void Apply(string sourceDirectory, string targetDirectory)
            {
                string newFolderPath = targetDirectory + filename + Path.DirectorySeparatorChar;

                Directory.CreateDirectory(newFolderPath);
                CopyDirectoryRecursive(new DirectoryInfo(sourceDirectory + filename), newFolderPath);
            }

            private void CopyDirectoryRecursive(DirectoryInfo sourceDir, string targetPath)
            {
                var files = sourceDir.GetFiles();
                for (int i = 0; i < files.Length; i++)
                    files[i].CopyTo(targetPath + files[i].Name, true);

                var subDirectories = sourceDir.GetDirectories();
                for (int i = 0; i < subDirectories.Length; i++)
                {
                    string directoryAbsolutePath = targetPath + subDirectories[i].Name + Path.DirectorySeparatorChar;
                    Directory.CreateDirectory(directoryAbsolutePath);
                    CopyDirectoryRecursive(subDirectories[i], directoryAbsolutePath);
                }
            }
        }

        public class DeletedFileEntry : DiffEntry
        {
            public DeletedFileEntry(string filename) : base(filename) { }
            public override string ToString() { return "Deleted file: " + filename; }

            public override void Apply(string sourceDirectory, string targetDirectory) { File.Delete(targetDirectory + filename); }
        }

        public class DeletedFolderEntry : DiffEntry
        {
            public DeletedFolderEntry(string filename) : base(filename) { }
            public override string ToString() { return "Deleted folder: " + filename; }

            public override void Apply(string sourceDirectory, string targetDirectory) { Directory.Delete(targetDirectory + filename, true); }
        }

        #endregion

#if USE_THREADS
        private const int THREAD_COUNT = 4;

        private class ThreadData
        {
            public List<DiffEntry> result;
            public FileInfo        sourceFile, targetFile;
            public string          relativePath;
        }

        private static readonly Thread[]         threads            = new Thread[THREAD_COUNT];
        private static readonly List<ThreadData> pendingTasks       = new List<ThreadData>(64);
        private static          int              runningThreadCount = 0;
#endif

        public static DiffEntry[] FindDifferences(string sourceDirectory, string targetDirectory)
        {
            ProcessInputs(ref sourceDirectory, ref targetDirectory);

#if COMPARE_FILE_CONTENTS && USE_THREADS
            for (int i = 0; i < THREAD_COUNT; i++)
            {
                threads[i] = new Thread(FileComparisonThread);
                threads[i].Start();
            }
#endif

            List<DiffEntry> result = new List<DiffEntry>(32);

#if USE_THREADS
            try
            {
#endif
                CompareDirectoriesRecursive(new DirectoryInfo(sourceDirectory), new DirectoryInfo(targetDirectory), "", result);
#if USE_THREADS
                bool shouldWait;
                do
                {
                    lock (pendingTasks)
                        shouldWait = pendingTasks.Count > 0;

                    if (!shouldWait)
                    {
                        lock (threads)
                            shouldWait = runningThreadCount > 0;
                    }

                    if (shouldWait)
                        Thread.Sleep(100);
                } while (shouldWait);
            }
            finally
            {
                for (int i = 0; i < THREAD_COUNT; i++)
                    threads[i].Abort();
            }
#endif

            return result.ToArray();
        }

        public static void ApplyDifferences(DiffEntry[] diffs, string sourceDirectory, string targetDirectory)
        {
            ProcessInputs(ref sourceDirectory, ref targetDirectory);

            for (int i = 0; i < diffs.Length; i++)
                diffs[i].Apply(sourceDirectory, targetDirectory);
        }

        private static void CompareDirectoriesRecursive(DirectoryInfo sourceDir, DirectoryInfo targetDir, string relativePath, List<DiffEntry> result)
        {
            var sourceFiles = sourceDir.GetFiles();
            var targetFiles = new List<FileInfo>(targetDir.GetFiles());
            for (int i = 0; i < sourceFiles.Length; i++)
            {
                var sourceFile = sourceFiles[i];
                var targetFile = PullEntryWithName(targetFiles, sourceFile.Name);
                if (targetFile == null)
                    result.Add(new NewFileEntry(relativePath + sourceFile.Name));
                else if (sourceFile.Length != targetFile.Length)
                    result.Add(new ChangedFileEntry(relativePath + sourceFile.Name));
#if COMPARE_FILE_CONTENTS
                else
                {
#if USE_THREADS
                    var threadData = new ThreadData()
                    {
                        result       = result,
                        sourceFile   = sourceFile,
                        targetFile   = targetFile,
                        relativePath = relativePath
                    };

                    lock (pendingTasks)
                        pendingTasks.Add(threadData);
#else
				if( !FileContentsAreSame( sourceFile, targetFile ) )
					result.Add( new ChangedFileEntry( relativePath + sourceFile.Name ) );
#endif
                }
#endif
            }

            for (int i = 0; i < targetFiles.Count; i++)
                result.Add(new DeletedFileEntry(relativePath + targetFiles[i].Name));

            var sourceDirectories = sourceDir.GetDirectories();
            var targetDirectories = new List<DirectoryInfo>(targetDir.GetDirectories());
            for (int i = 0; i < sourceDirectories.Length; i++)
            {
                DirectoryInfo sourceDirectory = sourceDirectories[i];
                DirectoryInfo targetDirectory = PullEntryWithName(targetDirectories, sourceDirectory.Name);
                if (targetDirectory == null)
                    result.Add(new NewFolderEntry(relativePath + sourceDirectory.Name));
                else
                    CompareDirectoriesRecursive(sourceDirectory, targetDirectory, relativePath + sourceDirectory.Name + Path.DirectorySeparatorChar, result);
            }

            for (int i = 0; i < targetDirectories.Count; i++)
                result.Add(new DeletedFolderEntry(relativePath + targetDirectories[i].Name));
        }

        private static FileInfo PullEntryWithName(List<FileInfo> entries, string filename)
        {
            for (int i = 0; i < entries.Count; i++)
            {
                if (entries[i].Name == filename)
                {
                    FileInfo result = entries[i];
                    entries.RemoveAt(i);
                    return result;
                }
            }

            return null;
        }

        private static DirectoryInfo PullEntryWithName(List<DirectoryInfo> entries, string filename)
        {
            for (int i = 0; i < entries.Count; i++)
            {
                if (entries[i].Name == filename)
                {
                    DirectoryInfo result = entries[i];
                    entries.RemoveAt(i);
                    return result;
                }
            }

            return null;
        }

#if COMPARE_FILE_CONTENTS
#if USE_THREADS
        private static void FileComparisonThread()
        {
            while (true)
            {
                ThreadData threadData = null;
                lock (pendingTasks)
                {
                    int taskIndex = pendingTasks.Count - 1;
                    if (taskIndex >= 0)
                    {
                        lock (threads)
                            runningThreadCount++;

                        threadData = pendingTasks[taskIndex];
                        pendingTasks.RemoveAt(taskIndex);
                    }
                }

                if (threadData == null)
                    Thread.Sleep(100);
                else
                {
                    if (!FileContentsAreSame(threadData.sourceFile, threadData.targetFile))
                        threadData.result.Add(new ChangedFileEntry(threadData.relativePath + threadData.sourceFile.Name));

                    lock (threads)
                        runningThreadCount--;
                }
            }
        }
#endif

        // Credit: https://stackoverflow.com/a/47422179/2373034
        private static bool FileContentsAreSame(FileInfo file1, FileInfo file2)
        {
            const int FILE_BUFFER_SIZE       = 1024 * 1024;
            const int COMPARISON_BUFFER_SIZE = 1024 * sizeof(Int64);

            using (FileStream stream1 = new FileStream(file1.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, FILE_BUFFER_SIZE))
            {
                using (FileStream stream2 = new FileStream(file2.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, FILE_BUFFER_SIZE))
                {
                    byte[] buffer1 = new byte[COMPARISON_BUFFER_SIZE];
                    byte[] buffer2 = new byte[COMPARISON_BUFFER_SIZE];

                    while (true)
                    {
                        int count1 = ReadFullBuffer(stream1, buffer1);
                        int count2 = ReadFullBuffer(stream2, buffer2);

                        if (count1 != count2)
                            return false;

                        if (count1 == 0)
                            return true;

                        for (int i = 0; i < count1; i += sizeof(Int64))
                        {
                            if (BitConverter.ToInt64(buffer1, i) != BitConverter.ToInt64(buffer2, i))
                                return false;
                        }
                    }
                }
            }
        }

        private static int ReadFullBuffer(Stream stream, byte[] buffer)
        {
            int bytesRead = 0;
            while (bytesRead < buffer.Length)
            {
                int read = stream.Read(buffer, bytesRead, buffer.Length - bytesRead);
                if (read == 0)
                    return bytesRead;

                bytesRead += read;
            }

            return bytesRead;
        }
#endif

        private static void ProcessInputs(ref string sourceDirectory, ref string targetDirectory)
        {
            if (sourceDirectory != null)
                sourceDirectory.Trim();
            if (targetDirectory != null)
                targetDirectory.Trim();

            if (string.IsNullOrEmpty(sourceDirectory))
                throw new ArgumentNullException("sourceDirectory");
            if (string.IsNullOrEmpty(targetDirectory))
                throw new ArgumentNullException("targetDirectory");

            if (!Directory.Exists(sourceDirectory))
                throw new DirectoryNotFoundException(sourceDirectory);
            if (!Directory.Exists(targetDirectory))
                throw new DirectoryNotFoundException(targetDirectory);

            char sourceDirectoryLastCh = sourceDirectory[sourceDirectory.Length - 1];
            char targetDirectoryLastCh = targetDirectory[targetDirectory.Length - 1];
            if (sourceDirectoryLastCh != Path.DirectorySeparatorChar && sourceDirectoryLastCh != Path.AltDirectorySeparatorChar)
                sourceDirectory += Path.DirectorySeparatorChar;
            if (targetDirectoryLastCh != Path.DirectorySeparatorChar && targetDirectoryLastCh != Path.AltDirectorySeparatorChar)
                targetDirectory += Path.DirectorySeparatorChar;
        }
    }
}

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释