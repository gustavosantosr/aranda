using AutoMapper;
using WebAPIProductos.Entidades;
using WebAPIProductos.DTOs;

namespace WebAPIProductos.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
      public AutoMapperProfiles()
      {
          CreateMap<ProductoCreacionDTOs, Producto>();
          CreateMap<CategoriaCreacionDTOs, Categoria>();
      }
       
    }
}