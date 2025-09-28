// Archivo: Forms/Exercise1Form.cs
using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArrayExercises.WinForms.Models;
using ArrayExercises.WinForms.Utils;

namespace ArrayExercises.WinForms.Forms
{
    /// <summary>
    /// Formulario para el Ejercicio 1: Contar ceros en cada renglón de una matriz
    /// </summary>
    public partial class Exercise1Form : BaseExerciseForm
    {
        private GroupBox grpInput;
        private Label lblMatrixInput;
        private DataGridView dgvMatrix;
        private Button btnLoadSample;
        private Button btnCustomMatrix;
        
        private GroupBox grpResults;
        private RichTextBox rtbResults;
        private Label lblInstructions;

        private Exercise1Logic logic;
        private const int MATRIX_ROWS = 5;
        private const int MATRIX_COLS = 5;

        public Exercise1Form()
        {
            logic = new Exercise1Logic();
            InitializeExercise1Components();
            SetupEvents();
            LoadSampleData();
        }

        private void InitializeExercise1Components()
        {
            SetExerciseInfo("Ejercicio 1: Contador de Ceros en Matriz", 
                          "Contar cuántos ceros aparecen en cada renglón de la matriz");

            // Instrucciones
            lblInstructions = new Label
            {
                Text = "Ingrese los valores de la matriz (5x5) o use los datos de ejemplo. " +
                      "El programa contará los ceros en cada fila.",
                Location = new Point(20, 20),
                Size = new Size(740, 40),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(52, 73, 94),
                BackColor = Color.Transparent
            };

            // Grupo de entrada
            grpInput = new GroupBox
            {
                Text = "Matriz de Entrada (5x5)",
                Location = new Point(20, 70),
                Size = new Size(360, 320),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            // DataGridView para la matriz
            dgvMatrix = new DataGridView
            {
                Location = new Point(15, 30),
                Size = new Size(330, 240),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeColumns = false,
                AllowUserToResizeRows = false,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                RowHeadersWidth = 60,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.CellSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Botones de control
            btnLoadSample = new Button
            {
                Text = "Cargar Ejemplo",
                Location = new Point(15, 280),
                Size = new Size(100, 30),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F)
            };
            btnLoadSample.FlatAppearance.BorderSize = 0;

            btnCustomMatrix = new Button
            {
                Text = "Matriz Personalizada",
                Location = new Point(125, 280),
                Size = new Size(130, 30),
                BackColor = Color.FromArgb(155, 89, 182),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F)
            };
            btnCustomMatrix.FlatAppearance.BorderSize = 0;

            // Grupo de resultados
            grpResults = new GroupBox
            {
                Text = "Resultados del Análisis",
                Location = new Point(400, 70),
                Size = new Size(360, 320),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            // RichTextBox para mostrar resultados
            rtbResults = new RichTextBox
            {
                Location = new Point(15, 30),
                Size = new Size(330, 270),
                Font = new Font("Consolas", 10F),
                BackColor = Color.FromArgb(248, 249, 250),
                ForeColor = Color.FromArgb(52, 73, 94),
                ReadOnly = true,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Agregar controles a los grupos
            grpInput.Controls.AddRange(new Control[] { dgvMatrix, btnLoadSample, btnCustomMatrix });
            grpResults.Controls.Add(rtbResults);

            // Agregar grupos al panel de contenido
            pnlContent.Controls.AddRange(new Control[] { lblInstructions, grpInput, grpResults });

            SetupMatrixGrid();
        }

        private void SetupMatrixGrid()
        {
            dgvMatrix.Columns.Clear();
            dgvMatrix.Rows.Clear();

            // Crear columnas
            for (int col = 0; col < MATRIX_COLS; col++)
            {
                var column = new DataGridViewTextBoxColumn
                {
                    HeaderText = $"Col {col + 1}",
                    Name = $"col{col}",
                    Width = 50,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleCenter,
                        Font = new Font("Segoe UI", 9F),
                        SelectionBackColor = Color.FromArgb(52, 152, 219),
                        SelectionForeColor = Color.White
                    }
                };
                dgvMatrix.Columns.Add(column);
            }

            // Crear filas
            for (int row = 0; row < MATRIX_ROWS; row++)
            {
                var rowIndex = dgvMatrix.Rows.Add();
                dgvMatrix.Rows[rowIndex].HeaderCell.Value = $"Fila {row + 1}";
                
                // Inicializar con ceros
                for (int col = 0; col < MATRIX_COLS; col++)
                {
                    dgvMatrix.Rows[rowIndex].Cells[col].Value = "0";
                }
            }

            // Configurar validación de celdas
            dgvMatrix.CellValidating += DgvMatrix_CellValidating;
            dgvMatrix.CellEndEdit += DgvMatrix_CellEndEdit;
        }

        private void DgvMatrix_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string value = e.FormattedValue.ToString();
            
            if (!ValidationUtils.IsValidInteger(value, out int result))
            {
                e.Cancel = true;
                ValidationUtils.ShowValidationError("Por favor, ingrese solo números enteros.");
                return;
            }

            // Permitir números en un rango razonable
            if (!ValidationUtils.IsInRange(result, -999, 999))
            {
                e.Cancel = true;
                ValidationUtils.ShowValidationError("El valor debe estar entre -999 y 999.");
            }
        }

