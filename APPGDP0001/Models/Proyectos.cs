namespace APPGDP0001.Models
{
    public class Proyectos
    {
        public int Id_proyecto { get; set; }
        public int Id_cliente { get; set; } 
        public string? Nombre_proyecto {get; set; }
        public DateTime Fecha_inicio { get; set; }
        public DateTime Fecha_fin {  get; set; }
        public EstadoProyectos EstadoProyectos { get; set; }
        public string? Descripcion { get; set; }
        public int Id_equipo { get; set; }
    }
    public enum EstadoProyectos
    {
        pendiente,
        en_proceso,
        completado
    }
}
