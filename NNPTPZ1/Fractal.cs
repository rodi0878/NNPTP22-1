using Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace NNPTPZ1
{
    class Fractal
    {
        private static readonly string DefaultFolderPath = "../../../out.png";
        private Bitmap _bitmap;
        private Polynomial _polynomial;
        private Polynomial _polynomDerivation;
        private ComplexNumber _complexNumber;
        private List<ComplexNumber> _roots;
        private Dictionary<string, double> _parameters;
        private string _output;
        private Color[] _colors;

        public Fractal(string[] arguments)
        {
            _roots = new List<ComplexNumber>();
            _colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            CreatePolynomials();
            AddParameters(arguments);
            CreateBitmap();
        }

        public void CreatePolynomials()
        {
            _polynomial = new Polynomial();

            _polynomial.ListOfComplexNumbers.Add(new ComplexNumber() { RealValue = 1 });
            _polynomial.ListOfComplexNumbers.Add(ComplexNumber.SetPropertiesZero);
            _polynomial.ListOfComplexNumbers.Add(ComplexNumber.SetPropertiesZero);
            _polynomial.ListOfComplexNumbers.Add(new ComplexNumber() { RealValue = 1 });
            
            _polynomDerivation = _polynomial.Derive();
        }

        public void CreateBitmap()
        {
            for (int i = 0; i < _bitmap.Width; i++)
            {
                for (int j = 0; j < _bitmap.Height; j++)
                {
                    FindWorldCoordinatesOfPixel(i, j);     
                    int iteration = FindSolutionOfEquation();
                    int id = FindSolutionOfRootNumber();
                    ColorizePixelAccordingToRootNumber(iteration, id, i, j);
                }
            }
            _bitmap.Save(_output);
        }

        public void AddParameters(string[] arguments)
        {
            _parameters = new Dictionary<string, double>();
            _bitmap = new Bitmap(int.Parse(arguments[0]), int.Parse(arguments[1]));

            double xmin = double.Parse(arguments[2]);
            double xmax = double.Parse(arguments[3]);
            double ymin = double.Parse(arguments[4]);
            double ymax = double.Parse(arguments[5]);

            double xstep = (xmax - xmin) / int.Parse(arguments[0]);
            double ystep = (ymax - ymin) / int.Parse(arguments[1]);

            _parameters.Add("xmin", xmin);
            _parameters.Add("ymin", ymin);
            _parameters.Add("xstep", xstep);
            _parameters.Add("ystep", ystep);

            _output = (arguments.Length == 6 || arguments[6] == null) ? DefaultFolderPath : arguments[6];
        }

        // find solution of equation using newton's iteration
        public int FindSolutionOfEquation()
        {
            int iteration = 0;
            for (int i = 0; i < 30; i++)
            {
                ComplexNumber difference = _polynomial.Evaluate(_complexNumber).Divide(_polynomDerivation.Evaluate(_complexNumber));
                _complexNumber = _complexNumber.Subtract(difference);

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
            var index = 0;
            for (int i = 0; i < _roots.Count; i++)
            {
                if (Math.Pow(_complexNumber.RealValue - _roots[i].RealValue, 2) + Math.Pow(_complexNumber.ImaginaryValue - _roots[i].ImaginaryValue, 2) <= 0.01)
                {
                    known = true;
                    index = i;
                }
            }
            if (!known)
            {
                _roots.Add(_complexNumber);
                index = _roots.Count;
            }
            return index;
        }

        // colorize pixel according to root number
        public void ColorizePixelAccordingToRootNumber(int iteration, int id, int i, int j)
        {
            Color color = _colors[id % _colors.Length];
            color = Color.FromArgb(Math.Min(Math.Max(0, color.R - iteration * 2), 255),
                Math.Min(Math.Max(0, color.G - iteration * 2), 255),
                Math.Min(Math.Max(0, color.B - iteration * 2), 255));
            _bitmap.SetPixel(j, i, color);
        }

        // find "world" coordinates of pixel
        public void FindWorldCoordinatesOfPixel(int i, int j)
        {
            double y = _parameters["ymin"] + i * _parameters["ystep"];
            double x = _parameters["xmin"] + j * _parameters["xstep"];

            _complexNumber = new ComplexNumber()
            {
                RealValue = x,
                ImaginaryValue = y
            };

            if (_complexNumber.RealValue == 0)
                _complexNumber.RealValue = 0.0001;
            if (_complexNumber.ImaginaryValue == 0)
                _complexNumber.ImaginaryValue = 0.0001f;
        }
    
    }
}
