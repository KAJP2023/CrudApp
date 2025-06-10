using CrudAppApi.Data;
using CrudAppApi.Model.Domain;
using CrudAppApi.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CrudAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelefonosController : ControllerBase
    {
        private readonly CrudAppDbContext dbContext;

        public TelefonosController(CrudAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // Get all telefonos
        [HttpGet]
        public IActionResult GetTelefonos()
        {
            // Get data from database - Domain Models
            var telefonosDomain = dbContext.Telefonos.ToList();

            // Map domain models to DTO
            var telefonosDto = new List<TelefonoDto>();
            foreach (var telefonoDomain in telefonosDomain)
            {
                telefonosDto.Add(new TelefonoDto()
                {
                    Id = telefonoDomain.Id,
                    Numero = telefonoDomain.Numero,
                    TipoDeNumero = telefonoDomain.TipoDeNumero,
                    UsuarioId = telefonoDomain.UsuarioId,
                });

            }
            // Return DTOs
            return Ok(telefonosDto);
        }

        // Get telefono by ID
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetTelefonoById([FromRoute] Guid id)
        {
            // Get data from database - domain models

            var telefono = dbContext.Telefonos.FirstOrDefault(x => x.Id == id);

            if (telefono == null)
            {
                return NotFound();
            }

            // Map data to Dto
            var telefonoDto = new TelefonoDto
            {
                Id = telefono.Id,
                Numero = telefono.Numero,
                TipoDeNumero = telefono.TipoDeNumero,
                UsuarioId = telefono.UsuarioId,
            };

            // Return DTO to client
            return Ok(telefonoDto);
        }
        [HttpPost]
        public IActionResult CreateTelefono([FromBody] AddTelefonoRequestDto addTelefonoRequestDto)
        {
            var telefonoDomainModel = new Telefono
            {
                Numero = addTelefonoRequestDto.Numero,
                TipoDeNumero = addTelefonoRequestDto.TipoDeNumero,
                UsuarioId = addTelefonoRequestDto.UsuarioId,
            };

            // Use domain model to create telefono
            dbContext.Telefonos.Add(telefonoDomainModel);
            dbContext.SaveChanges();

            // Map domain model back to DTO
            var telefonoDto = new TelefonoDto
            {
                Id = telefonoDomainModel.Id,
                Numero = telefonoDomainModel.Numero,
                TipoDeNumero = telefonoDomainModel.TipoDeNumero,
                UsuarioId = telefonoDomainModel.UsuarioId,
            };

            return CreatedAtAction(nameof(GetTelefonoById), new { id = telefonoDto.Id}, telefonoDto);
        }
        // Update telefono by PUT
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateTelefono([FromRoute] Guid id, [FromBody] UpdateTelefonoRequestDto updateTelefonoRequestDto)
        {
            var telefonoDomainModel = dbContext.Telefonos.FirstOrDefault(x => x.Id == id);

            if(telefonoDomainModel == null)
            {
                return NotFound();
            }

            // Map DTO to domain model
            telefonoDomainModel.Numero = updateTelefonoRequestDto.Numero;
            telefonoDomainModel.TipoDeNumero = updateTelefonoRequestDto.TipoDeNumero;

            dbContext.SaveChanges();

            // Convert domain model to DTO
            var telefonoDto = new TelefonoDto
            {
                Id = telefonoDomainModel.Id,
                Numero = telefonoDomainModel.Numero,
                TipoDeNumero = telefonoDomainModel.TipoDeNumero,
                UsuarioId = telefonoDomainModel.UsuarioId,
            };

            return Ok(telefonoDto);
        }

        // Delete telefono by Delete
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteTelefono([FromRoute] Guid id)
        {
            // Find telefono
            var telefonoDomainModel = dbContext.Telefonos.FirstOrDefault(x => x.Id == id);

            if (telefonoDomainModel == null)
            {
                return NotFound();
            }

            // Delete telefono
            dbContext.Telefonos.Remove(telefonoDomainModel);
            dbContext.SaveChanges();

            // return deleted telefono back (OPTIONAL)
            // map Domain Model to DTO
            var telefonoDto = new TelefonoDto
            {
                Id = telefonoDomainModel.Id,
                Numero = telefonoDomainModel.Numero,
                TipoDeNumero = telefonoDomainModel.TipoDeNumero,
                UsuarioId = telefonoDomainModel.UsuarioId
            };
            return Ok(telefonoDto);
        }
    }
}
