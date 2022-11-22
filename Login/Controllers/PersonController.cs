using Login.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using RestSharp;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Login.Controllers.Herramientas;
using System.Runtime.Intrinsics.X86;

namespace Login.Controllers
{
    [Route("loginPersona")]
    public class PersonController : ControllerBase
    {

        private readonly pruebaContext context;
        public PersonController(pruebaContext conexion)
        {
            context = conexion;
        }

        [HttpGet]
        [Route("logiartePersona")]
        public IActionResult Getjson(string json)
        {

            JObject job= JObject.Parse(json);
            Descifrador des= new Descifrador();
            Console.WriteLine(job.GetValue("user"));
            string usua = (String) job.GetValue("user");
            string pass = (String) job.GetValue("password");
            Console.WriteLine(usua);
            var usuarios = context.Users;
            var result = usuarios.Where(usuarios => usuarios.user.Equals(usua));
            Console.WriteLine("estos son el numero de datos: " + result.Count() + "\n");
            Console.WriteLine(result);
            
            if (result.Count() > 0)
            {
                foreach (var persona in result)
                {
                    Console.WriteLine(persona.user);
                    string resDesPassword = des.Decrypt(persona.password);
                    var resultpassword = usuarios.Where(usuarios => usuarios.password.Equals(resDesPassword));
                    foreach(var personb in resultpassword)
                    {
                        if (personb.password.Equals(pass))
                        {
                            return Ok(resultpassword);
                        }
                        else
                        {
                            return BadRequest();
                        }
                    }
                    
                }
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
            
            /*
            var usuarios = context.Users;
            var result = usuarios.Where(usuarios => usuarios.user.Equals(user1) && usuarios.password.Equals(password1));
            Console.WriteLine("estos son el numero de datos: " + result.Count() + "\n");
            if (result.Count() > 0)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }*/
        }

        [HttpGet]
        [Route("registraPersona")]
        public IActionResult GetjsonRegistro(string json)
        {
            JObject job = JObject.Parse(json);
            Descifrador des = new Descifrador();
            string usua = (String)job.GetValue("user");
            string pass = (String)job.GetValue("password");
            User user = new User { user = usua, password = des.Encrypt(pass) };
            context.Users.Add(user);
            context.SaveChanges();
            return Ok();
        }
       

    }


}

/*
public class Descifrador
{
    //cadena que ayuda a hacer la encriptacion o desencriptar 
    static string key { get; set; } = "A!9HHhi%XjjYY4YP2@Nob009X";
    public static string Encrypt(string text)
    {
        using (var md5 = new MD5CryptoServiceProvider())
        {
            using (var tdes = new TripleDESCryptoServiceProvider())
            {
                tdes.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                using (var transform = tdes.CreateEncryptor())
                {
                    byte[] textBytes = Encoding.UTF8.GetBytes(text);
                    byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                    return Convert.ToBase64String(bytes, 0, bytes.Length);
                }
            }
        }
    }

    public static string Decrypt(string cipher)
    {
        using (var md5 = new MD5CryptoServiceProvider())
        {
            using (var tdes = new TripleDESCryptoServiceProvider())
            {
                tdes.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                using (var transform = tdes.CreateDecryptor())
                {
                    byte[] cipherBytes = Convert.FromBase64String(cipher);
                    byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                    return Encoding.UTF8.GetString(bytes);
                }
            }
        }
    }
}
*/