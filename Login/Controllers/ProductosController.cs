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
            var listaProductos = _context.Users;
            bool band=false;
            foreach(var item in listaProductos)
            {
                Console.WriteLine("item: "+item.user);
                var user= MD5Hash(item.user);
                if(user.Equals(r.Usuario))
                {
                    Console.WriteLine("soy el usuario admin");
                    band = registraProducto(r);
                    break;
                }
                else
                {
                    Console.WriteLine("no soy el usuario");
                }
            }

            if (band==true)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("el usuario no existe");
            }

            /*
            var listaProductos = _context.Productos.Where(p => p.Nombre.Equals(r.Nombre));
            int numeroRegistros = (int)listaProductos.Count();
            //string input = "a94652aa97c7211ba8954dd15a3cf838";
            var tipoUsuario = _context.Users.Where(u => GetMD5Hash(u.user).Equals(r.Usuario)).Select(u => new { u.type });
            //Console.WriteLine("el tipo es:"+tipoUsuario);
            //return Ok(tipoUsuario);
            return Ok(tipoUsuario);
            */
            /*var array=tipoUsuario.ToArray();
            //return Ok(array[0]);
            
            if (numeroRegistros < 9 && array[0].Equals("admin"))
            {
                Producto producto = new Producto()
                {
                    Nombre = r.Usuario,
                    Marca = r.Marca
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
            }*/
        }

        public bool registraProducto(RequestProductoEncrip r)
        {
            var listaProductos = _context.Productos.Where(p => p.Nombre.Equals(r.Nombre));
            int numeroRegistros = (int)listaProductos.Count();
            //string input = "a94652aa97c7211ba8954dd15a3cf838";
            var tipoUsuario = _context.Users.Where(u => GetMD5Hash(u.user).Equals(r.Usuario)).Select(u => new { u.type });
            var array = tipoUsuario.ToArray();
            if (numeroRegistros < 9 && array[0].Equals("admin"))
            {
                Producto producto = new Producto()
                {
                    Nombre = r.Usuario,
                    Marca = r.Marca
                };
                try
                {
                    _context.Productos.Add(producto);
                    _context.SaveChanges();
                    return true;
                    //return NoContent();
                }
                catch (Exception ex)
                {
                    //return BadRequest(ex.Message);
                    return false;
                }
            }
            else
            {
                //return BadRequest("Ya hay 9 productos identicos registra uno distinto!");
                return false;
            }
        }


        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //metnin boyutundan hash hesaplar
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //hesapladıktan sonra hashi alır
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //her baytı 2 hexadecimal hane olarak değiştirir
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
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




