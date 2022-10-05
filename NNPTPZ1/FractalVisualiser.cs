using Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class FractalVisualiser
    {
        private readonly int widthOfImageInPixels;
        private readonly int heightOfImageInPixels;
        private readonly double xmin;
        private readonly double ymin;
        private readonly double xmax;
        private readonly double ymax;

        private Bitmap bitmap;
        private Color[] colors;

        private static double PRECISION_THRESHOLD_OF_ROOT_APPROXIMATION = 0.5;
        private static double PRECISION_THRESHOLD_FOR_ACTUAL_ROOT = 0.01;

        public FractalVisualiser(int width, int height, double xmin, double xmax, double ymin, double ymax)
        {
            this.widthOfImageInPixels = width;
            this.heightOfImageInPixels = height;
            this.xmin = xmin;
            this.ymin = ymin;
            this.xmax = xmax;
            this.ymax = ymax;
        }

        public void createFractalAndSaveToImageFile(string pathToImageFile)
        {
            Polynom polynom = createBaseFunction();
            Polynom polynomDerivative = polynom.Derive();
            colors = createColorArray();
            double xstep = (xmax - xmin) / widthOfImageInPixels;
            double ystep = (ymax - ymin) / heightOfImageInPixels;
            createBitmap(polynom, polynomDerivative, xstep, ystep);
            bitmap.Save(pathToImageFile ?? "../../../out.png");
            bitmap.Dispose();
        }

        private void createBitmap(Polynom polynom, Polynom polynomDerivative, double xstep, double ystep)
        {
            bitmap = new Bitmap(widthOfImageInPixels, heightOfImageInPixels);
            List<ComplexNumber> roots = new List<ComplexNumber>();
            for (int x = 0; x < widthOfImageInPixels; x++)
            {
                for (int y = 0; y < heightOfImageInPixels; y++)
                {
                    ComplexNumber functionRoot = createFirstApproximationOfRoot(xstep, ystep, x, y);
                    int numberOfTotalIterations = useNewtonsMethodToFindRoot(polynom, polynomDerivative, ref functionRoot);
                    int actualRootPosition = findPositionOfActualRootInArray(roots, functionRoot);
                    colorizePixel(x, y, numberOfTotalIterations, actualRootPosition);
                }
            }
        }

        private ComplexNumber createFirstApproximationOfRoot(double xstep, double ystep, int i, int j)
        {
            double x = xmin + j * xstep;
            double y = ymin + i * ystep;

            ComplexNumber functionRoot = new ComplexNumber()
            {
                RealPart = x,
                ImaginaryPart = (float)(y)
            };

            if (functionRoot.RealPart == 0)
                functionRoot.RealPart = 0.0001;
            if (functionRoot.ImaginaryPart == 0)
                functionRoot.ImaginaryPart = 0.0001f;
            return functionRoot;
        }

        private void colorizePixel(int x, int y, int numberOfTotalIterations, int actualRootPosition)
        {
            var vv = colors[actualRootPosition % colors.Length];
            vv = Color.FromArgb(Math.Min(Math.Max(0, vv.R - numberOfTotalIterations * 2), 255), Math.Min(Math.Max(0, vv.G - numberOfTotalIterations * 2), 255), Math.Min(Math.Max(0, vv.B - numberOfTotalIterations * 2), 255));
            bitmap.SetPixel(x, y, vv);
        }

        private static int findPositionOfActualRootInArray(List<ComplexNumber> roots, ComplexNumber functionRoot)
        {
            var actualRootFound = false;
            var actualRootPosition = -1;
            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(functionRoot.RealPart - roots[i].RealPart, 2) + Math.Pow(functionRoot.ImaginaryPart - roots[i].ImaginaryPart, 2)
                    <= PRECISION_THRESHOLD_FOR_ACTUAL_ROOT)
                {
                    actualRootFound = true;
                    actualRootPosition = i;
                }
            }
            if (!actualRootFound)
            {
                roots.Add(functionRoot);
                actualRootPosition = roots.Count;
            }

            return actualRootPosition;
        }

        private static int useNewtonsMethodToFindRoot(Polynom polynom, Polynom polynomDerivative, ref ComplexNumber functionRoot)
        {
            int numberOfTotalIterations = 0;
            for (int i = 0; i < 30; i++)
            {
                var result = polynom.Evaluate(functionRoot).Divide(polynomDerivative.Evaluate(functionRoot));
                functionRoot = functionRoot.Subtract(result);

                if (Math.Pow(result.RealPart, 2) + Math.Pow(result.ImaginaryPart, 2) >= PRECISION_THRESHOLD_OF_ROOT_APPROXIMATION)
                {
                    i--;
                }
            }

            return numberOfTotalIterations;
        }

        private static Color[] createColorArray()
        {
            return new Color[]
                        {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
                        };
        }

        private static Polynom createBaseFunction()
        {
            return new Polynom(new List<ComplexNumber> { new ComplexNumber() { RealPart = 1 },
                ComplexNumber.Zero,
                ComplexNumber.Zero,
            new ComplexNumber() { RealPart = 1 }});
        }
    }
}
