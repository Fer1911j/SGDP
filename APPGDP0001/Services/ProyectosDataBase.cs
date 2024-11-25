using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using APPGDP0001.Models;

namespace APPGDP0001.Services
{
    public partial class ProyectosDataBase
    {
        private readonly string ConnectionString = "Server=bty5zrienhqxuu2hfxso-mysql.services.clever-cloud.com;Port=3306;Database=bty5zrienhqxuu2hfxso;User=uhmemuoixa55byvg;Password=NcJmIS1hGhAhM0hiQAMG;";


        public async Task<List<Proyectos>> GetProyectosAsync()
        {
            var Proyectos = new List<Proyectos>();

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("sp_mostrarProyectos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var proyectos = new Proyectos
                            {
                                Id_proyecto = reader.GetInt32("id_proyecto"),
                                Id_cliente = reader.GetInt32("id_cliente"),
                                Nombre_proyecto = reader.GetString("nombre_proyecto"),
                                Fecha_inicio = reader.GetDateTime("fecha_inicio"),
                                Fecha_fin = reader.GetDateTime("fecha_fin"),
                                EstadoProyectos = (EstadoProyectos)Enum.Parse(typeof(EstadoProyectos), reader.GetString("estado")),
                                Descripcion = reader.GetString("descripcion"),
                                Id_equipo = reader.GetInt32("id_equipo")

                            };
                            Proyectos.Add(proyectos);
                        }
                    }
                }
            }

            return Proyectos;
        }
    }
}
