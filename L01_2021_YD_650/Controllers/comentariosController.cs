using L01_2021_YD_650.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2021_YD_650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentariosController : ControllerBase
    {
        private readonly usuarioContext _usuarioContexto;
        public comentariosController(usuarioContext usuarioContexto)
        {
            _usuarioContexto = usuarioContexto;
        }
        
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<comentarios> listadoComentarios = (from e in _usuarioContexto.comentarios
                                                    select e).ToList();
            if (listadoComentarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoComentarios);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Addcomentario ([FromBody] comentarios comentario)
        {
            try
            {
                _usuarioContexto.comentarios.Add(comentario);
                _usuarioContexto.SaveChanges();

                return Ok("El comentario se ha agregado .");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult ActualizarComentario(int id, [FromBody] comentarios comentarioActualizado)
        {
            var comentario = _usuarioContexto.comentarios.FirstOrDefault(c => c.comentarioId == id);
            if (comentario == null)
            {
                return NotFound($"El comentario con ID {id} no se ha encontrado");
            }

            comentario.publicacionId = comentarioActualizado.publicacionId;
            comentario.usuarioId = comentarioActualizado.usuarioId;
            comentario.comentario = comentarioActualizado.comentario;

            _usuarioContexto.SaveChanges();

            return Ok($"El comentario con ID {id} se ha actualizado exitosamente");
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult EliminarComentario(int id)
        {
            var comentario = _usuarioContexto.comentarios.FirstOrDefault(c => c.comentarioId == id);
            if (comentario == null)
            {
                return NotFound($"La calificación con ID {id} no se ha encontrado");
            }

            _usuarioContexto.comentarios.Remove(comentario);
            _usuarioContexto.SaveChanges();

            return Ok($"La calificación con ID {id} se ha eliminado");
        }


        [HttpGet]
        [Route("ComentariosPorUsuario/{usuarioId}")]
        public IActionResult ComentariosPorUsuario(int usuarioId)
        {
            var comentarios = _usuarioContexto.comentarios.Where(c => c.usuarioId == usuarioId).ToList();
            if (comentarios.Count == 0)
            {
                return NotFound($"No se encontraron comentarios de el usuario con el ID {usuarioId}");
            }

            return Ok(comentarios);
        }

    }
}
