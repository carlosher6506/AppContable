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
    public partial class MenuBalanceGeneral : Form
    {
        private Dictionary<string, List<CheckBox>> seccionesCuentas;
        public MenuBalanceGeneral()
        {
            InitializeComponent();
            InicializarDiccionarioCuentas();
        }

        private void InicializarDiccionarioCuentas()
        {
            seccionesCuentas = new Dictionary<string, List<CheckBox>>();

            // Inicializar las secciones
            seccionesCuentas.Add("Activo Circulante", new List<CheckBox>());
            seccionesCuentas.Add("Activo Fijo", new List<CheckBox>());
            seccionesCuentas.Add("Activo Diferido", new List<CheckBox>());
            seccionesCuentas.Add("Otros Activos", new List<CheckBox>());
            seccionesCuentas.Add("Pasivo Circulante", new List<CheckBox>());
            seccionesCuentas.Add("Capital Ganado", new List<CheckBox>());
            seccionesCuentas.Add("Capital Contribuido", new List<CheckBox>());

            // Recorrer todos los controles del formulario
            foreach (Control control in this.Controls)
            {
                if (control is CheckBox checkbox)
                {
                    AsignarCheckBoxASeccion(checkbox);
                }
            }
        }

        private void AsignarCheckBoxASeccion(CheckBox checkbox)
        {
            // Activo Circulante
            if (new[] { "Caja", "Bancos", "Instrumentos Financieros", "Clientes",
                "Documentos Por Cobrar", "Deudores", "Inventarios",
                "Papelería y Útiles" }.Contains(checkbox.Text))
            {
                seccionesCuentas["Activo Circulante"].Add(checkbox);
            }
            // Activo Fijo
            else if (new[] { "Terrenos", "Edificios", "Mobiliario y Equipo",
                     "Equipo De Transporte", "Equipo De Reparto" }.Contains(checkbox.Text))
            {
                seccionesCuentas["Activo Fijo"].Add(checkbox);
            }
            // Activo Diferido
            else if (new[] { "Gastos De Construcción", "Gastos De Organización",
                     "Gastos De Instalación" }.Contains(checkbox.Text))
            {
                seccionesCuentas["Activo Diferido"].Add(checkbox);
            }
            // Otros Activos
            else if (new[] { "Terrenos No Utilizados" }.Contains(checkbox.Text))
            {
                seccionesCuentas["Otros Activos"].Add(checkbox);
            }
            // Pasivo Circulante
            else if (new[] { "Proveedores", "Documentos Por Pagar",
                     "Acreedores" }.Contains(checkbox.Text))
            {
                seccionesCuentas["Pasivo Circulante"].Add(checkbox);
            }
            // Capital Ganado
            else if (new[] { "Utilidades Acumuladas", "Pérdidas Acumuladas",
                     "Utilidad Neta Del Ejercicio" }.Contains(checkbox.Text))
            {
                seccionesCuentas["Capital Ganado"].Add(checkbox);
            }
            // Capital Contribuido
            else if (new[] { "Capital Social",
                     "Aportaciones Para Futuros Aumentos De Capital",
                     "Donaciones" }.Contains(checkbox.Text))
            {
                seccionesCuentas["Capital Contribuido"].Add(checkbox);
            }
        }

        private List<string> ObtenerCuentasSeleccionadas()
        {
            List<string> cuentasSeleccionadas = new List<string>();

            foreach (var seccion in seccionesCuentas)
            {
                foreach (var checkbox in seccion.Value)
                {
                    if (checkbox.Checked)
                    {
                        cuentasSeleccionadas.Add(checkbox.Text);
                    }
                }
            }

            return cuentasSeleccionadas;
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            List<string> cuentasSeleccionadas = ObtenerCuentasSeleccionadas();

            if (cuentasSeleccionadas.Count > 0)
            {
                var formValores = new FormIngresarValores(cuentasSeleccionadas)
                {
                    Owner = this
                };
                formValores.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Por favor seleccione al menos una cuenta.",
                               "Aviso",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
            }
        }
    }
}
