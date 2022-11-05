using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NNPTPZ1;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace NNPTPZ1.Mathematics.Tests {
        [TestClass]
        public class PolynomialTests {

            [TestMethod()]
            public void AddTestPolynome() {
                Polynomial polynom = new Mathematics.Polynomial();
                polynom.Coeficients.Add(new ComplexNumber(1, 0));
                polynom.Coeficients.Add(new ComplexNumber(0, 0));
                polynom.Coeficients.Add(new ComplexNumber(1, 0));

                var resultString = polynom.ToString();
                var expectedString = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";

                Assert.AreEqual(expectedString, resultString);
            }
            [TestMethod()]
            public void EvaluateTestPolynomeOnValueZero() {
                Polynomial polynom = new Mathematics.Polynomial();
                polynom.Coeficients.Add(new ComplexNumber(1, 0));
                polynom.Coeficients.Add(new ComplexNumber(0, 0));
                polynom.Coeficients.Add(new ComplexNumber(1, 0));

                ComplexNumber resultValue = polynom.Evaluate(new ComplexNumber(0, 0));
                var expectedValue = new ComplexNumber(1, 0);

                Assert.AreEqual(expectedValue, resultValue);
            }
            [TestMethod()]
            public void EvaluateTestPolynomeOnValueOne() {
                Polynomial polynom = new Mathematics.Polynomial();
                polynom.Coeficients.Add(new ComplexNumber(1, 0));
                polynom.Coeficients.Add(new ComplexNumber(0, 0));
                polynom.Coeficients.Add(new ComplexNumber(1, 0));

                ComplexNumber resultValue = polynom.Evaluate(new ComplexNumber(1, 0));
                var expectedValue = new ComplexNumber(2, 0);

                Assert.AreEqual(expectedValue, resultValue);
            }
            [TestMethod()]
            public void EvaluateTestPolynomeOnValueTwo() {
                Polynomial polynom = new Mathematics.Polynomial();
                polynom.Coeficients.Add(new ComplexNumber(1, 0));
                polynom.Coeficients.Add(new ComplexNumber(0, 0));
                polynom.Coeficients.Add(new ComplexNumber(1, 0));

                ComplexNumber resultValue = polynom.Evaluate(new ComplexNumber(2, 0));
                var expectedValue = new ComplexNumber(5.0000000000, 0);

                Assert.AreEqual(expectedValue, resultValue);
            }
        }
    }
