namespace NNPTPZ1
{
    class Program
    {
        static void Main(string[] args)
        {
            NewtonFractalParameters fractalParameters = new NewtonFractalParameters(args);
            NewtonFractal newtonFractal = new NewtonFractal(fractalParameters);
            newtonFractal.CreateBitmap();
            newtonFractal.SaveBitmap();
        }
    }
}


