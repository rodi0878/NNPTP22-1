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
    public class ComplexNumberTest
    {

        [TestMethod()]
        public void ComplexNumberAddTest1()
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
            ComplexNumber expected = new ComplexNumber()
            {
                RealPart = 11,
                ImaginaryPart = 22
            };

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ComplexNumberAddTest2()
        {
            ComplexNumber a = new ComplexNumber()
            {
                RealPart = 1,
                ImaginaryPart = -1
            };
            ComplexNumber b = new ComplexNumber()
            {
                RealPart = 1,
                ImaginaryPart = 2
            };
            ComplexNumber expected = new ComplexNumber() { RealPart = 2, ImaginaryPart = 1 };
            ComplexNumber actual = a.Add(b);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ComplexNumberToStringTest1() {
            ComplexNumber a = new ComplexNumber()
            {
                RealPart = 10,
                ImaginaryPart = 20
            };

            string expected = "(10 + 20i)";
            string actual = a.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ComplexNumberToStringTest2()
        {
            ComplexNumber b = new ComplexNumber()
            {
                RealPart = 1,
                ImaginaryPart = 2
            };
            string expected = "(1 + 2i)";
            string actual = b.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ComplexNumberToStringTest3()
        {
            ComplexNumber a = new ComplexNumber()
            {
                RealPart = 1,
                ImaginaryPart = -1
            };
            string expected = "(1 + -1i)";
            string actual = a.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ComplexNumberToStringTest4()
        {
            ComplexNumber b = new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 };

            string expected = "(0 + 0i)";
            string actual = b.ToString();
            Assert.AreEqual(expected, actual);
        }

        private Polygon GetDefaultPolygon()
        {
            Polygon poly = new Polygon();
            poly.Coefficient.Add(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            poly.Coefficient.Add(new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 });
            poly.Coefficient.Add(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            return poly;
        }

        [TestMethod()]
        public void ComplexNumberPolygonAddPolynomeTest()
        {
            Polygon poly = GetDefaultPolygon();
            ComplexNumber result = poly.Evaluate(new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 });
            var expected = new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ComplexNumberPolygonEvaluateTest1()
        {
            Polygon poly = GetDefaultPolygon();
            ComplexNumber result = poly.Evaluate(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            ComplexNumber expected = new ComplexNumber() { RealPart = 2, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ComplexNumberPolygonEvaluateTest2()
        {
            Polygon poly = GetDefaultPolygon();
            ComplexNumber result = poly.Evaluate(new ComplexNumber() { RealPart = 2, ImaginaryPart = 0 });
            ComplexNumber expected = new ComplexNumber() { RealPart = 5.0000000000, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ComplexNumberPolygonToStringTest()
        {
            Polygon poly = GetDefaultPolygon();
            var actual = poly.ToString();
            var expected = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(expected, actual);
        }
    }
}


