using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ConvertLib
{
    public class FileEnumerator : IEnumerator<string>, IEnumerable<string>
    {
        public string Folder { get; set; }
        public string FileExtension { get; set; } = "*.*";

        public FileEnumerator(string folder)
        {
            Folder = folder;
        }

        public FileEnumerator(string folder, string fileExtension)
        {
            Folder = folder;
            FileExtension = fileExtension;
        }

        //public void EnumerateFilesInFolder(Action<string> a)
        //{
        //    var files = Directory.GetFiles(Folder, FileExtension, SearchOption.AllDirectories);
           
        //    var sw = new Stopwatch();
        //    sw.Start();
        //    double previous = sw.ElapsedMilliseconds - 1111;
        //    string last = files.Last();

        //    foreach (var sourceFile in files)
        //    {
        //        counter++;

        //        a(sourceFile);

        //        var current = sw.ElapsedMilliseconds;
        //        if (current > (previous + 1000) || sourceFile == last)
        //        {
        //            var targetFile = Path.GetFileNameWithoutExtension(sourceFile);
        //            Console.WriteLine($"Written {targetFile} which is {counter} of {files.Length}");
        //            previous = current;
        //            GC.Collect(); //Working with images is heavy
        //        }
        //    }
        //}

        private string[] fileCache = null;
        int counter = -1;
        private Stopwatch sw = new Stopwatch();
        private double previous = 0;
        private string last = string.Empty;

        public void Init()
        {
            if (fileCache == null)
            {
                fileCache = Directory.GetFiles(Folder, FileExtension, SearchOption.AllDirectories);
                sw.Start();
                previous = sw.ElapsedMilliseconds - 1111;
                last = fileCache.Last();
            }
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (counter < (fileCache.Length -1))
            {
                counter++;
                var current = sw.ElapsedMilliseconds;
                if (current > (previous + 500) || Current == last)
                {
                    var targetFile = Path.GetFileNameWithoutExtension(Current);
                    Console.WriteLine($"Written {targetFile} which is {counter} of {fileCache.Length}");
                    previous = current;
                    GC.Collect(); //Working with images is heavy
                }
                return true;
            }

            return false;
        }

        public void Reset()
        {
            counter = 0;
        }

        public string Current => fileCache[counter];

        object IEnumerator.Current => Current;
        public IEnumerator<string> GetEnumerator()
        {
            Init();
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}