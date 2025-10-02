// Archivo: Forms/Exercise3Form.cs
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ArrayExercises.WinForms.Models;
using ArrayExercises.WinForms.Utils;

namespace ArrayExercises.WinForms.Forms
{
    /// <summary>
    /// Formulario para el Ejercicio 3: Operaciones con Matrices 2x2
    /// </summary>
    public partial class Exercise3Form : BaseExerciseForm
    {
        private GroupBox grpMatrix1;
        private DataGridView dgvMatrix1;
        private Button btnLoadExample1;

        private GroupBox grpMatrix2;
        private DataGridView dgvMatrix2;
        private Button btnLoadExample2;

        private GroupBox grpResults;
        private RichTextBox rtbResults;
        private CheckBox chkShowDetails;

        private Label lblInstructions;
        private Exercise3Logic logic;

        public Exercise3Form()
        {
            logic = new Exercise3Logic();
            InitializeExercise3Components();
            SetupEvents();
            LoadDefaultExamples();
        }

        private void InitializeExercise3Components()
        {
            SetExerciseInfo("Ejercicio 3: Operaciones con Matrices 2x2",
                          "Realizar suma, resta, producto y división entre dos matrices 2x2");

            // Instrucciones
            lblInstructions = new Label
            {
                Text = "Ingrese dos matrices de 2x2 para realizar las operaciones básicas: " +
                      "suma, resta, producto simple (elemento por elemento) y división simple.",
                Location = new Point(20, 20),
                Size = new Size(740, 40),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(52, 73, 94),
                BackColor = Color.Transparent
            };

            // Primera matriz
            grpMatrix1 = new GroupBox
            {
                Text = "Matriz 1 (2x2)",
                Location = new Point(20, 70),
                Size = new Size(200, 180),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            dgvMatrix1 = CreateMatrixDataGridView(new Point(15, 30), "dgvMatrix1");

            btnLoadExample1 = new Button
            {
                Text = "Ejemplo 1",
                Location = new Point(15, 140),
                Size = new Size(80, 25),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F)
            };
            btnLoadExample1.FlatAppearance.BorderSize = 0;

            // Segunda matriz
            grpMatrix2 = new GroupBox
            {
                Text = "Matriz 2 (2x2)",
                Location = new Point(240, 70),
                Size = new Size(200, 180),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            dgvMatrix2 = CreateMatrixDataGridView(new Point(15, 30), "dgvMatrix2");

            btnLoadExample2 = new Button
            {
                Text = "Ejemplo 2",
                Location = new Point(15, 140),
                Size = new Size(80, 25),
                BackColor = Color.FromArgb(155, 89, 182),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F)
            };
            btnLoadExample2.FlatAppearance.BorderSize = 0;

            // Grupo de resultados
            grpResults = new GroupBox
            {
                Text = "Resultados de las Operaciones",
                Location = new Point(460, 70),
                Size = new Size(300, 320),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            rtbResults = new RichTextBox
            {
                Location = new Point(15, 50),
                Size = new Size(270, 250),
                Font = new Font("Consolas", 9F),
                BackColor = Color.FromArgb(248, 249, 250),
                ForeColor = Color.FromArgb(52, 73, 94),
                ReadOnly = true,
                BorderStyle = BorderStyle.FixedSingle
            };

            chkShowDetails = new CheckBox
            {
                Text = "Mostrar detalles adicionales",
                Location = new Point(15, 25),
                Size = new Size(200, 20),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(52, 73, 94),
                Checked = true
            };

            // Agregar controles a grupos
            grpMatrix1.Controls.AddRange(new Control[] { dgvMatrix1, btnLoadExample1 });
            grpMatrix2.Controls.AddRange(new Control[] { dgvMatrix2, btnLoadExample2 });
            grpResults.Controls.AddRange(new Control[] { chkShowDetails, rtbResults });

            // Agregar grupos al panel de contenido
            pnlContent.Controls.AddRange(new Control[] { 
                lblInstructions, grpMatrix1, grpMatrix2, grpResults 
            });
        }

        private DataGridView CreateMatrixDataGridView(Point location, string name)
        {
            var dgv = new DataGridView
            {
                Location = location,
                Size = new Size(170, 100),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeColumns = false,
                AllowUserToResizeRows = false,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                RowHeadersWidth = 40,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.CellSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Name = name
            };

            // Crear 2 columnas
            for (int col = 0; col < 2; col++)
            {
                var column = new DataGridViewTextBoxColumn
                {
                    HeaderText = $"Col {col + 1}",
                    Name = $"col{col}",
                    Width = 60,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleCenter,
                        Font = new Font("Segoe UI", 9F),
                        SelectionBackColor = Color.FromArgb(52, 152, 219),
                        SelectionForeColor = Color.White
                    }
                };
                dgv.Columns.Add(column);
            }

            // Crear 2 filas
            for (int row = 0; row < 2; row++)
            {
                var rowIndex = dgv.Rows.Add();
                dgv.Rows[rowIndex].HeaderCell.Value = $"Fila {row + 1}";
                dgv.Rows[rowIndex].Height = 35;

                // Inicializar con valores por defecto
                for (int col = 0; col < 2; col++)
                {
                    dgv.Rows[rowIndex].Cells[col].Value = "1";
                }
            }

            // Configurar validación
            dgv.CellValidating += DgvMatrix_CellValidating;
            dgv.CellEndEdit += DgvMatrix_CellEndEdit;

            return dgv;
        }

