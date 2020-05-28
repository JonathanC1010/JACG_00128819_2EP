using System;
using System.Windows.Forms;

namespace Parcial_02
{
    public partial class ViewPedidos : UserControl
    {
        public ViewPedidos()
        {
            InitializeComponent();
        }

        private void ViewPedidos_Load(object sender, EventArgs e)
        {
            try
            {
                var dt = BaseDatos.ExecuteQuery($"SELECT ao.idOrder, ao.createDate, pr.name, au.fullname, ad.address " +
                                                $"FROM APPORDER ao, ADDRESS ad, PRODUCT pr, APPUSER au " +
                                                $"WHERE ao.idProduct = pr.idProduct " +
                                                $"AND ao.idAddress = ad.idAddress " +
                                                $"AND ad.idUser = au.idUser; ");
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un problema");
            }
        }
    }
}