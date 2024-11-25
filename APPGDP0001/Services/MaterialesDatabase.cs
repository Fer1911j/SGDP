using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using APPGDP0001.Models;

namespace APPGDP0001.Services
{
    public class MaterialesDatabase
    {
        private readonly string ConnectionString = "Server=bty5zrienhqxuu2hfxso-mysql.services.clever-cloud.com;Port=3306;Database=bty5zrienhqxuu2hfxso;User=uhmemuoixa55byvg;Password=NcJmIS1hGhAhM0hiQAMG;";

        public async Task<List<Materiales>> GetClientesAsync()
        {
            var Materiales = new List<Materiales>();

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("sp_mostrarMateriales", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var materiales = new Materiales
                            {
                                IdMateriales = reader.GetInt32("id_material"),
                                NombreMaterial = reader.GetString("nombre_material"),
                                CantidadDisponible = reader.GetInt32("cantidad_disponible"),
                                CantidadUsada = reader.GetInt32("cantidad_usada")

                            };
                            Materiales.Add(materiales);
                        }
                    }
                }
            }

            return Materiales;
        }
    }
}
