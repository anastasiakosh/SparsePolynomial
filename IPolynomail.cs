// реалізувати окремий проєкт де описуємо сет інструментів котрі використані для наших вже описаних методів ( сума двох поліномів, віднімання одного поліному від іншого, множення до полінома числа та
// додавання до полінома числа, обчислення значення полінома для заданого
// значення змінної, перемноження двох поліномів) 
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace SparsePolynomials
{
    public interface ISparsePolynomial
    {
        void AddTerm(int exponent, double coefficient);
        ISparsePolynomial Add(SparsePolynomial other);
        ISparsePolynomial Subtract(SparsePolynomial other);
        ISparsePolynomial Multiply(double scalar);
        ISparsePolynomial Add(double scalar);
        ISparsePolynomial Multiply(SparsePolynomial other);

        double Evaluate(double x);
    }

    public class SparsePolynomial : ISparsePolynomial
    {
        // Вузол для зберігання коефіцієнта та степеня
        private class Node
        {
            public int Exponent { get; set; }
            public double Coefficient { get; set; }
            public Node Next { get; set; }

            public Node(int exponent, double coefficient)
            {
                Exponent = exponent;
                Coefficient = coefficient;
                Next = null;
            }
        }

        private Node head; // Вказівник на початок списку

        public SparsePolynomial()
        {
            head = null;
        }

        public void AddTerm(int exponent, double coefficient)
        {
            if (Math.Abs(coefficient) < 1e-12)
                return;

            if (head == null)
            {
                head = new Node(exponent, coefficient);
                return;
            }

            Node current = head;
            Node prev = null;

            while (current != null && current.Exponent > exponent)
            {
                prev = current;
                current = current.Next;
            }

            if (current != null && current.Exponent == exponent)
            {
                current.Coefficient += coefficient;
                if (Math.Abs(current.Coefficient) < 1e-12)
                {
                    if (prev == null)
                        head = current.Next;
                    else
                        prev.Next = current.Next;
                }
            }
            else
            {
                Node newNode = new Node(exponent, coefficient);
                if (prev == null)
                {
                    newNode.Next = head;
                    head = newNode;
                }
                else
                {
                    newNode.Next = current;
                    head = newNode;
                }
            }
        }

        public ISparsePolynomial Add(SparsePolynomial other)
        {
            SparsePolynomial result = new SparsePolynomial();

            Node poly1 = this.head;
            Node poly2 = other.head;

            while (poly1 != null || poly2 != null)
            {
                if (poly2 == null || (poly1 != null && poly1.Exponent > poly2.Exponent))
                {
                    result.AddTerm(poly1.Exponent, poly1.Coefficient);
                    poly1 = poly1.Next;
                }
                else if (poly1 == null || poly1.Exponent < poly2.Exponent)
                {
                    result.AddTerm(poly2.Exponent, poly2.Coefficient);
                    poly2 = poly2.Next;
                }
                else
                {
                    result.AddTerm(poly1.Exponent, poly1.Coefficient + poly2.Coefficient);
                    poly1 = poly1.Next;
                    poly2 = poly2.Next;
                }
            }

            return result;
        }

        public ISparsePolynomial Subtract(SparsePolynomial other)
        {
            SparsePolynomial result = new SparsePolynomial();

            Node poly1 = this.head;
            Node poly2 = other.head;

            while (poly1 != null || poly2 != null)
            {
                if (poly2 == null || (poly1 != null && poly1.Exponent > poly2.Exponent))
                {
                    result.AddTerm(poly1.Exponent, poly1.Coefficient);
                    poly1 = poly1.Next;
                }
                else if (poly1 == null || poly1.Exponent < poly2.Exponent)
                {
                    result.AddTerm(poly2.Exponent, -poly2.Coefficient);
                    poly2 = poly2.Next;
                }
                else
                {
                    result.AddTerm(poly1.Exponent, poly1.Coefficient - poly2.Coefficient);
                    poly1 = poly1.Next;
                    poly2 = poly2.Next;
                }
            }

            return result;

        }

        public ISparsePolynomial Multiply(double scalar)
        {
            SparsePolynomial result = new SparsePolynomial();

            if (Math.Abs(scalar) < 1e-12)
                return result;

            Node current = head;
            while (current != null)
            {
                result.AddTerm(current.Exponent, current.Coefficient * scalar);
                current = current.Next;
            }

            return result;
        }

        public ISparsePolynomial Add(double scalar)
        {
            SparsePolynomial result = new SparsePolynomial();

            Node current = head;
            while (current != null)
            {
                result.AddTerm(current.Exponent, current.Coefficient);
                current = current.Next;
            }
            result.AddTerm(0, scalar);

            return result;
        }

        public ISparsePolynomial Multiply(SparsePolynomial other)
        {
            SparsePolynomial result = new SparsePolynomial();

            for (Node poly1 = this.head; poly1 != null; poly1 = poly1.Next)
            {
                for (Node poly2 = other.head; poly2 != null; poly2 = poly2.Next)
                {
                    int newExponent = poly1.Exponent + poly2.Exponent;
                    double newCoefficient = poly1.Coefficient * poly2.Coefficient;
                    result.AddTerm(newExponent, newCoefficient);
                }
            }

            return result;
        }

        public double Evaluate(double x)
        {
            double result = 0;
            Node current = head;

            while (current != null)
            {
                result += current.Coefficient * Math.Pow(x, current.Exponent);
                current = current.Next;
            }

            return result;
        }

        public override string ToString()
        {
            if (head == null)
                return "0";

            StringBuilder sb = new StringBuilder();
            Node current = head;

            while (current != null)
            {
                double coefficient = current.Coefficient;
                int exponent = current.Exponent;

                if (sb.Length > 0)
                {
                    sb.Append(coefficient >= 0 ? " + " : " - ");
                    coefficient = Math.Abs(coefficient);
                }
                else if (coefficient < 0)
                {
                    sb.Append("-");
                    coefficient = Math.Abs(coefficient);
                }

                if (exponent == 0)
                    sb.Append($"{coefficient}");
                else if (exponent == 1)
                    sb.Append($"{coefficient}x");
                else
                    sb.Append($"{coefficient}x^{exponent}");

                current = current.Next;
            }

            return sb.ToString();
        }

    }
}
