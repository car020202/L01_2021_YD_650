using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2021_YD_650.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2021_YD_650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class calificacionesController : ControllerBase
    {
        private readonly usuarioContext _usuarioContexto;
        public calificacionesController(usuarioContext usuarioContexto)
        {
            _usuarioContexto = usuarioContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<calificaciones> listadoCalificaciones = (from e in _usuarioContexto.calificaciones
                                                          select e).ToList();
            if (listadoCalificaciones.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoCalificaciones);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult PostCalificacion([FromBody] calificaciones calificacion)
        {
            try
            {
                _usuarioContexto.calificaciones.Add(calificacion);
                _usuarioContexto.SaveChanges();

                return Ok("La calificación se ha agregado.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult ActualizarCalificacion(int id, [FromBody] calificaciones calificacionActualizada)
        {
            var calificacion = _usuarioContexto.calificaciones.FirstOrDefault(c => c.calificacionId == id);
            if (calificacion == null)
            {
                return NotFound($"La calificación con ID {id} no se ha encontrado");
            }

            calificacion.publicacionId = calificacionActualizada.publicacionId;
            calificacion.usuarioId = calificacionActualizada.usuarioId;
            calificacion.calificacion = calificacionActualizada.calificacion;

            _usuarioContexto.SaveChanges();

            return Ok($"La calificación con ID {id} se ha actualizado");
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult EliminarCalificacion(int id)
        {
            var calificacion = _usuarioContexto.calificaciones.FirstOrDefault(c => c.calificacionId == id);
            if (calificacion == null)
            {
                return NotFound($"La calificación con ID {id} no se ha encontrado");
            }

            _usuarioContexto.calificaciones.Remove(calificacion);
            _usuarioContexto.SaveChanges();

            return Ok($"La Calificación con ID {id} se ha eliminado");
        }

        [HttpGet]
        [Route("PorPublicacion/{publicacionId}")]
        public IActionResult PorPublicacion(int publicacionId)
        {
            var calificaciones = _usuarioContexto.calificaciones.Where(c => c.publicacionId == publicacionId).ToList();
            if (calificaciones.Count == 0)
            {
                return NotFound($"No se ha encontrado calificaciones para la publicación con ID {publicacionId}");
            }

            return Ok(calificaciones);
        }


    }
}
