// Archivo: Models/Exercise4Logic.cs
using System;

namespace ArrayExercises.WinForms.Models
{
    /// <summary>
    /// Lógica para el Ejercicio 4: Crear una matriz identidad.
    /// </summary>
    public class Exercise4Logic
    {
        /// <summary>
        /// Crea una matriz identidad de un tamaño específico.
        /// </summary>
        /// <param name="size">El número de filas y columnas de la matriz cuadrada.</param>
        /// <returns>Una matriz identidad de tamaño [size, size].</returns>
        public int[,] CreateIdentityMatrix(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentException("El tamaño debe ser mayor que cero.");
            }

            int[,] matrix = new int[size, size];

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    // Si la fila y la columna son iguales, es la diagonal principal
                    if (row == col)
                    {
                        matrix[row, col] = 1;
                    }
                    else
                    {
                        matrix[row, col] = 0;
                    }
                }
            }
            return matrix;
        }
    }
}