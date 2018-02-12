using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    class Rectangle : IShape
    {
        private double aSide;
        private double bSide;

        public Rectangle(double aSide, double bSide)
        {
            this.aSide = aSide;
            this.bSide = bSide;
        }

        public double GetWidth()
        {
            return Math.Max(aSide, bSide);
        }
        public double GetHeight()
        {
            return Math.Min(aSide, bSide);
        }
        public double GetArea()
        {
            return aSide * bSide;
        }
        public double GetPerimeter()
        {
            return 2 * (aSide + bSide);
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

            var other = (Rectangle)obj;

            return aSide == other.aSide && bSide == other.bSide;
        }

        public override int GetHashCode()
        {
            const int factor = 100;
            return (int)Math.Truncate(GetPerimeter() * factor);
        }

        public override string ToString()
        {
            return $"Прямоугольник, стороны: {aSide} и {bSide}";
        }
    }
}
