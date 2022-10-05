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
using System.Linq.Expressions;
using System.Threading;
using Mathematics;
using Graphics;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
   static class Program
    {

        static void Main(string[] args)
        {
            int widthOfImageInPixels = int.Parse(args[0]);
            int heightOfImageInPixels = int.Parse(args[1]);

            double xmin = double.Parse(args[2]);
            double xmax = double.Parse(args[3]);
            double ymin = double.Parse(args[4]);
            double ymax = double.Parse(args[5]);
            string pathToImageFile = null;
            if (args.Length == 7)
            {
                pathToImageFile = args[6];
            }
            FractalVisualiser fractalVisualiser = new FractalVisualiser(widthOfImageInPixels, heightOfImageInPixels, xmin, xmax, ymin, ymax);
            fractalVisualiser.createFractalAndSaveToImageFile(pathToImageFile);
        }
    }
}
