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
        /*
        [HttpGet]
        [Route("logiarteJson")]
        public IActionResult Getjeson(string json)
        {
            var jobject=JObject.Parse(json);
            Console.WriteLine(jobject.ToString());
            Console.WriteLine();

            //return Ok();
            
            var usuarios = context.Users;
            var result = usuarios.Where(usuarios => usuarios.user.Equals(jobject.GetValue("user")) && usuarios.password.Equals(jobject.GetValue("password")));
            Console.WriteLine("estos son el numero de datos: " + result.Count() + "\n");
            if (result.Count() > 0)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }

        }
        //cadena que ayuda a hacer la encriptacion o desencriptar 
        static string key { get; set; } = "A!9HHhi%XjjYY4YP2@Nob009X";
        public static string Encrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
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
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }*/

    }

    
}
