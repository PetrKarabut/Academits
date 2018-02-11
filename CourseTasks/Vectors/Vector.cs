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
                throw new ArgumentException("размерность вектора меньше единицы", "size");
            }

            components = new double[size];
        }

        public Vector(int size, double[] array) : this(size)
        {
            var minimum = Math.Min(size, array.Length);
            Array.Copy(array, components, minimum);
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

        public void Plus(Vector other)
        {
            var array = components;
            if (Size < other.Size)
            {
                array = new double[other.Size];
                Array.Copy(components, array, Size);
            }

            for (var i = 0; i < other.Size; i++)
            {
                array[i] += other.components[i];
            }

            components = array;
        }

        public void Minus(Vector other)
        {
            var array = components;
            if (Size < other.Size)
            {
                array = new double[other.Size];
                Array.Copy(components, array, Size);
            }

            for (var i = 0; i < other.Size; i++)
            {
                array[i] -= other.components[i];
            }

            components = array;
        }

        public void Multiply(double x)
        {
            for (var i = 0; i < Size; i++)
            {
                components[i] *= x;
            }
        }

        public void Turn()
        {
            Multiply(-1);
        }

        public double Magnitude => Math.Sqrt(Dot(this, this));

        public double GetComponent(int i)
        {
            return components[i];
        }

        public void SetComponent(int i, double value)
        {
            components[i] = value;
        }

        private const double epsilon = double.Epsilon * 100;

        public static bool AreNear(Vector v, Vector w)
        {
            return Difference(v, w).Magnitude < epsilon;
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (Vector)obj;

            if (Size != other.Size)
            {
                return false;
            }

            for (var i = 0; i < Size; i++)
            {
                if (components[i] != other.components[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            const int factor = 100;
            return (int)Math.Truncate(Magnitude * factor);
        }

        public static double Dot(Vector v, Vector w)
        {
            var minimumSize = Math.Min(v.Size, w.Size);

            double dot = 0;
            for (var i = 0; i < minimumSize; i++)
            {
                dot += w.components[i] * v.components[i];
            }

            return dot;
        }

        public static Vector Sum(Vector vector1, Vector vector2)
        {
            var vector = new Vector(Math.Max(vector1.Size, vector2.Size));
            for (var i = 0; i < vector.Size; i++)
            {
                var v = i < vector1.Size ? vector1.components[i] : 0;
                var w = i < vector2.Size ? vector2.components[i] : 0;
                vector.components[i] = v + w;
            }

            return vector;
        }

        public static Vector Difference(Vector vector1, Vector vector2)
        {
            var vector = new Vector(Math.Max(vector1.Size, vector2.Size));
            for (var i = 0; i < vector.Size; i++)
            {
                var v = i < vector1.Size ? vector1.components[i] : 0;
                var w = i < vector2.Size ? vector2.components[i] : 0;
                vector.components[i] = v - w;
            }

            return vector;
        }
    }
}
