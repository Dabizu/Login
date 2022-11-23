﻿using Login.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Login.Controllers.Herramientas;


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
            bool banderaUsuario = false, banderaPassword = false;
            if (result.Count() > 0)
            {
                //la razon porque entro aqui es porque la consulta encontro un usuario igual
                Console.WriteLine("entro al usuario");
                foreach (var persona in result)
                {
                    Console.WriteLine(persona.user);
                    string resDesPassword = des.Decrypt(persona.password);
                    if (resDesPassword.Equals(pass))
                    {
                        Console.WriteLine("el password es correcto");
                        banderaPassword= true;
                    }
                    else
                    {
                        banderaPassword= false;
                    }
                }
                banderaUsuario = true;
            }
            else
            {
                banderaUsuario=false;
            }

            if(banderaUsuario==true&&banderaPassword==true)
            {
                return Ok(true);
            }
            else
            {
                return BadRequest();
            }
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
            return Ok(true);
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