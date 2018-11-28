namespace ConvertFontToPng
{
    class Program
    {
        static void Main(string[] args)
        {
            FontToPngConverter.ConvertFontToPng(@"C:\SVN\Font Awesome 5 Free-Regular-400.otf", @"C:\SVN\FA5Output\", @"C:\SVN\faEntries.txt");
        }
    }
}