        private void DgvMatrix_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Asegurar que el valor sea válido después de la edición
            var cell = dgvMatrix.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString()))
            {
                cell.Value = "0";
            }
        }

        private void SetupEvents()
        {
            btnExecute.Click += BtnExecute_Click;
            btnClear.Click += BtnClear_Click;
            btnBack.Click += BtnBack_Click;
            btnLoadSample.Click += BtnLoadSample_Click;
            btnCustomMatrix.Click += BtnCustomMatrix_Click;
        }

        private void BtnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                var matrix = GetMatrixFromGrid();
                var result = logic.CountZerosPerRow(matrix);
                DisplayResults(matrix, result);
                ValidationUtils.ShowSuccess("Análisis completado exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar la matriz: {ex.Message}", 
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearMatrix();
            rtbResults.Clear();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnLoadSample_Click(object sender, EventArgs e)
        {
            LoadSampleData();
        }

        private void BtnCustomMatrix_Click(object sender, EventArgs e)
        {
            ShowCustomMatrixDialog();
        }

        private void LoadSampleData()
        {
            // Datos del ejemplo del PDF
            int[,] sampleData = {
                { 0, 2, 5, 7, 6 },
                { 0, 0, 0, 3, 8 },
                { 2, 9, 6, 3, 4 },
                { 1, 5, 6, 1, 4 },
                { 0, 9, 2, 5, 0 }
            };

            LoadMatrixToGrid(sampleData);
        }

        private void LoadMatrixToGrid(int[,] matrix)
        {
            for (int row = 0; row < MATRIX_ROWS; row++)
            {
                for (int col = 0; col < MATRIX_COLS; col++)
                {
                    dgvMatrix.Rows[row].Cells[col].Value = matrix[row, col].ToString();
                }
            }
        }

        private void ClearMatrix()
        {
            for (int row = 0; row < MATRIX_ROWS; row++)
            {
                for (int col = 0; col < MATRIX_COLS; col++)
                {
                    dgvMatrix.Rows[row].Cells[col].Value = "0";
                }
            }
        }

        private int[,] GetMatrixFromGrid()
        {
            int[,] matrix = new int[MATRIX_ROWS, MATRIX_COLS];

            for (int row = 0; row < MATRIX_ROWS; row++)
            {
                for (int col = 0; col < MATRIX_COLS; col++)
                {
                    string cellValue = dgvMatrix.Rows[row].Cells[col].Value?.ToString() ?? "0";
                    if (ValidationUtils.IsValidInteger(cellValue, out int value))
                    {
                        matrix[row, col] = value;
                    }
                    else
                    {
                        throw new ArgumentException($"Valor inválido en la posición [{row + 1}, {col + 1}]: {cellValue}");
                    }
                }
            }

            return matrix;
        }

        private void DisplayResults(int[,] matrix, int[] zerosPerRow)
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== ANÁLISIS DE CEROS POR FILA ===");
            sb.AppendLine();

            sb.AppendLine("Matriz ingresada:");
            for (int row = 0; row < MATRIX_ROWS; row++)
            {
                sb.Append($"Fila {row + 1}: [ ");
                for (int col = 0; col < MATRIX_COLS; col++)
                {
                    sb.Append($"{matrix[row, col],3} ");
                }
                sb.AppendLine("]");
            }

            sb.AppendLine();
            sb.AppendLine("Cantidad de ceros por fila:");
            sb.AppendLine("================================");

            int totalZeros = 0;
            for (int row = 0; row < MATRIX_ROWS; row++)
            {
                sb.AppendLine($"Fila {row + 1}: {zerosPerRow[row]} cero(s)");
                totalZeros += zerosPerRow[row];
            }

            sb.AppendLine();
            sb.AppendLine($"RESUMEN:");
            sb.AppendLine($"Total de ceros en la matriz: {totalZeros}");
            sb.AppendLine($"Fila con más ceros: {GetRowWithMostZeros(zerosPerRow)}");
            sb.AppendLine($"Fila con menos ceros: {GetRowWithLeastZeros(zerosPerRow)}");

            rtbResults.Text = sb.ToString();

            // Aplicar formato de color
            FormatResultsText();
        }

        private void FormatResultsText()
        {
            // Resaltar el título
            int titleStart = rtbResults.Text.IndexOf("=== ANÁLISIS DE CEROS POR FILA ===");
            if (titleStart >= 0)
            {
                rtbResults.Select(titleStart, 34);
                rtbResults.SelectionColor = Color.FromArgb(41, 128, 185);
                rtbResults.SelectionFont = new Font("Consolas", 10F, FontStyle.Bold);
            }

            // Resaltar la sección de resumen
            int summaryStart = rtbResults.Text.IndexOf("RESUMEN:");
            if (summaryStart >= 0)
            {
                rtbResults.Select(summaryStart, 8);
                rtbResults.SelectionColor = Color.FromArgb(46, 204, 113);
                rtbResults.SelectionFont = new Font("Consolas", 10F, FontStyle.Bold);
            }

            // Deseleccionar
            rtbResults.Select(0, 0);
        }

        private string GetRowWithMostZeros(int[] zerosPerRow)
        {
            int maxZeros = zerosPerRow.Max();
            var rowsWithMax = zerosPerRow.Select((zeros, index) => new { zeros, index })
                                       .Where(x => x.zeros == maxZeros)
                                       .Select(x => x.index + 1);
            return $"{string.Join(", ", rowsWithMax)} ({maxZeros} ceros)";
        }

        private string GetRowWithLeastZeros(int[] zerosPerRow)
        {
            int minZeros = zerosPerRow.Min();
            var rowsWithMin = zerosPerRow.Select((zeros, index) => new { zeros, index })
                                       .Where(x => x.zeros == minZeros)
                                       .Select(x => x.index + 1);
            return $"{string.Join(", ", rowsWithMin)} ({minZeros} ceros)";
        }

        private void ShowCustomMatrixDialog()
        {
            using (var dialog = new CustomMatrixDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    LoadMatrixToGrid(dialog.Matrix);
                }
            }
        }
    }
}