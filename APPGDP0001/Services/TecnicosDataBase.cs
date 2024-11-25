
using System.Text;
using APPGDP0001.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace APPGDP0001.Services
{
    public class TecnicosDataBase
    {
        private readonly string ConnectionString = "Server=bty5zrienhqxuu2hfxso-mysql.services.clever-cloud.com;Port=3306;Database=bty5zrienhqxuu2hfxso;User=uhmemuoixa55byvg;Password=NcJmIS1hGhAhM0hiQAMG;";


        public async Task<List<Tecnico>> GetClientesAsync()
        {
            var Tecnicos = new List<Tecnico>();

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
                            var tecnicos = new Tecnico
                            {
                                IdTecnico = reader.GetInt32("id_tecnico"),
                                NombreTecnico = reader.GetString("nombre_tecnico"),
                                Correo = reader.GetString("correo"),
                                Telefono = reader.GetString("telefono"),
                                EsSupervisor = reader.GetString("es_supervisor")
                                 
                            };
                            Tecnicos.Add(tecnicos);
                        }
                    }
                }
            }

            return Tecnicos;
        }
    }
}
