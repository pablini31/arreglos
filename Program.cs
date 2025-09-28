// Archivo: Program.cs
using System;
using System.Windows.Forms;
using ArrayExercises.WinForms.Forms;

namespace ArrayExercises.WinForms
{
    /// <summary>
    /// Clase principal de la aplicación
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal de la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Configurar la aplicación para alta resolución DPI
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            
            // Habilitar estilos visuales
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // Iniciar la aplicación con el menú principal
                var mainForm = new MainMenuForm();
                Application.Run(mainForm);
            }
            catch (Exception ex)
            {
                // Manejo global de errores
                MessageBox.Show($"Error crítico en la aplicación: {ex.Message}", 
                              "Error Fatal", 
                              MessageBoxButtons.OK, 
                              MessageBoxIcon.Error);
            }
        }
    }
}