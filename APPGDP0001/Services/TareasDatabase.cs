using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using APPGDP0001.Models;
namespace APPGDP0001.Services
{
    public class TareasDatabase
    {
        private readonly string ConnectionString = "Server=bty5zrienhqxuu2hfxso-mysql.services.clever-cloud.com;Port=3306;Database=bty5zrienhqxuu2hfxso;User=uhmemuoixa55byvg;Password=NcJmIS1hGhAhM0hiQAMG;";

        public async Task<List<Tareas>> GetClientesAsync()
        {
            var Tareas = new List<Tareas>();

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("sp_mostrarTareas", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var tareas = new Tareas
                            {
                                IdTarea = reader.GetInt32("id_tarea"),
                                IdInstalacion = reader.GetInt32("id_instalacion"),
                                Descripcion = reader.GetString("descripcion"),
                                EstadoTareas = (EstadoTareas)Enum.Parse(typeof(EstadoTareas), reader.GetString("estado")),
                                IdTecnico = reader.GetInt32("id_tecnico"),
                                FechaInicio = reader.GetDateTime("fecha_inicio"),
                                FechaFin = reader.GetDateTime("fecha_fin")

                            };
                            Tareas.Add(tareas);
                        }
                    }
                }
            }

            return Tareas;
        }
    }
}
