using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Linq.Expressions;
using System.Threading;


namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //int widthOfImageInPixels = int.Parse(args[0]);
            //int heightOfImageInPixels = int.Parse(args[1]);
            //double xmin = double.Parse(args[2]);
            //double xmax = double.Parse(args[3]);
            //double ymin = double.Parse(args[4]);
            //double ymax = double.Parse(args[5]);
            int widthOfImageInPixels = 200;
            int heightOfImageInPixels = 200;
            double xmin = -20;
            double xmax = 20;
            double ymin = -20;
            double ymax = 20;
            string pathToImageFile = "../../../out.png";
            //if (args.Length == 7)
            //{
            //    pathToImageFile = "../../../out.png";
            //}
            FractalGenerator fractalVisualiser = new FractalGenerator(widthOfImageInPixels, heightOfImageInPixels, xmin, xmax, ymin, ymax);
            fractalVisualiser.generateFractalImage(pathToImageFile);
        }
    }
    }

    


