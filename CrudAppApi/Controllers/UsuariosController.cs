using CrudAppApi.Data;
using CrudAppApi.Model.Domain;
using CrudAppApi.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CrudAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly CrudAppDbContext dbContext;

        public UsuariosController(CrudAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // Get all usuarios
        [HttpGet]
        public IActionResult GetUsuarios()
        {
            // Get data from database - domain models
            var usuariosDomainModel = dbContext.Usuarios.ToList();

            var usuariosDto = new List<UsuarioDto>();
            foreach (var usuarioDomainModel in usuariosDomainModel)
            {
                usuariosDto.Add(new UsuarioDto()
                {
                    Id = usuarioDomainModel.Id,
                    Nombre = usuarioDomainModel.Nombre,
                });
            }

            // Return DTOs
            return Ok(usuariosDto);

        }

        // Get Usuario and numbers by id
        [HttpGet]
        [Route("{id:Guid}")]
        
        public IActionResult GetUsuarioById([FromRoute] Guid id)
        {
            var usuario = dbContext.Usuarios.FirstOrDefault(x => x.Id == id);
            //var telefono = dbContext.Telefonos.FirstOrDefault(x => x.UsuarioId == id);
            var telefonoDomainModel = dbContext.Telefonos.ToList();
            var telefonos = telefonoDomainModel.FindAll(x => x.UsuarioId == id);
            if (usuario == null || telefonos == null)
            {
                return NotFound();
            }

            // Map usuario domain model to DTO
            var telefonosDto = new List<UsuarioNumeroDto>();
            foreach(var telefono in telefonos)
            {
                telefonosDto.Add(new UsuarioNumeroDto{
                    Nombre = usuario.Nombre,
                    Id = telefono.Id,
                    Numero = telefono.Numero,
                    TipoDeNumero = telefono.TipoDeNumero,
                    UsuarioId = telefono.UsuarioId,
                });
            }

            //var usuarioDto = new UsuarioDto
            //{
            //    Id = usuario.Id,
            //    Nombre = usuario.Nombre,
            //};

            //var usuarioNumeroDto = new 

            return Ok(telefonosDto);
        }

        // Create usuario by Post
        [HttpPost]

        public IActionResult CreateUsuario([FromBody] AddUsuarioRequestDto addUsuarioRequestDto)
        {
            // Map or convert Dto to domain model
            var usuarioDomainModel = new Usuario
            {
                Nombre = addUsuarioRequestDto.Nombre,
            };

            // Use domain model to create usuario
            dbContext.Add(usuarioDomainModel);
            dbContext.SaveChanges();

            // Map domain model back to DTO
            var usuarioDto = new UsuarioDto
            {
                Id = usuarioDomainModel.Id,
                Nombre = usuarioDomainModel.Nombre,
            };

            return CreatedAtAction(nameof(GetUsuarioById), new { id = usuarioDto.Id }, usuarioDto);
        }

        // Update Usuario by PUT
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateUsuario([FromRoute] Guid id ,[FromBody] UpdateUsuarioRequestDto updateUsuarioRequestDto)
        {
            // Check if usuario exists
            var usuarioDomainModel = dbContext.Usuarios.FirstOrDefault(x => x.Id == id);

            if(usuarioDomainModel == null)
            {
                return NotFound();
            }

            // Map DTO to domain model
            usuarioDomainModel.Nombre = updateUsuarioRequestDto.Nombre;

            dbContext.SaveChanges();

            // Map domain model to DTO
            var usuarioDto = new UsuarioDto
            {
                Id = usuarioDomainModel.Id,
                Nombre = usuarioDomainModel.Nombre,
            };

            return Ok(usuarioDto);
        }

        // Delete Usuario by Delete
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteUsuario([FromRoute] Guid id)
        {
            // Find usuario
            var usuarioDomainModel = dbContext.Usuarios.FirstOrDefault(x => x.Id == id);

            if(usuarioDomainModel == null)
            {
                return NotFound();
            }

            // Delete usuario
            dbContext.Usuarios.Remove(usuarioDomainModel);
            dbContext.SaveChanges();

            // Return deleted usuario back (OPTIONAL)
            // Map domain model back to DTO
            var usuarioDto = new UsuarioDto
            {
                Id = usuarioDomainModel.Id,
                Nombre = usuarioDomainModel.Nombre,
            };

            return Ok(usuarioDto);
        }
    }
}
