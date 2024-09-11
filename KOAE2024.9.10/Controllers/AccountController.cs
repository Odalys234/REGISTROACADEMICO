//Importa el espacio de nombres necesarios para la utenticacion por cookies
using Microsoft.AspNetCore.Authentication.Cookies;
//importa el espacio de nombres necesario para la autenticacion
using Microsoft.AspNetCore.Authentication;
//importa el espacio de nombres necesarios para trabajar con controladores
using Microsoft.AspNetCore.Mvc;
//importa el espacio de nombres necesario para trabajar con reclamaciones(claims)
using System.Security.Claims;


namespace KOAE2024._9._10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login(string login, string password)
        {
            //Comprueba si las credenciales son validas
            if (login == "admin" && password == "12345")
            {
                //crea una lista de reclamaciones claims
                var claims = new List<Claim>{
                    //Agrega una reclamcion de nombre con el valor de login
                    new Claim(ClaimTypes.Name, login),
                };

                //crea una identidad de reclamaciones (claims identity)
                //con el esquema de autenticacion por cookies
                var claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                //crea propiedades de autenticacion adicionales 
                //
                var authPropieties = new AuthenticationProperties
                {
                    //
                };

                //inicia sesion del usuario
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authPropieties);

                //devuelve respuesta exitosa
                return Ok("Inicio de sesión correctamente. ");
            }
            else
            {
                //devuelve una respuesta no autorizada si las credenciales son incorrectas
                return Unauthorized("Credenciales incorrectas");
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            //cierra la sesion del usuario
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //devuelve una respuesta exitosa
            return Ok("Cerro sesion correctamente. ");
        }

    }
}
