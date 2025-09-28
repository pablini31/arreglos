// Archivo: Models/Exercise1Logic.cs
using System;
using System.Linq;
using ArrayExercises.WinForms.Utils;

namespace ArrayExercises.WinForms.Models
{
    /// <summary>
    /// Lógica de negocio para el Ejercicio 1: Contar ceros en matriz
    /// Separar la lógica de la interfaz permite mejor testing y reutilización
    /// </summary>
    public class Exercise1Logic
    {
        /// <summary>
        /// Cuenta la cantidad de ceros en cada fila de una matriz
        /// </summary>
        /// <param name="matrix">Matriz de entrada</param>
        /// <returns>Array con la cantidad de ceros por fila</returns>
        /// <exception cref="ArgumentNullException">Si la matriz es null</exception>
        /// <exception cref="ArgumentException">Si la matriz está vacía</exception>
        public int[] CountZerosPerRow(int[,] matrix)
        {
            ValidateMatrix(matrix);

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int[] zerosPerRow = new int[rows];

            for (int row = 0; row < rows; row++)
            {
                int zeroCount = 0;
                for (int col = 0; col < cols; col++)
                {
                    if (matrix[row, col] == 0)
                    {
                        zeroCount++;
                    }
                }
                zerosPerRow[row] = zeroCount;
            }

            return zerosPerRow;
        }

        /// <summary>
        /// Cuenta el total de ceros en toda la matriz
        /// </summary>
        /// <param name="matrix">Matriz de entrada</param>
        /// <returns>Número total de ceros</returns>
        public int CountTotalZeros(int[,] matrix)
        {
            ValidateMatrix(matrix);

            int totalZeros = 0;
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (matrix[row, col] == 0)
                    {
                        totalZeros++;
                    }
                }
            }

            return totalZeros;
        }

        /// <summary>
        /// Obtiene la fila con mayor cantidad de ceros
        /// </summary>
        /// <param name="matrix">Matriz de entrada</param>
        /// <returns>Índice de la fila (base 0) con más ceros</returns>
        public int GetRowWithMostZeros(int[,] matrix)
        {
            var zerosPerRow = CountZerosPerRow(matrix);
            int maxZeros = zerosPerRow.Max();
            
            for (int i = 0; i < zerosPerRow.Length; i++)
            {
                if (zerosPerRow[i] == maxZeros)
                    return i;
            }

            return 0; // Fallback, nunca debería llegar aquí
        }

        /// <summary>
        /// Obtiene la fila con menor cantidad de ceros
        /// </summary>
        /// <param name="matrix">Matriz de entrada</param>
        /// <returns>Índice de la fila (base 0) con menos ceros</returns>
        public int GetRowWithLeastZeros(int[,] matrix)
        {
            var zerosPerRow = CountZerosPerRow(matrix);
            int minZeros = zerosPerRow.Min();
            
            for (int i = 0; i < zerosPerRow.Length; i++)
            {
                if (zerosPerRow[i] == minZeros)
                    return i;
            }

            return 0; // Fallback, nunca debería llegar aquí
        }

        /// <summary>
        /// Obtiene estadísticas detalladas sobre los ceros en la matriz
        /// </summary>
        /// <param name="matrix">Matriz de entrada</param>
        /// <returns>Objeto con estadísticas detalladas</returns>
        public ZeroAnalysisResult AnalyzeZeros(int[,] matrix)
        {
            ValidateMatrix(matrix);
            
            var zerosPerRow = CountZerosPerRow(matrix);
            var result = new ZeroAnalysisResult
            {
                ZerosPerRow = zerosPerRow,
                TotalZeros = zerosPerRow.Sum(),
                MaxZerosInRow = zerosPerRow.Max(),
                MinZerosInRow = zerosPerRow.Min(),
                RowWithMostZeros = GetRowWithMostZeros(matrix),
                RowWithLeastZeros = GetRowWithLeastZeros(matrix),
                AverageZerosPerRow = zerosPerRow.Average(),
                RowsWithoutZeros = zerosPerRow.Count(count => count == 0),
                RowsWithAllZeros = zerosPerRow.Count(count => count == matrix.GetLength(1))
            };

            return result;
        }

        /// <summary>
        /// Valida que la matriz sea válida para procesamiento
        /// </summary>
        /// <param name="matrix">Matriz a validar</param>
        /// <exception cref="ArgumentNullException">Si la matriz es null</exception>
        /// <exception cref="ArgumentException">Si la matriz está vacía</exception>
        private void ValidateMatrix(int[,] matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix), "La matriz no puede ser null");

            if (matrix.Length == 0)
                throw new ArgumentException("La matriz no puede estar vacía", nameof(matrix));

            if (matrix.GetLength(0) == 0 || matrix.GetLength(1) == 0)
                throw new ArgumentException("La matriz debe tener al menos una fila y una columna", nameof(matrix));
        }
    }

    /// <summary>
    /// Resultado del análisis de ceros en una matriz
    /// </summary>
    public class ZeroAnalysisResult
    {
        /// <summary>
        /// Array con la cantidad de ceros por fila
        /// </summary>
        public int[] ZerosPerRow { get; set; } = new int[0];

        /// <summary>
        /// Total de ceros en la matriz
        /// </summary>
        public int TotalZeros { get; set; }

        /// <summary>
        /// Máxima cantidad de ceros en una fila
        /// </summary>
        public int MaxZerosInRow { get; set; }

        /// <summary>
        /// Mínima cantidad de ceros en una fila
        /// </summary>
        public int MinZerosInRow { get; set; }

        /// <summary>
        /// Índice de la fila con más ceros (base 0)
        /// </summary>
        public int RowWithMostZeros { get; set; }

        /// <summary>
        /// Índice de la fila con menos ceros (base 0)
        /// </summary>
        public int RowWithLeastZeros { get; set; }

        /// <summary>
        /// Promedio de ceros por fila
        /// </summary>
        public double AverageZerosPerRow { get; set; }

        /// <summary>
        /// Número de filas sin ceros
        /// </summary>
        public int RowsWithoutZeros { get; set; }

        /// <summary>
        /// Número de filas con todos ceros
        /// </summary>
        public int RowsWithAllZeros { get; set; }
    }
}