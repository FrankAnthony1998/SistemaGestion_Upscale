namespace MiWebApp.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int IntentosFallidos { get; set; }
        public bool EstaBloqueado { get; set; }
        public DateTime? FechaBloqueo { get; set; }
    }
}