// Archivo: Forms/CustomMatrixDialog.cs
using System;
using System.Drawing;
using System.Windows.Forms;
using ArrayExercises.WinForms.Utils;

namespace ArrayExercises.WinForms.Forms
{
    /// <summary>
    /// Diálogo para crear una matriz personalizada
    /// </summary>
    public partial class CustomMatrixDialog : Form
    {
        private Label lblInstructions;
        private TextBox txtMatrixInput;
        private Button btnOK;
        private Button btnCancel;
        private Button btnExample;
        private Label lblFormat;

        public int[,] Matrix { get; private set; } = new int[5, 5];

        public CustomMatrixDialog()
        {
            InitializeComponents();
            SetupForm();
        }

        private void InitializeComponents()
        {
            this.SuspendLayout();

            // Instrucciones
            lblInstructions = new Label
            {
                Text = "Ingrese los valores de la matriz separados por espacios o comas.\n" +
                      "Cada fila debe estar en una línea separada.",
                Location = new Point(20, 20),
                Size = new Size(360, 40),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            // Formato de ejemplo
            lblFormat = new Label
            {
                Text = "Formato esperado (5 filas x 5 columnas):",
                Location = new Point(20, 70),
                Size = new Size(250, 20),
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
            this.Text = "Matriz Personalizada";
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
            txtMatrixInput.Text = "0 2 5 7 6\r\n" +
                                 "0 0 0 3 8\r\n" +
                                 "2 9 6 3 4\r\n" +
                                 "1 5 6 1 4\r\n" +
                                 "0 9 2 5 0";
        }

        private bool ValidateAndParseMatrix()
        {
            try
            {
                string[] lines = txtMatrixInput.Text.Split(new[] { '\r', '\n' }, 
                                                          StringSplitOptions.RemoveEmptyEntries);

                if (lines.Length != 5)
                {
                    ValidationUtils.ShowValidationError("La matriz debe tener exactamente 5 filas.");
                    return false;
                }

                Matrix = new int[5, 5];

                for (int row = 0; row < 5; row++)
                {
                    var numbers = ValidationUtils.ExtractIntegers(lines[row]);
                    
                    if (numbers.Length != 5)
                    {
                        ValidationUtils.ShowValidationError($"La fila {row + 1} debe tener exactamente 5 números.");
                        return false;
                    }

                    for (int col = 0; col < 5; col++)
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