using Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace NNPTPZ1
{
    class Fractal
    {
        private static readonly string DefaultFolderPath = "../../../out.png";
        private Bitmap bitmap { get; set; }
        private Polynomial polynomial { get; set; }
        private Polynomial polynomDerivation { get; set; }
        private ComplexNumber complexNumber { get; set; }
        private List<ComplexNumber> roots { get; set; }
        private Dictionary<string, double> parameters { get; set; }
        private string output { get; set; }
        private Color[] colors { get; set; }

        public Fractal(string[] arguments)
        {
            roots = new List<ComplexNumber>();
            colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            CreatePolynomials();
            AddParameters(arguments);
            CreateBitmap();
        }

        public void CreatePolynomials()
        {
            polynomial = new Polynomial();

            polynomial.ListOfComplexNumbers.Add(new ComplexNumber() { RealValue = 1 });
            polynomial.ListOfComplexNumbers.Add(ComplexNumber.SetPropertiesZero);
            polynomial.ListOfComplexNumbers.Add(ComplexNumber.SetPropertiesZero);
            polynomial.ListOfComplexNumbers.Add(new ComplexNumber() { RealValue = 1 });
            
            polynomDerivation = polynomial.Derive();
        }

        public void CreateBitmap()
        {
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    FindWorldCoordinatesOfPixel(i, j);     
                    float iteration = FindSolutionOfEquation();
                    int id = FindSolutionOfRootNumber();
                    ColorizePixelAccordingToRootNumber(iteration, id, i, j);
                }
            }
            bitmap.Save(output);
        }

        public void AddParameters(string[] arguments)
        {
            parameters = new Dictionary<string, double>();
            bitmap = new Bitmap(int.Parse(arguments[0]), int.Parse(arguments[1]));

            double xmin = double.Parse(arguments[2]);
            double xmax = double.Parse(arguments[3]);
            double ymin = double.Parse(arguments[4]);
            double ymax = double.Parse(arguments[5]);

            double xstep = (xmax - xmin) / int.Parse(arguments[0]);
            double ystep = (ymax - ymin) / int.Parse(arguments[1]);

            parameters.Add("xmin", xmin);
            parameters.Add("ymin", ymin);
            parameters.Add("xstep", xstep);
            parameters.Add("ystep", ystep);

            output = (arguments.Length == 6 || arguments[6] == null) ? DefaultFolderPath : arguments[6];
        }

        // find solution of equation using newton's iteration
        public float FindSolutionOfEquation()
        {
            float iteration = 0;
            for (int i = 0; i < 30; i++)
            {
                var difference = polynomial.Evaluate(complexNumber).Divide(polynomDerivation.Evaluate(complexNumber));
                complexNumber = complexNumber.Subtract(difference);

                if (Math.Pow(difference.RealValue, 2) + Math.Pow(difference.ImaginaryValue, 2) >= 0.5)
                {
                    i--;
                }
                iteration++;
            }
            return iteration;
        }

        // find solution root number
        public int FindSolutionOfRootNumber()
        {
            var known = false;
            var id = 0;
            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(complexNumber.RealValue - roots[i].RealValue, 2) + Math.Pow(complexNumber.ImaginaryValue - roots[i].ImaginaryValue, 2) <= 0.01)
                {
                    known = true;
                    id = i;
                }
            }
            if (!known)
            {
                roots.Add(complexNumber);
                id = roots.Count;
            }
            return id;
        }

        // colorize pixel according to root number
        public void ColorizePixelAccordingToRootNumber(float iteration, int id, int i, int j)
        {
            Color color = colors[id % colors.Length];
            color = Color.FromArgb(color.R, color.G, color.B);
            color = Color.FromArgb(Math.Min(Math.Max(0, color.R - (int)iteration * 2), 255), Math.Min(Math.Max(0, color.G - (int)iteration * 2), 255), Math.Min(Math.Max(0, color.B - (int)iteration * 2), 255));
            bitmap.SetPixel(j, i, color);
        }

        // find "world" coordinates of pixel
        public void FindWorldCoordinatesOfPixel(int i, int j)
        {
            double y = parameters["ymin"] + i * parameters["ystep"];
            double x = parameters["xmin"] + j * parameters["xstep"];

            complexNumber = new ComplexNumber()
            {
                RealValue = x,
                ImaginaryValue = (float)(y)
            };

            if (complexNumber.RealValue == 0)
                complexNumber.RealValue = 0.0001;
            if (complexNumber.ImaginaryValue == 0)
                complexNumber.ImaginaryValue = 0.0001f;
        }
    
    }
}
