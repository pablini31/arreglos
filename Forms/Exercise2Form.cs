// Archivo: Forms/Exercise2Form.cs
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ArrayExercises.WinForms.Models;
using ArrayExercises.WinForms.Utils;

namespace ArrayExercises.WinForms.Forms
{
    /// <summary>
    /// Formulario para el Ejercicio 2: Verificador de Cuadrado Mágico
    /// </summary>
    public partial class Exercise2Form : BaseExerciseForm
    {
        private GroupBox grpSize;
        private Label lblSize;
        private NumericUpDown nudSize;
        private Button btnCreateGrid;

        private GroupBox grpMatrix;
        private DataGridView dgvMatrix;
        private Button btnGenerateExample;
        private Button btnLoadCustom;

        private GroupBox grpResults;
        private RichTextBox rtbResults;
        private Label lblInstructions;

        private Exercise2Logic logic;
        private int currentSize = 3;

        public Exercise2Form()
        {
            logic = new Exercise2Logic();
            InitializeExercise2Components();
            SetupEvents();
            CreateMatrixGrid(currentSize);
            LoadDefaultExample();
        }

        private void InitializeExercise2Components()
        {
            SetExerciseInfo("Ejercicio 2: Verificador de Cuadrado Mágico",
                          "Analizar si una matriz es un cuadrado mágico y calcular la constante");

            // Instrucciones
            lblInstructions = new Label
            {
                Text = "Un cuadrado mágico es una matriz donde la suma de filas, columnas y diagonales es igual. " +
                      "Seleccione el tamaño y ingrese los valores para verificar.",
                Location = new Point(20, 20),
                Size = new Size(740, 40),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(52, 73, 94),
                BackColor = Color.Transparent
            };

            // Grupo de tamaño
            grpSize = new GroupBox
            {
                Text = "Configuración de Tamaño",
                Location = new Point(20, 70),
                Size = new Size(200, 80),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            lblSize = new Label
            {
                Text = "Tamaño (n x n):",
                Location = new Point(15, 25),
                Size = new Size(80, 20),
                Font = new Font("Segoe UI", 9F)
            };

            nudSize = new NumericUpDown
            {
                Location = new Point(100, 23),
                Size = new Size(60, 25),
                Minimum = 3,
                Maximum = 8,
                Value = 3,
                Font = new Font("Segoe UI", 9F)
            };

            btnCreateGrid = new Button
            {
                Text = "Crear Matriz",
                Location = new Point(15, 50),
                Size = new Size(85, 25),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8F)
            };
            btnCreateGrid.FlatAppearance.BorderSize = 0;

            // Grupo de matriz
            grpMatrix = new GroupBox
            {
                Text = "Matriz de Entrada",
                Location = new Point(240, 70),
                Size = new Size(280, 320),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            dgvMatrix = new DataGridView
            {
                Location = new Point(15, 30),
                Size = new Size(250, 240),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeColumns = false,
                AllowUserToResizeRows = false,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                RowHeadersWidth = 40,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.CellSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            btnGenerateExample = new Button
            {
                Text = "Generar Ejemplo",
                Location = new Point(15, 280),
                Size = new Size(110, 30),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F)
            };
            btnGenerateExample.FlatAppearance.BorderSize = 0;

            btnLoadCustom = new Button
            {
                Text = "Personalizar",
                Location = new Point(135, 280),
                Size = new Size(85, 30),
                BackColor = Color.FromArgb(155, 89, 182),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F)
            };
            btnLoadCustom.FlatAppearance.BorderSize = 0;

            // Grupo de resultados
            grpResults = new GroupBox
            {
                Text = "Análisis y Resultados",
                Location = new Point(540, 70),
                Size = new Size(220, 320),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            rtbResults = new RichTextBox
            {
                Location = new Point(15, 30),
                Size = new Size(190, 270),
                Font = new Font("Consolas", 9F),
                BackColor = Color.FromArgb(248, 249, 250),
                ForeColor = Color.FromArgb(52, 73, 94),
                ReadOnly = true,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Agregar controles a grupos
            grpSize.Controls.AddRange(new Control[] { lblSize, nudSize, btnCreateGrid });
            grpMatrix.Controls.AddRange(new Control[] { dgvMatrix, btnGenerateExample, btnLoadCustom });
            grpResults.Controls.Add(rtbResults);

            // Agregar grupos al panel de contenido
            pnlContent.Controls.AddRange(new Control[] { 
                lblInstructions, grpSize, grpMatrix, grpResults 
            });
        }

        private void SetupEvents()
        {
            btnExecute.Click += BtnExecute_Click;
            btnClear.Click += BtnClear_Click;
            btnBack.Click += BtnBack_Click;
            btnCreateGrid.Click += BtnCreateGrid_Click;
            btnGenerateExample.Click += BtnGenerateExample_Click;
            btnLoadCustom.Click += BtnLoadCustom_Click;
            nudSize.ValueChanged += NudSize_ValueChanged;
        }

        private void BtnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                var matrix = GetMatrixFromGrid();
                var result = logic.AnalyzeMagicSquare(matrix);
                DisplayResults(result);
                
                string message = result.IsMagicSquare 
                    ? $"¡Es un cuadrado mágico! Constante: {result.MagicConstant}"
                    : "No es un cuadrado mágico válido.";
                    
                ValidationUtils.ShowSuccess(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al analizar la matriz: {ex.Message}",
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

        private void BtnCreateGrid_Click(object sender, EventArgs e)
        {
            currentSize = (int)nudSize.Value;
            CreateMatrixGrid(currentSize);
            rtbResults.Clear();
        }

        private void BtnGenerateExample_Click(object sender, EventArgs e)
        {
            try
            {
                var magicSquare = logic.GenerateMagicSquare(currentSize);
                LoadMatrixToGrid(magicSquare);
                ValidationUtils.ShowInformation($"Se generó un cuadrado mágico de {currentSize}x{currentSize}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar ejemplo: {ex.Message}",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnLoadCustom_Click(object sender, EventArgs e)
        {
            ShowCustomMatrixDialog();
        }

        private void NudSize_ValueChanged(object sender, EventArgs e)
        {
            // Actualizar automáticamente cuando cambie el tamaño
            if ((int)nudSize.Value != currentSize)
            {
                currentSize = (int)nudSize.Value;
                CreateMatrixGrid(currentSize);
                rtbResults.Clear();
            }
        }

        private void CreateMatrixGrid(int size)
        {
            dgvMatrix.Columns.Clear();
            dgvMatrix.Rows.Clear();

            // Ajustar tamaño del DataGridView
            int cellSize = Math.Min(180 / size, 35);
            dgvMatrix.DefaultCellStyle.Font = new Font("Segoe UI", Math.Max(8, 12 - size));

            // Crear columnas
            for (int col = 0; col < size; col++)
            {
                var column = new DataGridViewTextBoxColumn
                {
                    HeaderText = $"{col + 1}",
                    Name = $"col{col}",
                    Width = cellSize,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleCenter,
                        SelectionBackColor = Color.FromArgb(52, 152, 219),
                        SelectionForeColor = Color.White
                    }
                };
                dgvMatrix.Columns.Add(column);
            }

            // Crear filas
            for (int row = 0; row < size; row++)
            {
                var rowIndex = dgvMatrix.Rows.Add();
                dgvMatrix.Rows[rowIndex].HeaderCell.Value = $"{row + 1}";
                dgvMatrix.Rows[rowIndex].Height = cellSize;

                // Inicializar con valores por defecto
                for (int col = 0; col < size; col++)
                {
                    dgvMatrix.Rows[rowIndex].Cells[col].Value = "1";
                }
            }

            // Configurar validación
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

            if (!ValidationUtils.IsInRange(result, 1, 999))
            {
                e.Cancel = true;
                ValidationUtils.ShowValidationError("El valor debe estar entre 1 y 999.");
            }
        }

        private void DgvMatrix_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var cell = dgvMatrix.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString()))
            {
                cell.Value = "1";
            }
        }

        private void LoadDefaultExample()
        {
            // Ejemplo de cuadrado mágico 3x3 clásico
            int[,] defaultSquare = {
                { 2, 7, 6 },
                { 9, 5, 1 },
                { 4, 3, 8 }
            };

            LoadMatrixToGrid(defaultSquare);
        }

        private void LoadMatrixToGrid(int[,] matrix)
        {
            int size = matrix.GetLength(0);
            
            if (size != currentSize)
            {
                currentSize = size;
                nudSize.Value = size;
                CreateMatrixGrid(size);
            }

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    dgvMatrix.Rows[row].Cells[col].Value = matrix[row, col].ToString();
                }
            }
        }

