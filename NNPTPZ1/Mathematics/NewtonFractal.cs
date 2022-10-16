using System;
using System.Collections.Generic;
using System.Drawing;

namespace Mathematics
{
    public class NewtonFractalToBitmapCreator
    {

        private Color[] Colors
        {
            get { return new Color[]
                    {
                        Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
                    };
            }
        }
        private int WidthOfBitmap { get; set; }
        private int HeightOfBitmap { get; set; }
        private double Xmax { get; set; }
        private double Xmin { get; set; }
        private double Ymax { get; set; }
        private double Ymin { get; set; }
        private double XStep
        {
            get => (Xmax - Xmin) / WidthOfBitmap;
        }
        private double YStep
        {
            get => (Ymax - Ymin) / HeightOfBitmap;
        }
        private Bitmap BitmapToDraw { get; set; }
        private string PathToSaveBitmap { get; set; }

        private List<ComplexNumber> Roots { get; }

        public NewtonFractalToBitmapCreator(string[] inicialisationParameters)
        {
            int pointerForArguments = 0;
            WidthOfBitmap = 512;
            HeightOfBitmap = 512;
            if (inicialisationParameters.Length >= 2)
            {
                try
                {
                    WidthOfBitmap = int.Parse(inicialisationParameters[pointerForArguments++]);
                    HeightOfBitmap = int.Parse(inicialisationParameters[pointerForArguments++]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    pointerForArguments = 0;
                }
            }

            Xmax = 0;
            Xmin = 0;
            Ymax = 0;
            Ymin = 0;

            if (inicialisationParameters.Length >= 6)
            {
                try
                {
                    Xmin = double.Parse(inicialisationParameters[pointerForArguments++]);
                    Xmax = double.Parse(inicialisationParameters[pointerForArguments++]);
                    Ymin = double.Parse(inicialisationParameters[pointerForArguments++]);
                    Ymax = double.Parse(inicialisationParameters[pointerForArguments++]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            PathToSaveBitmap = null;

            if (inicialisationParameters.Length == 7)
            {
                PathToSaveBitmap = inicialisationParameters[pointerForArguments];
            }

            BitmapToDraw = new Bitmap(WidthOfBitmap, HeightOfBitmap);
        }

        public void Draw()
        {

            for (int i = 0; i < WidthOfBitmap; i++)
            {
                for (int j = 0; j < HeightOfBitmap; j++)
                {
                    double x = Xmin + j * XStep;
                    double y = Ymin + i * YStep;

                    ComplexNumber point = new ComplexNumber()
                    {
                        RealNumber = x,
                        ImaginaryUnit = y
                    };

                    if (point.RealNumber == 0)
                        point.RealNumber = 0.0001;
                    if (point.ImaginaryUnit == 0)
                        point.ImaginaryUnit = 0.0001f;

                    int iteration = CalculateIteration(ref point);
                    int rootNumber = CalculateRootNumber(point);
                    
                    Color pixelColor = Colors[rootNumber % Colors.Length];
                    pixelColor = Color.FromArgb(
                        Math.Min(Math.Max(0, pixelColor.R - iteration * 2), 255),
                        Math.Min(Math.Max(0, pixelColor.G - iteration * 2), 255),
                        Math.Min(Math.Max(0, pixelColor.B - iteration * 2), 255)
                    );
                    BitmapToDraw.SetPixel(j, i, pixelColor);
                }
            }
        }

        private int CalculateIteration(ref ComplexNumber point)
        {
            Polynomial polynomials = new Polynomial();
            polynomials.Coefficients.Add(new ComplexNumber() { RealNumber = 1 });
            polynomials.Coefficients.Add(ComplexNumber.Zero);
            polynomials.Coefficients.Add(ComplexNumber.Zero);
            polynomials.Coefficients.Add(new ComplexNumber() { RealNumber = 1 });
            Polynomial derivedPolynomials = polynomials.Derive();

            Console.WriteLine(polynomials);
            Console.WriteLine(derivedPolynomials);

            int iteration = 0;
            for (int i = 0; i < 30; i++)
            {
                ComplexNumber difference = polynomials.Evaluate(point).Divide(derivedPolynomials.Evaluate(point));
                point = point.Subtract(difference);

                if (Math.Pow(difference.RealNumber, 2) + Math.Pow(difference.ImaginaryUnit, 2) >= 0.5)
                {
                    i--;
                }
                iteration++;
            }
            return iteration;
        }

        private int CalculateRootNumber(ComplexNumber point)
        {
            bool known = false;
            int rootNumber = 0;

            for (int i = 0; i < Roots.Count; i++)
            {
                if (Math.Pow(point.RealNumber - Roots[i].RealNumber, 2) + Math.Pow(point.ImaginaryUnit - Roots[i].ImaginaryUnit, 2) <= 0.01)
                {
                    known = true;
                    rootNumber = i;
                }
            }
            if (!known)
            {
                Roots.Add(point);
                rootNumber = Roots.Count;
            }
            return rootNumber;
        }
        public void Save(string path = null)
        {
            if (path is null)
                BitmapToDraw.Save(PathToSaveBitmap ?? "../../../out.png");
            else
                BitmapToDraw.Save(path);
        }
    }
}
