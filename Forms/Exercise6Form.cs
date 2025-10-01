// Archivo: Forms/Exercise6Form.cs
using System;
using System.Drawing;
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

        public Exercise6Form()
        {
            logic = new Exercise6Logic();
            InitializeExercise6Components();
            SetupEvents();
            DisplaySalesData();
        }

        private void InitializeExercise6Components()
        {
            SetExerciseInfo("Ejercicio 6: Análisis de Ventas",
                          "Analizar una tabla de ventas para obtener estadísticas clave.");

            dgvSales = new DataGridView
            {
                Location = new Point(20, 20),
                Size = new Size(740, 280),
                ReadOnly = true,
                AllowUserToAddRows = false,
                Font = new Font("Segoe UI", 8)
            };
            
            rtbResults = new RichTextBox
            {
                Location = new Point(20, 310),
                Size = new Size(740, 140),
                ReadOnly = true,
                Font = new Font("Consolas", 10)
            };
            
            btnExecute.Text = "Analizar Ventas";
            pnlContent.Controls.AddRange(new Control[] { dgvSales, rtbResults });
        }
        
        private void SetupEvents()
        {
            btnExecute.Click += (s, e) => AnalyzeAndDisplay();
            btnClear.Click += (s, e) => rtbResults.Clear();
            btnBack.Click += (s, e) => this.Close();
        }

        private void DisplaySalesData()
        {
            var salesData = logic.GetSalesData();
            int rows = salesData.GetLength(0);
            int cols = salesData.GetLength(1);

            dgvSales.Columns.Clear();
            dgvSales.Rows.Clear();

            for (int j = 0; j < cols; j++) dgvSales.Columns.Add(days[j], days[j]);
            
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
            var salesData = logic.GetSalesData();
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
    }
}