        private void ClearMatrix()
        {
            for (int row = 0; row < currentSize; row++)
            {
                for (int col = 0; col < currentSize; col++)
                {
                    dgvMatrix.Rows[row].Cells[col].Value = "1";
                }
            }
        }

        private int[,] GetMatrixFromGrid()
        {
            int[,] matrix = new int[currentSize, currentSize];

            for (int row = 0; row < currentSize; row++)
            {
                for (int col = 0; col < currentSize; col++)
                {
                    string cellValue = dgvMatrix.Rows[row].Cells[col].Value?.ToString() ?? "1";
                    if (ValidationUtils.IsValidInteger(cellValue, out int value))
                    {
                        matrix[row, col] = value;
                    }
                    else
                    {
                        throw new ArgumentException($"Valor inválido en [{row + 1}, {col + 1}]: {cellValue}");
                    }
                }
            }

            return matrix;
        }

        private void DisplayResults(MagicSquareResult result)
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== ANÁLISIS DE CUADRADO MÁGICO ===");
            sb.AppendLine();

            // Información básica
            sb.AppendLine($"Tamaño: {result.Size} x {result.Size}");
            sb.AppendLine($"Es cuadrado mágico: {(result.IsMagicSquare ? "SÍ" : "NO")}");
            
            if (result.IsMagicSquare)
            {
                sb.AppendLine($"Constante mágica: {result.MagicConstant}");
            }

