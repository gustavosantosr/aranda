using WebAPIProductos.Entidades;
using WebAPIProductos.DTOs;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebAPIProductos.Utilidades
{
    public static class IQueryableExtensions
    {
      public  static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, PaginacionDTOs paginacionDTO)
      {
       return queryable
       .Skip((paginacionDTO.Pagina-1)* paginacionDTO.RecordsPorPagina)
       .Take(paginacionDTO.RecordsPorPagina);
      }
       
    }
}