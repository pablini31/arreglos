// Archivo: Forms/BaseExerciseForm.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ArrayExercises.WinForms.Forms
{
    /// <summary>
    /// Formulario base para todos los ejercicios
    /// Proporciona funcionalidad común y estilo consistente
    /// </summary>
    public class BaseExerciseForm : Form
    {
        protected Panel pnlHeader;
        protected Label lblTitle;
        protected Label lblDescription;
        protected Panel pnlContent;
        protected Panel pnlButtons;
        protected Button btnExecute;
        protected Button btnClear;
        protected Button btnBack;

        public BaseExerciseForm()
        {
            InitializeBaseComponents();
            SetupFormProperties();
            ApplyStyles();
        }

        private void InitializeBaseComponents()
        {
            this.SuspendLayout();

            // Panel de encabezado
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.FromArgb(41, 128, 185),
                Name = "pnlHeader"
            };

            // Título
            lblTitle = new Label
            {
                Dock = DockStyle.Top,
                Height = 40,
                Text = "Ejercicio",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Name = "lblTitle"
            };

            // Descripción
            lblDescription = new Label
            {
                Dock = DockStyle.Fill,
                Text = "Descripción del ejercicio",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Name = "lblDescription"
            };

            // Panel de contenido
            pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(236, 240, 241),
                Name = "pnlContent"
            };

            // Panel de botones
            pnlButtons = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.FromArgb(189, 195, 199),
                Name = "pnlButtons"
            };

            // Botón ejecutar
            btnExecute = new Button
            {
                Text = "Ejecutar",
                Size = new Size(100, 35),
                Location = new Point(20, 12),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Name = "btnExecute"
            };
            btnExecute.FlatAppearance.BorderSize = 0;

            // Botón limpiar
            btnClear = new Button
            {
                Text = "Limpiar",
                Size = new Size(100, 35),
                Location = new Point(130, 12),
                BackColor = Color.FromArgb(230, 126, 34),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Name = "btnClear"
            };
            btnClear.FlatAppearance.BorderSize = 0;

            // Botón volver
            btnBack = new Button
            {
                Text = "Volver al Menú",
                Size = new Size(120, 35),
                Location = new Point(240, 12),
                BackColor = Color.FromArgb(52, 73, 94),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Name = "btnBack"
            };
            btnBack.FlatAppearance.BorderSize = 0;

            // Agregar controles
            pnlHeader.Controls.Add(lblDescription);
            pnlHeader.Controls.Add(lblTitle);
            
            pnlButtons.Controls.Add(btnExecute);
            pnlButtons.Controls.Add(btnClear);
            pnlButtons.Controls.Add(btnBack);

            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlButtons);
            this.Controls.Add(pnlHeader);

            this.ResumeLayout(false);
        }

        private void SetupFormProperties()
        {
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void ApplyStyles()
        {
            // Eventos de hover para botones
            ApplyButtonHoverEffect(btnExecute, Color.FromArgb(39, 174, 96));
            ApplyButtonHoverEffect(btnClear, Color.FromArgb(211, 84, 0));
            ApplyButtonHoverEffect(btnBack, Color.FromArgb(44, 62, 80));
        }

        private void ApplyButtonHoverEffect(Button button, Color hoverColor)
        {
            Color originalColor = button.BackColor;
            
            button.MouseEnter += (s, e) => button.BackColor = hoverColor;
            button.MouseLeave += (s, e) => button.BackColor = originalColor;
        }

        /// <summary>
        /// Método virtual para que las clases derivadas implementen la lógica de ejecución
        /// </summary>
        protected virtual void OnExecute()
        {
            MessageBox.Show("Implementar lógica en clase derivada", "Información", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Método virtual para limpiar los controles
        /// </summary>
        protected virtual void OnClear()
        {
            // Lógica base de limpieza
        }

        /// <summary>
        /// Configura el título y descripción del ejercicio
        /// </summary>
        /// <param name="title">Título del ejercicio</param>
        /// <param name="description">Descripción del ejercicio</param>
        protected void SetExerciseInfo(string title, string description)
        {
            lblTitle.Text = title;
            lblDescription.Text = description;
            this.Text = title;
        }
    }
}