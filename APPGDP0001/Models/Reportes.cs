
namespace APPGDP0001.Models
{
    public class Reportes
    {
        public int Id_reporte { get; set; }
        public int Id_instalacion { get;set; }
        public string? descripcion { get; set; }
        public string? Acciones_tomadas { get; set; }
        public DateTime Fecha_reporte { get; set; } 
        public int Id_usuario { get; set; }
    }
}
