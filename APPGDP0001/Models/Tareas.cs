
namespace APPGDP0001.Models
{
    public class Tareas
    {
        public int IdTarea { get; set; }
        public int IdInstalacion { get; set; }
        public string? Descripcion { get; set; }
        public EstadoTareas? EstadoTareas { get; set; } 
        public int IdTecnico { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
    public enum EstadoTareas
    {
        pendiente,
        en_proceso,
        completado
    }
}