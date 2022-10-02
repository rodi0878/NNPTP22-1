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
            int widthOfImageInPixels = int.Parse(args[0]);
            int heightOfImageInPixels = int.Parse(args[1]);

            double xmin = double.Parse(args[2]);
            double xmax = double.Parse(args[3]);
            double ymin = double.Parse(args[4]);
            double ymax = double.Parse(args[5]);
            string pathToImageFile = args[6];

            Bitmap bitmap = new Bitmap(widthOfImageInPixels, heightOfImageInPixels);
            double xstep = (xmax - xmin) / widthOfImageInPixels;
            double ystep = (ymax - ymin) / heightOfImageInPixels;

            List<ComplexNumber> roots = new List<ComplexNumber>();
            Polynom polynom = new Polynom(new List<ComplexNumber> { new ComplexNumber() { RealPart = 1 },
                ComplexNumber.Zero,
                ComplexNumber.Zero,
            new ComplexNumber() { RealPart = 1 }});
            //p.Coe.Add(Cplx.Zero);
            Polynom polynomDerivative = polynom.Derive();

            Console.WriteLine(polynom);
            Console.WriteLine(polynomDerivative);

            var colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            var maxid = 0;

            // for every pixel in image...
            for (int i = 0; i < widthOfImageInPixels; i++)
            {
                for (int j = 0; j < heightOfImageInPixels; j++)
                {
                    // find "world" coordinates of pixel
                    double y = ymin + i * ystep;
                    double x = xmin + j * xstep;

                    ComplexNumber ox = new ComplexNumber()
                    {
                        RealPart = x,
                        ImaginaryPart = (float)(y)
                    };

                    if (ox.RealPart == 0)
                        ox.RealPart = 0.0001;
                    if (ox.ImaginaryPart == 0)
                        ox.ImaginaryPart = 0.0001f;

                    Console.WriteLine(ox);

                    // find solution of equation using newton's iteration
                    float it = 0;
                    for (int q = 0; q < 30; q++)
                    {
                        var diff = polynom.Evaluate(ox).Divide(polynomDerivative.Evaluate(ox));
                        ox = ox.Subtract(diff);

                        Console.WriteLine($"{q} {ox} -({diff})");
                        if (Math.Pow(diff.RealPart, 2) + Math.Pow(diff.ImaginaryPart, 2) >= 0.5)
                        {
                            q--;
                        }
                        it++;
                    }

                    //Console.ReadKey();

                    // find solution root number
                    var known = false;
                    var id = 0;
                    for (int w = 0; w < roots.Count; w++)
                    {
                        if (Math.Pow(ox.RealPart - roots[w].RealPart, 2) + Math.Pow(ox.ImaginaryPart - roots[w].ImaginaryPart, 2) <= 0.01)
                        {
                            known = true;
                            id = w;
                        }
                    }
                    if (!known)
                    {
                        roots.Add(ox);
                        id = roots.Count;
                        maxid = id + 1;
                    }

                    // colorize pixel according to root number
                    //int vv = id;
                    //int vv = id * 50 + (int)it*5;
                    var vv = colors[id % colors.Length];
                    vv = Color.FromArgb(vv.R, vv.G, vv.B);
                    //  vv = Color.FromArgb(Math.Min(Math.Max(0, vv.R - (int)it * 2), 255), Math.Min(Math.Max(0, vv.G - (int)it * 2), 255), Math.Min(Math.Max(0, vv.B - (int)it * 2), 255));
                    //vv = Math.Min(Math.Max(0, vv), 255);
                    bitmap.SetPixel(i, j, vv);
                    //bmp.SetPixel(j, i, Color.FromArgb(vv, vv, vv));
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

            bitmap.Save(pathToImageFile ?? "../../../out.png");
            // Console.ReadKey();
        }
    }
}
