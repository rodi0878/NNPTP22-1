using System;
using NNPTPZ1.Fractal;

namespace NNPTPZ1 {
    /// <summary>
    /// This program produces Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program {
        static void Main(string[] args) {

            try {
                ArgumentParser parser = new ArgumentParser(args);
                NewtonFractal fractal = new NewtonFractal(parser);

                fractal.CreateFractalBitmap();
                fractal.SaveResultImage();
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}
