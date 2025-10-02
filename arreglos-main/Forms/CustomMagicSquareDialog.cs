// Archivo: Forms/CustomMagicSquareDialog.cs
using System;
using System.Drawing;
using System.Windows.Forms;
using ArrayExercises.WinForms.Utils;

namespace ArrayExercises.WinForms.Forms
{
    /// <summary>
    /// Diálogo para crear un cuadrado mágico personalizado
    /// </summary>
    public partial class CustomMagicSquareDialog : Form
    {
        private Label lblInstructions;
        private TextBox txtMatrixInput;
        private Button btnOK;
        private Button btnCancel;
        private Button btnExample;
        private Label lblFormat;

        private int matrixSize;
        public int[,] Matrix { get; private set; }

        public CustomMagicSquareDialog(int size)
        {
            matrixSize = size;
            Matrix = new int[size, size];
            InitializeComponents();
            SetupForm();
        }

        private void InitializeComponents()
        {
            this.SuspendLayout();

            // Instrucciones
            lblInstructions = new Label
            {
                Text = $"Ingrese los valores del cuadrado mágico {matrixSize}x{matrixSize} separados por espacios o comas.\n" +
                      "Cada fila debe estar en una línea separada.",
                Location = new Point(20, 20),
                Size = new Size(360, 40),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            // Formato de ejemplo
            lblFormat = new Label
            {
                Text = $"Formato esperado ({matrixSize} filas x {matrixSize} columnas):",
                Location = new Point(20, 70),
                Size = new Size(300, 20),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            // TextBox para entrada de matriz
            txtMatrixInput = new TextBox
            {
                Location = new Point(20, 100),
                Size = new Size(360, 200),
                Multiline = true,
                ScrollBars = ScrollBars.Both,
                Font = new Font("Consolas", 10F),
                WordWrap = false,
                AcceptsReturn = true,
                AcceptsTab = true
            };

            // Botón de ejemplo
            btnExample = new Button
            {
                Text = "Cargar Ejemplo",
                Location = new Point(20, 320),
                Size = new Size(100, 30),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F)
            };
            btnExample.FlatAppearance.BorderSize = 0;

            // Botón OK
            btnOK = new Button
            {
                Text = "Aceptar",
                Location = new Point(200, 320),
                Size = new Size(80, 30),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
                DialogResult = DialogResult.OK
            };
            btnOK.FlatAppearance.BorderSize = 0;

            // Botón Cancel
            btnCancel = new Button
            {
                Text = "Cancelar",
                Location = new Point(300, 320),
                Size = new Size(80, 30),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
                DialogResult = DialogResult.Cancel
            };
            btnCancel.FlatAppearance.BorderSize = 0;

            // Agregar controles al formulario
            this.Controls.AddRange(new Control[] {
                lblInstructions, lblFormat, txtMatrixInput,
                btnExample, btnOK, btnCancel
            });

            this.ResumeLayout(false);
        }

        private void SetupForm()
        {
            this.Text = $"Cuadrado Mágico {matrixSize}x{matrixSize}";
            this.Size = new Size(420, 400);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            // Eventos
            btnExample.Click += BtnExample_Click;
            btnOK.Click += BtnOK_Click;
            
            // Cargar ejemplo inicial
            LoadExampleData();
        }

        private void BtnExample_Click(object sender, EventArgs e)
        {
            LoadExampleData();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (ValidateAndParseMatrix())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void LoadExampleData()
        {
            switch (matrixSize)
            {
                case 3:
                    txtMatrixInput.Text = "2 7 6\r\n" +
                                         "9 5 1\r\n" +
                                         "4 3 8";
                    break;
                case 4:
                    txtMatrixInput.Text = "16 3 2 13\r\n" +
                                         "5 10 11 8\r\n" +
                                         "9 6 7 12\r\n" +
                                         "4 15 14 1";
                    break;
                case 5:
                    txtMatrixInput.Text = "17 24 1 8 15\r\n" +
                                         "23 5 7 14 16\r\n" +
                                         "4 6 13 20 22\r\n" +
                                         "10 12 19 21 3\r\n" +
                                         "11 18 25 2 9";
                    break;
                default:
                    // Para tamaños mayores, generar un patrón simple
                    GenerateSimplePattern();
                    break;
            }
        }

        private void GenerateSimplePattern()
        {
            var lines = new string[matrixSize];
            int value = 1;
            
            for (int row = 0; row < matrixSize; row++)
            {
                var rowValues = new string[matrixSize];
                for (int col = 0; col < matrixSize; col++)
                {
                    rowValues[col] = value.ToString();
                    value++;
                }
                lines[row] = string.Join(" ", rowValues);
            }
            
            txtMatrixInput.Text = string.Join("\r\n", lines);
        }

        private bool ValidateAndParseMatrix()
        {
            try
            {
                string[] lines = txtMatrixInput.Text.Split(new[] { '\r', '\n' }, 
                                                          StringSplitOptions.RemoveEmptyEntries);

                if (lines.Length != matrixSize)
                {
                    ValidationUtils.ShowValidationError($"La matriz debe tener exactamente {matrixSize} filas.");
                    return false;
                }

                Matrix = new int[matrixSize, matrixSize];

                for (int row = 0; row < matrixSize; row++)
                {
                    var numbers = ValidationUtils.ExtractIntegers(lines[row]);
                    
                    if (numbers.Length != matrixSize)
                    {
                        ValidationUtils.ShowValidationError($"La fila {row + 1} debe tener exactamente {matrixSize} números.");
                        return false;
                    }

                    for (int col = 0; col < matrixSize; col++)
                    {
                        Matrix[row, col] = numbers[col];
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ValidationUtils.ShowValidationError($"Error al procesar la matriz: {ex.Message}");
                return false;
            }
        }
    }
}