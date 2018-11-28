using ConvertLib;
using System.IO;
using Svg;
using System.Drawing.Imaging;

namespace ConvertSvgToPng
{
    class Program : BaseIOApp
    {
        static void Main(string[] args)
        {
            Init(args);

            var e = new FileEnumerator(inputFolder,"*.svg");

            foreach (var sourceFile in e)
            {
                var targetFile = Path.GetFileNameWithoutExtension(sourceFile);
                var f = Path.GetFileName(sourceFile);
                var bonusFolder = sourceFile.Replace(inputFolder, "").Replace(f, "").Replace(@"\\", "");
                var pngFile = Path.Combine(outputFolder, bonusFolder, targetFile + ".png");

                EnsureFoldersExist(Path.GetDirectoryName(pngFile));

                var svgDocument = SvgDocument.Open(sourceFile);

                using (var bitmap = svgDocument.Draw())
                {
                    bitmap.Save(pngFile, ImageFormat.Png);
                }
            }

            PressEnterToExit();
        }
    }
}
