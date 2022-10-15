using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NNPTPZ1;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class ComplexNumberTests
    {

        [TestMethod()]
        public void AddTest()
        {
            ComplexNumber baseComplexNumber = new ComplexNumber()
            {
                RealPart = 10,
                ImaginaryPart = 20
            };
            ComplexNumber addedComplexNumber = new ComplexNumber()
            {
                RealPart = 1,
                ImaginaryPart = 2
            };

            ComplexNumber actual = baseComplexNumber.Add(addedComplexNumber);
            ComplexNumber expected = new ComplexNumber()
            {
                RealPart = 11,
                ImaginaryPart = 22
            };

            Assert.AreEqual(expected, actual);

            var expectedString = "(10 + 20i)";
            var actualString = baseComplexNumber.ToString();
            Assert.AreEqual(expectedString, actualString);
            expectedString = "(1 + 2i)";
            actualString = addedComplexNumber.ToString();
            Assert.AreEqual(expectedString, actualString);

            baseComplexNumber = new ComplexNumber()
            {
                RealPart = 1,
                ImaginaryPart = -1
            };
            addedComplexNumber = new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 };
            expected = new ComplexNumber() { RealPart = 1, ImaginaryPart = -1 };
            actual = baseComplexNumber.Add(addedComplexNumber);
            Assert.AreEqual(expected, actual);

            expectedString = "(1 + -1i)";
            actualString = baseComplexNumber.ToString();
            Assert.AreEqual(expectedString, actualString);

            expectedString = "(0 + 0i)";
            actualString = addedComplexNumber.ToString();
            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod()]
        public void AddTestPolynome()
        {
            Polynomial poly = new Polynomial();
            poly.Coefficients.Add(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            poly.Coefficients.Add(new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 });
            poly.Coefficients.Add(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            ComplexNumber result = poly.Eval(new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 });
            var expected = new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);
            result = poly.Eval(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            expected = new ComplexNumber() { RealPart = 2, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);
            result = poly.Eval(new ComplexNumber() { RealPart = 2, ImaginaryPart = 0 });
            expected = new ComplexNumber() { RealPart = 5.0000000000, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);

            var r2 = poly.ToString();
            var e2 = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(e2, r2);
        }
        [TestMethod()]
        public void MultiplyTest()
        {
            ComplexNumber baseComplexNumber = new ComplexNumber()
            {
                RealPart = 10,
                ImaginaryPart = 20
            };
            ComplexNumber multiplyingComplexNumber = new ComplexNumber()
            {
                RealPart = 3,
                ImaginaryPart = 4
            };

            ComplexNumber actual = baseComplexNumber.Multiply(multiplyingComplexNumber);
            ComplexNumber expected = new ComplexNumber()
            {
                RealPart = -50,
                ImaginaryPart = 100
            };
            Assert.AreEqual(expected, actual);
        }
    }
}


