// Archivo: Models/ExerciseInfo.cs
using System;

namespace ArrayExercises.WinForms.Models
{
    /// <summary>
    /// Modelo que contiene la información básica de un ejercicio
    /// </summary>
    public class ExerciseInfo
    {
        /// <summary>
        /// Número identificador del ejercicio
        /// </summary>
        public int ExerciseNumber { get; set; }

        /// <summary>
        /// Nombre descriptivo del ejercicio
        /// </summary>
        public string ExerciseName { get; set; } = string.Empty;

        /// <summary>
        /// Descripción detallada del problema a resolver
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Indica si el ejercicio está implementado
        /// </summary>
        public bool IsImplemented { get; set; }

        /// <summary>
        /// Fecha de creación del ejercicio
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Dificultad estimada del ejercicio (1-5)
        /// </summary>
        public int Difficulty { get; set; } = 1;

        /// <summary>
        /// Categoría del ejercicio
        /// </summary>
        public string Category { get; set; } = "Arreglos";

        /// <summary>
        /// Devuelve una representación string del ejercicio
        /// </summary>
        /// <returns>String con formato: "Ejercicio {número}: {nombre}"</returns>
        public override string ToString()
        {
            return $"Ejercicio {ExerciseNumber}: {ExerciseName}";
        }

        /// <summary>
        /// Compara dos ejercicios por su número
        /// </summary>
        /// <param name="obj">Objeto a comparar</param>
        /// <returns>True si son el mismo ejercicio</returns>
        public override bool Equals(object? obj)
        {
            if (obj is ExerciseInfo other)
            {
                return ExerciseNumber == other.ExerciseNumber;
            }
            return false;
        }

        /// <summary>
        /// Obtiene el hash code basado en el número del ejercicio
        /// </summary>
        /// <returns>Hash code del ejercicio</returns>
        public override int GetHashCode()
        {
            return ExerciseNumber.GetHashCode();
        }
    }
}