// Archivo: Models/Exercise2Logic.cs
using System;
using System.Linq;

namespace ArrayExercises.WinForms.Models
{
    /// <summary>
    /// Lógica de negocio para el Ejercicio 2: Verificador de Cuadrado Mágico
    /// Un cuadrado mágico es una matriz donde la suma de filas, columnas y diagonales es igual
    /// </summary>
    public class Exercise2Logic
    {
        /// <summary>
        /// Verifica si una matriz es un cuadrado mágico
        /// </summary>
        /// <param name="matrix">Matriz a verificar</param>
        /// <returns>Resultado del análisis</returns>
        public MagicSquareResult AnalyzeMagicSquare(int[,] matrix)
        {
            ValidateMatrix(matrix);

            var result = new MagicSquareResult
            {
                Size = matrix.GetLength(0),
                Matrix = matrix
            };

            // Calcular todas las sumas
            result.RowSums = CalculateRowSums(matrix);
            result.ColumnSums = CalculateColumnSums(matrix);
            result.DiagonalSums = CalculateDiagonalSums(matrix);

            // Determinar si es cuadrado mágico
            result.IsMagicSquare = IsValidMagicSquare(result.RowSums, result.ColumnSums, result.DiagonalSums);
            
            if (result.IsMagicSquare)
            {
                result.MagicConstant = result.RowSums[0];
            }

            // Verificar si usa números consecutivos del 1 al n²
            result.UsesConsecutiveNumbers = UsesConsecutiveNumbers(matrix);

            return result;
        }

        /// <summary>
        /// Calcula la suma de cada fila
        /// </summary>
        private int[] CalculateRowSums(int[,] matrix)
        {
            int size = matrix.GetLength(0);
            int[] rowSums = new int[size];

            for (int row = 0; row < size; row++)
            {
                int sum = 0;
                for (int col = 0; col < size; col++)
                {
                    sum += matrix[row, col];
                }
                rowSums[row] = sum;
            }

            return rowSums;
        }

        /// <summary>
        /// Calcula la suma de cada columna
        /// </summary>
        private int[] CalculateColumnSums(int[,] matrix)
        {
            int size = matrix.GetLength(0);
            int[] columnSums = new int[size];

            for (int col = 0; col < size; col++)
            {
                int sum = 0;
                for (int row = 0; row < size; row++)
                {
                    sum += matrix[row, col];
                }
                columnSums[col] = sum;
            }

            return columnSums;
        }

        /// <summary>
        /// Calcula la suma de las diagonales principales
        /// </summary>
        private int[] CalculateDiagonalSums(int[,] matrix)
        {
            int size = matrix.GetLength(0);
            int[] diagonalSums = new int[2]; // [0] = principal, [1] = secundaria

            // Diagonal principal (de arriba-izquierda a abajo-derecha)
            for (int i = 0; i < size; i++)
            {
                diagonalSums[0] += matrix[i, i];
            }

            // Diagonal secundaria (de arriba-derecha a abajo-izquierda)
            for (int i = 0; i < size; i++)
            {
                diagonalSums[1] += matrix[i, size - 1 - i];
            }

            return diagonalSums;
        }

        /// <summary>
        /// Verifica si todas las sumas son iguales (condición de cuadrado mágico)
        /// </summary>
        private bool IsValidMagicSquare(int[] rowSums, int[] columnSums, int[] diagonalSums)
        {
            if (rowSums.Length == 0) return false;

            int expectedSum = rowSums[0];

            // Verificar que todas las filas tengan la misma suma
            if (!rowSums.All(sum => sum == expectedSum)) return false;

            // Verificar que todas las columnas tengan la misma suma
            if (!columnSums.All(sum => sum == expectedSum)) return false;

            // Verificar que ambas diagonales tengan la misma suma
            if (!diagonalSums.All(sum => sum == expectedSum)) return false;

            return true;
        }

