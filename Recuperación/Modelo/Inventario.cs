namespace Preparcial.Modelo
{
    public class Inventario
    {//Se renombró las variables al igual que se editó el constructor
        public string IdArticulo { get; }
        public string Producto { get; }
        public string Descripcion { get; }
        public string Precio { get; }
        public string Stock { get; }

        public Inventario(string idArticulo, string producto, string descripcion, string precio, string stock)
        {
            IdArticulo = idArticulo;
            Producto = producto;
            Descripcion = descripcion;
            Precio = precio;
            Stock = stock;
        }
    }
}
