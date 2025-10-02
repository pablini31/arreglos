// Archivo: Forms/Exercise5Form.cs
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ArrayExercises.WinForms.Models;

namespace ArrayExercises.WinForms.Forms
{
    public partial class Exercise5Form : BaseExerciseForm
    {
        private DataGridView dgvMatrix;
        private RichTextBox rtbResults;
        private Exercise5Logic logic;
        private int[,] currentMatrix;

        public Exercise5Form()
        {
            logic = new Exercise5Logic();
            InitializeExercise5Components();
            SetupEvents();
            GenerateAndDisplayMatrix();
        }

        private void InitializeExercise5Components()
        {
            SetExerciseInfo("Ejercicio 5: Estadísticas de Matriz Aleatoria",
                          "Calcular suma y promedio por filas y columnas de una matriz 5x10.");

            dgvMatrix = new DataGridView
            {
                Location = new Point(20, 20),
                Size = new Size(740, 180),
                ReadOnly = true,
                AllowUserToAddRows = false
            };
            
            rtbResults = new RichTextBox
            {
                Location = new Point(20, 210),
                Size = new Size(740, 240),
                ReadOnly = true,
                Font = new Font("Consolas", 9)
            };

            btnExecute.Text = "Calcular Estadísticas";
            pnlContent.Controls.AddRange(new Control[] { dgvMatrix, rtbResults });
        }
        
        private void SetupEvents()
        {
            btnExecute.Click += (s, e) => CalculateAndDisplayStatistics();
            btnClear.Click += (s, e) => GenerateAndDisplayMatrix();
            btnBack.Click += (s, e) => this.Close();
        }

        private void GenerateAndDisplayMatrix()
        {
            currentMatrix = logic.GenerateRandomMatrix();
            DisplayMatrix(currentMatrix);
            rtbResults.Clear();
        }

        private void CalculateAndDisplayStatistics()
        {
            if (currentMatrix == null) return;
            var stats = logic.CalculateStatistics(currentMatrix);
            DisplayStats(stats);
        }

        private void DisplayMatrix(int[,] matrix)
        {
            dgvMatrix.Rows.Clear();
            dgvMatrix.Columns.Clear();
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int j = 0; j < cols; j++) dgvMatrix.Columns.Add("", $"Col {j+1}");
            for (int i = 0; i < rows; i++)
            {
                dgvMatrix.Rows.Add();
                dgvMatrix.Rows[i].HeaderCell.Value = $"Fila {i+1}";
                for (int j = 0; j < cols; j++)
                {
                    dgvMatrix.Rows[i].Cells[j].Value = matrix[i, j];
                }
            }
            dgvMatrix.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        private void DisplayStats(MatrixStatisticsResult stats)
        {
            var sb = new StringBuilder();
            sb.AppendLine("--- ESTADÍSTICAS DE LA MATRIZ ---");
            sb.AppendLine("\nArreglo A (Suma por Fila):");
            sb.AppendLine(string.Join(", ", stats.RowSums));
            
            sb.AppendLine("\nArreglo B (Promedio por Fila):");
            sb.AppendLine(string.Join(", ", stats.RowAverages.Select(avg => avg.ToString("F2"))));

            sb.AppendLine("\nArreglo C (Suma por Columna):");
            sb.AppendLine(string.Join(", ", stats.ColSums));

            sb.AppendLine("\nArreglo D (Promedio por Columna):");
            sb.AppendLine(string.Join(", ", stats.ColAverages.Select(avg => avg.ToString("F2"))));

            rtbResults.Text = sb.ToString();
        }
    }
}