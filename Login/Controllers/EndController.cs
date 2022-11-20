using Login.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;

namespace Login.Controllers
{
    [Route("loginUsuarios")]
    public class EndController : ControllerBase
    {

        private readonly pruebaContext context;
        public EndController(pruebaContext conexion)
        {
            context = conexion;
        }
        
        [HttpGet]
        [Route("logiarte")]
        public IActionResult Get(string user1, string password1)
        {
            var usuarios = context.Users;
            var result = usuarios.Where(usuarios => usuarios.user.Equals(user1) && usuarios.password.Equals(password1));
            Console.WriteLine("estos son el numero de datos: "+result.Count()+"\n");
            if (result.Count() > 0) {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
            
        }
    }

    
}
