
namespace APPGDP0001.Models
{
    public class Instalaciones
    {
        public int Id_instalacion { get; set; }
        public int Id_proyecto { get; set; }
        public string? Ubicacion { get; set; }
        public DateTime Fecha_inicio { get; set; }
        public DateTime Fecha_fin { get;set; }
        public EstadoInstalacion? EstadoInstalacion { get; set; }
        public string? Coordenadas_gps { get; set; }
        public int Id_tecnico { get; set; }
    }
    public enum EstadoInstalacion
    {
        pendiente,
        en_proceso,
        completado
    }
}
