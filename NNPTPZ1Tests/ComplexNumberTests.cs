using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1;

namespace NNPTPZ1Tests
{
    [TestClass()]
    public class ComplexNumberTests
    {

        [TestMethod()]
        public void AddTest()
        {
            ComplexNumber firstNumber = new ComplexNumber(10, 20);
            ComplexNumber secondNumber = new ComplexNumber(1, 2);

            ComplexNumber result = firstNumber.Add(secondNumber);
            ComplexNumber shouldBe = new ComplexNumber(11, 22);

            Assert.AreEqual(shouldBe, result);
        }

        [TestMethod()]
        public void ToStringTestComplexNumber()
        {
            ComplexNumber complexNumber = new ComplexNumber(10, 20);
            var expectedString = "(10 + 20i)";
            var resultString = complexNumber.ToString();

            Assert.AreEqual(expectedString, resultString);
        }
    }
}


