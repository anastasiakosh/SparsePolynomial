#nullable enable
using System;
using System.Text;

namespace SparsePolynomials
{
    public class SparsePolynomial : IPolynomial
    {
        private class Node
        {
            public int Exponent { get; set; }
            public double Coefficient { get; set; }
            public Node? Next { get; set; } = null;

            public Node(int exponent, double coefficient)
            {
                Exponent = exponent;
                Coefficient = coefficient;
                Next = null;
            }
        }

        private Node? head = null;

        public SparsePolynomial() { }

        public void AddTerm(int exponent, double coefficient)
        {
            if (Math.Abs(coefficient) < 1e-12) return;

            Node? prev = null;
            Node? current = head;

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
                    if (prev == null) head = current.Next;
                    else prev.Next = current.Next;
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
                    prev.Next = newNode;
                }
            }
        }

        public IPolynomial Add(IPolynomial other)
        {
            if (other is not SparsePolynomial o) throw new ArgumentException("Invalid polynomial type");

            SparsePolynomial result = new SparsePolynomial();
            Node? p1 = head;
            Node? p2 = o.head;

            while (p1 != null || p2 != null)
            {
                if (p2 == null || (p1 != null && p1.Exponent > p2.Exponent))
                {
                    result.AddTerm(p1!.Exponent, p1.Coefficient);
                    p1 = p1.Next;
                }
                else if (p1 == null || p1.Exponent < p2.Exponent)
                {
                    result.AddTerm(p2!.Exponent, p2.Coefficient);
                    p2 = p2.Next;
                }
                else
                {
                    result.AddTerm(p1.Exponent, p1.Coefficient + p2!.Coefficient);
                    p1 = p1.Next;
                    p2 = p2.Next;
                }
            }

            return result;
        }

        public IPolynomial Subtract(IPolynomial other)
        {
            if (other is not SparsePolynomial o) throw new ArgumentException("Invalid polynomial type");

            SparsePolynomial result = new SparsePolynomial();
            Node? p1 = head;
            Node? p2 = o.head;

            while (p1 != null || p2 != null)
            {
                if (p2 == null || (p1 != null && p1.Exponent > p2.Exponent))
                {
                    result.AddTerm(p1!.Exponent, p1.Coefficient);
                    p1 = p1.Next;
                }
                else if (p1 == null || p1.Exponent < p2.Exponent)
                {
                    result.AddTerm(p2!.Exponent, -p2.Coefficient);
                    p2 = p2.Next;
                }
                else
                {
                    result.AddTerm(p1.Exponent, p1.Coefficient - p2!.Coefficient);
                    p1 = p1.Next;
                    p2 = p2.Next;
                }
            }

            return result;
        }

        public IPolynomial Multiply(double scalar)
        {
            SparsePolynomial result = new SparsePolynomial();
            if (Math.Abs(scalar) < 1e-12) return result;

            Node? current = head;
            while (current != null)
            {
                result.AddTerm(current.Exponent, current.Coefficient * scalar);
                current = current.Next;
            }

            return result;
        }

        public IPolynomial Add(double scalar)
        {
            SparsePolynomial result = new SparsePolynomial();
            Node? current = head;
            while (current != null)
            {
                result.AddTerm(current.Exponent, current.Coefficient);
                current = current.Next;
            }
            result.AddTerm(0, scalar);
            return result;
        }

        public IPolynomial Multiply(IPolynomial other)
        {
            if (other is not SparsePolynomial o) throw new ArgumentException("Invalid polynomial type");

            SparsePolynomial result = new SparsePolynomial();
            Node? p1 = head;

            while (p1 != null)
            {
                Node? p2 = o.head;
                while (p2 != null)
                {
                    result.AddTerm(p1.Exponent + p2.Exponent, p1.Coefficient * p2.Coefficient);
                    p2 = p2.Next;
                }
                p1 = p1.Next;
            }

            return result;
        }

        public double Evaluate(double x)
        {
            double result = 0;
            Node? current = head;

            while (current != null)
            {
                result += current.Coefficient * Math.Pow(x, current.Exponent);
                current = current.Next;
            }

            return result;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            Node? current = head;

            while (current != null)
            {
                hash = hash * 31 + current.Exponent.GetHashCode();
                hash = hash * 31 + current.Coefficient.GetHashCode();
                current = current.Next;
            }

            return hash;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not SparsePolynomial other) return false;

            Node? c1 = head;
            Node? c2 = other.head;

            while (c1 != null && c2 != null)
            {
                if (c1.Exponent != c2.Exponent || Math.Abs(c1.Coefficient - c2.Coefficient) > 1e-12)
                    return false;
                c1 = c1.Next;
                c2 = c2.Next;
            }

            return c1 == null && c2 == null;
        }

        public override string ToString()
        {
            if (head == null) return "0";

            StringBuilder sb = new StringBuilder();
            Node? current = head;

            while (current != null)
            {
                double coef = current.Coefficient;
                int exp = current.Exponent;

                if (sb.Length > 0)
                {
                    sb.Append(coef >= 0 ? " + " : " - ");
                    coef = Math.Abs(coef);
                }
                else if (coef < 0)
                {
                    sb.Append("-");
                    coef = Math.Abs(coef);
                }

                if (exp == 0) sb.Append(coef);
                else if (exp == 1) sb.Append($"{coef}x");
                else sb.Append($"{coef}x^{exp}");

                current = current.Next;
            }

            return sb.ToString();
        }
    }
}
