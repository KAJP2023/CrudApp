namespace CrudAppApi.Models.DTO
{
    public class UsuarioNumeroDto
    {
        public string Nombre { get; set; }

        public Guid Id { get; set; }
        public string Numero { get; set; }

        public string TipoDeNumero { get; set; }

        public Guid UsuarioId { get; set; }
    }
}
