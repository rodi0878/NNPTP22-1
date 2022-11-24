using INPTPZ1;

namespace NNPTPZ1
{

    class Program
    {
        static void Main(string[] args)
        {
            NewtonFractal fractal = new NewtonFractal(args);
            fractal.CreateFractalImage();
            fractal.SaveImage();
        }
    }


}
