// Archivo: Models/Exercise3Logic.cs
using System;

namespace ArrayExercises.WinForms.Models
{
    /// <summary>
    /// Lógica de negocio para el Ejercicio 3: Operaciones con Matrices 2x2
    /// Implementa suma, resta, producto y división elemento por elemento
    /// </summary>
    public class Exercise3Logic
    {
        /// <summary>
        /// Realiza todas las operaciones básicas entre dos matrices 2x2
        /// </summary>
        /// <param name="matrix1">Primera matriz</param>
        /// <param name="matrix2">Segunda matriz</param>
        /// <returns>Resultado con todas las operaciones</returns>
        public MatrixOperationsResult PerformAllOperations(decimal[,] matrix1, decimal[,] matrix2)
        {
            ValidateMatrices(matrix1, matrix2);

            var result = new MatrixOperationsResult
            {
                Matrix1 = matrix1,
                Matrix2 = matrix2,
                Sum = AddMatrices(matrix1, matrix2),
                Difference = SubtractMatrices(matrix1, matrix2),
                Product = MultiplyMatrices(matrix1, matrix2),
                Division = DivideMatrices(matrix1, matrix2)
            };

            return result;
        }

        /// <summary>
        /// Suma dos matrices elemento por elemento
        /// </summary>
        /// <param name="matrix1">Primera matriz</param>
        /// <param name="matrix2">Segunda matriz</param>
        /// <returns>Matriz resultado de la suma</returns>
        public decimal[,] AddMatrices(decimal[,] matrix1, decimal[,] matrix2)
        {
            ValidateMatrices(matrix1, matrix2);

            decimal[,] result = new decimal[2, 2];

            for (int row = 0; row < 2; row++)
            {
                for (int col = 0; col < 2; col++)
                {
                    result[row, col] = matrix1[row, col] + matrix2[row, col];
                }
            }

            return result;
        }

        /// <summary>
        /// Resta dos matrices elemento por elemento (matrix1 - matrix2)
        /// </summary>
        /// <param name="matrix1">Primera matriz (minuendo)</param>
        /// <param name="matrix2">Segunda matriz (sustraendo)</param>
        /// <returns>Matriz resultado de la resta</returns>
        public decimal[,] SubtractMatrices(decimal[,] matrix1, decimal[,] matrix2)
        {
            ValidateMatrices(matrix1, matrix2);

            decimal[,] result = new decimal[2, 2];

            for (int row = 0; row < 2; row++)
            {
                for (int col = 0; col < 2; col++)
                {
                    result[row, col] = matrix1[row, col] - matrix2[row, col];
                }
            }

            return result;
        }

        /// <summary>
        /// Multiplica dos matrices elemento por elemento (Hadamard product)
        /// No es multiplicación matricial tradicional
        /// </summary>
        /// <param name="matrix1">Primera matriz</param>
        /// <param name="matrix2">Segunda matriz</param>
        /// <returns>Matriz resultado del producto elemento por elemento</returns>
        public decimal[,] MultiplyMatrices(decimal[,] matrix1, decimal[,] matrix2)
        {
            ValidateMatrices(matrix1, matrix2);

            decimal[,] result = new decimal[2, 2];

            for (int row = 0; row < 2; row++)
            {
                for (int col = 0; col < 2; col++)
                {
                    result[row, col] = matrix1[row, col] * matrix2[row, col];
                }
            }

            return result;
        }

        /// <summary>
        /// Divide dos matrices elemento por elemento (matrix1 / matrix2)
        /// </summary>
        /// <param name="matrix1">Primera matriz (dividendo)</param>
        /// <param name="matrix2">Segunda matriz (divisor)</param>
        /// <returns>Matriz resultado de la división elemento por elemento</returns>
        /// <exception cref="DivideByZeroException">Si algún elemento del divisor es cero</exception>
        public decimal[,] DivideMatrices(decimal[,] matrix1, decimal[,] matrix2)
        {
            ValidateMatrices(matrix1, matrix2);

            decimal[,] result = new decimal[2, 2];

            for (int row = 0; row < 2; row++)
            {
                for (int col = 0; col < 2; col++)
                {
                    if (matrix2[row, col] == 0)
                    {
                        throw new DivideByZeroException($"División por cero en la posición [{row + 1}, {col + 1}]");
                    }

                    result[row, col] = matrix1[row, col] / matrix2[row, col];
                }
            }

            return result;
        }

