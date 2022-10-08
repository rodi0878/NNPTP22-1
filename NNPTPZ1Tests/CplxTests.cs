using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NNPTPZ1;
using Mathematics;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class CplxTests
    {

        [TestMethod()]
        public void AddTest()
        {
            ComplexNumber a = new ComplexNumber()
            {
                RealPart = 10,
                ImaginaryPart = 20
            };
            ComplexNumber b = new ComplexNumber()
            {
                RealPart = 1,
                ImaginaryPart = 2
            };

            ComplexNumber actual = a.Add(b);
            ComplexNumber shouldBe = new ComplexNumber()
            {
                RealPart = 11,
                ImaginaryPart = 22
            };

            Assert.AreEqual(shouldBe, actual);

            var e2 = "(10 + 20i)";
            var r2 = a.ToString();
            Assert.AreEqual(e2, r2);
            e2 = "(1 + 2i)";
            r2 = b.ToString();
            Assert.AreEqual(e2, r2);

            a = new ComplexNumber()
            {
                RealPart = 1,
                ImaginaryPart = -1
            };
            b = new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 };
            shouldBe = new ComplexNumber() { RealPart = 1, ImaginaryPart = -1 };
            actual = a.Add(b);
            Assert.AreEqual(shouldBe, actual);

            e2 = "(1 + -1i)";
            r2 = a.ToString();
            Assert.AreEqual(e2, r2);

            e2 = "(0 + 0i)";
            r2 = b.ToString();
            Assert.AreEqual(e2, r2);
        }

        [TestMethod()]
        public void AddTestPolynome()
        {
            Polynom poly = new Polynom();
            poly.Coefficients.Add(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            poly.Coefficients.Add(new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 });
            poly.Coefficients.Add(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            ComplexNumber result = poly.Evaluate(new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 });
            var expected = new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);
            result = poly.Evaluate(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            expected = new ComplexNumber() { RealPart = 2, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);
            result = poly.Evaluate(new ComplexNumber() { RealPart = 2, ImaginaryPart = 0 });
            expected = new ComplexNumber() { RealPart = 5.0000000000, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);

            var r2 = poly.ToString();
            var e2 = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(e2, r2);
        }
        [TestMethod()]
        public void TestComplexNumbersAreEqual()
        {
            ComplexNumber cx = new ComplexNumber();
            cx.ImaginaryPart = 1;
            cx.RealPart = 2;
            ComplexNumber cy = new ComplexNumber();
            cy.ImaginaryPart = 1;
            cy.RealPart = 2;
            Assert.AreEqual(cx, cy);
        }
        [TestMethod()]
        public void TestComplexNumberDivide()
        {
            ComplexNumber cx = new ComplexNumber();
            cx.ImaginaryPart = 7;
            cx.RealPart = 3;
            ComplexNumber cy = new ComplexNumber();
            cy.ImaginaryPart = 2;
            cy.RealPart = 6;
            ComplexNumber expectedResult = new ComplexNumber();
            expectedResult.ImaginaryPart = (float)(9.0 / 10);
            expectedResult.RealPart = 4.0 / 5;
            Assert.AreEqual(cx.Divide(cy), expectedResult);
        }
        [TestMethod()]
        public void TestPolynomDerivative()
        {
            ComplexNumber cx = new ComplexNumber();
            cx.ImaginaryPart = 0;
            cx.RealPart = 2;
            ComplexNumber cy = new ComplexNumber();
            cy.ImaginaryPart = 0;
            cy.RealPart = 6;
            ComplexNumber cz = new ComplexNumber();
            cz.ImaginaryPart = 0;
            cz.RealPart = 9;
            List<ComplexNumber> coefficients = new List<ComplexNumber> { cz, cy, cx };
            Polynom polynom = new Polynom(coefficients);
            Polynom derivative = polynom.Derive();
            Assert.AreEqual(new ComplexNumber { RealPart = 6, ImaginaryPart = 0 }, derivative.Coefficients[0]);
            Assert.AreEqual(new ComplexNumber { RealPart = 4, ImaginaryPart = 0 }, derivative.Coefficients[1]);
        }
    }
}


