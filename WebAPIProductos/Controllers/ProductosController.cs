using Microsoft.AspNetCore.Mvc;
using WebAPIProductos.Entidades;
using WebAPIProductos.DTOs;
using WebAPIProductos.Utilidades;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace WebAPIProductos.Controllers
{
    [ApiController]
    [Route("Api/Productos")]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public IMapper Mapper { get; }

        public ProductosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            Mapper = mapper;
        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult<Producto>> get(int id)
        {
            var producto = await context.Productos.FirstOrDefaultAsync(x => x.Id == id);
            if (producto == null)
            {
                return NotFound();
            }
            return producto;
        }
        [HttpGet("filtro/{nombre}/{descripcion?}")]
        public async Task<ActionResult<List<Producto>>> get(string nombre, string descripcion)
        {
            var productos = await context.Productos.Include(x => x.Categoria).Where(x => x.Nombre.Contains(nombre) || x.Descripcion.Contains(descripcion)).ToListAsync();
            return productos;
        }
        [HttpGet]
        public async Task<ActionResult<List<Producto>>> get([FromQuery] PaginacionDTOs paginacionDTO)
        {
            var queryable= context.Productos.AsQueryable();
            await HttpContext.InsertarParametrosPaginacion(queryable);
            var productos= await queryable.OrderBy(producto=>producto.Nombre).Paginar(paginacionDTO).ToListAsync();
            return productos;
        }

        [HttpPost]

        public async Task<ActionResult> Post(ProductoCreacionDTOs productoCreacionDTO)
        {
            var existe = await context.Productos.AnyAsync(x=>x.Nombre== productoCreacionDTO.Nombre);
            if (existe){
                return BadRequest($"Ya existe un producto con el nombre {productoCreacionDTO.Nombre}");
            }
            var producto=Mapper.Map<Producto>(productoCreacionDTO);
            context.Add(producto);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Producto producto, int id)
        {
            if (producto.Id != id)
            {
                return BadRequest("El producto no coincide con el id de la URL");
            }
            var existe = await context.Productos.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            context.Update(producto);
            await context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Productos.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            context.Remove(new Producto() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}