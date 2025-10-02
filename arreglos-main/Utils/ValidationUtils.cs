// Archivo: Utils/ValidationUtils.cs
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ArrayExercises.WinForms.Utils
{
    /// <summary>
    /// Clase de utilidades para validación de datos
    /// Proporciona métodos comunes de validación reutilizables
    /// </summary>
    public static class ValidationUtils
    {
        /// <summary>
        /// Valida si un string representa un número entero válido
        /// </summary>
        /// <param name="value">Valor a validar</param>
        /// <param name="result">Resultado de la conversión</param>
        /// <returns>True si es un entero válido</returns>
        public static bool IsValidInteger(string value, out int result)
        {
            result = 0;
            if (string.IsNullOrWhiteSpace(value))
                return false;

            value = value.Trim();
            return int.TryParse(value, out result);
        }

        /// <summary>
        /// Valida si un string representa un número decimal válido
        /// </summary>
        /// <param name="value">Valor a validar</param>
        /// <param name="result">Resultado de la conversión</param>
        /// <returns>True si es un decimal válido</returns>
        public static bool IsValidDecimal(string value, out decimal result)
        {
            result = 0;
            if (string.IsNullOrWhiteSpace(value))
                return false;

            value = value.Trim().Replace(',', '.');
            return decimal.TryParse(value, NumberStyles.Number, 
                                  CultureInfo.InvariantCulture, out result);
        }

        /// <summary>
        /// Valida si un string representa un número double válido
        /// </summary>
        /// <param name="value">Valor a validar</param>
        /// <param name="result">Resultado de la conversión</param>
        /// <returns>True si es un double válido</returns>
        public static bool IsValidDouble(string value, out double result)
        {
            result = 0;
            if (string.IsNullOrWhiteSpace(value))
                return false;

            value = value.Trim().Replace(',', '.');
            return double.TryParse(value, NumberStyles.Number, 
                                 CultureInfo.InvariantCulture, out result);
        }

        /// <summary>
        /// Valida si un entero está dentro de un rango específico
        /// </summary>
        /// <param name="value">Valor a validar</param>
        /// <param name="min">Valor mínimo</param>
        /// <param name="max">Valor máximo</param>
        /// <returns>True si está en el rango</returns>
        public static bool IsInRange(int value, int min, int max)
        {
            return value >= min && value <= max;
        }

        /// <summary>
        /// Valida si un decimal está dentro de un rango específico
        /// </summary>
        /// <param name="value">Valor a validar</param>
        /// <param name="min">Valor mínimo</param>
        /// <param name="max">Valor máximo</param>
        /// <returns>True si está en el rango</returns>
        public static bool IsInRange(decimal value, decimal min, decimal max)
        {
            return value >= min && value <= max;
        }

        /// <summary>
        /// Valida que un string no esté vacío
        /// </summary>
        /// <param name="value">Valor a validar</param>
        /// <returns>True si no está vacío</returns>
        public static bool IsNotEmpty(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Valida que una matriz tenga las dimensiones especificadas
        /// </summary>
        /// <param name="matrix">Matriz a validar</param>
        /// <param name="expectedRows">Filas esperadas</param>
        /// <param name="expectedCols">Columnas esperadas</param>
        /// <returns>True si las dimensiones son correctas</returns>
        public static bool ValidateMatrixDimensions(int[,] matrix, int expectedRows, int expectedCols)
        {
            if (matrix == null)
                return false;

            return matrix.GetLength(0) == expectedRows && matrix.GetLength(1) == expectedCols;
        }

        /// <summary>
        /// Valida que una matriz sea cuadrada
        /// </summary>
        /// <param name="matrix">Matriz a validar</param>
        /// <returns>True si es cuadrada</returns>
        public static bool IsSquareMatrix(int[,] matrix)
        {
            if (matrix == null)
                return false;

            return matrix.GetLength(0) == matrix.GetLength(1);
        }

        /// <summary>
        /// Valida los datos de un TextBox para números enteros
        /// </summary>
        /// <param name="textBox">TextBox a validar</param>
        /// <param name="fieldName">Nombre del campo para mensajes de error</param>
        /// <param name="result">Resultado de la validación</param>
        /// <param name="min">Valor mínimo permitido (opcional)</param>
        /// <param name="max">Valor máximo permitido (opcional)</param>
        /// <returns>True si es válido</returns>
        public static bool ValidateIntegerTextBox(TextBox textBox, string fieldName, 
                                                out int result, int? min = null, int? max = null)
        {
            result = 0;

            if (!IsValidInteger(textBox.Text, out result))
            {
                ShowValidationError($"El campo '{fieldName}' debe contener un número entero válido.");
                textBox.Focus();
                textBox.SelectAll();
                return false;
            }

            if (min.HasValue && result < min.Value)
            {
                ShowValidationError($"El campo '{fieldName}' debe ser mayor o igual a {min.Value}.");
                textBox.Focus();
                textBox.SelectAll();
                return false;
            }

            if (max.HasValue && result > max.Value)
            {
                ShowValidationError($"El campo '{fieldName}' debe ser menor o igual a {max.Value}.");
                textBox.Focus();
                textBox.SelectAll();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida los datos de un TextBox para números decimales
        /// </summary>
        /// <param name="textBox">TextBox a validar</param>
        /// <param name="fieldName">Nombre del campo para mensajes de error</param>
        /// <param name="result">Resultado de la validación</param>
        /// <param name="min">Valor mínimo permitido (opcional)</param>
        /// <param name="max">Valor máximo permitido (opcional)</param>
        /// <returns>True si es válido</returns>
        public static bool ValidateDecimalTextBox(TextBox textBox, string fieldName, 
                                                out decimal result, decimal? min = null, decimal? max = null)
        {
            result = 0;

            if (!IsValidDecimal(textBox.Text, out result))
            {
                ShowValidationError($"El campo '{fieldName}' debe contener un número decimal válido.");
                textBox.Focus();
                textBox.SelectAll();
                return false;
            }

            if (min.HasValue && result < min.Value)
            {
                ShowValidationError($"El campo '{fieldName}' debe ser mayor o igual a {min.Value}.");
                textBox.Focus();
                textBox.SelectAll();
                return false;
            }

            if (max.HasValue && result > max.Value)
            {
                ShowValidationError($"El campo '{fieldName}' debe ser menor o igual a {max.Value}.");
                textBox.Focus();
                textBox.SelectAll();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida que una cadena contenga solo números separados por espacios o comas
        /// </summary>
        /// <param name="input">Cadena a validar</param>
        /// <returns>True si solo contiene números válidos</returns>
        public static bool ContainsOnlyNumbers(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            // Patrón que permite números enteros y decimales separados por espacios, comas o punto y coma
            var pattern = @"^[\d\s,;.\-]+$";
            return Regex.IsMatch(input.Trim(), pattern);
        }

        /// <summary>
        /// Extrae números de una cadena de texto
        /// </summary>
        /// <param name="input">Cadena de entrada</param>
        /// <returns>Array de números extraídos</returns>
        public static int[] ExtractIntegers(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new int[0];

            // Separar por espacios, comas o punto y coma
            var parts = input.Split(new char[] { ' ', ',', ';' }, 
                                  StringSplitOptions.RemoveEmptyEntries);

            return parts.Where(part => IsValidInteger(part.Trim(), out _))
                       .Select(part => { IsValidInteger(part.Trim(), out int val); return val; })
                       .ToArray();
        }

        /// <summary>
        /// Muestra un mensaje de error de validación
        /// </summary>
        /// <param name="message">Mensaje de error</param>
        public static void ShowValidationError(string message)
        {
            MessageBox.Show(message, "Error de Validación", 
                          MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Muestra un mensaje de información
        /// </summary>
        /// <param name="message">Mensaje informativo</param>
        public static void ShowInformation(string message)
        {
            MessageBox.Show(message, "Información", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Muestra un mensaje de éxito
        /// </summary>
        /// <param name="message">Mensaje de éxito</param>
        public static void ShowSuccess(string message)
        {
            MessageBox.Show(message, "Operación Exitosa", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Configura un TextBox para aceptar solo números
        /// </summary>
        /// <param name="textBox">TextBox a configurar</param>
        /// <param name="allowDecimals">Permitir decimales</param>
        /// <param name="allowNegatives">Permitir números negativos</param>
        public static void SetupNumericTextBox(TextBox textBox, bool allowDecimals = false, bool allowNegatives = false)
        {
            textBox.KeyPress += (sender, e) =>
            {
                // Permitir teclas de control (backspace, delete, etc.)
                if (char.IsControl(e.KeyChar))
                    return;

                // Permitir dígitos
                if (char.IsDigit(e.KeyChar))
                    return;

                // Permitir punto decimal si está habilitado
                if (allowDecimals && e.KeyChar == '.' && !textBox.Text.Contains('.'))
                    return;

                // Permitir signo negativo si está habilitado y es la primera posición
                if (allowNegatives && e.KeyChar == '-' && textBox.SelectionStart == 0 && !textBox.Text.Contains('-'))
                    return;

                // Cancelar cualquier otra tecla
                e.Handled = true;
            };
        }
    }
}