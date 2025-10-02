// Archivo: Forms/Exercise7Form.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArrayExercises.WinForms.Models;

namespace ArrayExercises.WinForms.Forms
{
    public partial class Exercise7Form : BaseExerciseForm
    {
        private DataGridView dgvGrades;
        private RichTextBox rtbResults;
        private Exercise7Logic logic;
        private Label lblInstructions;
        private Button btnGenerateRandom;
        private Button btnLoadExample;
        private double[,] currentGradesData;

        public Exercise7Form()
        {
            logic = new Exercise7Logic();
            InitializeExercise7Components();
            SetupEvents();
            LoadExampleData();
        }

        private void InitializeExercise7Components()
        {
            SetExerciseInfo("Ejercicio 7: Análisis de Calificaciones",
                          "Procesar una tabla de calificaciones para generar estadísticas.");

            lblInstructions = new Label
            {
                Text = "Ingrese las calificaciones (0-10) o use los datos de ejemplo. Puede editar directamente en la tabla.",
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

            dgvGrades = new DataGridView
            {
                Location = new Point(20, 90),
                Size = new Size(740, 200),
                ReadOnly = false,
                AllowUserToAddRows = false,
                Font = new Font("Segoe UI", 8),
                BorderStyle = BorderStyle.Fixed3D
            };
            
            // Agregar validación en tiempo real
            dgvGrades.CellValidating += DgvGrades_CellValidating;
            dgvGrades.CellEndEdit += DgvGrades_CellEndEdit;
            
            rtbResults = new RichTextBox
            {
                Location = new Point(20, 300),
                Size = new Size(740, 150),
                ReadOnly = true,
                Font = new Font("Consolas", 10)
            };

            btnExecute.Text = "Analizar Calificaciones";
            pnlContent.Controls.AddRange(new Control[] { 
                lblInstructions, btnLoadExample, btnGenerateRandom, dgvGrades, rtbResults 
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
            currentGradesData = logic.GetGradesData();
            DisplayGradesData(currentGradesData);
            rtbResults.Clear();
        }

        private void GenerateRandomData()
        {
            currentGradesData = logic.GenerateRandomGradesData();
            DisplayGradesData(currentGradesData);
            rtbResults.Clear();
        }

        private void DisplayGradesData(double[,] gradesData)
        {
            int rows = gradesData.GetLength(0);
            int cols = gradesData.GetLength(1);

            dgvGrades.Columns.Clear();
            dgvGrades.Rows.Clear();

            for (int j = 0; j < cols; j++) 
            {
                var column = new DataGridViewTextBoxColumn
                {
                    Name = $"Parcial{j+1}",
                    HeaderText = $"Parcial {j+1}",
                    Width = 120
                };
                dgvGrades.Columns.Add(column);
            }
            
            for (int i = 0; i < rows; i++)
            {
                dgvGrades.Rows.Add();
                dgvGrades.Rows[i].HeaderCell.Value = $"Alumno {i + 1}";
                for (int j = 0; j < cols; j++)
                {
                    dgvGrades.Rows[i].Cells[j].Value = gradesData[i, j].ToString("F1");
                }
            }
            dgvGrades.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        private void AnalyzeAndDisplay()
        {
            try
            {
                // Obtener datos de la tabla actual
                var gradesData = GetGradesDataFromGrid();
                var analysis = logic.AnalyzeGrades(gradesData);

                var sb = new StringBuilder();
                sb.AppendLine("--- ANÁLISIS DE CALIFICACIONES ---");
                sb.AppendLine($"a) Promedio por Alumno: {string.Join(", ", Array.ConvertAll(analysis.StudentAverages, avg => avg.ToString("F2")))}");
                sb.AppendLine($"b) Promedio más Alto: {analysis.HighestAverage:F2}");
                sb.AppendLine($"c) Promedio más Bajo: {analysis.LowestAverage:F2}");
                sb.AppendLine($"d) Parciales Reprobados (< 7.0): {analysis.FailedPartialsCount}");
                sb.AppendLine("\ne) Distribución de Calificaciones Finales:");
                
                foreach(KeyValuePair<string, int> entry in analysis.GradeDistribution)
                {
                    sb.AppendLine($"   - {entry.Key}: {entry.Value} Alumno(s)");
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

        private double[,] GetGradesDataFromGrid()
        {
            int rows = dgvGrades.Rows.Count;
            int cols = dgvGrades.Columns.Count;
            double[,] gradesData = new double[rows, cols];
            var errors = new List<string>();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    var cellValue = dgvGrades.Rows[i].Cells[j].Value;
                    string cellText = cellValue?.ToString()?.Trim() ?? "";
                    
                    // Verificar si la celda está vacía
                    if (string.IsNullOrEmpty(cellText))
                    {
                        errors.Add($"Celda vacía en Alumno {i + 1}, Parcial {j + 1}");
                        gradesData[i, j] = 0.0;
                        continue;
                    }
                    
                    // Verificar si contiene letras o caracteres no numéricos
                    if (!IsValidGrade(cellText))
                    {
                        errors.Add($"Dato inválido en Alumno {i + 1}, Parcial {j + 1}: '{cellText}' (solo se permiten números decimales entre 0 y 10)");
                        gradesData[i, j] = 0.0;
                        continue;
                    }
                    
                    // Verificar si es un número válido
                    if (double.TryParse(cellText, out double value))
                    {
                        // Verificar si el valor está en el rango válido (0-10)
                        if (value < 0 || value > 10)
                        {
                            errors.Add($"Valor fuera de rango en Alumno {i + 1}, Parcial {j + 1}: {value} (las calificaciones deben estar entre 0 y 10)");
                        }
                        gradesData[i, j] = value;
                    }
                    else
                    {
                        errors.Add($"Error de formato en Alumno {i + 1}, Parcial {j + 1}: '{cellText}' (formato numérico inválido)");
                        gradesData[i, j] = 0.0;
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

            return gradesData;
        }

        private bool IsValidGrade(string text)
        {
            if (string.IsNullOrEmpty(text)) return false;
            
            // Verificar que solo contenga dígitos, puntos decimales, signos + o - al inicio, y espacios
            foreach (char c in text)
            {
                if (!char.IsDigit(c) && c != '.' && c != '+' && c != '-' && c != ' ')
                {
                    return false;
                }
            }
            
            // Verificar que el punto decimal solo aparezca una vez
            int dotCount = text.Count(c => c == '.');
            if (dotCount > 1) return false;
            
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

        private void DgvGrades_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string value = e.FormattedValue?.ToString()?.Trim() ?? "";
            
            // Si está vacío, permitir (se manejará en el análisis)
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            
            // Validar formato numérico
            if (!IsValidGrade(value))
            {
                e.Cancel = true;
                MessageBox.Show($"Valor inválido: '{value}'\n\nSolo se permiten números decimales entre 0 y 10.", 
                              "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // Validar si es un número válido
            if (!double.TryParse(value, out double numValue))
            {
                e.Cancel = true;
                MessageBox.Show($"Formato numérico inválido: '{value}'\n\nPor favor, ingrese un número decimal válido.", 
                              "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // Validar rango (0-10)
            if (numValue < 0 || numValue > 10)
            {
                e.Cancel = true;
                MessageBox.Show($"Valor fuera de rango: '{value}'\n\nLas calificaciones deben estar entre 0 y 10.", 
                              "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DgvGrades_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var cell = dgvGrades.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string value = cell.Value?.ToString()?.Trim() ?? "";
            
            // Limpiar el resultado si se editó una celda
            if (!string.IsNullOrEmpty(rtbResults.Text))
            {
                rtbResults.Text = "ℹ️ Los datos han sido modificados. Presione 'Analizar Calificaciones' para actualizar los resultados.";
            }
        }
    }
}