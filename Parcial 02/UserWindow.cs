using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Parcial_02
{
    public partial class UserWindow : Form
    {
        public UserWindow()
        {
            InitializeComponent();
        }

        private void UserWindow_Load(object sender, EventArgs e)//Carga todos los datos
        {
        var product = BaseDatos.ExecuteQuery("SELECT name FROM PRODUCT ");
        var productCombo = new List<string>();
        var adress = BaseDatos.ExecuteQuery($"SELECT ad.address FROM ADDRESS ad WHERE idUser = '{UsuarioIn.id}' ");
        var adressCombo = new List<string>();
        var business = BaseDatos.ExecuteQuery("SELECT name FROM BUSINESS");
        var businessCombo = new List<string>();
        
        
        foreach (DataRow dr in business.Rows)
        {
            businessCombo.Add(dr[0].ToString());
        }
        foreach (DataRow dr in product.Rows)
        {
            productCombo.Add(dr[0].ToString());
        }
        foreach (DataRow dr in adress.Rows)
        {
            adressCombo.Add(dr[0].ToString());
        }
            
        comboBox1.DataSource = businessCombo;
        comboBox2.DataSource = productCombo;
        comboBox3.DataSource = adressCombo;
        comboBox4.DataSource = adressCombo;
        comboBox5.DataSource = adressCombo;
        
        try
        {
            var dt = BaseDatos.ExecuteQuery($"SELECT ao.idOrder, ao.createDate, pr.name, au.fullname, ad.address " +
                                            $"FROM APPORDER ao, ADDRESS ad, PRODUCT pr, APPUSER au " +
                                            $"WHERE ao.idProduct = pr.idProduct " +
                                            $"AND ao.idAddress = ad.idAddress " +
                                            $"AND ad.idUser = au.idUser " +
                                            $"AND au.idUser = '{UsuarioIn.id}'; ");
            dataGridView1.DataSource = dt;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ha ocurrido un problema");
        }
        }


        private void button1_Click(object sender, EventArgs e)//Realizar pedido
        {
            int idProduct = 0, idAddress = 0;
            
            //Obtiene id de la direccion segun name
            var aux1 = BaseDatos.ExecuteQuery($"SELECT us.idAddress FROM ADDRESS us WHERE address = '{comboBox3.Text}' ");
            var aux1list = new List<string>();
               
            foreach (DataRow dr in aux1.Rows)
            {
                aux1list.Add(dr[0].ToString());
            }
            idAddress = Convert.ToInt32(aux1list[0]);
            
            //Obtiene id del product segun name
            var aux = BaseDatos.ExecuteQuery($"SELECT us.idProduct FROM PRODUCT us WHERE name = '{comboBox2.Text}' ");
            var auxlist = new List<string>();
               
            foreach (DataRow dr in aux.Rows)
            {
                auxlist.Add(dr[0].ToString());
            }
            idProduct = Convert.ToInt32(auxlist[0]);
            try
            {
                BaseDatos.ExecuteNonQuery($"INSERT INTO APPORDER(createDate, idProduct, idAddress) " +
                                          $"VALUES('{UsuarioIn.fecha}' , '{idProduct}' , '{idAddress}'); ");

                MessageBox.Show("Se ha registrado el pedido");
                UserWindow_Load(null, null);
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ha ocurrido un error");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)//cambia las opciones del combo box 2
        {
            int idBusiness = 0;
            
            try
            {//Obtiene id del business segun name
                var aux = BaseDatos.ExecuteQuery($"SELECT us.idBusiness FROM BUSINESS us WHERE name = '{comboBox1.Text}' ");
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
        
            comboBox2.DataSource = productCombo;
        }

        private void button3_Click(object sender, EventArgs e)//Eliminar direccion
        {       
            int idEliminar = 0;   
            try
            {//Obtiene id de la direccion segun name
                var aux = BaseDatos.ExecuteQuery($"SELECT us.idAddress FROM ADDRESS us WHERE address = '{comboBox4.Text}' ");
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
                BaseDatos.ExecuteNonQuery($"DELETE FROM ADDRESS WHERE idAddress = '{idEliminar}' ");
                MessageBox.Show("Se borró con éxito");
                UserWindow_Load(null, null);
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ha ocurrido un error");
            }
        }

        private void button4_Click(object sender, EventArgs e)//Agregar dirección
        {
        
            if (textBox4.Text.Equals(""))
            {
                MessageBox.Show("No se pueden dejar campos vacios");
            }
            else
            {
                try
                {
                    BaseDatos.ExecuteNonQuery($"INSERT INTO ADDRESS(idUser, address) " +
                                              $"VALUES('{UsuarioIn.id}', '{textBox4.Text}'); ");

                    MessageBox.Show("Se ha registrado la dirección");
                    UserWindow_Load(null,null);
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Ha ocurrido un error");
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)//Actualizar Direccion
        {
        
            int idActualizar = 0;   
            try
            {//Obtiene id de la direccion segun name
                var aux = BaseDatos.ExecuteQuery($"SELECT us.idAddress FROM ADDRESS us WHERE address = '{comboBox5.Text}' ");
                var auxlist = new List<string>();
               
                foreach (DataRow dr in aux.Rows)
                {
                    auxlist.Add(dr[0].ToString());
                }
                idActualizar = Convert.ToInt32(auxlist[0]);
               
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ha ocurrido un errorxd");
            }   
            
            try
            {
                BaseDatos.ExecuteNonQuery($"UPDATE ADDRESS SET address = '{textBox1.Text}' WHERE idAddress = '{idActualizar}' ");
                MessageBox.Show("Se actualizó con éxito");
                UserWindow_Load(null, null);
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ha ocurrido un error");
            }
        }

        private void button5_Click(object sender, EventArgs e)//Actualizar contrasena
        {
            string viejaPass = "";
            if (textBox3.Text.Equals("") || textBox2.Text.Equals(""))
            {
                MessageBox.Show("No se pueden dejar campos vacios");
            }
            else
            {
                try
                {//Obtiene id de la direccion segun name
                    var aux = BaseDatos.ExecuteQuery($"SELECT us.password FROM APPUSER us WHERE idUser = '{UsuarioIn.id}' ");
                    var auxlist = new List<string>();
               
                    foreach (DataRow dr in aux.Rows)
                    {
                        auxlist.Add(dr[0].ToString());
                    }
                    viejaPass = auxlist[0];
               
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Ha ocurrido un errorxd");
                }
                if (viejaPass == textBox2.Text)
                {
                    try
                    {
                        BaseDatos.ExecuteNonQuery($"UPDATE APPUSER SET password = '{textBox3.Text}' WHERE idUser = '{UsuarioIn.id}' ");

                        MessageBox.Show("Se actualizó la contraseña");
                        UserWindow_Load(null,null);
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("Ha ocurrido un error");
                    }
                }
                else
                {
                    MessageBox.Show("Contraseña incorrecta");                    
                }       
            }
        }

        private void button6_Click(object sender, EventArgs e)//Eliminar orden
        {
        
        if (textBox5.Text.Equals(""))
        {
            MessageBox.Show("No se pueden dejar campos vacios");
        }
        else
        {
            int idEliminar = Convert.ToInt32(textBox5.Text);   
            try
            {
                BaseDatos.ExecuteNonQuery($"DELETE FROM APPORDER WHERE idOrder = '{idEliminar}' ");
                MessageBox.Show("Se borró con éxito");
                UserWindow_Load(null, null);
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ha ocurrido un error");
            }
        }
        
        
        
        }
    }
}