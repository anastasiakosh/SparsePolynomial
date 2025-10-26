//Реалізуємо окремий test project. Перевірка функціональності в тестах. Тестуємо в тестах, а не за допомогою консолі. Hash code check зробити. Тобто тут робимо перевірку наших методів (додавання, віднімання двох поліномів, множення та
//додавання до полінома числа, обчислення значення полінома для заданого
//значення змінної, перемноження поліномів)
using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SparsePolynomials;

namespace SparsePolynomialTests
{
    [TestClass]
    public class SparsePolynomialTest
    {
        [TestMethod]
        public void TestAddition()
        {
            // Тест для перевірки додавання двох поліномів
            SparsePolynomial poly1 = new SparsePolynomial();
            poly1.AddTerm(2, 3.0); // 3x^2
            poly1.AddTerm(0, 1); // +1
            SparsePolynomial poly2 = new SparsePolynomial();
            poly2.AddTerm(2, -3); // -3x^2
            poly2.AddTerm(1, 5); // +5x

            SparsePolynomial expected = new SparsePolynomial();
            expected.AddTerm(1, 5); // 5x
            expected.AddTerm(0, 1); // +1

            SparsePolynomial result = poly1.Add(poly2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestSubtraction()
        {
            // Arrange 
            SparsePolynomial poly1 = new SparsePolynomial();
            poly1.AddTerm(2, 4);
            poly1.AddTerm(1, 3);

            SparsePolynomial poly2 = new SparsePolynomial();
            poly2.AddTerm(1, 1);
            poly2.AddTerm(0, 2);

            SparsePolynomial expected = new SparsePolynomial();
            expected.AddTerm(2, 4);
            expected.AddTerm(1, 2);
            expected.AddTerm(0, -2);
            // Тест для перевірки віднімання двох поліномів
            SparsePolynomial result = poly1.Subtract(poly2);
            Assert.AreEqual(expected.Equals(result));
        }

        [TestMethod]
        public void TestMultiplication()
        {
            // Тест для перевірки множення полінома на число
            SparsePolynomial poly = new SparsePolynomial();
            poly.AddTerm(2, 2);
            poly.AddTerm(1, -3);
            poly.AddTerm(0, 1);

            double scalar = 2.0;
            // Додайте коефіцієнти до poly тут

            SparsePolynomial expected = new SparsePolynomial();
            expected.AddTerm(2, 4);
            expected.AddTerm(1, -6);
            expected.AddTerm(0, 2);

            SparsePolynomial e = poly.Multiply(scalar);
            Assert.AreEqual(expected.Equals(e));
        }

        [TestMethod]
        public void TestSubtraction()
        {
            // Тест для перевірки додавання до полінома числа
            SparsePolynomial poly = new SparsePolynomial();
            poly.AddTerm(2, 2);
            poly.AddTerm(0, 3);

            double constant = 5.0;
            // Додайте коефіцієнти до poly тут

            SparsePolynomial expected = new SparsePolynomial();
            expected.AddTerm(2, 2);
            expected.AddTerm(0, 8);

            // Act
            SparsePolynomial result = poly.Add(constant);

            // Assert
            Assert.AreEqual(expected.Equals(result));
        }

        [TestMethod]
        public void TestEvaluate()
        {
            // Arrange
            SparsePolynomial poly1 = new SparsePolynomial();
            poly1.AddTerm(1, 1);
            poly1.AddTerm(0, 1);

            SparsePolynomial poly2 = new SparsePolynomial();
            poly2.AddTerm(1, 1);
            poly2.AddTerm(0, -1);

            SparsePolynomial expected = new SparsePolynomial();
            expected.AddTerm(2, 1);
            expected.AddTerm(0, -1);

            // Act
            SparsePolynomial result = poly1.Multiply(poly2);
            // Assert
            Assert.AreEqual(expected.Equals(result));
        }

        [TestMethod]
        public void TestEvaluate()
        {
            // Тест для перевірки множення двох поліномів
            SparsePolynomialTest poly = new SparsePolynomialTest();
            poly.AddTerm(2, 2);
            poly.AddTerm(1, 3);
            poly.AddTerm(0, 4);

            double x = 2.0;
            double expected = 2 * Math.Pow(2, 2) + 3 * 2 + 4;

            // Act
            double result = poly.Evaluete(x);

            // Assert
            Assert.AreEqual(expected, result, 1e-9, "Polynomial evaluation failed");
        }

        public void GetHashCode_Test()
        {
            // Arrange
            SparsePolynomial poly1 = new SparsePolynomial();
            poly1.AddTerm(2, 3);
            poly1.AddTerm(0, 1);

            SparsePolynomial poly2 = new SparsePolynomial();
            poly2.AddTerm(2, 3);
            poly2.AddTerm(0, 1);

            // Act
            int hash1 = poly1.GetHashCode();
            int hash2 = poly2.GetHashCode();

            // Assert
            Assert.AreEqual(hash1, hash2, "Hash codes should be equal for identical polynomials");
        }

        public void ToString_Test()
        {
            // Arrange
            SparsePolynomial poly = new SparsePolynomial();
            poly.AddTerm(2, -3);
            poly.AddTerm(1, 2);
            poly.AddTerm(0, 1);

            string expected = "-3x^2 - 2x + 1";

            // Act
            string result = poly.ToString().Trim();

            // Assert
            Assert.AreEqual(expected, result, "ToString method failed");
        }
    }
}