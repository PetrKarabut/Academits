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

        public Vector GetVector(int index)
        {
            return new Vector(rows[index]);
        }

        public void SeTVector(int index, Vector vector)
        {
            rows[index] = new Vector(vector);
        }

        public Vector GetVectorColumn(int index)
        {
            var array = new double[height];

            for (var i = 0; i < array.Length; i++)
            {
                array[i] = rows[i].GetComponent(index);
            }

            return new Vector(array);
        }

        public void Transpose()
        {
            var vectors = new Vector[width];

            for (var i = 0; i < vectors.Length; i++)
            {
                vectors[i] = GetVectorColumn(i);
            }

            rows = vectors;
        }

        public void Multiply(double x)
        {
            foreach (var vector in rows)
            {
                vector.Multiply(x);
            }
        }

        public Vector GetMultiplicationByColumn(Vector vector)
        {
            var array = new double[height];

            for (var i = 0; i < array.Length; i++)
            {
                array[i] = Vector.Dot(vector, rows[i]);
            }

            return new Vector(array);
        }

        public Vector GetMultiplicationByRow(Vector vector)
        {
            var array = new double[width];

            for (var i = 0; i < array.Length; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    array[i] += vector.GetComponent(j) * rows[j].GetComponent(i);
                }
            }

            return new Vector(array);
        }
       
    }
}
