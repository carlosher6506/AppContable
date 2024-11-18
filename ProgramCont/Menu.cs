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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void btnEstado_Click_1(object sender, EventArgs e)
        {
            MenuEstadoResultado menuEstadoResultado = new MenuEstadoResultado();
            this.Hide();
            menuEstadoResultado.ShowDialog();
            this.Show();
        }

        private void btnBalance_Click(object sender, EventArgs e)
        {
            MenuBalanceGeneral menuBalanceGeneral = new MenuBalanceGeneral();
            this.Hide();
            menuBalanceGeneral.ShowDialog();
            this.Show();
        }
    }
}
