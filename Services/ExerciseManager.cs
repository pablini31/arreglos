// Archivo: Services/ExerciseManager.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ArrayExercises.WinForms.Forms;
using ArrayExercises.WinForms.Interfaces;
using ArrayExercises.WinForms.Models;

namespace ArrayExercises.WinForms.Services
{
    /// <summary>
    /// Administrador centralizado de ejercicios
    /// Facilita la escalabilidad y mantenimiento del código
    /// </summary>
    public class ExerciseManager
    {
        private readonly Dictionary<int, Func<Form>> exerciseFormFactories;
        private readonly List<ExerciseInfo> exerciseInfos;

        public ExerciseManager()
        {
            exerciseFormFactories = new Dictionary<int, Func<Form>>();
            exerciseInfos = new List<ExerciseInfo>();
            InitializeExercises();
        }

        private void InitializeExercises()
        {
            // Ejercicio 1: Contar ceros en matriz
            RegisterExercise(1, 
                "Contador de Ceros en Matriz", 
                "Contar cuántos ceros aparecen en cada renglón de una matriz dada",
                () => new Exercise1Form());

            // Ejercicio 2: Cuadrado mágico
            RegisterExercise(2, 
                "Verificador de Cuadrado Mágico", 
                "Verificar si una matriz es un cuadrado mágico y calcular la constante",
                () => new Exercise2Form());

            // Ejercicio 3: Operaciones con matrices
            RegisterExercise(3, 
                "Operaciones con Matrices 2x2", 
                "Realizar suma, resta, producto y división entre dos matrices 2x2",
                () => new Exercise3Form());

            // Placeholder para ejercicios futuros
            RegisterExercise(4, 
                "Matriz Identidad", 
                "Crear una matriz identidad (diagonal principal con 1s, resto con 0s)",
                () => new Exercise4Form()); // <-- Actualizado

            RegisterExercise(5, 
                "Estadísticas de Matriz Aleatoria", 
                "Calcular suma y promedio por filas y columnas de matriz 5x10",
                () => new Exercise5Form()); // <-- Actualizado

            RegisterExercise(6, 
                "Análisis de Ventas", 
                "Analizar tabla de ventas mensuales por días de la semana",
                () => new Exercise6Form()); // <-- Actualizado

            // Ejercicio 7: Análisis de Calificaciones
            RegisterExercise(7, 
                "Análisis de Calificaciones", 
                "Procesar tabla de calificaciones y generar estadísticas",
                () => new Exercise7Form()); // <-- Actualizado
        }

        /// <summary>
        /// Registra un ejercicio en el sistema
        /// </summary>
        /// <param name="number">Número del ejercicio</param>
        /// <param name="name">Nombre del ejercicio</param>
        /// <param name="description">Descripción del ejercicio</param>
        /// <param name="formFactory">Factory para crear el formulario</param>
        private void RegisterExercise(int number, string name, string description, Func<Form> formFactory)
        {
            exerciseInfos.Add(new ExerciseInfo
            {
                ExerciseNumber = number,
                ExerciseName = name,
                Description = description,
                IsImplemented = formFactory != null
            });

            if (formFactory != null)
            {
                exerciseFormFactories[number] = formFactory;
            }
        }

        /// <summary>
        /// Obtiene la lista de ejercicios disponibles
        /// </summary>
        /// <returns>Lista de información de ejercicios</returns>
        public List<ExerciseInfo> GetAvailableExercises()
        {
            return exerciseInfos.OrderBy(e => e.ExerciseNumber).ToList();
        }

        /// <summary>
        /// Obtiene la lista de ejercicios implementados
        /// </summary>
        /// <returns>Lista de ejercicios implementados</returns>
        public List<ExerciseInfo> GetImplementedExercises()
        {
            return exerciseInfos.Where(e => e.IsImplemented)
                               .OrderBy(e => e.ExerciseNumber)
                               .ToList();
        }

        /// <summary>
        /// Crea una instancia del formulario del ejercicio especificado
        /// </summary>
        /// <param name="exerciseNumber">Número del ejercicio</param>
        /// <returns>Formulario del ejercicio o null si no está implementado</returns>
        public Form GetExerciseForm(int exerciseNumber)
        {
            if (exerciseFormFactories.ContainsKey(exerciseNumber))
            {
                return exerciseFormFactories[exerciseNumber]();
            }
            return null;
        }

        /// <summary>
        /// Verifica si un ejercicio está implementado
        /// </summary>
        /// <param name="exerciseNumber">Número del ejercicio</param>
        /// <returns>True si está implementado</returns>
        public bool IsExerciseImplemented(int exerciseNumber)
        {
            return exerciseFormFactories.ContainsKey(exerciseNumber);
        }

        /// <summary>
        /// Obtiene información de un ejercicio específico
        /// </summary>
        /// <param name="exerciseNumber">Número del ejercicio</param>
        /// <returns>Información del ejercicio o null si no existe</returns>
        public ExerciseInfo GetExerciseInfo(int exerciseNumber)
        {
            return exerciseInfos.FirstOrDefault(e => e.ExerciseNumber == exerciseNumber);
        }

        /// <summary>
        /// Obtiene el total de ejercicios registrados
        /// </summary>
        /// <returns>Número total de ejercicios</returns>
        public int GetTotalExercisesCount()
        {
            return exerciseInfos.Count;
        }

        /// <summary>
        /// Obtiene el número de ejercicios implementados
        /// </summary>
        /// <returns>Número de ejercicios implementados</returns>
        public int GetImplementedExercisesCount()
        {
            return exerciseFormFactories.Count;
        }

        /// <summary>
        /// Valida si un número de ejercicio es válido
        /// </summary>
        /// <param name="exerciseNumber">Número del ejercicio</param>
        /// <returns>True si es válido</returns>
        public bool IsValidExerciseNumber(int exerciseNumber)
        {
            return exerciseInfos.Any(e => e.ExerciseNumber == exerciseNumber);
        }
    }
}