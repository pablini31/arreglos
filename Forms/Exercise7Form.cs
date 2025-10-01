// Archivo: Forms/Exercise7Form.cs
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using ArrayExercises.WinForms.Models;

namespace ArrayExercises.WinForms.Forms
{
    public partial class Exercise7Form : BaseExerciseForm
    {
        private DataGridView dgvGrades;
        private RichTextBox rtbResults;
        private Exercise7Logic logic;

        public Exercise7Form()
        {
            logic = new Exercise7Logic();
            InitializeExercise7Components();
            SetupEvents();
            DisplayGradesData();
        }

        private void InitializeExercise7Components()
        {
            SetExerciseInfo("Ejercicio 7: Análisis de Calificaciones",
                          "Procesar una tabla de calificaciones para generar estadísticas.");

            dgvGrades = new DataGridView
            {
                Location = new Point(20, 20),
                Size = new Size(740, 250),
                ReadOnly = true,
                AllowUserToAddRows = false
            };
            
            rtbResults = new RichTextBox
            {
                Location = new Point(20, 280),
                Size = new Size(740, 170),
                ReadOnly = true,
                Font = new Font("Consolas", 10)
            };

            btnExecute.Text = "Analizar Calificaciones";
            pnlContent.Controls.AddRange(new Control[] { dgvGrades, rtbResults });
        }

        private void SetupEvents()
        {
            btnExecute.Click += (s, e) => AnalyzeAndDisplay();
            btnClear.Click += (s, e) => rtbResults.Clear();
            btnBack.Click += (s, e) => this.Close();
        }

        private void DisplayGradesData()
        {
            var gradesData = logic.GetGradesData();
            int rows = gradesData.GetLength(0);
            int cols = gradesData.GetLength(1);

            dgvGrades.Columns.Clear();
            dgvGrades.Rows.Clear();

            for (int j = 0; j < cols; j++) dgvGrades.Columns.Add($"Parcial{j+1}", $"Parcial {j+1}");
            
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
            var gradesData = logic.GetGradesData();
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
    }
}