using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using APPGDP0001.Models;

namespace APPGDP0001.Services
{
    public class UsuariosDatabaseService
    {
        private readonly string ConnectionString = "Server=bty5zrienhqxuu2hfxso-mysql.services.clever-cloud.com;Port=3306;Database=bty5zrienhqxuu2hfxso;User=uhmemuoixa55byvg;Password=NcJmIS1hGhAhM0hiQAMG;";


        public async Task<List<Usuarios>> GetUsuariosAsync()
        {
            var usuarios = new List<Usuarios>();

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("sp_mostrarUsuarios", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var usuario = new Usuarios
                            {
                                Id = reader.GetInt32("id"),
                                Nombre = reader.GetString("nombre_usuario"),
                                Email = reader.GetString("email"),
                                Contrasena = reader.GetString("contrasena_hash"),
                                Rol = (Rol)Enum.Parse(typeof(Rol), reader.GetString("rol")), // Cambia el string del rol a enum
                                Estado = (Estado)Enum.Parse(typeof(Estado), reader.GetString("estado")) // Cambia el string del estado a enum
                            };
                            usuarios.Add(usuario);
                        }
                    }
                }
            }

            return usuarios;
        }
        public async Task EliminarUsuarioAsync(int idUsuario)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("sp_eliminarUsuario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("sp_id", idUsuario);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task EditarClienteAsync(Usuarios usuario)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("sp_actualizarUsuario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("p_id", usuario.Id);
                    command.Parameters.AddWithValue("p_nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("p_email", usuario.Email);
                    command.Parameters.AddWithValue("p_contrasena", usuario.Contrasena);
                    command.Parameters.AddWithValue("p_rol", usuario.Rol.ToString()); // Convierte el enum a string
                    command.Parameters.AddWithValue("p_estado", usuario.Estado.ToString()); // Convierte el enum a string

                    await command.ExecuteNonQueryAsync();

                }
            }
        }
    }
}
