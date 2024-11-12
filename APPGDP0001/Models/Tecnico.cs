// Models/Tecnico.cs
namespace APPGDP0001.Models
{
    public class Tecnico
    {
        public int IdTecnico { get; set; }
        public string ?NombreTecnico { get; set; }
        public string ?Correo { get; set; }
        public string ?Telefono { get; set; }
        public string  ?EsSupervisor { get; set; }
    }
}