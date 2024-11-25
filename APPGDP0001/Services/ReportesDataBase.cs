using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using APPGDP0001.Models;
namespace APPGDP0001.Services
{
    public partial class ReportesDataBase
    {
        private readonly string ConnectionString = "Server=bty5zrienhqxuu2hfxso-mysql.services.clever-cloud.com;Port=3306;Database=bty5zrienhqxuu2hfxso;User=uhmemuoixa55byvg;Password=NcJmIS1hGhAhM0hiQAMG;";


        public async Task<List<Reportes>> GetReportesAsync()
        {
            var Reportes = new List<Reportes>();

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("sp_mostrarReportes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var reportes = new Reportes
                            {
                                Id_reporte = reader.GetInt32("id_reporte"),
                                Id_instalacion = reader.GetInt32("id_instalacion"),
                                descripcion = reader.GetString("descripcion"),
                                Acciones_tomadas = reader.GetString("acciones_tomadas"),
                                Fecha_reporte = reader.GetDateTime("fecha_reporte"),
                                Id_usuario = reader.GetInt32("id_usuario")

                            };
                            Reportes.Add(reportes);
                        }
                    }
                }
            }

            return Reportes;
        }
    }
}
