using System;

namespace NNPTPZ1.Fractal {
    public class ArgumentParser {
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public double XMin { get; set; }
        public double YMin { get; set; }
        public double XMax { get; set; }
        public double YMax { get; set; }
        public string ImagePath { get; set; }

        public ArgumentParser(string[] arguments) {
            if (arguments == null)
                throw new ArgumentNullException(nameof(arguments), "Input arguments are null.");

            try {
                ImageWidth = int.Parse(arguments[0]);
                ImageHeight = int.Parse(arguments[1]);
                XMin = double.Parse(arguments[2]);
                XMax = double.Parse(arguments[3]);
                YMin = double.Parse(arguments[4]);
                YMax = double.Parse(arguments[5]);
                ImagePath = arguments[6];
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
    }
}
