using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    class ShapeComparerArea : IComparer<IShape>
    {
        public int Compare(IShape x, IShape y)
        {
            return x.getArea().CompareTo(y.getArea());
        }

        
    }
}
