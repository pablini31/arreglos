// Archivo: Forms/MainMenuForm.cs
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ArrayExercises.WinForms.Services;

namespace ArrayExercises.WinForms.Forms
{
    /// <summary>
    /// Formulario principal que actúa como menú de navegación
    /// </summary>
    public partial class MainMenuForm : Form
    {
        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel pnlContent;
        private FlowLayoutPanel flpExercises;
        private Panel pnlFooter;
        private Label lblFooter;
        private readonly ExerciseManager exerciseManager;

        public MainMenuForm()
        {
            exerciseManager = new ExerciseManager();
            InitializeComponents();
            SetupForm();
            CreateExerciseButtons();
        }

        private void InitializeComponents()
        {
            this.SuspendLayout();

            // Panel de encabezado
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = Color.FromArgb(52, 152, 219),
                Name = "pnlHeader"
            };

            // Título principal
            lblTitle = new Label
            {
                Text = "ESTRUCTURA DE DATOS",
                Dock = DockStyle.Top,
                Height = 50,
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Name = "lblTitle"
            };

            // Subtítulo
            lblSubtitle = new Label
            {
                Text = "Práctica de Arreglos - Seleccione un ejercicio para comenzar",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 12F),
                ForeColor = Color.FromArgb(236, 240, 241),
                TextAlign = ContentAlignment.MiddleCenter,
                Name = "lblSubtitle"
            };

            // Panel de contenido
            pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(236, 240, 241),
                Padding = new Padding(50),
                Name = "pnlContent"
            };

            // FlowLayoutPanel para los botones de ejercicios
            flpExercises = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.Transparent,
                Name = "flpExercises"
            };

            // Panel de pie de página
            pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = Color.FromArgb(44, 62, 80),
                Name = "pnlFooter"
            };

            // Etiqueta de pie de página
            lblFooter = new Label
            {
                Text = "Ing. Mirian Magaly Canché Caamal | Estructura de Datos | 2025",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Name = "lblFooter"
            };

            // Agregar controles
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblTitle);
            
            pnlContent.Controls.Add(flpExercises);
            pnlFooter.Controls.Add(lblFooter);

            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlFooter);
            this.Controls.Add(pnlHeader);

            this.ResumeLayout(false);
        }

        private void SetupForm()
        {
            this.Text = "Práctica de Arreglos - Estructura de Datos";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Icon = SystemIcons.Application;
        }

        private void CreateExerciseButtons()
        {
            var exercises = exerciseManager.GetAvailableExercises();
            
            foreach (var exercise in exercises.Take(3)) // Solo los primeros 3 ejercicios
            {
                var button = CreateExerciseButton(exercise.ExerciseNumber, 
                                                exercise.ExerciseName, 
                                                exercise.Description);
                flpExercises.Controls.Add(button);
            }

            // Botón para agregar más ejercicios (placeholder)
            var btnAddMore = CreatePlaceholderButton();
            flpExercises.Controls.Add(btnAddMore);

            // Botón de salir
            var btnExit = CreateExitButton();
            flpExercises.Controls.Add(btnExit);
        }

        private Panel CreateExerciseButton(int exerciseNumber, string title, string description)
        {
            var panel = new Panel
            {
                Size = new Size(750, 80),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(10, 5, 10, 5),
                Cursor = Cursors.Hand
            };

            var lblNumber = new Label
            {
                Text = exerciseNumber.ToString(),
                Location = new Point(20, 25),
                Size = new Size(40, 30),
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var lblTitle = new Label
            {
                Text = title,
                Location = new Point(70, 15),
                Size = new Size(600, 25),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            var lblDesc = new Label
            {
                Text = description,
                Location = new Point(70, 40),
                Size = new Size(600, 25),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(127, 140, 141)
            };

            var btnArrow = new Label
            {
                Text = "▶",
                Location = new Point(680, 25),
                Size = new Size(30, 30),
                Font = new Font("Segoe UI", 16F),
                ForeColor = Color.FromArgb(46, 204, 113),
                TextAlign = ContentAlignment.MiddleCenter
            };

            panel.Controls.AddRange(new Control[] { lblNumber, lblTitle, lblDesc, btnArrow });

            // Efectos hover
            ApplyHoverEffect(panel);

            // Evento click
            panel.Click += (s, e) => OpenExercise(exerciseNumber);
            foreach (Control control in panel.Controls)
            {
                control.Click += (s, e) => OpenExercise(exerciseNumber);
            }

            return panel;
        }

        private Panel CreatePlaceholderButton()
        {
            var panel = new Panel
            {
                Size = new Size(750, 60),
                BackColor = Color.FromArgb(149, 165, 166),
                BorderStyle = BorderStyle.None,
                Margin = new Padding(10, 5, 10, 5)
            };

            var lblText = new Label
            {
                Text = "Ejercicios 4-7: Disponibles para implementación futura",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 11F, FontStyle.Italic),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };

            panel.Controls.Add(lblText);
            return panel;
        }

        private Panel CreateExitButton()
        {
            var panel = new Panel
            {
                Size = new Size(750, 60),
                BackColor = Color.FromArgb(231, 76, 60),
                BorderStyle = BorderStyle.None,
                Margin = new Padding(10, 15, 10, 5),
                Cursor = Cursors.Hand
            };

            var lblText = new Label
            {
                Text = "🚪 Salir de la aplicación",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };

            panel.Controls.Add(lblText);

            // Efectos hover
            ApplyHoverEffect(panel, Color.FromArgb(192, 57, 43));

            // Evento click
            panel.Click += (s, e) => ExitApplication();
            lblText.Click += (s, e) => ExitApplication();

            return panel;
        }

        private void ApplyHoverEffect(Panel panel, Color? hoverColor = null)
        {
            Color originalColor = panel.BackColor;
            Color hover = hoverColor ?? Color.FromArgb(245, 245, 245);

            panel.MouseEnter += (s, e) => panel.BackColor = hover;
            panel.MouseLeave += (s, e) => panel.BackColor = originalColor;
        }

        private void OpenExercise(int exerciseNumber)
        {
            try
            {
                var form = exerciseManager.GetExerciseForm(exerciseNumber);
                if (form != null)
                {
                    this.Hide();
                    form.FormClosed += (s, e) => this.Show();
                    form.Show();
                }
                else
                {
                    MessageBox.Show($"El ejercicio {exerciseNumber} aún no está implementado.", 
                                  "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el ejercicio: {ex.Message}", 
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExitApplication()
        {
            var result = MessageBox.Show("¿Está seguro que desea salir de la aplicación?", 
                                       "Confirmar salida", MessageBoxButtons.YesNo, 
                                       MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}