        /// <summary>
        /// Realiza multiplicación matricial tradicional (A × B)
        /// Para matrices 2x2: C[i,j] = A[i,0]*B[0,j] + A[i,1]*B[1,j]
        /// </summary>
        /// <param name="matrix1">Matriz A</param>
        /// <param name="matrix2">Matriz B</param>
        /// <returns>Resultado de A × B</returns>
        public decimal[,] MatrixMultiplication(decimal[,] matrix1, decimal[,] matrix2)
        {
            ValidateMatrices(matrix1, matrix2);

            decimal[,] result = new decimal[2, 2];

            for (int row = 0; row < 2; row++)
            {
                for (int col = 0; col < 2; col++)
                {
                    result[row, col] = matrix1[row, 0] * matrix2[0, col] + 
                                      matrix1[row, 1] * matrix2[1, col];
                }
            }

            return result;
        }

        /// <summary>
        /// Calcula el determinante de una matriz 2x2
        /// det(A) = a11*a22 - a12*a21
        /// </summary>
        /// <param name="matrix">Matriz 2x2</param>
        /// <returns>Determinante de la matriz</returns>
        public decimal CalculateDeterminant(decimal[,] matrix)
        {
            ValidateMatrix(matrix);

            return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
        }

        /// <summary>
        /// Calcula la matriz transpuesta
        /// </summary>
        /// <param name="matrix">Matriz original</param>
        /// <returns>Matriz transpuesta</returns>
        public decimal[,] Transpose(decimal[,] matrix)
        {
            ValidateMatrix(matrix);

            decimal[,] result = new decimal[2, 2];

            for (int row = 0; row < 2; row++)
            {
                for (int col = 0; col < 2; col++)
                {
                    result[col, row] = matrix[row, col];
                }
            }

            return result;
        }

        /// <summary>
        /// Convierte matriz de enteros a decimales
        /// </summary>
        /// <param name="intMatrix">Matriz de enteros</param>
        /// <returns>Matriz de decimales</returns>
        public decimal[,] ConvertToDecimal(int[,] intMatrix)
        {
            if (intMatrix == null)
                throw new ArgumentNullException(nameof(intMatrix));

            int rows = intMatrix.GetLength(0);
            int cols = intMatrix.GetLength(1);
            decimal[,] result = new decimal[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    result[row, col] = intMatrix[row, col];
                }
            }

            return result;
        }

        /// <summary>
        /// Convierte matriz de decimales a formato string para mostrar
        /// </summary>
        /// <param name="matrix">Matriz de decimales</param>
        /// <param name="decimals">Número de decimales a mostrar</param>
        /// <returns>Matriz formateada como strings</returns>
        public string[,] FormatMatrix(decimal[,] matrix, int decimals = 2)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            string[,] result = new string[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    result[row, col] = Math.Round(matrix[row, col], decimals).ToString();
                }
            }

            return result;
        }

        /// <summary>
        /// Valida que ambas matrices sean válidas para operaciones
        /// </summary>
        private void ValidateMatrices(decimal[,] matrix1, decimal[,] matrix2)
        {
            ValidateMatrix(matrix1, nameof(matrix1));
            ValidateMatrix(matrix2, nameof(matrix2));
        }

        /// <summary>
        /// Valida que una matriz sea válida (2x2)
        /// </summary>
        private void ValidateMatrix(decimal[,] matrix, string parameterName = "matrix")
        {
            if (matrix == null)
                throw new ArgumentNullException(parameterName, "La matriz no puede ser null");

            if (matrix.GetLength(0) != 2 || matrix.GetLength(1) != 2)
                throw new ArgumentException("La matriz debe ser de 2x2", parameterName);
        }
    }

    /// <summary>
    /// Resultado de las operaciones entre matrices
    /// </summary>
    public class MatrixOperationsResult
    {
        /// <summary>
        /// Primera matriz original
        /// </summary>
        public decimal[,] Matrix1 { get; set; } = new decimal[2, 2];

        /// <summary>
        /// Segunda matriz original
        /// </summary>
        public decimal[,] Matrix2 { get; set; } = new decimal[2, 2];

        /// <summary>
        /// Resultado de la suma (Matrix1 + Matrix2)
        /// </summary>
        public decimal[,] Sum { get; set; } = new decimal[2, 2];

        /// <summary>
        /// Resultado de la resta (Matrix1 - Matrix2)
        /// </summary>
        public decimal[,] Difference { get; set; } = new decimal[2, 2];

        /// <summary>
        /// Resultado del producto elemento por elemento
        /// </summary>
        public decimal[,] Product { get; set; } = new decimal[2, 2];

        /// <summary>
        /// Resultado de la división elemento por elemento
        /// </summary>
        public decimal[,] Division { get; set; } = new decimal[2, 2];

        /// <summary>
        /// Indica si la división fue exitosa (no hubo división por cero)
        /// </summary>
        public bool DivisionSuccessful { get; set; } = true;

        /// <summary>
        /// Mensaje de error si la división falló
        /// </summary>
        public string DivisionError { get; set; } = string.Empty;
    }
}