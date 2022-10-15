using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    public class FractalGenerator
    {
        private Polynomial polynomial;
        private Polynomial derivatedPolynomial;
        private Color[] colors =
            {
                Color.Red,
                Color.Green,
                Color.Blue,
                Color.Yellow,
                Color.Orange,
                Color.Fuchsia,
                Color.Gold,
                Color.Cyan,
                Color.Magenta
            };
        private Bitmap bitmap;
        private int bitmapHeight;
        private int bitmapWidth;

        private double xMin;
        private double yMin;
        private double xMax;
        private double yMax;

        private double xStep;
        private double yStep;
        //private string outputLocation;
        //private List<ComplexNumber> roots;

        public FractalGenerator(int bitmapWidth, int bitmapHeight, double xMin, double xMax, double yMin, double yMax)
        {
            this.bitmapWidth = bitmapWidth;
            this.bitmapHeight = bitmapHeight;
            this.xMin = xMin;
            this.yMin = yMin;
            this.xMax = xMax;
            this.yMax = yMax;

            polynomial = new Polynomial(new List<ComplexNumber> { new ComplexNumber() { RealPart = 1 }, ComplexNumber.Zero, ComplexNumber.Zero, new ComplexNumber() { RealPart = 1 } });
            derivatedPolynomial = polynomial.Derive();

            xStep = (xMax - xMin) / bitmapWidth;
            yStep = (yMax - yMin) / bitmapHeight;

            bitmap = new Bitmap(bitmapWidth, bitmapHeight);
        }

        public void generateFractalImage(string pathToImageFile)
        {
            createBitmap(polynomial, derivatedPolynomial, xStep, yStep);
            bitmap.Save(pathToImageFile ?? "../../../out.png");
            bitmap.Dispose();
        }

        private void createBitmap(Polynomial polynom, Polynomial polynomDerivative, double xstep, double ystep)
        {
            List<ComplexNumber> roots = new List<ComplexNumber>();
            for (int x = 0; x < bitmapWidth; x++)
            {
                for (int y = 0; y < bitmapHeight; y++)
                {
                    ComplexNumber startingRoot = GenerateStartingRoot(xstep, ystep, x, y);
                    colorizePixel(x, y, CalculateNewtonsIterations(polynom, polynomDerivative, ref startingRoot), findRootInList(roots, startingRoot));
                }
            }
        }

        private ComplexNumber GenerateStartingRoot(double xStep, double yStep, int x, int y)
        {
            double aproximatedX = xMin + x * xStep;
            double aproximatedY = yMin + y * yStep;

            ComplexNumber root = new ComplexNumber()
            {
                RealPart = aproximatedX,
                ImaginaryPart = aproximatedY
            };

            if (root.RealPart == 0)
                root.RealPart = 0.0001;
            if (root.ImaginaryPart == 0)
                root.ImaginaryPart = 0.0001f;
            return root;
        }

        private void colorizePixel(int x, int y, int numberOfTotalIterations, int actualRootPosition)
        {
            Color selectedColor = colors[actualRootPosition % colors.Length];
            selectedColor = Color.FromArgb(Math.Min(Math.Max(0, selectedColor.R - numberOfTotalIterations * 2), 255), Math.Min(Math.Max(0, selectedColor.G - numberOfTotalIterations * 2), 255), Math.Min(Math.Max(0, selectedColor.B - numberOfTotalIterations * 2), 255));
            bitmap.SetPixel(x, y, selectedColor);
        }

        private static int findRootInList(List<ComplexNumber> roots, ComplexNumber functionRoot)
        {
            bool rootFound = false;
            int rootNumber = 0;
            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(functionRoot.RealPart - roots[i].RealPart, 2) + Math.Pow(functionRoot.ImaginaryPart - roots[i].ImaginaryPart, 2) <= 0.01)
                {
                    rootFound = true;
                    rootNumber = i;
                }
            }
            if (!rootFound)
            {
                roots.Add(functionRoot);
                rootNumber = roots.Count;
            }

            return rootNumber;
        }

        private static int CalculateNewtonsIterations(Polynomial polynomial, Polynomial derivatedPolynomial, ref ComplexNumber root)
        {
            int numberOfTotalIterations = 0;
            for (int i = 0; i < 30; i++)
            {
                var result = polynomial.Eval(root).Divide(derivatedPolynomial.Eval(root));
                root = root.Subtract(result);

                if (Math.Pow(result.RealPart, 2) + Math.Pow(result.ImaginaryPart, 2) >= 0.5)
                {
                    i--;
                }
               numberOfTotalIterations++;
            }

            return numberOfTotalIterations;
        }
    }
}
