using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace ConvertFontToPng
{
    public class FontToPngConverter
    {
        private static int Size = 512;
        public static void ConvertFontToPng(string inputFile, string outputFolder, string entryFile)
        {
            FontProvider.Initialize(inputFile);

            var lines = File.ReadLines(entryFile);
            var entries = lines.Select(h => Parse(h)).ToList();

            foreach (var letter in entries)
            {
                ConvertToPng(letter.Letter, Path.Combine(outputFolder, letter.Name + ".png"));
            }
        }

        private static FontEntry Parse(string s)
        {
            var parts = s.Split(',');
            return new FontEntry()
            {
                Letter = ((char) Convert.ToInt32(parts[0], 16)).ToString(),
                Name = parts[1]
            };
        }

        public static void ConvertToPng(string letter, string fileName)
        {
            var bitmapBuffer = new Bitmap(Size * 2, Size * 2);
            var bufferGraphics = Graphics.FromImage(bitmapBuffer);
            var font = FontProvider.GetFont(400);
            bufferGraphics.DrawString(letter, font, Brushes.Black, 0, 0);

            bitmapBuffer.Save(fileName);

        }
    }
}