using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1;

namespace NNPTPZ1.Mathematics.Tests {
        [TestClass()]
        public class ComplexNumberTests {

            [TestMethod()]
            public void ToStringTestComplexNumberRe0Im0() {
                ComplexNumber complexNumber = new ComplexNumber(0, 0);
                var expectedString = "(0 + 0i)";
                var resultString = complexNumber.ToString();

                Assert.AreEqual(expectedString, resultString);
            }
            [TestMethod()]
            public void ToStringTestComplexNumberRe10Im20() {
                ComplexNumber complexNumber = new ComplexNumber(10, 20);
                var expectedString = "(10 + 20i)";
                var resultString = complexNumber.ToString();

                Assert.AreEqual(expectedString, resultString);
            }
            [TestMethod()]
            public void ToStringTestComplexNumberRe1Im2() {
                ComplexNumber complexNumber = new ComplexNumber(1, 2);
                var expectedString = "(1 + 2i)";
                var resultString = complexNumber.ToString();

                Assert.AreEqual(expectedString, resultString);
            }
            [TestMethod()]
            public void ToStringTestComplexNumberRe1NegativeIm1() {
                ComplexNumber complexNumber = new ComplexNumber(1, -1);
                var expectedString = "(1 + -1i)";
                var resultString = complexNumber.ToString();

                Assert.AreEqual(expectedString, resultString);
            }
            [TestMethod()]
            public void AddTestComplexNumberRe10Im20AndRe1Im2() {
                ComplexNumber complexNumber = new ComplexNumber(10, 20);
                ComplexNumber actualNumber = complexNumber.Add(new ComplexNumber(1, 2));
                ComplexNumber expectedNumber = new ComplexNumber(11, 22);

                Assert.AreEqual(expectedNumber, actualNumber);

            }
            [TestMethod()]
            public void AddTestComplexNumberRe1NegativeIm1AndRe0Im0() {
                ComplexNumber complexNumber = new ComplexNumber(1, -1);
                ComplexNumber actualNumber = complexNumber.Add(new ComplexNumber(0, 0));
                ComplexNumber expectedNumber = new ComplexNumber(1, -1);
                Assert.AreEqual(expectedNumber, actualNumber);

            }
        }
    }


