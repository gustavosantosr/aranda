namespace WebAPIProductos.Entidades
{
    public class Producto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public string Imagen { get; set; }
    }
}