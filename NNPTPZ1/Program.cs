using System;
using System.Drawing;
using Mathematics;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO;
using System.Net.Http.Headers;

namespace NNPTPZ1 {
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program {
        static void Main(string[] args) {
            string defaultOutputFilename = "out.png";
            NewtonFractalParameters parameters = null;

            try {
                parameters = new NewtonFractalParameters(args);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return;
            }

            Polynomial polynomial = new Polynomial(new ComplexNumber(2, 0), ComplexNumber.Zero, ComplexNumber.Zero, new ComplexNumber(2, 0));
            NewtonFractal newtonFractal = new NewtonFractal(parameters, polynomial);
            Console.WriteLine(newtonFractal.GetPolynomialAsString());
            Console.WriteLine(newtonFractal.GetDerivedPolynomialAsString());

            Bitmap bitmap = newtonFractal.GenerateAsBitmap();

            string savePath = args.Length < 7 || args[6] is null ? Path.Combine(Directory.GetCurrentDirectory(), defaultOutputFilename) : args[6];
            bitmap.Save(savePath);

            Console.WriteLine("Bitmap was saved to: " + savePath);
            Console.ReadKey();
        }
    }
}