        /// <summary>
        /// Verifica si la matriz usa números consecutivos del 1 al n²
        /// </summary>
        private bool UsesConsecutiveNumbers(int[,] matrix)
        {
            int size = matrix.GetLength(0);
            int expectedCount = size * size;

            // Obtener todos los valores de la matriz
            var values = new int[expectedCount];
            int index = 0;
            
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    values[index++] = matrix[row, col];
                }
            }

            // Ordenar y verificar que sean consecutivos del 1 al n²
            Array.Sort(values);

            for (int i = 0; i < expectedCount; i++)
            {
                if (values[i] != i + 1)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Genera un cuadrado mágico de tamaño impar usando el método de Siamese
        /// </summary>
        /// <param name="size">Tamaño del cuadrado (debe ser impar)</param>
        /// <returns>Matriz de cuadrado mágico</returns>
        public int[,] GenerateMagicSquare(int size)
        {
            if (size % 2 == 0 || size < 3)
            {
                throw new ArgumentException("El tamaño debe ser un número impar mayor o igual a 3");
            }

            int[,] magicSquare = new int[size, size];

            // Comenzar en el centro de la primera fila
            int row = 0;
            int col = size / 2;

            for (int num = 1; num <= size * size; num++)
            {
                magicSquare[row, col] = num;

                // Calcular siguiente posición
                int newRow = (row - 1 + size) % size;
                int newCol = (col + 1) % size;

                // Si la posición está ocupada, ir abajo de la posición actual
                if (magicSquare[newRow, newCol] != 0)
                {
                    row = (row + 1) % size;
                }
                else
                {
                    row = newRow;
                    col = newCol;
                }
            }

            return magicSquare;
        }

        /// <summary>
        /// Calcula la constante mágica esperada para un cuadrado mágico de tamaño n
        /// usando números consecutivos del 1 al n²
        /// </summary>
        /// <param name="size">Tamaño del cuadrado</param>
        /// <returns>Constante mágica esperada</returns>
        public int CalculateExpectedMagicConstant(int size)
        {
            return size * (size * size + 1) / 2;
        }

        /// <summary>
        /// Valida que la matriz sea válida para análisis de cuadrado mágico
        /// </summary>
        private void ValidateMatrix(int[,] matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix), "La matriz no puede ser null");

            if (matrix.Length == 0)
                throw new ArgumentException("La matriz no puede estar vacía", nameof(matrix));

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            if (rows != cols)
                throw new ArgumentException("La matriz debe ser cuadrada", nameof(matrix));

            if (rows < 3)
                throw new ArgumentException("La matriz debe ser al menos de 3x3", nameof(matrix));
        }
    }

    /// <summary>
    /// Resultado del análisis de cuadrado mágico
    /// </summary>
    public class MagicSquareResult
    {
        /// <summary>
        /// Tamaño del cuadrado (n x n)
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Matriz analizada
        /// </summary>
        public int[,] Matrix { get; set; } = new int[0, 0];

        /// <summary>
        /// Indica si es un cuadrado mágico válido
        /// </summary>
        public bool IsMagicSquare { get; set; }

        /// <summary>
        /// Constante mágica (suma común) si es un cuadrado mágico
        /// </summary>
        public int MagicConstant { get; set; }

        /// <summary>
        /// Suma de cada fila
        /// </summary>
        public int[] RowSums { get; set; } = new int[0];

        /// <summary>
        /// Suma de cada columna
        /// </summary>
        public int[] ColumnSums { get; set; } = new int[0];

        /// <summary>
        /// Suma de las diagonales [principal, secundaria]
        /// </summary>
        public int[] DiagonalSums { get; set; } = new int[2];

        /// <summary>
        /// Indica si usa números consecutivos del 1 al n²
        /// </summary>
        public bool UsesConsecutiveNumbers { get; set; }

        /// <summary>
        /// Constante mágica esperada para números consecutivos
        /// </summary>
        public int ExpectedMagicConstant => Size * (Size * Size + 1) / 2;
    }
}