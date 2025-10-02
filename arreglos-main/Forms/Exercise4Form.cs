// Archivo: Forms/Exercise4Form.cs
using System;
using System.Drawing;
using System.Windows.Forms;
using ArrayExercises.WinForms.Models;

namespace ArrayExercises.WinForms.Forms
{
    public partial class Exercise4Form : BaseExerciseForm
    {
        private Label lblInstructions;
        private Label lblSize;
        private NumericUpDown nudSize;
        private DataGridView dgvMatrix;
        private Exercise4Logic logic;

        public Exercise4Form()
        {
            logic = new Exercise4Logic();
            InitializeExercise4Components();
            SetupEvents();
        }

        private void InitializeExercise4Components()
        {
            SetExerciseInfo("Ejercicio 4: Matriz Identidad",
                          "Crear una matriz cuadrada con 1's en la diagonal principal y 0's en el resto.");

            lblInstructions = new Label
            {
                Text = "Seleccione el tamaño de la matriz para generar la matriz identidad correspondiente.",
                Location = new Point(20, 20),
                Size = new Size(740, 20)
            };

            lblSize = new Label
            {
                Text = "Tamaño de la matriz (n x n):",
                Location = new Point(20, 60),
                Size = new Size(150, 20)
            };

            nudSize = new NumericUpDown
            {
                Location = new Point(180, 60),
                Minimum = 2,
                Maximum = 15,
                Value = 5 // Valor por defecto
            };

            dgvMatrix = new DataGridView
            {
                Location = new Point(20, 100),
                Size = new Size(740, 350),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                ColumnHeadersVisible = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            pnlContent.Controls.AddRange(new Control[] { lblInstructions, lblSize, nudSize, dgvMatrix });
        }

        private void SetupEvents()
        {
            btnExecute.Click += BtnExecute_Click;
            btnClear.Click += BtnClear_Click;
            btnBack.Click += (sender, e) => this.Close();
        }

        private void BtnExecute_Click(object sender, EventArgs e)
        {
            int size = (int)nudSize.Value;
            var identityMatrix = logic.CreateIdentityMatrix(size);
            DisplayMatrix(identityMatrix);
        }

        private void DisplayMatrix(int[,] matrix)
        {
            dgvMatrix.Rows.Clear();
            dgvMatrix.Columns.Clear();

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int j = 0; j < cols; j++)
            {
                dgvMatrix.Columns.Add($"col{j}", $"Col {j + 1}");
            }

            for (int i = 0; i < rows; i++)
            {
                dgvMatrix.Rows.Add();
                for (int j = 0; j < cols; j++)
                {
                    dgvMatrix.Rows[i].Cells[j].Value = matrix[i, j];
                }
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            dgvMatrix.Rows.Clear();
            dgvMatrix.Columns.Clear();
            nudSize.Value = 5;
        }
    }
}