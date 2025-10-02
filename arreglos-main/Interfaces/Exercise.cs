// Archivo: Interfaces/IExercise.cs
using System.Windows.Forms;

namespace ArrayExercises.WinForms.Interfaces
{
    /// <summary>
    /// Interface base para todos los ejercicios
    /// Garantiza escalabilidad y consistencia
    /// </summary>
    public interface IExercise
    {
        /// <summary>
        /// Nombre descriptivo del ejercicio
        /// </summary>
        string ExerciseName { get; }
        
        /// <summary>
        /// Descripción detallada del problema
        /// </summary>
        string Description { get; }
        
        /// <summary>
        /// Número del ejercicio
        /// </summary>
        int ExerciseNumber { get; }
        
        /// <summary>
        /// Ejecuta la lógica principal del ejercicio
        /// </summary>
        /// <returns>Resultado del ejercicio</returns>
        object Execute();
        
        /// <summary>
        /// Valida los datos de entrada
        /// </summary>
        /// <returns>True si los datos son válidos</returns>
        bool ValidateInput();
        
        /// <summary>
        /// Obtiene el formulario asociado al ejercicio
        /// </summary>
        /// <returns>Formulario del ejercicio</returns>
        Form GetForm();
    }
}