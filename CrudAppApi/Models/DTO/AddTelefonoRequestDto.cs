namespace CrudAppApi.Models.DTO
{
    public class AddTelefonoRequestDto
    {
        public string Numero { get; set; }

        public string TipoDeNumero { get; set; }

        public Guid UsuarioId { get; set; }
    }
}
