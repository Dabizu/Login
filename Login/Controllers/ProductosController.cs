using Login.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Login.Controllers
{
    [Route("productos")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly pruebaContext _context;
        public ProductosController(pruebaContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("RegisterProducto")]
        public IActionResult RegistarProducto([FromBody] RequestProducto res)
        {
            var listaProductos = _context.Productos.Where(p => p.Nombre.Equals(res.Nombre));
            int numeroRegistros = (int)listaProductos.Count();
            if( numeroRegistros < 9){
                Producto producto = new Producto { Nombre = res.Nombre , Marca =res.Marca};
                //producto.Nombre = res.Nombre;
                //producto.Marca = res.Marca;
                try
                {
                    _context.Productos.Add(producto);
                    _context.SaveChanges();
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("El usuario o la contraseña no coinciden!");
            }
        }
        [HttpGet]
        [Route("ListarProducto")]
        public IActionResult Get()
        {
            var listaProductos = _context.Productos.Where(p => p.Nombre.Equals("arroz")).ToList();
            if (listaProductos.Count() > 0)
                return Ok(listaProductos);
            else
                return BadRequest("El usuario o la contraseña no coinciden!");
        }

    }
}




