// Archivo: Models/Exercise7Logic.cs
using System;
using System.Linq;
using System.Collections.Generic;

namespace ArrayExercises.WinForms.Models
{
    public class Exercise7Logic
    {
        [cite_start]// Matriz con los datos de calificaciones del PDF [cite: 229-265]
        public double[,] GetGradesData()
        {
            return new double[,]
            {
                { 5.5, 8.6, 10, 8.0 },
                { 5.5, 10, 9.0, 4.1 },
                { 7.8, 10, 2.2, 8.1 },
                { 7.0, 9.2, 7.1, 9.0 },
                { 4.0, 6.0, 6.5, 5.0 },
                { 5.0, 4.0, 7.0, 4.0 },
                { 8.0, 8.0, 9.0, 10 },
                { 9.0, 9.2, 5.0, 10 },
                { 8.4, 9.0, 4.6, 7.5 }
            };
        }

        public GradesAnalysisResult AnalyzeGrades(double[,] grades)
        {
            int studentCount = grades.GetLength(0);
            int partialCount = grades.GetLength(1);

            var studentAverages = new double[studentCount];
            int failedPartials = 0;

            for (int i = 0; i < studentCount; i++)
            {
                double sum = 0;
                for (int j = 0; j < partialCount; j++)
                {
                    double grade = grades[i, j];
                    sum += grade;
                    if (grade < 7.0)
                    {
                        failedPartials++;
                    }
                }
                studentAverages[i] = sum / partialCount;
            }

            // Mapeo de rangos para la distribución
            var distribution = new Dictionary<string, int>
            {
                { "0 - 4.9", 0 }, { "5.0 - 5.9", 0 }, { "6.0 - 6.9", 0 },
                { "7.0 - 7.9", 0 }, { "8.0 - 8.9", 0 }, { "9.0 - 10", 0 }
            };

            foreach (var avg in studentAverages)
            {
                if (avg < 5.0) distribution["0 - 4.9"]++;
                else if (avg < 6.0) distribution["5.0 - 5.9"]++;
                else if (avg < 7.0) distribution["6.0 - 6.9"]++;
                else if (avg < 8.0) distribution["7.0 - 7.9"]++;
                else if (avg < 9.0) distribution["8.0 - 8.9"]++;
                else distribution["9.0 - 10"]++;
            }

            return new GradesAnalysisResult
            {
                StudentAverages = studentAverages,
                HighestAverage = studentAverages.Max(),
                LowestAverage = studentAverages.Min(),
                FailedPartialsCount = failedPartials,
                GradeDistribution = distribution
            };
        }
    }

    public class GradesAnalysisResult
    {
        public double[] StudentAverages { get; set; }
        public double HighestAverage { get; set; }
        public double LowestAverage { get; set; }
        public int FailedPartialsCount { get; set; }
        public Dictionary<string, int> GradeDistribution { get; set; }
    }
}