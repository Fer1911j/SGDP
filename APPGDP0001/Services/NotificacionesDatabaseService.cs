using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using APPGDP0001.Models;

namespace APPGDP0001.Services
{
    public class NotificacionesDatabaseService
    {
        private readonly string ConnectionString = "Server=bty5zrienhqxuu2hfxso-mysql.services.clever-cloud.com;Port=3306;Database=bty5zrienhqxuu2hfxso;User=uhmemuoixa55byvg;Password=NcJmIS1hGhAhM0hiQAMG;";

        public async Task<List<Notificaciones>> GetClientesAsync()
        {
            var Notificaciones = new List<Notificaciones>();

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("sp_GetAllTecnicos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var notificaciones = new Notificaciones
                            {
                                IdNotificaciones = reader.GetInt32("id_notificacion"),
                                IdUsuario = reader.GetInt32("id_usuario"),
                                Mensaje = reader.GetString("mensaje"),
                                Leido = reader.GetString("leido"),
                                FechaEnvio = reader.GetDateTime("fecha_envio")

                            };
                            Notificaciones.Add(notificaciones);
                        }
                    }
                }
            }

            return Notificaciones;
        }
    }
}
