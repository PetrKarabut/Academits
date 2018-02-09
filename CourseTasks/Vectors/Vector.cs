using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vectors
{
    class Vector
    {
        private double[] components;

        public Vector(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentException();
            }

            components = new double[size];
        }

        public Vector(int size, double[] array) : this(size)
        {
            var minimum = Math.Min(size, array.Length);
            for (var i = 0; i < minimum; i++)
            {
                components[i] = array[i];
            }
        }

        public Vector(double[] array) : this(array.Length, array)
        {
        }

        public Vector(Vector other) : this(other.components)
        {
        }

        public int Size => components.Length;

        public override string ToString()
        {
            return $"({string.Join(";", components)})";
        }

        public Vector Cut(int size)
        {
            return new Vector(size, components);
        }

        public Vector Plus(Vector other)
        {
            var maximumSize = Math.Max(Size, other.Size);

            var vector1 = Cut(maximumSize);
            var vector2 = other.Cut(maximumSize);

            for (var i = 0; i < maximumSize; i++)
            {
                vector1.components[i] += vector2.components[i];
            }

            return vector1;
        }

        public Vector Minus(Vector other)
        {
            var maximumSize = Math.Max(Size, other.Size);

            var vector1 = Cut(maximumSize);
            var vector2 = other.Cut(maximumSize);

            for (var i = 0; i < maximumSize; i++)
            {
                vector1.components[i] -= vector2.components[i];
            }

            return vector1;
        }

        public Vector Multiply(double x)
        {
            var vector = new Vector(this);

            for (var i = 0; i < Size; i++)
            {
                vector.components[i] *= x;
            }

            return vector;
        }

        public Vector Opposite => Multiply(-1);

        public double Magnitude => Math.Sqrt(Dot(this, this));

        public double GetComponent(int i)
        {
            return components[i];
        }

        public void SetComponent(int i, double value)
        {
            components[i] = value;
        }

        public const double epsilon = double.Epsilon * 100;

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var other = obj as Vector;
            if (other == null)
            {
                return false;
            }

            return Size == other.Size && Minus(other).Magnitude < epsilon;
        }

        public override int GetHashCode()
        {
            return Size;
        }

        public static double Dot(Vector v, Vector w)
        {
            var maximumSize = Math.Max(v.Size, w.Size);

            var vector1 = v.Cut(maximumSize);
            var vector2 = w.Cut(maximumSize);

            double dot = 0;
            for (var i = 0; i < maximumSize; i++)
            {
                dot += vector1.components[i] * vector2.components[i];
            }

            return dot;
        }

        public static Vector Sum(Vector vector1, Vector vector2)
        {
            return vector1.Plus(vector2);
        }

        public static Vector Difference(Vector vector1, Vector vector2)
        {
            return vector1.Minus(vector2);
        }
    }
}
