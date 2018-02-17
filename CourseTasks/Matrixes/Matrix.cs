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
            if (height <= 0 || width <= 0)
            {
                throw new ArgumentException("размеры матрицы должны быть больше нуля");
            }

            rows = new Vector[height];
            for (var i = 0; i < height; i++)
            {
                rows[i] = new Vector(width);
            }
        }

        public Matrix(Matrix other)
        {
            rows = new Vector[other.Height];
            for (var i = 0; i < Height; i++)
            {
                rows[i] = new Vector(other.rows[i]);
            }
        }

        public Matrix(double[,] array)
        {
            if (array.GetLength(0) == 0)
            {
                throw new ArgumentException("размеры матрицы должны быть больше нуля");
            }

            rows = new Vector[array.GetLength(0)];
            for (var i = 0; i < rows.Length; i++)
            {
                var arrayRow = new double[array.GetLength(1)];

                for (var j = 0; j < arrayRow.Length; j++)
                {
                    arrayRow[j] = array[i, j];
                }

                rows[i] = new Vector(arrayRow);
            }
        }

        public Matrix(Vector[] vectors)
        {
            if (vectors.Length == 0)
            {
                throw new ArgumentException("размеры матрицы должны быть больше нуля");
            }

            var maximumLength = vectors[0].Size;

            bool sizesDifferent = false;

            foreach (var v in vectors)
            {
                if (v.Size != maximumLength)
                {
                    sizesDifferent = true;
                }
                maximumLength = Math.Max(v.Size, maximumLength);
            }

            if (sizesDifferent)
            {
                var zero = new Vector(maximumLength);
                foreach (var v in vectors)
                {
                    v.Plus(zero);
                }
            }

            rows = new Vector[vectors.Length];
            for (var i = 0; i < rows.Length; i++)
            {
                rows[i] = new Vector(vectors[i]);
            }
        }

        public int Height => rows.Length;
        public int Width => rows[0].Size;

        public override string ToString()
        {
            return "(" + string.Join(";", rows as object[]) + ")";
        }

        public Vector GetRow(int index)
        {
            return new Vector(rows[index]);
        }

        public void SetRow(int index, Vector vector)
        {
            if (Height > 1 && vector.Size != Width)
            {
                throw new ArgumentException("неправильный размер вектора");
            }

            rows[index] = new Vector(vector);
        }

        public Vector GetColumn(int index)
        {
            var array = new double[Height];

            for (var i = 0; i < array.Length; i++)
            {
                array[i] = rows[i].GetComponent(index);
            }

            return new Vector(array);
        }

        public void Transpose()
        {
            var vectors = new Vector[Width];

            for (var i = 0; i < vectors.Length; i++)
            {
                vectors[i] = GetColumn(i);
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
            if (vector.Size != Width)
            {
                throw new ArgumentException("неправильный размер вектора");
            }

            var array = new double[Height];

            for (var i = 0; i < array.Length; i++)
            {
                array[i] = Vector.Dot(vector, rows[i]);
            }

            return new Vector(array);
        }

        public Vector GetMultiplicationByRow(Vector vector)
        {
            if (vector.Size != Height)
            {
                throw new ArgumentException("неправильный размер вектора");
            }

            var array = new double[Width];

            for (var i = 0; i < array.Length; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    array[i] += vector.GetComponent(j) * rows[j].GetComponent(i);
                }
            }

            return new Vector(array);
        }

        public double GetDeterminant()
        {
            if (Height != Width)
            {
                throw new ArgumentException("Для вычисления определителя матрица должна быть квадратной.");
            }

            deletedRows = new int[Height];
            deletedColumns = new int[Width];
            remainingSize = Height;
            var factor = 1;
            double determinant = 0;

            for (var i = 0; i < Width; i++)
            {
                determinant += factor * rows[0].GetComponent(i) * GetMinor(0, i);
                factor *= -1;
            }

            return determinant;
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

            for (var i = 0; i < Height; i++)
            {
                if (deletedRows[i] == 0)
                {
                    upperRow = i;
                    break;
                }
            }

            var factor = 1;
            double minor = 0;

            for (var i = 0; i < Width; i++)
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
            if (Height != other.Height || Width != other.Width)
            {
                throw new ArgumentException("размеры матриц должны совпадать");
            }

            for (var i = 0; i < Height; i++)
            {
                rows[i].Plus(other.rows[i]);
            }
        }

        public void Minus(Matrix other)
        {
            if (Height != other.Height || Width != other.Width)
            {
                throw new ArgumentException("размеры матриц должны совпадать");
            }

            for (var i = 0; i < Height; i++)
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

            var matrix = new Matrix(matrix1.Width, matrix1.Height);
            for (var i = 0; i < matrix1.Height; i++)
            {
                try
                {
                    matrix.rows[i] = matrix2.GetMultiplicationByRow(matrix1.rows[i]);
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException("неправильные размеры перемножаемых матриц");
                }
            }

            return matrix;
        }
    }
}
