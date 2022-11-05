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
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program {
        static void Main(string[] args) {
            Polynomial polynom = new Polynomial();
            polynom.Add(new ComplexNumber(1, 0));
            polynom.Add(ComplexNumber.Zero);
            polynom.Add(ComplexNumber.Zero);
            polynom.Add(new ComplexNumber(1, 0));

            Point minPoint = new Point(double.Parse(args[2]), Double.Parse(args[4]));
            Point maxPoint = new Point(double.Parse(args[3]), Double.Parse(args[5]));
            Image image = new Image(int.Parse(args[0]), int.Parse(args[1]), args[6]);
            NewtonFractal fractal = new NewtonFractal(minPoint, maxPoint, ref image);
            fractal.SetPolynomial(polynom);
            fractal.RenderNewtonFractal();
            image.saveImage();
        }
    }
}