        private void SetupEvents()
        {
            btnExecute.Click += BtnExecute_Click;
            btnClear.Click += BtnClear_Click;
            btnBack.Click += BtnBack_Click;
            btnLoadExample1.Click += BtnLoadExample1_Click;
            btnLoadExample2.Click += BtnLoadExample2_Click;
            chkShowDetails.CheckedChanged += ChkShowDetails_CheckedChanged;
        }

        private void BtnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                var matrix1 = GetMatrixFromGrid(dgvMatrix1);
                var matrix2 = GetMatrixFromGrid(dgvMatrix2);
                
                var result = logic.PerformAllOperations(matrix1, matrix2);
                DisplayResults(result);
                
                ValidationUtils.ShowSuccess("Operaciones completadas exitosamente.");
            }
            catch (DivideByZeroException ex)
            {
                MessageBox.Show($"Error en división: {ex.Message}", 
                              "División por cero", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar las matrices: {ex.Message}", 
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearMatrix(dgvMatrix1);
            ClearMatrix(dgvMatrix2);
            rtbResults.Clear();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnLoadExample1_Click(object sender, EventArgs e)
        {
            LoadMatrixToGrid(dgvMatrix1, new decimal[,] { { 10, 5 }, { 8, 2 } });
        }

        private void BtnLoadExample2_Click(object sender, EventArgs e)
        {
            LoadMatrixToGrid(dgvMatrix2, new decimal[,] { { 2, 4 }, { 6, 8 } });
        }

        private void ChkShowDetails_CheckedChanged(object sender, EventArgs e)
        {
            // Si hay resultados mostrados, volver a mostrarlos con el nuevo formato
            if (!string.IsNullOrEmpty(rtbResults.Text))
            {
                // Re-ejecutar el último cálculo si es posible
                try
                {
                    var matrix1 = GetMatrixFromGrid(dgvMatrix1);
                    var matrix2 = GetMatrixFromGrid(dgvMatrix2);
                    var result = logic.PerformAllOperations(matrix1, matrix2);
                    DisplayResults(result);
                }
                catch
                {
                    // Ignore si no se puede re-ejecutar
                }
            }
        }

        private void DgvMatrix_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string value = e.FormattedValue.ToString();

            if (!ValidationUtils.IsValidDecimal(value, out decimal result))
            {
                e.Cancel = true;
                ValidationUtils.ShowValidationError("Por favor, ingrese solo números válidos.");
                return;
            }

            if (!ValidationUtils.IsInRange(result, -9999m, 9999m))
            {
                e.Cancel = true;
                ValidationUtils.ShowValidationError("El valor debe estar entre -9999 y 9999.");
            }
        }

        private void DgvMatrix_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = sender as DataGridView;
            if (dgv == null) return;

            var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString()))
            {
                cell.Value = "1";
            }
        }

        private void LoadDefaultExamples()
        {
            // Cargar los ejemplos del PDF
            LoadMatrixToGrid(dgvMatrix1, new decimal[,] { { 10, 5 }, { 8, 2 } });
            LoadMatrixToGrid(dgvMatrix2, new decimal[,] { { 2, 4 }, { 6, 8 } });
        }

        private void LoadMatrixToGrid(DataGridView dgv, decimal[,] matrix)
        {
            for (int row = 0; row < 2; row++)
            {
                for (int col = 0; col < 2; col++)
                {
                    dgv.Rows[row].Cells[col].Value = matrix[row, col].ToString();
                }
            }
        }

        private void ClearMatrix(DataGridView dgv)
        {
            for (int row = 0; row < 2; row++)
            {
                for (int col = 0; col < 2; col++)
                {
                    dgv.Rows[row].Cells[col].Value = "0";
                }
            }
        }

        private decimal[,] GetMatrixFromGrid(DataGridView dgv)
        {
            decimal[,] matrix = new decimal[2, 2];

            for (int row = 0; row < 2; row++)
            {
                for (int col = 0; col < 2; col++)
                {
                    string cellValue = dgv.Rows[row].Cells[col].Value?.ToString() ?? "0";
                    if (ValidationUtils.IsValidDecimal(cellValue, out decimal value))
                    {
                        matrix[row, col] = value;
                    }
                    else
                    {
                        throw new ArgumentException($"Valor inválido en posición [{row + 1}, {col + 1}]: {cellValue}");
                    }
                }
            }

            return matrix;
        }

        private void DisplayResults(MatrixOperationsResult result)
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== OPERACIONES CON MATRICES 2x2 ===");
            sb.AppendLine();

            // Mostrar matrices originales
            sb.AppendLine("MATRIZ 1:");
            sb.AppendLine(FormatMatrixForDisplay(result.Matrix1));
            sb.AppendLine();

            sb.AppendLine("MATRIZ 2:");
            sb.AppendLine(FormatMatrixForDisplay(result.Matrix2));
            sb.AppendLine();

            sb.AppendLine("RESULTADOS:");
            sb.AppendLine("===================");
            sb.AppendLine();

            // Suma
            sb.AppendLine("a) SUMA (Matriz 1 + Matriz 2):");
            sb.AppendLine(FormatMatrixForDisplay(result.Sum));
            sb.AppendLine();

            // Resta
            sb.AppendLine("b) RESTA (Matriz 1 - Matriz 2):");
            sb.AppendLine(FormatMatrixForDisplay(result.Difference));
            sb.AppendLine();

            // Producto
            sb.AppendLine("c) PRODUCTO SIMPLE (elemento × elemento):");
            sb.AppendLine(FormatMatrixForDisplay(result.Product));
            sb.AppendLine();

            // División
            sb.AppendLine("d) DIVISIÓN SIMPLE (elemento ÷ elemento):");
            try
            {
                sb.AppendLine(FormatMatrixForDisplay(result.Division));
            }
            catch
            {
                sb.AppendLine("  Error: División por cero detectada");
            }

            // Detalles adicionales si está activado
            if (chkShowDetails.Checked)
            {
                sb.AppendLine();
                sb.AppendLine("INFORMACIÓN ADICIONAL:");
                sb.AppendLine("===================");
                
                // Determinantes
                decimal det1 = logic.CalculateDeterminant(result.Matrix1);
                decimal det2 = logic.CalculateDeterminant(result.Matrix2);
                sb.AppendLine($"Determinante Matriz 1: {Math.Round(det1, 2)}");
                sb.AppendLine($"Determinante Matriz 2: {Math.Round(det2, 2)}");
                sb.AppendLine();

                // Transpuestas
                sb.AppendLine("Transpuesta de Matriz 1:");
                sb.AppendLine(FormatMatrixForDisplay(logic.Transpose(result.Matrix1)));
                sb.AppendLine();

                sb.AppendLine("Transpuesta de Matriz 2:");
                sb.AppendLine(FormatMatrixForDisplay(logic.Transpose(result.Matrix2)));
                sb.AppendLine();

                // Multiplicación matricial tradicional
                sb.AppendLine("Multiplicación Matricial (A × B):");
                sb.AppendLine(FormatMatrixForDisplay(logic.MatrixMultiplication(result.Matrix1, result.Matrix2)));
            }

            rtbResults.Text = sb.ToString();
            FormatResultsText();
        }

        private string FormatMatrixForDisplay(decimal[,] matrix)
        {
            var sb = new StringBuilder();
            
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                sb.Append("  [ ");
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    sb.Append($"{Math.Round(matrix[row, col], 2),8} ");
                }
                sb.AppendLine("]");
            }

            return sb.ToString();
        }

        private void FormatResultsText()
        {
            // Resaltar el título principal
            int titleStart = rtbResults.Text.IndexOf("=== OPERACIONES CON MATRICES 2x2 ===");
            if (titleStart >= 0)
            {
                rtbResults.Select(titleStart, 37);
                rtbResults.SelectionColor = Color.FromArgb(41, 128, 185);
                rtbResults.SelectionFont = new Font("Consolas", 9F, FontStyle.Bold);
            }

            // Resaltar las operaciones
            string[] operations = { "a) SUMA", "b) RESTA", "c) PRODUCTO", "d) DIVISIÓN" };
            foreach (var op in operations)
            {
                int opStart = rtbResults.Text.IndexOf(op);
                if (opStart >= 0)
                {
                    rtbResults.Select(opStart, op.Length);
                    rtbResults.SelectionColor = Color.FromArgb(46, 204, 113);
                    rtbResults.SelectionFont = new Font("Consolas", 9F, FontStyle.Bold);
                }
            }

            // Resaltar sección de resultados
            int resultsStart = rtbResults.Text.IndexOf("RESULTADOS:");
            if (resultsStart >= 0)
            {
                rtbResults.Select(resultsStart, 11);
                rtbResults.SelectionColor = Color.FromArgb(230, 126, 34);
                rtbResults.SelectionFont = new Font("Consolas", 9F, FontStyle.Bold);
            }

            // Resaltar información adicional
            int infoStart = rtbResults.Text.IndexOf("INFORMACIÓN ADICIONAL:");
            if (infoStart >= 0)
            {
                rtbResults.Select(infoStart, 22);
                rtbResults.SelectionColor = Color.FromArgb(155, 89, 182);
                rtbResults.SelectionFont = new Font("Consolas", 9F, FontStyle.Bold);
            }

            rtbResults.Select(0, 0);
        }
    }
}