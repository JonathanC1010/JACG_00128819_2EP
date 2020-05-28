using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial_02
{
    public partial class Form1 : Form
    {
        public UserWindow hugo = new UserWindow();
        public Form1()
        {
            InitializeComponent();
        }

        private void Admin_Click(object sender, EventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            
        }


        private void Acceder_Click(object sender, EventArgs e)
        {
            if (UsuarioIn.access)
            {
                this.Hide();
                hugo.Show();
            }
            else
            {
                MessageBox.Show("Ingrese datos correctos y presione OK");
            }
        }
    }
}