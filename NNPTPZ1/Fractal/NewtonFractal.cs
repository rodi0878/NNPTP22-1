using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1.Fractal {
    public class NewtonFractal {
        private readonly ArgumentParser _parsedArguments;
        private readonly Bitmap _bmpImage;
        private readonly double _xStep;
        private readonly double _yStep;
        private readonly Polynomial _polynomial;
        private readonly Polynomial _derivedPolynomial;
        private readonly List<ComplexNumber> _roots;

        private readonly Color[] _colors = {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan,
            Color.Magenta
        };

        public NewtonFractal(ArgumentParser arguments) {
            _parsedArguments = arguments;

            _bmpImage = new Bitmap(_parsedArguments.ImageWidth, _parsedArguments.ImageHeight);
            _roots = new List<ComplexNumber>();

            _xStep = (_parsedArguments.XMax - _parsedArguments.XMin) / _parsedArguments.ImageWidth;
            _yStep = (_parsedArguments.YMax - _parsedArguments.YMin) / _parsedArguments.ImageHeight;

            _polynomial = new Polynomial() {
                Coefficients = {
                    new ComplexNumber() {RealPart = 1},
                    ComplexNumber.Zero,
                    ComplexNumber.Zero,
                    new ComplexNumber() {RealPart = 1}
                }
            };
            _derivedPolynomial = _polynomial.Derive();

            Console.WriteLine(_polynomial);
            Console.WriteLine(_derivedPolynomial);
        }

        public void CreateFractalBitmap() {
            for (int i = 0; i < _parsedArguments.ImageWidth; i++) {
                for (int j = 0; j < _parsedArguments.ImageHeight; j++) {
                    
                    ComplexNumber complexPoint = new ComplexNumber() {
                        RealPart = _parsedArguments.XMin + j * _xStep,
                        ImaginaryPart = _parsedArguments.YMin + i * _yStep
                    };

                    if (complexPoint.RealPart == 0)
                        complexPoint.RealPart = 0.0001;
                    if (complexPoint.ImaginaryPart == 0)
                        complexPoint.ImaginaryPart = 0.0001f;
                    
                    int iteration = FindNewtonsIteration(ref complexPoint);
                    
                    int rootNumber = FindRootNumber(complexPoint);
                    
                    ColorizePixel(rootNumber, iteration, j, i);
                }
            }
        }

        public void SaveResultImage() {
            _bmpImage.Save(_parsedArguments.ImagePath ?? "../../../out.png");
        }

        private int FindNewtonsIteration(ref ComplexNumber complexPoint) {
            int iteration = 0;
            for (int i = 0; i < 30; i++) {
                ComplexNumber divisionResult = _polynomial.Eval(complexPoint).Divide(_derivedPolynomial.Eval(complexPoint));
                complexPoint = complexPoint.Subtract(divisionResult);

                if (Math.Pow(divisionResult.RealPart, 2) + Math.Pow(divisionResult.ImaginaryPart, 2) >= 0.5) {
                    i--;
                }
                iteration++;
            }
            return iteration;
        }

        private int FindRootNumber(ComplexNumber complexPoint) {
            bool known = false;
            int rootNumber = 0;
            for (int i = 0; i < _roots.Count; i++) {
                if (Math.Pow(complexPoint.RealPart - _roots[i].RealPart, 2) + Math.Pow(complexPoint.ImaginaryPart - _roots[i].ImaginaryPart, 2) <= 0.01) {
                    known = true;
                    rootNumber = i;
                }
            }
            if (!known) {
                _roots.Add(complexPoint);
                rootNumber = _roots.Count;
            }
            return rootNumber;
        }

        private void ColorizePixel(int rootNumber, int iteration, int x, int y) {
            Color selectedColor = _colors[rootNumber % _colors.Length];
            selectedColor = Color.FromArgb(
                Math.Min(Math.Max(0, selectedColor.R - iteration * 2), 255),
                Math.Min(Math.Max(0, selectedColor.G - iteration * 2), 255),
                Math.Min(Math.Max(0, selectedColor.B - iteration * 2), 255));
            _bmpImage.SetPixel(x, y, selectedColor);
        }
    }
}
