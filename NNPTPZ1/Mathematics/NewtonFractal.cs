using Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace Mathematics {

    public class NewtonFractal {

        private const double FractalRootTolerance = 0.01;
        private const int MaxIteration = 30;

        private Color[] _colors;
        private List<ComplexNumber> _roots = new List<ComplexNumber>();
        private Polynomial _polynomial;
        private Polynomial _derivedPolynomial;
        private double _xStep, _yStep;

        public double Xmin { get; set; }

        public double Xmax { get; set; }

        public double Ymin { get; set; }

        public double Ymax { get; set; }

        public int BitmapWidth { get; set; }

        public int BitmapHeight { get; set; }

        public Color[] Colors {
            get { return _colors; }
            set {
                if (value is null) {
                    throw new ArgumentNullException("Array of colors cannot be null.");
                }
                if (value.Length < 1) {
                    throw new ArgumentException("Array of colors cannot be empty.");
                }
                _colors = value;
            }
        }

        public NewtonFractal(double xmin, double xmax, double ymin, double ymax, int bitmapWidth, int bitmapHeight, Polynomial polynomial) {
            Xmin = xmin;
            Xmax = xmax;
            Ymin = ymin;
            Ymax = ymax;
            BitmapWidth = bitmapWidth;
            BitmapHeight = bitmapHeight;
            
            ChangePolynomial(polynomial);

            // Default colors
            Colors = new Color[] { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta };
        }

        public NewtonFractal(double xmin, double xmax, double ymin, double ymax, int bitmapWidth, int bitmapHeight, Polynomial polynomial, Color[] colors) : 
            this(xmin, xmax, ymin, ymax, bitmapWidth, bitmapHeight, polynomial) {
            Colors = colors;
        }

        public NewtonFractal(NewtonFractalParameters parameters, Polynomial polynomial) : 
            this(parameters.Xmin, parameters.Xmax, parameters.Ymin, parameters.Ymax, parameters.BitmapWidth, parameters.BitmapHeight, polynomial) {
        }

        public void ChangePolynomial(Polynomial newPolynomial) {
            _polynomial = newPolynomial ?? throw new ArgumentNullException(nameof(newPolynomial));
            _derivedPolynomial = _polynomial.Derive();
        }

        public Bitmap GenerateAsBitmap(int width, int height) {
            if (width <= 0 || height <= 0) {
                throw new ArgumentException("Width and height cannot be less or equal to zero.");
            }

            Bitmap bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            _xStep = (Xmax - Xmin) / width;
            _yStep = (Ymax - Ymin) / height;

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    bitmap.SetPixel(x, y, ComputePixelColor(x, y, width, height));
                }
            }

            return bitmap;
        }

        public Bitmap GenerateAsBitmap() {
            return GenerateAsBitmap(BitmapWidth, BitmapHeight);
        }

        public string GetPolynomialAsString() { 
            return _polynomial.ToString();
        }

        public string GetDerivedPolynomialAsString() {
            return _derivedPolynomial.ToString();
        }

        private int FindFractalRootNumber(ComplexNumber complexNumber) {
            bool isKnown = false;
            int rootNumber = 0;
            double expr1;
            double expr2;

            for (int i = 0; i < _roots.Count; i++) {
                expr1 = complexNumber.Real - _roots[i].Real;
                expr2 = complexNumber.Imaginary - _roots[i].Imaginary;

                if (expr1 * expr1 + expr2 * expr2 <= FractalRootTolerance) {
                    isKnown = true;
                    rootNumber = i;
                }
            }
            if (!isKnown) {
                _roots.Add(complexNumber);
                rootNumber = _roots.Count;
            }
            return rootNumber;
        }

        private int FindIterationNumber(ref ComplexNumber complexNumber) {
            int iterations = 0;
            double expr1;
            double expr2;

            for (int i = 0; i < MaxIteration; i++) {
                ComplexNumber diff = _polynomial.Evaluate(complexNumber).Divide(_derivedPolynomial.Evaluate(complexNumber));
                complexNumber = complexNumber.Subtract(diff);

                expr1 = diff.Real;
                expr2 = diff.Imaginary;

                if (expr1 * expr1 + expr2 * expr2 > 0.5) {
                    i--;
                }

                iterations++;
            }
            return iterations;
        }

        private ComplexNumber CoordinatesToComplexNumber(double x, double y) {
            return new ComplexNumber(
                x == 0 ? 0.0001 : x,
                y == 0 ? 0.0001 : y
            );
        }

        private Color ComputePixelColor(int x, int y, int width, int height) {
            // Find "world" coordinates of pixel
            double wX = Xmin + x * _xStep;
            double wY = Ymin + y * _yStep;

            ComplexNumber ox = CoordinatesToComplexNumber(wX, wY);
            int iterations = FindIterationNumber(ref ox);
            int fractalNumber = FindFractalRootNumber(ox);

            Color color = _colors[fractalNumber % _colors.Length];
            color = Color.FromArgb(
                Math.Min(Math.Max(0, color.R - iterations * 2), 255),
                Math.Min(Math.Max(0, color.G - iterations * 2), 255),
                Math.Min(Math.Max(0, color.B - iterations * 2), 255));

            return color;
        }
    }
}
