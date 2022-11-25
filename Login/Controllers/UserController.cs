using Login.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly pruebaContext _context;
        public UserController(pruebaContext context) 
        { 
            _context = context;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] User user)
        {
            string encryptedPass = GetMD5Hash(user.password);
            var correctUser = _context.Users.Where(a => a.user == user.user && a.password == encryptedPass).FirstOrDefault();
            if(correctUser != null)
                return Ok(true);
            else
                return BadRequest("El usuario o la contraseña no coinciden!");
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] User user)
        {
            try
            {
                user.password = GetMD5Hash(user.password);
                _context.Users.Add(user);
                _context.SaveChanges();
                return NoContent();
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
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
        [Route("BuscaTipo")]
        public IActionResult dameTipo(string tipo) {
            var encontradoTipo = _context.Users.Where(a => a.type == tipo).Select(a=>new {a.user,a.type});
            if (encontradoTipo.Count() >0)
                return Ok(encontradoTipo);
            else
                return BadRequest("El usuario o la contraseña no coinciden!");
        }
    }
}
