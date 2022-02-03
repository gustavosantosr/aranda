namespace WebAPIProductos.DTOs
{
    public class ProductoCreacionDTOs
    {
       
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public int CategoriaId { get; set; }
        
        public string Imagen { get; set; }
    }
}