using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.NewtonFractal.Mathematics;

namespace NNPTPZ1.NewtonFractal
{
    public class NewtonFractalGenerator
    {
        private readonly Color[] _colors = {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };
        private readonly ParsedArgumentData _parsedArgumentData;
        private readonly Polynomial _polynomial;
        private readonly Polynomial _derivedPolynomial;
        private readonly Bitmap _bitmapImage;
        private readonly double _xStep;
        private readonly double _yStep;
        private readonly List<ComplexNumber> _roots;

        public NewtonFractalGenerator(ParsedArgumentData parsedArgumentData)
        {
            _parsedArgumentData = parsedArgumentData;

            _polynomial = new Polynomial(new List<ComplexNumber> {
                new ComplexNumber { RealPart = 1 },
                ComplexNumber.Zero,
                ComplexNumber.Zero,
                new ComplexNumber { RealPart = 1 }
            });

            _derivedPolynomial = _polynomial.Derive();

            Console.WriteLine(_polynomial);
            Console.WriteLine(_derivedPolynomial);

            _bitmapImage = new Bitmap(parsedArgumentData.BitmapWidth, parsedArgumentData.BitmapHeight);

            _xStep = (parsedArgumentData.XMax - parsedArgumentData.XMin) / parsedArgumentData.BitmapWidth;
            _yStep = (parsedArgumentData.YMax - parsedArgumentData.YMin) / parsedArgumentData.BitmapHeight;

            _roots = new List<ComplexNumber>();
        }

        public void Generate()
        {
            for (int i = 0; i < _parsedArgumentData.BitmapWidth; i++)
            {
                for (int j = 0; j < _parsedArgumentData.BitmapHeight; j++)
                {
                    ComplexNumber coordinates = new ComplexNumber
                    {
                        RealPart = _parsedArgumentData.XMin + j * _xStep,
                        ImaginaryPart = _parsedArgumentData.YMin + i * _yStep
                    };

                    if (coordinates.RealPart == 0) coordinates.RealPart = 0.0001;
                    if (coordinates.ImaginaryPart == 0) coordinates.ImaginaryPart = 0.0001f;

                    int iteration = FindIterationNumber(ref coordinates);
                    int rootNumber = FindRootNumber(coordinates);
                    Color pixelColor = ColorizePixelByRootNumber(rootNumber, iteration);

                    _bitmapImage.SetPixel(j, i, pixelColor);
                }
            }
            _bitmapImage.Save(_parsedArgumentData.FilePath ?? "../../../out.png");
        }

        private int FindIterationNumber(ref ComplexNumber coordinates)
        {
            int iteration = 0;

            for (int i = 0; i < 30; i++)
            {
                ComplexNumber diff = _polynomial.Evaluate(coordinates).Divide(_derivedPolynomial.Evaluate(coordinates));

                coordinates = coordinates.Subtract(diff);

                if (Math.Pow(diff.RealPart, 2) + Math.Pow(diff.ImaginaryPart, 2) >= 0.5)
                {
                    i--;
                }

                iteration++;
            }

            return iteration;
        }

        private int FindRootNumber(ComplexNumber coordinates)
        {
            bool known = false;
            int rootNumber = 0;

            for (int i = 0; i < _roots.Count; i++)
            {
                if (Math.Pow(coordinates.RealPart - _roots[i].RealPart, 2) + Math.Pow(coordinates.ImaginaryPart - _roots[i].ImaginaryPart, 2) <= 0.01)
                {
                    known = true;
                    rootNumber = i;
                }
            }

            if (!known)
            {
                _roots.Add(coordinates);
                rootNumber = _roots.Count;
            }

            return rootNumber;
        }

        private Color ColorizePixelByRootNumber(int rootNumber, int iteration)
        {
            Color color = _colors[rootNumber % _colors.Length];

            color = Color.FromArgb(
                Math.Min(Math.Max(0, color.R - iteration * 2), 255),
                Math.Min(Math.Max(0, color.G - iteration * 2), 255),
                Math.Min(Math.Max(0, color.B - iteration * 2), 255)
            );

            return color;
        }
    }
}