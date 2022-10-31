using Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathematics {

    public class NewtonFractalParameters {

        public double Xmin { get; set; }
        public double Xmax { get; set; }
        public double Ymin { get; set; }
        public double Ymax { get; set; }
        public int BitmapWidth { get; set; }
        public int BitmapHeight { get; set; }

        public NewtonFractalParameters() {
        }

        public NewtonFractalParameters(double xmin, double xmax, double ymin, double ymax, int bitmapWidth, int bitmapHeight) {
            Xmin = xmin;
            Xmax = xmax;
            Ymin = ymin;
            Ymax = ymax;
            BitmapWidth = bitmapWidth;
            BitmapHeight = bitmapHeight;
        }

        public NewtonFractalParameters(string xmin, string xmax, string ymin, string ymax, string bitmapWidth, string bitmapHeight) {
            Xmin = double.Parse(xmin);
            Xmax = double.Parse(xmax);
            Ymin = double.Parse(ymin);
            Ymax = double.Parse(ymax);
            BitmapWidth = int.Parse(bitmapWidth);
            BitmapHeight = int.Parse(bitmapHeight);
        }

        public NewtonFractalParameters(string[] parameters) : 
            this(parameters[0], parameters[1], parameters[2], parameters[3], parameters[4], parameters[5]) {
        }
    }
}