            sb.AppendLine($"Usa números consecutivos: {(result.UsesConsecutiveNumbers ? "SÍ" : "NO")}");
            sb.AppendLine();

            // Sumas por fila
            sb.AppendLine("Sumas por fila:");
            for (int i = 0; i < result.RowSums.Length; i++)
            {
                sb.AppendLine($"  Fila {i + 1}: {result.RowSums[i]}");
            }
            sb.AppendLine();

            // Sumas por columna
            sb.AppendLine("Sumas por columna:");
            for (int i = 0; i < result.ColumnSums.Length; i++)
            {
                sb.AppendLine($"  Col {i + 1}: {result.ColumnSums[i]}");
            }
            sb.AppendLine();

            // Sumas de diagonales
            sb.AppendLine("Sumas de diagonales:");
            sb.AppendLine($"  Principal: {result.DiagonalSums[0]}");
            sb.AppendLine($"  Secundaria: {result.DiagonalSums[1]}");
            sb.AppendLine();

            // Información adicional
            if (result.UsesConsecutiveNumbers)
            {
                sb.AppendLine($"Constante esperada: {result.ExpectedMagicConstant}");
            }

            rtbResults.Text = sb.ToString();
            FormatResultsText(result.IsMagicSquare);
        }

        private void FormatResultsText(bool isMagicSquare)
        {
            // Resaltar el título
            int titleStart = rtbResults.Text.IndexOf("=== ANÁLISIS DE CUADRADO MÁGICO ===");
            if (titleStart >= 0)
            {
                rtbResults.Select(titleStart, 35);
                rtbResults.SelectionColor = Color.FromArgb(41, 128, 185);
                rtbResults.SelectionFont = new Font("Consolas", 9F, FontStyle.Bold);
            }

            // Resaltar el resultado
            string resultText = isMagicSquare ? "SÍ" : "NO";
            int resultStart = rtbResults.Text.IndexOf($"Es cuadrado mágico: {resultText}");
            if (resultStart >= 0)
            {
                rtbResults.Select(resultStart + 20, resultText.Length);
                rtbResults.SelectionColor = isMagicSquare ? Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60);
                rtbResults.SelectionFont = new Font("Consolas", 9F, FontStyle.Bold);
            }

            rtbResults.Select(0, 0);
        }

        private void ShowCustomMatrixDialog()
        {
            using (var dialog = new CustomMagicSquareDialog(currentSize))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    LoadMatrixToGrid(dialog.Matrix);
                }
            }
        }
    }
}