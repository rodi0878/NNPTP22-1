using System;

namespace NNPTPZ1.NewtonFractals
{
    public class Config
    {
        private int _bitmapWidth, _bitmapHeight;
        private double _xMin, _xMax, _yMin, _yMax;
        private string _outputFilename;
        public int BitmapWidth { get => _bitmapWidth; private set => _bitmapWidth = value; }
        public int BitmapHeight { get => _bitmapHeight; private set => _bitmapHeight = value; }
        public double XMin { get => _xMin; private set => _xMin = value; }
        public double XMax { get => _xMax; private set => _xMax = value; }
        public double YMin { get => _yMin; private set => _yMin = value; }
        public double YMax { get => _yMax; private set => _yMax = value; }
        public string OutputFilename { get => _outputFilename; private set => _outputFilename = value; }
        public static Config GetConfigFromArguments(string[] arguments)
        {
            Config config = new Config();
            if (
                arguments.Length > 5
                && int.TryParse(arguments[0], out config._bitmapWidth)
                && int.TryParse(arguments[1], out config._bitmapHeight)
                && double.TryParse(arguments[2], out config._xMin)
                && double.TryParse(arguments[3], out config._xMax)
                && double.TryParse(arguments[4], out config._yMin)
                && double.TryParse(arguments[5], out config._yMax)
            )
            {
                config.OutputFilename = arguments[6] ?? String.Empty;
                return config;
            }
            throw new NewtonFractalsException("Arguments contract violated");
        }
    }
}
