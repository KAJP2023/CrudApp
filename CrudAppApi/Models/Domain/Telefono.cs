namespace CrudAppApi.Model.Domain
{
    public class Telefono
    {
        public Guid Id { get; set; }

        public string Numero { get; set; }

        public string TipoDeNumero { get; set; }

        public Guid UsuarioId { get; set; }


        // Navigation properties
        public Usuario Usuarios { get; set; }
    }
}
