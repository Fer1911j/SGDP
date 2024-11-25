using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using APPGDP0001.Models;

namespace APPGDP0001.Services
{
    public partial class InstalacionesDataBase
    {
        private readonly string ConnectionString = "Server=bty5zrienhqxuu2hfxso-mysql.services.clever-cloud.com;Port=3306;Database=bty5zrienhqxuu2hfxso;User=uhmemuoixa55byvg;Password=NcJmIS1hGhAhM0hiQAMG;";


        public async Task<List<Instalaciones>> GetInstalacionesAsync()
        {
            var Instalaciones = new List<Instalaciones>();

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("sp_mostrarInstalaciones", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var instalaciones = new Instalaciones
                            {
                                Id_instalacion = reader.GetInt32("id_instalacion"),
                                Id_proyecto= reader.GetInt32("id_proyecto"),
                                Ubicacion = reader.GetString("ubicacion"),
                                Fecha_inicio = reader.GetDateTime("fecha_inicio"),
                                Fecha_fin = reader.GetDateTime("fecha_fin"),
                                EstadoInstalacion = (EstadoInstalacion)Enum.Parse(typeof(EstadoInstalacion), reader.GetString("estado_instalacion")),
                                Coordenadas_gps = reader.GetString("coordenadas_gps"),
                                Id_tecnico = reader.GetInt32("id_tecnico")

                            };
                            Instalaciones.Add(instalaciones);
                        }
                    }
                }
            }

            return Instalaciones;
        }
    }
}
