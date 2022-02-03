namespace WebAPIProductos.DTOs
{
    public class PaginacionDTOs
    {
         public int Pagina { get; set; }=1;
         private int recordsPorPagina =10;
         private readonly int cantidadMaxPagina =50;

         public int RecordsPorPagina
         {
             get{
                 return recordsPorPagina;
             }
             set{
                 recordsPorPagina=(value > cantidadMaxPagina)?cantidadMaxPagina: value;
             }
         }
       
    }
}