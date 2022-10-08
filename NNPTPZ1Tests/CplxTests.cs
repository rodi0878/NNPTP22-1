﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class CplxTests
    {

        [TestMethod()]
        public void AddTest()
        {
            ComplexNumber a = new ComplexNumber()
            {
                RealNumber = 10,
                ImaginaryUnit = 20
            };
            ComplexNumber b = new ComplexNumber()
            {
                RealNumber = 1,
                ImaginaryUnit = 2
            };

            ComplexNumber actual = a.Add(b);
            ComplexNumber shouldBe = new ComplexNumber()
            {
                RealNumber = 11,
                ImaginaryUnit = 22
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
                RealNumber = 1,
                ImaginaryUnit = -1
            };
            b = new ComplexNumber() { RealNumber = 0, ImaginaryUnit = 0 };
            shouldBe = new ComplexNumber() { RealNumber = 1, ImaginaryUnit = -1 };
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
            Polynomial poly = new Mathematics.Polynomial();
            poly.Coefficients.Add(new ComplexNumber() { RealNumber = 1, ImaginaryUnit = 0 });
            poly.Coefficients.Add(new ComplexNumber() { RealNumber = 0, ImaginaryUnit = 0 });
            poly.Coefficients.Add(new ComplexNumber() { RealNumber = 1, ImaginaryUnit = 0 });
            ComplexNumber result = poly.Evaluate(new ComplexNumber() { RealNumber = 0, ImaginaryUnit = 0 });
            var expected = new ComplexNumber() { RealNumber = 1, ImaginaryUnit = 0 };
            Assert.AreEqual(expected, result);
            result = poly.Evaluate(new ComplexNumber() { RealNumber = 1, ImaginaryUnit = 0 });
            expected = new ComplexNumber() { RealNumber = 2, ImaginaryUnit = 0 };
            Assert.AreEqual(expected, result);
            result = poly.Evaluate(new ComplexNumber() { RealNumber = 2, ImaginaryUnit = 0 });
            expected = new ComplexNumber() { RealNumber = 5.0000000000, ImaginaryUnit = 0 };
            Assert.AreEqual(expected, result);

            var r2 = poly.ToString();
            var e2 = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(e2, r2);
        }
    }
}


