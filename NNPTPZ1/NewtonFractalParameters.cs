using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    class NewtonFractalParameters
    {
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }

        public double XMin { get; set; }
        public double XMax { get; set; }
        public double YMin { get; set; }
        public double YMax { get; set; }

        public string ImagePath { get; set; }

        public NewtonFractalParameters(string[] args)
        {
            ImageWidth = int.Parse(args[0]);
            ImageHeight = int.Parse(args[1]);

            XMin = double.Parse(args[2]);
            XMax = double.Parse(args[3]);
            YMin = double.Parse(args[4]);
            YMax = double.Parse(args[5]);

            ImagePath = args[6];
        }
    }
}
