using Microsoft.AspNetCore.Mvc;
using WebAPIProductos.Entidades;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebAPIProductos.DTOs;
using AutoMapper;

namespace WebAPIProductos.Controllers
{
    [ApiController]
    [Route("Api/Categorias")]
    public class CategoriasController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public IMapper Mapper { get; }

        public CategoriasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            Mapper = mapper;
        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult<Categoria>> get(int id)
        {
            var categoria = await context.Categorias.FirstOrDefaultAsync(x => x.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }
            return categoria;
        }
        [HttpGet("nombre/{nombre}")]
        public async Task<ActionResult<List<Categoria>>> get(string nombre)
        {
        var categorias = await context.Categorias.Include(x=>x.Productos).Where(x => x.Nombre.Contains(nombre)).ToListAsync();
          
          
            return categorias;
        }

    
        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> get()
        {
            return await context.Categorias.Include(x=>x.Productos).ToListAsync();
        }

        [HttpPost]

        public async Task<ActionResult> Post(CategoriaCreacionDTOs categoriaCreacionDTO)
        {
            var existe = await context.Categorias.AnyAsync(x=>x.Nombre== categoriaCreacionDTO.Nombre);
            if (existe){
                return BadRequest($"Ya existe un categoria con el nombre {categoriaCreacionDTO.Nombre}");
            }
             var categoria=Mapper.Map<Categoria>(categoriaCreacionDTO);
            context.Add(categoria);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Categoria categoria, int id)
        {
            if (categoria.Id != id)
            {
                return BadRequest("El categoria no coincide con el id de la URL");
            }
            var existe = await context.Categorias.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            context.Update(categoria);
            await context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Categorias.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            context.Remove(new Categoria() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}