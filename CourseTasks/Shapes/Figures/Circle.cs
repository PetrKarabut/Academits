using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    class Circle : IShape
    {
        private double radius;

        public Circle(double radius)
        {
            this.radius = radius;
        }

       
        public double GetArea()
        {
            return Math.PI * radius * radius;
        }

        public double GetPerimeter()
        {
            return 2 * Math.PI * radius;
        }

        public double GetWidth()
        {
            return 2 * radius;
        }

        public double GetHeight()
        {
            return 2 * radius;
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

            var other = (Circle)obj;

            return radius == other.radius;
        }

        public override int GetHashCode()
        {
            const int factor = 100;
            return (int)Math.Truncate(GetPerimeter() * factor); 
        }

        public override string ToString()
        {
            return $"Круг: радиус {radius}";
        }
    }
}
