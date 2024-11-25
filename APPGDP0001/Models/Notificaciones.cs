
namespace APPGDP0001.Models
{
    public class Notificaciones
    {
        public int IdNotificaciones { get; set; }
        public int IdUsuario { get; set; }
        public string? Mensaje { get; set; }
        public string? Leido { get; set; }
        public DateTime FechaEnvio { get; set; }
    }
}
