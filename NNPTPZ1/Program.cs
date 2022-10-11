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
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// This program produces Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            int[] bitmapSize = new int[2];
            for (int i = 0; i < bitmapSize.Length; i++)
            {
                bitmapSize[i] = int.Parse(args[i]);
            }

            double[] coefficients = new double[4];
            for (int i = 0; i < coefficients.Length; i++)
            {
                coefficients[i] = double.Parse(args[i + 2]);
            }

            string output = args[6] ?? "../../../out.png";

            Bitmap bmp = new Bitmap(bitmapSize[0], bitmapSize[1]);
            double xmin = coefficients[0];
            double xmax = coefficients[1];
            double ymin = coefficients[2];
            double ymax = coefficients[3];

            double xstep = (xmax - xmin) / bitmapSize[0];
            double ystep = (ymax - ymin) / bitmapSize[1];

            List<ComplexNumber> koreny = new List<ComplexNumber>();

            Polygon polygon = new Polygon();
            polygon.Coefficient.Add(new ComplexNumber() { RealPart = 1 });
            polygon.Coefficient.Add(ComplexNumber.Zero);
            polygon.Coefficient.Add(ComplexNumber.Zero);
            polygon.Coefficient.Add(new ComplexNumber() { RealPart = 1 });
            
            Polygon derived = polygon.Derive();

            Console.WriteLine(polygon);
            Console.WriteLine(derived);

            Color[] colours = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            // for every pixel in image...
            for (int i = 0; i < bitmapSize[0]; i++)
            {
                for (int j = 0; j < bitmapSize[1]; j++)
                {
                    double x = xmin + i * xstep;
                    double y = ymin + j * ystep;

                    ComplexNumber ox = new ComplexNumber()
                    {
                        RealPart = x,
                        ImaginaryPart = (float)(y)
                    };

                    if (ox.RealPart == 0)
                        ox.RealPart = 0.0001;
                    if (ox.ImaginaryPart == 0)
                        ox.ImaginaryPart = 0.0001f;

                    // find solution of equation using newton's iteration
                    int iteration = 0;
                    for (int k = 0; k< 30; k++)
                    {
                        ComplexNumber difference = polygon.Evaluate(ox).Divide(derived.Evaluate(ox));
                        ox = ox.Subtract(difference);

                        if (Math.Pow(difference.RealPart, 2) + Math.Pow(difference.ImaginaryPart, 2) >= 0.5)
                        {
                            k--;
                        }
                        iteration++;
                    }

                    // find solution root number
                    bool known = false;
                    int id = 0;
                    for (int k = 0; k <koreny.Count;k++)
                    {
                        if (Math.Pow(ox.RealPart- koreny[k].RealPart, 2) + Math.Pow(ox.ImaginaryPart - koreny[k].ImaginaryPart, 2) <= 0.01)
                        {
                            known = true;
                            id = k;
                        }
                    }

                    if (!known)
                    {
                        koreny.Add(ox);
                        id = koreny.Count;
                    }

                    // colorize pixel according to root number
                    Color vv = colours[id % colours.Length];

                    vv = Color.FromArgb(vv.R, vv.G, vv.B);
                    vv = Color.FromArgb(Math.Min(Math.Max(0, vv.R - iteration*2), 255), Math.Min(Math.Max(0, vv.G - iteration*2), 255), Math.Min(Math.Max(0, vv.B - iteration*2), 255));
                    
                    bmp.SetPixel(i, j, vv);
                }
            }

            // TODO: delete I suppose...
            //for (int i = 0; i < 300; i++)
            //{
            //    for (int j = 0; j < 300; j++)
            //    {
            //        Color c = bmp.GetPixel(j, i);
            //        int nv = (int)Math.Floor(c.R * (255.0 / maxid));
            //        bmp.SetPixel(j, i, Color.FromArgb(nv, nv, nv));
            //    }
            //}

            bmp.Save(output);
        }
    }
}
