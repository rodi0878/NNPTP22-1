using System;
using System.Collections.Generic;
using System.Drawing;

namespace Mathematics
{
    public class NewtonFractalToBitmapCreator
    {
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
            List<ComplexNumber> roots = new List<ComplexNumber>();

            Polynomial polynomials = new Polynomial();
            polynomials.Coefficients.Add(new ComplexNumber() { RealNumber = 1 });
            polynomials.Coefficients.Add(ComplexNumber.Zero);
            polynomials.Coefficients.Add(ComplexNumber.Zero);
            polynomials.Coefficients.Add(new ComplexNumber() { RealNumber = 1 });
            Polynomial derivedPolynomials = polynomials.Derive();

            Console.WriteLine(polynomials);
            Console.WriteLine(derivedPolynomials);

            var colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };
            for (int i = 0; i < WidthOfBitmap; i++)
            {
                for (int j = 0; j < HeightOfBitmap; j++)
                {
                    double x = Xmin + j * XStep;
                    double y = Ymin + i * YStep;

                    ComplexNumber Point = new ComplexNumber()
                    {
                        RealNumber = x,
                        ImaginaryUnit = y
                    };

                    if (Point.RealNumber == 0)
                        Point.RealNumber = 0.0001;
                    if (Point.ImaginaryUnit == 0)
                        Point.ImaginaryUnit = 0.0001f;

                    int iteration = 0;
                    for (int k = 0; k < 30; k++)
                    {
                        var difference = polynomials.Evaluate(Point).Divide(derivedPolynomials.Evaluate(Point));
                        Point = Point.Subtract(difference);

                        if (Math.Pow(difference.RealNumber, 2) + Math.Pow(difference.ImaginaryUnit, 2) >= 0.5)
                        {
                            k--;
                        }
                        iteration++;
                    }

                    var known = false;
                    var id = 0;
                    for (int k = 0; k < roots.Count; k++)
                    {
                        if (Math.Pow(Point.RealNumber - roots[k].RealNumber, 2) + Math.Pow(Point.ImaginaryUnit - roots[k].ImaginaryUnit, 2) <= 0.01)
                        {
                            known = true;
                            id = k;
                        }
                    }
                    if (!known)
                    {
                        roots.Add(Point);
                        id = roots.Count;
                    }

                    var pixelColor = colors[id % colors.Length];
                    pixelColor = Color.FromArgb(
                        Math.Min(Math.Max(0, pixelColor.R - (int)iteration * 2), 255),
                        Math.Min(Math.Max(0, pixelColor.G - (int)iteration * 2), 255),
                        Math.Min(Math.Max(0, pixelColor.B - (int)iteration * 2), 255)
                    );
                    BitmapToDraw.SetPixel(j, i, pixelColor);
                }
            }
        }
        public void Save(string path = null)
        {
            if(path is null)
                BitmapToDraw.Save(PathToSaveBitmap ?? "../../../out.png");
            else
                BitmapToDraw.Save(path);
        }
    }
}
