using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgramCont
{
    public partial class FormIngresarValores : Form
    {
        private Dictionary<string, TextBox> camposValores;
        private List<string> cuentasSeleccionadas;
        private FlowLayoutPanel panelPrincipal;
        private Panel panelActivos;
        private Panel panelPasivos;
        private Panel panelCapital;

        public FormIngresarValores(List<string> cuentas)
        {
            InitializeComponent();
            cuentasSeleccionadas = cuentas;
            camposValores = new Dictionary<string, TextBox>();
            ConfigurarFormulario();
            CrearPanelesSeccion();
            GenerarCamposDeValores();
            AgregarBotonesControl();
        }

        private void ConfigurarFormulario()
        {
            this.Text = "Ingreso de Valores - Balance General";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Panel principal con scroll
            panelPrincipal = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(20)
            };

            this.Controls.Add(panelPrincipal);
        }

        private void CrearPanelesSeccion()
        {
            // Panel para Activos
            panelActivos = CrearPanelSeccion("ACTIVOS");
            panelPrincipal.Controls.Add(panelActivos);

            // Panel para Pasivos
            panelPasivos = CrearPanelSeccion("PASIVOS");
            panelPrincipal.Controls.Add(panelPasivos);

            // Panel para Capital
            panelCapital = CrearPanelSeccion("CAPITAL");
            panelPrincipal.Controls.Add(panelCapital);
        }

        private Panel CrearPanelSeccion(string titulo)
        {
            Panel panel = new Panel
            {
                Width = 700,
                AutoSize = true,
                Padding = new Padding(10),
                Margin = new Padding(0, 0, 0, 20)
            };

            Label lblTitulo = new Label
            {
                Text = titulo,
                Font = new Font("Arial", 12, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(0, 0),
                Margin = new Padding(0, 0, 0, 10)
            };

            panel.Controls.Add(lblTitulo);
            return panel;
        }

        private void GenerarCamposDeValores()
        {
            foreach (string cuenta in cuentasSeleccionadas)
            {
                Panel panelDestino = DeterminarPanelDestino(cuenta);
                CrearCampoValor(cuenta, panelDestino);
            }
        }

        private Panel DeterminarPanelDestino(string cuenta)
        {
            if (EsActivo(cuenta)) return panelActivos;
            if (EsPasivo(cuenta)) return panelPasivos;
            return panelCapital;
        }

        private bool EsActivo(string cuenta)
        {
            string[] activosCuentas = new[]
            {
        "Caja", "Bancos", "Instrumentos Financieros", "Clientes",
        "Documentos Por Cobrar", "Deudores", "Inventarios", "Papelería y Útiles",
        "Terrenos", "Edificios", "Mobiliario y Equipo", "Equipo De Transporte",
        "Equipo De Reparto", "Gastos De Construcción", "Gastos De Organización",
        "Gastos De Instalación", "Terrenos No Utilizados"
    };
            return activosCuentas.Contains(cuenta);
        }

        private bool EsPasivo(string cuenta)
        {
            string[] pasivosCuentas = new[]
            {
        "Proveedores", "Documentos Por Pagar", "Acreedores"
    };
            return pasivosCuentas.Contains(cuenta);
        }

        private void CrearCampoValor(string nombreCuenta, Panel panelDestino)
        {
            // Container para cada campo
            Panel containerCampo = new Panel
            {
                Width = 650,
                Height = 35,
                Margin = new Padding(0, 5, 0, 5)
            };

            // Label para el nombre de la cuenta
            Label lblCuenta = new Label
            {
                Text = nombreCuenta,
                AutoSize = true,
                Location = new Point(10, 8)
            };

            // TextBox para el valor
            TextBox txtValor = new TextBox
            {
                Width = 150,
                Location = new Point(400, 5),
                TextAlign = HorizontalAlignment.Right
            };

            // Agregar validación para solo números y punto decimal
            txtValor.KeyPress += (sender, e) =>
            {
                // Permitir solo números, punto decimal y teclas de control
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }

                // Permitir solo un punto decimal
                if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
                {
                    e.Handled = true;
                }
            };

            containerCampo.Controls.Add(lblCuenta);
            containerCampo.Controls.Add(txtValor);
            panelDestino.Controls.Add(containerCampo);

            camposValores.Add(nombreCuenta, txtValor);
        }

        private void AgregarBotonesControl()
        {
            Panel panelBotones = new Panel
            {
                Width = 700,
                Height = 50,
                Padding = new Padding(10)
            };

            Button btnGenerar = new Button
            {
                Text = "Generar Balance General",
                Width = 200,
                Height = 30,
                Location = new Point(400, 10),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White
            };
            btnGenerar.Click += BtnGenerar_Click;

            Button btnVolver = new Button
            {
                Text = "Volver",
                Width = 100,
                Height = 30,
                Location = new Point(290, 10)
            };
            btnVolver.Click += (s, e) =>
            {
                this.Owner?.Show();
                this.Close();
            };

            panelBotones.Controls.Add(btnGenerar);
            panelBotones.Controls.Add(btnVolver);
            panelPrincipal.Controls.Add(panelBotones);
        }

        private void BtnGenerar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                //GenerarBalanceGeneralPDF();
            }
            else
            {
                MessageBox.Show("Por favor complete todos los campos con valores válidos.",
                               "Error de validación",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
            }
        }

        private bool ValidarCampos()
        {
            foreach (var campo in camposValores)
            {
                if (string.IsNullOrWhiteSpace(campo.Value.Text))
                    return false;

                if (!decimal.TryParse(campo.Value.Text, out _))
                    return false;
            }
            return true;
        }






















    }
}
