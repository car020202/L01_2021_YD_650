using L01_2021_YD_650.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2021_YD_650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly usuarioContext _usuarioDbContexto;
        public usuariosController(usuarioContext usuarioDbContexto)
        {
            _usuarioDbContexto = usuarioDbContexto;
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get() 
        {
            List<usuarios> listadoUsuarios = (from e in _usuarioDbContexto.usuarios
                                              select e).ToList();
            if (listadoUsuarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoUsuarios);
        }
        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarUsuario([FromBody]usuarios usuarios) 
        {
            try
            {
                _usuarioDbContexto.usuarios.Add(usuarios);
                _usuarioDbContexto.SaveChanges();
                return Ok(usuarios);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);

            }
        
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            var usuario = _usuarioDbContexto.usuarios.FirstOrDefault(u => u.usuarioId == id);
            if (usuario == null)
            {
                return NotFound($"El Usuario con ID {id} no se ha encontrado");
            }

            _usuarioDbContexto.usuarios.Remove(usuario);
            _usuarioDbContexto.SaveChanges();

            return Ok($"El usuario con ID {id} se ha eliminado");
        }

        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult ActualizarUsuario(int id, [FromBody] usuarios usuarioActualizar)
        {
            var usuario = _usuarioDbContexto.usuarios.FirstOrDefault(u => u.usuarioId == id);
            if (usuario == null)
            {
                return NotFound($"El usuario con ID {id} no se ha encontrado");
            }

            usuario.rolId = usuarioActualizar.rolId;
            usuario.nombreUsuario = usuarioActualizar.nombreUsuario;
            usuario.clave = usuarioActualizar.clave;
            usuario.nombre = usuarioActualizar.nombre;
            usuario.apellido = usuarioActualizar.apellido;

            _usuarioDbContexto.SaveChanges();

            return Ok($"El usuario con ID {id} se ha actualizado con exito");
        }

        [HttpGet]
        [Route("Rol")]
        public IActionResult BuscarPorRol(int rolId)
        {
            var usuariosFiltro = _usuarioDbContexto.usuarios
                .Where(u => u.rolId == rolId)
                .ToList();

            if (usuariosFiltro.Count == 0)
            {
                return NotFound("No han se han encontraron usuarios con ese rol");
            }

            return Ok(usuariosFiltro);
        }


        [HttpGet]
        [Route("NombreApellido")]
        public IActionResult BuscarPorNombreApellido(string nombre, string apellido)
        {
            var usuariosFiltro = _usuarioDbContexto.usuarios
                                    .Where(u => u.nombre.Contains(nombre) && u.apellido.Contains(apellido))
                                    .ToList();

            if (usuariosFiltro.Count == 0)
            {
                return NotFound("No se han encontraron usuarios con el nombre y apellido ingresados");
            }

            return Ok(usuariosFiltro);
        }

    }
}
