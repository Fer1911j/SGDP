using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using APPGDP0001.Models;
namespace APPGDP0001.Services
{
    public class EquiposDatabaseService
    {
        private readonly string ConnectionString = "Server=bty5zrienhqxuu2hfxso-mysql.services.clever-cloud.com;Port=3306;Database=bty5zrienhqxuu2hfxso;User=uhmemuoixa55byvg;Password=NcJmIS1hGhAhM0hiQAMG;";

        public async Task<List<Equipos>> GetEquiposAsync()
        {
            var equipos = new List<Equipos>();

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("sp_mostrarEquipos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var equipo = new Equipos
                            {
                                Idequipo= reader.GetInt32("id_equipo"),
                                NombreEquipo = reader.GetString("nombre_equipo"),
                                Descripcion = reader.GetString("descripcion")
                            };
                            equipos.Add(equipo);
                        }
                    }
                }
            }

            return equipos;
        }
    }
}
