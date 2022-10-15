using System;

namespace NNPTPZ1.NewtonFractal
{
    public class ParsedArgumentData
    {
        public int BitmapWidth { get; set; }
        public int BitmapHeight { get; set; }
        public double XMin { get; set; }
        public double XMax { get; set; }
        public double YMin { get; set; }
        public double YMax { get; set; }
        public string FilePath { get; set; }

        public ParsedArgumentData(string[] args)
        {
            if (args is null) throw new ArgumentNullException(nameof(args));

            try
            {
                BitmapWidth = int.Parse(args[0]);
                BitmapHeight = int.Parse(args[1]);
                XMin = double.Parse(args[2]);
                XMax = double.Parse(args[3]);
                YMin = double.Parse(args[4]);
                YMax = double.Parse(args[5]);
                FilePath = args[6];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}