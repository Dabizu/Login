using Login.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

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
        /*
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
                return BadRequest("Ya hay 9 productos identicos registra uno distinto!");
            }
        }*/

        [HttpPost]
        [Route("RegisterProducto")]
        public IActionResult usuarioProductoREgistrar([FromBody] RequestProductoEncrip r)
        {
            var listaProductos = _context.Productos.Where(p => p.Nombre.Equals(r.Nombre));
            int numeroRegistros = (int)listaProductos.Count();
            //string input = "a94652aa97c7211ba8954dd15a3cf838";
            var tipoUsuario = _context.Users.Where(u => u.user.Equals(r.Usuario)).Select(u => new { u.type }).FirstOrDefault();
            string tipo = (String)tipoUsuario.type.ToString();
            if (numeroRegistros < 9 && tipo.Equals("admin"))
            {
                Producto producto = new Producto()
                {
                    Nombre = r.Usuario,
                    Marca = r.Marca,
                    Usuario = GetMD5Hash(r.Usuario)
                };
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
                return BadRequest("Ya hay 9 productos identicos registra uno distinto!");
            }
        }

        private static String GetMD5Hash(string input)
        {
            string result = String.Empty;
            using (var myHash = MD5.Create())
            {
                var byteArrayResultOfRawData = Encoding.UTF8.GetBytes(input);
                var byteArrayResult = myHash.ComputeHash(byteArrayResultOfRawData);
                result = string.Concat(Array.ConvertAll(byteArrayResult, h => h.ToString("X2")));
            }
            return result;
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




