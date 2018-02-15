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
        
        private int[] deletedRows;
        private int[] deletedColumns;
        private int remainingSize;

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

        public double Determinant
        {
            get
            {
                if (height != width)
                {
                    throw new ArgumentException("Для вычисления определителя матрица должна быть квадратной.");
                }

                deletedRows = new int[height];
                deletedColumns = new int[width];
                remainingSize = height;
                var factor = 1;
                double determinant = 0;

                for (var i = 0; i < width; i++)
                {
                    determinant += factor * rows[0].GetComponent(i) * GetMinor(0, i);
                    factor *= -1;
                }

                return determinant;
            }
        }

        private double GetMinor(int deletedRow, int deletedColumn)
        {
            deletedRows[deletedRow] = 1;
            deletedColumns[deletedColumn] = 1;
            remainingSize--;

            if (remainingSize == 0)
            {
                deletedRows[deletedRow] = 0;
                deletedColumns[deletedColumn] = 0;

                remainingSize++;
                return 1;
            }

            var upperRow = 0;

            for (var i = 0; i < height; i++)
            {
                if (deletedRows[i] == 0)
                {
                    upperRow = i;
                    break;
                }
            }

            var factor = 1;
            double minor = 0;

            for (var i = 0; i < width; i++)
            {
                if (deletedColumns[i] == 1)
                {
                    continue;
                }

                minor += factor * rows[upperRow].GetComponent(i) * GetMinor(upperRow, i);
                factor *= -1;
            }

            deletedRows[deletedRow] = 0;
            deletedColumns[deletedColumn] = 0;

            remainingSize++;

            return minor;
        }

        public void Plus(Matrix other)
        {
            for (var i = 0; i < height; i++)
            {
                rows[i].Plus(other.rows[i]);
            }
        }

        public void Minus(Matrix other)
        {
            for (var i = 0; i < height; i++)
            {
                rows[i].Minus(other.rows[i]);
            }
        }

        public static Matrix Sum(Matrix matrix1, Matrix matrix2)
        {
            var matrix = new Matrix(matrix1);
            matrix.Plus(matrix2);
            return matrix;
        }

        public static Matrix Difference(Matrix matrix1, Matrix matrix2)
        {
            var matrix = new Matrix(matrix1);
            matrix.Minus(matrix2);
            return matrix;
        }

        public static Matrix Multiplication(Matrix matrix1, Matrix matrix2)
        {
            var matrix = new Matrix(matrix1.width, matrix1.height);
            for (var i = 0; i < matrix1.height; i++)
            {
               matrix.rows[i] = matrix2.GetMultiplicationByRow(matrix1.rows[i]);
            }

            return matrix;
        }
    }
}
