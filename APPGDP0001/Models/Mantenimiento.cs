using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPGDP0001.Models
{
    public  class Mantenimiento
    {
        public int IdMantenimiento { get;set; }
        public int Id_proyecto { get;set; }
        public int Id_tecnico {  get;set; }
        public DateTime FechaProgramada { get;set; }
        public string? descriopcion {  get;set; }
        public EstadoMantenimiento? EstadoMantenimiento { get;set; } 
    }
    public enum EstadoMantenimiento { 
        programado,
        realizado,
        pendiente
    }
}
