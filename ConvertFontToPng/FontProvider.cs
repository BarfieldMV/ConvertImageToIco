using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace ConvertFontToPng
{
    class FontProvider
    {
        private static PrivateFontCollection _fontsCache;

        public static Font GetFont(int size)
        {
            if (_fontsCache == null) throw new ArgumentException($"Please setup a font first using {nameof(Initialize)}");
            return new Font(_fontsCache.Families.First(), size + 0.5F);
        }

        public static void Initialize(string fileName)
        {
            if (_fontsCache != null) throw new ArgumentException($"{nameof(Initialize)} can only be called once");

            _fontsCache = new PrivateFontCollection();

            var bytes = File.ReadAllBytes(fileName);
            var memoryPointer = Marshal.AllocCoTaskMem(bytes.Length);
            Marshal.Copy(bytes, 0, memoryPointer, bytes.Length);

            _fontsCache.AddMemoryFont(memoryPointer, bytes.Length);
        }
    }
}