using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Parcial_02
{
    public partial class AdminWindow : Form
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void AdminWindow_Load(object sender, EventArgs e)//carga los combo box y datagrid
        {
            
            var users = BaseDatos.ExecuteQuery("SELECT username FROM APPUSER");
            var usersCombo = new List<string>();
            var business = BaseDatos.ExecuteQuery("SELECT name FROM BUSINESS");
            var businessCombo = new List<string>();
            var product = BaseDatos.ExecuteQuery("SELECT name FROM PRODUCT");
            var productCombo = new List<string>();
            

            foreach (DataRow dr in users.Rows)
            {
                usersCombo.Add(dr[0].ToString());
            }
            foreach (DataRow dr in business.Rows)
            {
                businessCombo.Add(dr[0].ToString());
            }
            foreach (DataRow dr in product.Rows)
            {
                productCombo.Add(dr[0].ToString());
            }

            comboBox1.DataSource = usersCombo;
            comboBox2.DataSource = businessCombo;
            comboBox4.DataSource = businessCombo;
            comboBox3.DataSource = productCombo;
            
            try
            {
                var dt = BaseDatos.ExecuteQuery($"SELECT * FROM APPUSER; ");
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un problema");
            }
            
            try
            {
                var dt = BaseDatos.ExecuteQuery($"SELECT ao.idOrder, ao.createDate, pr.name, au.fullname, ad.address " +
                                                $"FROM APPORDER ao, ADDRESS ad, PRODUCT pr, APPUSER au " +
                                                $"WHERE ao.idProduct = pr.idProduct " +
                                                $"AND ao.idAddress = ad.idAddress " +
                                                $"AND ad.idUser = au.idUser; " );
                dataGridView2.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un problema");
            }
        }

        private void button2_Click(object sender, EventArgs e)//Agregar usuario
        {
            bool tipoUser = radioButton1.Checked;

            if (textBox1.Text.Equals("") || textBox2.Text.Equals(""))
            {
                MessageBox.Show("No se pueden dejar campos vacios");
            }
            else
            {
                try
                {
                    BaseDatos.ExecuteNonQuery($"INSERT INTO APPUSER(fullname, username, password, userType) " +
                                              $"VALUES('{textBox1.Text}', '{textBox2.Text}', '{textBox2.Text}', {tipoUser}); ");

                    MessageBox.Show("Se ha registrado al usuario");
                    AdminWindow_Load(null, null);
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Ha ocurrido un error");
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)//Eliminar usuario
        {         
            int idEliminar = 0;   
            try
            {//Obtiene id del user segun name
                var aux = BaseDatos.ExecuteQuery($"SELECT us.idUser FROM APPUSER us WHERE username = '{comboBox1.Text}' ");
                var auxlist = new List<string>();
               
                foreach (DataRow dr in aux.Rows)
                {
                    auxlist.Add(dr[0].ToString());
                }
                idEliminar = Convert.ToInt32(auxlist[0]);
               
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ha ocurrido un errorxd");
            }   
            
            try
            {
                BaseDatos.ExecuteNonQuery($"DELETE FROM APPUSER WHERE idUser = '{idEliminar}' ");
                MessageBox.Show("Se borró con éxito");
                AdminWindow_Load(null, null);
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ha ocurrido un error");
            }
        }

        private void button4_Click(object sender, EventArgs e)//Agregar negocio
        {
            if (textBox4.Text.Equals("") || textBox3.Text.Equals(""))
            {
                MessageBox.Show("No se pueden dejar campos vacios");
            }
            else
            {
                try
                {
                    BaseDatos.ExecuteNonQuery($"INSERT INTO BUSINESS(name, description) " +
                                              $"VALUES ('{textBox4.Text}', '{textBox3.Text}'); ");

                    MessageBox.Show("Se ha registrado el negocio");
                    AdminWindow_Load(null, null);
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Ha ocurrido un error");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)//Eliminar negocio
        { 
            int idEliminar = 0;   
            try
            {//Obtiene id del business segun name
                var aux = BaseDatos.ExecuteQuery($"SELECT us.idBusiness FROM BUSINESS us WHERE name = '{comboBox2.Text}' ");
                var auxlist = new List<string>();
               
                foreach (DataRow dr in aux.Rows)
                {
                    auxlist.Add(dr[0].ToString());
                }
                idEliminar = Convert.ToInt32(auxlist[0]);
               
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ha ocurrido un errorxd");
            }   
            
            try
            {
                BaseDatos.ExecuteNonQuery($"DELETE FROM BUSINESS WHERE idBusiness = '{idEliminar}' ");
                MessageBox.Show("Se borró con éxito");
                AdminWindow_Load(null, null);
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ha ocurrido un error");
            }
        }

        private void button6_Click(object sender, EventArgs e)//Agregar producto
        {
            int idBusiness;
            var aux = BaseDatos.ExecuteQuery($"SELECT us.idBusiness FROM BUSINESS us WHERE name = '{comboBox2.Text}' ");
            var auxlist = new List<string>();
               
            foreach (DataRow dr in aux.Rows)
            {
                auxlist.Add(dr[0].ToString());
            }
            idBusiness = Convert.ToInt32(auxlist[0]);
            
            if (textBox5.Text.Equals(""))
            {
                MessageBox.Show("No se pueden dejar campos vacios");
            }
            else
            {
                try
                {
                    BaseDatos.ExecuteNonQuery($"INSERT INTO PRODUCT(idBusiness, name) " +
                                              $"VALUES('{idBusiness}', '{textBox5.Text}');");

                    MessageBox.Show("Se ha registrado el producto");
                    AdminWindow_Load(null, null);
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Ha ocurrido un error");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)//Eliminar Producto
        {
            int idEliminar = 0;   
            try
            {//Obtiene id del product segun name
                var aux = BaseDatos.ExecuteQuery($"SELECT us.idProduct FROM PRODUCT us WHERE name = '{comboBox3.Text}' ");
                var auxlist = new List<string>();
               
                foreach (DataRow dr in aux.Rows)
                {
                    auxlist.Add(dr[0].ToString());
                }
                idEliminar = Convert.ToInt32(auxlist[0]);
               
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ha ocurrido un errorxd");
            }   
            
            try
            {
                BaseDatos.ExecuteNonQuery($"DELETE FROM PRODUCT WHERE idProduct = '{idEliminar}' ");
                MessageBox.Show("Se borró con éxito");
                AdminWindow_Load(null, null);

            }
            catch (Exception exp)
            {
                MessageBox.Show("Ha ocurrido un error");
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            int idBusiness = 0;
            
            try
            {//Obtiene id del business segun name
                var aux = BaseDatos.ExecuteQuery($"SELECT us.idBusiness FROM BUSINESS us WHERE name = '{comboBox4.Text}' ");
                var auxlist = new List<string>();
                           
                foreach (DataRow dr in aux.Rows)
                {
                    auxlist.Add(dr[0].ToString());
                }
                idBusiness = Convert.ToInt32(auxlist[0]);
                           
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ha ocurrido un errorxd");
            } 
            
            var product = BaseDatos.ExecuteQuery($"SELECT bu.name FROM PRODUCT bu WHERE idBusiness = '{idBusiness}' ");
            var productCombo = new List<string>();
        
            foreach (DataRow dr in product.Rows)
            {
                productCombo.Add(dr[0].ToString());
            }
        
            comboBox3.DataSource = productCombo;
        }
    }
}