using System;
using System.Collections.Generic;
using System.Drawing;
using Mathematics;

namespace NNPTPZ1
{
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    class NewtonFractalProducer
    {
        static void Main(string[] args)
        {
            NewtonFractalToBitmapCreator creator = new NewtonFractalToBitmapCreator(args);

            creator.Draw();

            creator.Save();
        }
    }
}
