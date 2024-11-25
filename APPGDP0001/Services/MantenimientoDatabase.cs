using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using APPGDP0001.Models;
namespace APPGDP0001.Services
{
    public class MantenimientoDatabase
    {
        private readonly string ConnectionString = "Server=bty5zrienhqxuu2hfxso-mysql.services.clever-cloud.com;Port=3306;Database=bty5zrienhqxuu2hfxso;User=uhmemuoixa55byvg;Password=NcJmIS1hGhAhM0hiQAMG;";


        public async Task<List<Mantenimiento>> GetMantenimientoAsync()
        {
            var Mantenimiento = new List<Mantenimiento>();

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("sp_mostrarmantenimiento", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var mantenimientos = new Mantenimiento
                            {
                                IdMantenimiento = reader.GetInt32("id_mantenimiento"),
                                Id_proyecto = reader.GetInt32("id_proyecto"),
                                Id_tecnico = reader.GetInt32("id_tecnico"),
                                FechaProgramada = DateTime.TryParse(reader.GetString("fecha_programada"), out var fecha) ? fecha : default(DateTime),
                                descriopcion = reader.GetString("descripcion"),
                                EstadoMantenimiento = (EstadoMantenimiento)Enum.Parse(typeof(EstadoMantenimiento), reader.GetString("estado"))
                            };
                            Mantenimiento.Add(mantenimientos);
                        }
                    }
                }
            }

            return Mantenimiento;
        }
    }
}
