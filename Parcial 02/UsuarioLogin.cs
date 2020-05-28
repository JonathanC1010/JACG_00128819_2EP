using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Parcial_02
{
    public partial class UsuarioLogin : UserControl
    {
        private UserControl current = null;
        public UsuarioLogin()
        {
            InitializeComponent();
        }


        private void UsuarioLogin_Load(object sender, EventArgs e)
        {
            var users = BaseDatos.ExecuteQuery("SELECT username FROM APPUSER");
            var usersCombo = new List<string>();
            

            foreach (DataRow dr in users.Rows)
            {
                usersCombo.Add(dr[0].ToString());
            }

            comboBox1.DataSource = usersCombo;
        }

        private void button1_Click(object sender, EventArgs e)//Elegir datos
        {
            string contra;
            try
            {//Obtiene id del user segun name
                var aux = BaseDatos.ExecuteQuery($"SELECT us.idUser FROM APPUSER us WHERE username = '{comboBox1.Text}' ");
                var auxlist = new List<string>();
                var aux2 = BaseDatos.ExecuteQuery($"SELECT us.password FROM APPUSER us WHERE username = '{comboBox1.Text}' ");
                var aux2list = new List<string>();
               
                foreach (DataRow dr in aux.Rows)
                {
                    auxlist.Add(dr[0].ToString());
                }
                
                foreach (DataRow dr in aux2.Rows)
                {
                    aux2list.Add(dr[0].ToString());
                }
                contra = aux2list[0];

                if (contra != textBox2.Text)
                {
                    MessageBox.Show("Datos incorrectos");
                }
                else
                {
                    UsuarioIn.id = Convert.ToInt32(auxlist[0]);
                    MessageBox.Show("Datos correctos");
                    UsuarioIn.access = true;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ha ocurrido un errorxd");
            }  
        }
    }
}