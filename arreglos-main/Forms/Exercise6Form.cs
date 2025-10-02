// Archivo: Forms/Exercise6Form.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArrayExercises.WinForms.Models;

namespace ArrayExercises.WinForms.Forms
{
    public partial class Exercise6Form : BaseExerciseForm
    {
        private DataGridView dgvSales;
        private RichTextBox rtbResults;
        private Exercise6Logic logic;
        private string[] days = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };
        private Label lblInstructions;
        private Button btnGenerateRandom;
        private Button btnLoadExample;
        private int[,] currentSalesData;

        public Exercise6Form()
        {
            logic = new Exercise6Logic();
            InitializeExercise6Components();
            SetupEvents();
            LoadExampleData();
        }

        private void InitializeExercise6Components()
        {
            SetExerciseInfo("Ejercicio 6: Análisis de Ventas",
                          "Analizar una tabla de ventas para obtener estadísticas clave.");

            lblInstructions = new Label
            {
                Text = "Ingrese los valores de ventas o use los datos de ejemplo. Puede editar directamente en la tabla.",
                Location = new Point(20, 20),
                Size = new Size(740, 20),
                Font = new Font("Segoe UI", 9F)
            };

            btnLoadExample = new Button
            {
                Text = "Cargar Datos de Ejemplo",
                Location = new Point(20, 50),
                Size = new Size(150, 30),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btnGenerateRandom = new Button
            {
                Text = "Generar Datos Aleatorios",
                Location = new Point(190, 50),
                Size = new Size(180, 30),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            dgvSales = new DataGridView
            {
                Location = new Point(20, 90),
                Size = new Size(740, 200),
                ReadOnly = false,
                AllowUserToAddRows = false,
                Font = new Font("Segoe UI", 8),
                BorderStyle = BorderStyle.Fixed3D
            };
            
            // Agregar validación en tiempo real
            dgvSales.CellValidating += DgvSales_CellValidating;
            dgvSales.CellEndEdit += DgvSales_CellEndEdit;
            
            rtbResults = new RichTextBox
            {
                Location = new Point(20, 300),
                Size = new Size(740, 150),
                ReadOnly = true,
                Font = new Font("Consolas", 10)
            };
            
            btnExecute.Text = "Analizar Ventas";
            pnlContent.Controls.AddRange(new Control[] { 
                lblInstructions, btnLoadExample, btnGenerateRandom, dgvSales, rtbResults 
            });
        }
        
        private void SetupEvents()
        {
            btnExecute.Click += (s, e) => AnalyzeAndDisplay();
            btnClear.Click += (s, e) => ClearResults();
            btnBack.Click += (s, e) => this.Close();
            btnLoadExample.Click += (s, e) => LoadExampleData();
            btnGenerateRandom.Click += (s, e) => GenerateRandomData();
        }

        private void LoadExampleData()
        {
            currentSalesData = logic.GetSalesData();
            DisplaySalesData(currentSalesData);
            rtbResults.Clear();
        }

        private void GenerateRandomData()
        {
            currentSalesData = logic.GenerateRandomSalesData();
            DisplaySalesData(currentSalesData);
            rtbResults.Clear();
        }

        private void DisplaySalesData(int[,] salesData)
        {
            int rows = salesData.GetLength(0);
            int cols = salesData.GetLength(1);

            dgvSales.Columns.Clear();
            dgvSales.Rows.Clear();

            for (int j = 0; j < cols; j++) 
            {
                var column = new DataGridViewTextBoxColumn
                {
                    Name = days[j],
                    HeaderText = days[j],
                    Width = 90
                };
                dgvSales.Columns.Add(column);
            }
            
            for (int i = 0; i < rows; i++)
            {
                dgvSales.Rows.Add();
                dgvSales.Rows[i].HeaderCell.Value = $"Mes {i + 1}";
                for (int j = 0; j < cols; j++)
                {
                    dgvSales.Rows[i].Cells[j].Value = salesData[i, j];
                }
            }
            dgvSales.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        private void AnalyzeAndDisplay()
        {
            try
            {
                // Obtener datos de la tabla actual
                var salesData = GetSalesDataFromGrid();
                var analysis = logic.AnalyzeSales(salesData);

                var sb = new StringBuilder();
                sb.AppendLine("--- ANÁLISIS DE VENTAS ---");
                sb.AppendLine($"a) Venta Total: ${analysis.TotalSales:N0}");
                sb.AppendLine($"b) Menor Venta: ${analysis.MinSale.Amount} (Mes: {analysis.MinSale.Month + 1}, Día: {days[analysis.MinSale.DayOfWeek]})");
                sb.AppendLine($"c) Mayor Venta: ${analysis.MaxSale.Amount} (Mes: {analysis.MaxSale.Month + 1}, Día: {days[analysis.MaxSale.DayOfWeek]})");
                sb.AppendLine("\nd) Venta total por día:");
                for(int i = 0; i < analysis.DailyTotals.Length; i++)
                {
                    sb.AppendLine($"   - {days[i]}: ${analysis.DailyTotals[i]:N0}");
                }

                rtbResults.Text = sb.ToString();
            }
            catch (InvalidOperationException ex)
            {
                // Error de validación de datos
                MessageBox.Show(ex.Message, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rtbResults.Text = "❌ No se pudo realizar el análisis debido a errores en los datos.\n" +
                                 "Por favor, corrija los errores indicados y vuelva a intentar.";
            }
            catch (Exception ex)
            {
                // Otros errores
                MessageBox.Show($"Error inesperado al analizar los datos:\n\n{ex.Message}", 
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rtbResults.Text = "❌ Error inesperado durante el análisis.";
            }
        }

        private int[,] GetSalesDataFromGrid()
        {
            int rows = dgvSales.Rows.Count;
            int cols = dgvSales.Columns.Count;
            int[,] salesData = new int[rows, cols];
            var errors = new List<string>();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    var cellValue = dgvSales.Rows[i].Cells[j].Value;
                    string cellText = cellValue?.ToString()?.Trim() ?? "";
                    
                    // Verificar si la celda está vacía
                    if (string.IsNullOrEmpty(cellText))
                    {
                        errors.Add($"Celda vacía en Mes {i + 1}, {days[j]}");
                        salesData[i, j] = 0;
                        continue;
                    }
                    
                    // Verificar si contiene letras o caracteres no numéricos
                    if (!IsValidNumber(cellText))
                    {
                        errors.Add($"Dato inválido en Mes {i + 1}, {days[j]}: '{cellText}' (solo se permiten números enteros)");
                        salesData[i, j] = 0;
                        continue;
                    }
                    
                    // Verificar si es un número válido
                    if (int.TryParse(cellText, out int value))
                    {
                        // Verificar si el valor es negativo
                        if (value < 0)
                        {
                            errors.Add($"Valor negativo en Mes {i + 1}, {days[j]}: {value} (las ventas no pueden ser negativas)");
                        }
                        salesData[i, j] = value;
                    }
                    else
                    {
                        errors.Add($"Error de formato en Mes {i + 1}, {days[j]}: '{cellText}' (formato numérico inválido)");
                        salesData[i, j] = 0;
                    }
                }
            }

            // Si hay errores, mostrarlos y lanzar excepción
            if (errors.Count > 0)
            {
                string errorMessage = "Se encontraron los siguientes errores en los datos:\n\n" + 
                                    string.Join("\n", errors.Take(10)); // Mostrar máximo 10 errores
                
                if (errors.Count > 10)
                {
                    errorMessage += $"\n\n... y {errors.Count - 10} errores más.";
                }
                
                throw new InvalidOperationException(errorMessage);
            }

            return salesData;
        }

        private bool IsValidNumber(string text)
        {
            if (string.IsNullOrEmpty(text)) return false;
            
            // Verificar que solo contenga dígitos, signos + o - al inicio, y espacios
            foreach (char c in text)
            {
                if (!char.IsDigit(c) && c != '+' && c != '-' && c != ' ')
                {
                    return false;
                }
            }
            
            // Verificar que el signo solo esté al inicio
            if (text.Length > 1)
            {
                for (int i = 1; i < text.Length; i++)
                {
                    if (text[i] == '+' || text[i] == '-')
                    {
                        return false;
                    }
                }
            }
            
            return true;
        }

        private void ClearResults()
        {
            rtbResults.Clear();
        }

        private void DgvSales_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string value = e.FormattedValue?.ToString()?.Trim() ?? "";
            
            // Si está vacío, permitir (se manejará en el análisis)
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            
            // Validar formato numérico
            if (!IsValidNumber(value))
            {
                e.Cancel = true;
                MessageBox.Show($"Valor inválido: '{value}'\n\nSolo se permiten números enteros (positivos o negativos).", 
                              "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // Validar si es un número válido
            if (!int.TryParse(value, out int numValue))
            {
                e.Cancel = true;
                MessageBox.Show($"Formato numérico inválido: '{value}'\n\nPor favor, ingrese un número entero válido.", 
                              "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DgvSales_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var cell = dgvSales.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string value = cell.Value?.ToString()?.Trim() ?? "";
            
            // Limpiar el resultado si se editó una celda
            if (!string.IsNullOrEmpty(rtbResults.Text))
            {
                rtbResults.Text = "ℹ️ Los datos han sido modificados. Presione 'Analizar Ventas' para actualizar los resultados.";
            }
        }
    }
}