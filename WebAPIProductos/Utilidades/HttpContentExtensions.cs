using WebAPIProductos.Entidades;
using WebAPIProductos.DTOs;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebAPIProductos.Utilidades
{
    public static class HttpContentExtensions
    {
      public async static Task InsertarParametrosPaginacion<T>(this HttpContext httpContext, 
      IQueryable<T> queryable)
      {
        if (httpContext==null){throw new ArgumentException(nameof(httpContext));}
        double cantidad = await queryable.CountAsync();
        httpContext.Response.Headers.Add("CantidadTotalRegistros", cantidad.ToString());
      }
       
    }
}