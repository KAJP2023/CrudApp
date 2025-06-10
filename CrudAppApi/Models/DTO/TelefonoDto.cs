namespace CrudAppApi.Models.DTO
{
    public class TelefonoDto
    {

        public Guid Id { get; set; }
        public string Numero { get; set; }

        public string TipoDeNumero { get; set; }

        public Guid UsuarioId { get; set; }

    }
}
