using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vectors;





namespace Matrixes
{
    class Matrix
    {
        private Vector[] rows;

        public Matrix(int width, int height)
        {
            rows = new Vector[height];
            for (var i = 0; i < height; i++)
            {
                rows[i] = new Vector(width);
            }
        }

        public Matrix(Matrix other)
        {
            rows = new Vector[other.height];
            for (var i = 0; i < height; i++)
            {
                rows[i] = new Vector(other.rows[i]);
            }
        }

        public Matrix(double[,] array)
        {
            rows = new Vector[array.GetLength(0)];
            for (var i = 0; i < rows.Length; i++)
            {
                rows[i] = new Vector(GetRow(array, i));
            }
        }

        public Matrix(Vector[] vectors)
        {
            rows = new Vector[vectors.Length];
            for (var i = 0; i < rows.Length; i++)
            {
                rows[i] = new Vector(vectors[i]);
            }
        }

                
        public int height => rows.Length;
        public int width => rows[0].Size;

        public static double[] GetRow(double[,] array, int n)
        {
            var arrayRow = new double[array.GetLength(1)];

            for (var i = 0; i < arrayRow.Length; i++)
            {
                arrayRow[i] = array[n, i];
            }

            return arrayRow;
        }

        public override string ToString()
        {
            return "(" + string.Join(";", rows as object[]) + ")";
        }
    }
}
