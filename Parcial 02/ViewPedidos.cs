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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {/*
            var dt = ConnectionDB.ExecuteQuery($"SELECT ins.idMateria,mat.nombre " +
                                                   "FROM MATERIA mat, INSCRIPCION ins, ESTUDIANTE est " +
                                                   "WHERE ins.idMateria = mat.idMateria AND ins.carnet = est.carnet "+
                                                   $"AND est.carnet='{textBox1.Text}'");
                dataGridView1.DataSource = dt;
            */
               
                MessageBox.Show("Datos obtenidos exitosamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un problema");
            }
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}