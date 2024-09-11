// Importa el espacio de nombres necesario para la autorización
using Microsoft.AspNetCore.Authorization;
// Importa el espacio de nombres necesario para trabajar con controladores
using Microsoft.AspNetCore.Mvc;

namespace KOAE2024._9._10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Aplica la autorización a todas las acciones del controlador
    public class MatriculaController : ControllerBase
    {
        // Crea una lista estática de objetos 'matricula' para almacenar las matrículas
        static List<Matricula> matriculas = new List<Matricula>();

        // GET: api/Matricula/5
        // Devuelve una matrícula específica por Id
        [HttpGet("{id}")]
        public IActionResult ObtenerPorIdMatricula(int id)
        {
            var matricula = matriculas.FirstOrDefault(m => m.Id == id);
            if (matricula != null)
            {
                return Ok(matricula);
            }
            return NotFound("Matrícula no encontrada");
        }

        // POST: api/Matricula
        // Crea una nueva matrícula
        [HttpPost("CrearMatricula")]
        public IActionResult CrearMatricula([FromBody] Matricula matricula)
        {
            matriculas.Add(matricula);
            return Ok("Matrícula creada exitosamente");
        }

        // PUT: api/Matricula/5
        // Modifica una matrícula existente por su Id
        [HttpPut("ModificarMatricula/{id}")]
        public IActionResult ModificarMatricula(int id, [FromBody] Matricula matriculaModificada)
        {
            var matricula = matriculas.FirstOrDefault(m => m.Id == id);
            if (matricula != null)
            {
                // Actualiza los campos de la matrícula existente
                matricula.Estudiante = matriculaModificada.Estudiante;
                matricula.Curso = matriculaModificada.Curso;
                matricula.FechaMatricula = matriculaModificada.FechaMatricula;

                return Ok("Matrícula modificada con éxito");
            }
            return NotFound("Matrícula no encontrada");
        }
    }

    // Clase que representa la entidad Matricula
    public class Matricula
    {
        public int Id { get; set; }
        public string Estudiante { get; set; }
        public string Curso { get; set; }
        public string FechaMatricula { get; set; }
    }
}
