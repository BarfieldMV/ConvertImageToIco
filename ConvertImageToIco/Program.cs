using ConvertLib;
using System;
using System.IO;
using FreeImageAPI;

namespace ConvertImageToIco
{
    class Program : BaseIOApp
    {
        static void Main(string[] args)
        {
            Init(args);

            var files = new FileEnumerator(inputFolder);

            foreach (var sourceFile in files)
            {
                var targetFile = Path.GetFileNameWithoutExtension(sourceFile);
                string icoFile = Path.Combine(outputFolder, targetFile + ".ico"); //TODO add existing folder? skip existing files?

                FreeImageBitmap fiBitmap = new FreeImageBitmap(sourceFile);
                fiBitmap.Rescale(256, 256, FREE_IMAGE_FILTER.FILTER_BICUBIC);
                fiBitmap.Save(icoFile);
                fiBitmap.Rescale(64, 64, FREE_IMAGE_FILTER.FILTER_BICUBIC);
                fiBitmap.SaveAdd(icoFile);
                fiBitmap.Rescale(48, 48, FREE_IMAGE_FILTER.FILTER_BICUBIC);
                fiBitmap.SaveAdd(icoFile);
                fiBitmap.Rescale(32, 32, FREE_IMAGE_FILTER.FILTER_BICUBIC);
                fiBitmap.SaveAdd(icoFile);
                fiBitmap.Rescale(16, 16, FREE_IMAGE_FILTER.FILTER_BICUBIC);
                fiBitmap.SaveAdd(icoFile);
            }

            PressEnterToExit();
        }
    }
}
