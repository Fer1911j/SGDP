namespace APPGDP0001.Models
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Contrasena { get; set; }
        public Rol? Rol { get; set; }
        public Estado? Estado { get; set; }
    }
    public enum Rol
    {
        admin,
        usuario
    }
    public enum Estado
    {
        activo,
        suspendido,
        inactivo
    }
}