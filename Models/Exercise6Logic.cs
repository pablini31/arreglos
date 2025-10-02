// Archivo: Models/Exercise6Logic.cs
using System;
using System.Linq;

namespace ArrayExercises.WinForms.Models
{
    public class Exercise6Logic
    {
        // Matriz con los datos de ventas del PDF
        public int[,] GetSalesData()
        {
            return new int[,]
            {
                { 5, 16, 10, 12, 24, 40, 55 },
                { 10, 11, 18, 15, 41, 78, 14 },
                { 51, 35, 22, 81, 15, 12, 50 },
                { 12, 71, 10, 20, 70, 40, 60 },
                { 28, 22, 50, 50, 50, 36, 25 },
                { 40, 70, 40, 11, 20, 20, 20 },
                { 30, 12, 18, 10, 40, 32, 13 },
                { 16, 50, 3, 24, 15, 82, 40 },
                { 46, 15, 46, 22, 10, 20, 20 },
                { 20, 30, 12, 18, 10, 40, 32 },
                { 13, 16, 50, 3, 24, 15, 82 },
                { 40, 46, 15, 46, 22, 12, 18 }
            };
        }

        public SalesAnalysisResult AnalyzeSales(int[,] sales)
        {
            int rows = sales.GetLength(0);
            int cols = sales.GetLength(1);
            
            var result = new SalesAnalysisResult
            {
                MinSale = new SaleInfo { Amount = int.MaxValue },
                MaxSale = new SaleInfo { Amount = int.MinValue },
                DailyTotals = new int[cols]
            };

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int currentSale = sales[i, j];
                    result.TotalSales += currentSale;
                    result.DailyTotals[j] += currentSale;

                    if (currentSale < result.MinSale.Amount)
                    {
                        result.MinSale.Amount = currentSale;
                        result.MinSale.Month = i;
                        result.MinSale.DayOfWeek = j;
                    }
                    if (currentSale > result.MaxSale.Amount)
                    {
                        result.MaxSale.Amount = currentSale;
                        result.MaxSale.Month = i;
                        result.MaxSale.DayOfWeek = j;
                    }
                }
            }
            return result;
        }
    }

    public class SaleInfo
    {
        public int Amount { get; set; }
        public int Month { get; set; } // 0-11
        public int DayOfWeek { get; set; } // 0-6
    }

    public class SalesAnalysisResult
    {
        public SaleInfo MinSale { get; set; }
        public SaleInfo MaxSale { get; set; }
        public int TotalSales { get; set; }
        public int[] DailyTotals { get; set; }
    }
}