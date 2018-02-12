using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    class Triangle : IShape
    {
        private double x1;
        private double x2;
        private double x3;
        private double y1;
        private double y2;
        private double y3;

        private const double epsilon = double.Epsilon * 100;

        public Triangle(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.x3 = x3;
            this.y1 = y1;
            this.y1 = y2;
            this.y1 = y3;

            if (Math.Abs((x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1)) < epsilon)
            {
                throw new ArgumentException("Все точки лежат на одной прямой");
            }
        }

        private double side1 => magnitude(x1, y1, x2, y2);
        private double side2 => magnitude(x1, y1, x3, y3);
        private double side3 => magnitude(x2, y2, x3, y3);

        private double magnitude(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }

        public double GetArea()
        {
            var p = GetPerimeter() / 2;
            return Math.Sqrt(p * (p - side1) * (p - side2) * (p - side3));
        }

        public double GetPerimeter()
        {
            return side1 + side2 + side3;
        }

        public double GetWidth()
        {
            return Math.Max(x1, Math.Max(x2, x3)) - Math.Min(x1, Math.Min(x2, x3));
        }

        public double GetHeight()
        {
            return Math.Max(y1, Math.Max(y2, y3)) - Math.Min(y1, Math.Min(y2, y3));
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

            var other = (Triangle)obj;

            return side1 == other.side1 && side2 == other.side2 && side3 == other.side3;
        }

        public override int GetHashCode()
        {
            const int factor = 100;
            return (int)Math.Truncate(GetPerimeter() * factor);
        }

        public override string ToString()
        {
            return $"Треугольник, стороны: {side1}, {side2}, {side3}";
        }
    }
}
