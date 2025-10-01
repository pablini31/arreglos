// Archivo: Models/Exercise5Logic.cs
using System;
using System.Linq;

namespace ArrayExercises.WinForms.Models
{
    public class Exercise5Logic
    {
        private Random random = new Random();
        private const int ROWS = 5;
        private const int COLS = 10;

        public int[,] GenerateRandomMatrix()
        {
            int[,] matrix = new int[ROWS, COLS];
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    matrix[i, j] = random.Next(1, 101); // Números aleatorios entre 1 y 100
                }
            }
            return matrix;
        }

        public MatrixStatisticsResult CalculateStatistics(int[,] matrix)
        {
            var result = new MatrixStatisticsResult
            {
                RowSums = new double[ROWS],
                RowAverages = new double[ROWS],
                ColSums = new double[COLS],
                ColAverages = new double[COLS]
            };

            // Calcular sumas y promedios por fila
            for (int i = 0; i < ROWS; i++)
            {
                int rowSum = 0;
                for (int j = 0; j < COLS; j++)
                {
                    rowSum += matrix[i, j];
                }
                result.RowSums[i] = rowSum;
                result.RowAverages[i] = (double)rowSum / COLS;
            }

            // Calcular sumas y promedios por columna
            for (int j = 0; j < COLS; j++)
            {
                int colSum = 0;
                for (int i = 0; i < ROWS; i++)
                {
                    colSum += matrix[i, j];
                }
                result.ColSums[j] = colSum;
                result.ColAverages[j] = (double)colSum / ROWS;
            }

            return result;
        }
    }

    public class MatrixStatisticsResult
    {
        public double[] RowSums { get; set; }       // Arreglo A
        public double[] RowAverages { get; set; } // Arreglo B
        public double[] ColSums { get; set; }       // Arreglo C
        public double[] ColAverages { get; set; } // Arreglo D
    }
}