// Importa los espacios de nombres necesarios para la autorización y los controladores
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KOAE2024._9._10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotaController : ControllerBase
    {
        // Crea una lista de objetos 'notas' para almacenar las notas
        static List<object> notas = new List<object>();
       
        [HttpGet("ObtenerNotas")]
        // Permite el acceso público a esta acción, incluso si no se ha autenticado el usuario
        [AllowAnonymous]
        public IEnumerable<object> ObtenerNotas()
        {
            // Devuelve las notas almacenadas en la lista en respuesta a una solicitud GET
            return notas;
        }

        [HttpPost("RegistrarNotas")]
        // Solo permite el acceso a esta acción a los usuarios autenticados
        [Authorize]
        public IActionResult RegistrarNotas(string nombre, double calificacion)
        {
            // Agrega la nueva nota a la lista 'notas'
            notas.Add(new {nombre,calificacion});

            // Devuelve una respuesta HTTP exitosa
            return Ok("Nota registrada con éxito");
        }
    }
}
