using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using BCrypt.Net;
using MySql.Data.MySqlClient;
using APPGDP0001.Models; // Asegúrate de tener este namespace
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace APPGDP0001.Services
{
    public class DataBaseService
    {
        // Cadena de conexión con MySQL
        private readonly string ConnectionString = "Server=bty5zrienhqxuu2hfxso-mysql.services.clever-cloud.com;Port=3306;Database=bty5zrienhqxuu2hfxso;User=uhmemuoixa55byvg;Password=NcJmIS1hGhAhM0hiQAMG;";



        public async Task<bool> ValidateUser(string username, string password)
        {

            using (var cn = new MySqlConnection(ConnectionString))
            {
                await cn.OpenAsync();

                using (var cmd = new MySqlCommand("sp_ValidarUsuario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_nombre_usuario", username);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            string storedHash = reader.GetString("contrasena_hash");
                            string userStatus = reader.GetString("estado");

                            // Verificar si está activo
                            if (userStatus != "activo")
                            {
                                return false;
                            }

                            // Comparar el hash almacenado con la contraseña ingresada
                            if (BCrypt.Net.BCrypt.Verify(password, storedHash))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public async Task<string> GetUserRole(string username, string password)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("sp_ValidarUsuario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@p_nombre_usuario", username);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            string storedHash = reader.GetString("contrasena_hash");
                            if (BCrypt.Net.BCrypt.Verify(password, storedHash))
                            {
                                return reader.GetString("rol");
                            }
                        }
                    }
                }
            }
            return null; // Usuario no encontrado o contraseña incorrecta
        }
        public async Task<List<Cliente>> GetClientesAsync()
        {
            var clientes = new List<Cliente>();

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("sp_mostrarClientes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var cliente = new Cliente
                            {
                                Id = reader.GetInt32("id_cliente"),
                                Nombre = reader.GetString("nombre"),
                                Direccion = reader.GetString("direccion"),
                                Telefono = reader.GetString("telefono"),
                                Email = reader.GetString("email"),
                                FechaRegistro = reader.GetDateTime("fecha_registro")
                            };
                            clientes.Add(cliente);
                        }
                    }
                }
            }

            return clientes;
        }
        public async Task AgregarClienteAsync(string nombre, string direccion, string telefono, string email)
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    Console.WriteLine("Conexión a la base de datos abierta.");

                    using (var command = new MySqlCommand("sp_agregarCliente", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("p_nombre", nombre);
                        command.Parameters.AddWithValue("p_direccion", direccion);
                        command.Parameters.AddWithValue("p_telefono", telefono);
                        command.Parameters.AddWithValue("p_email", email);

                        await command.ExecuteNonQueryAsync();
                        Console.WriteLine("Cliente agregado en la base de datos.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la base de datos: {ex.Message}");
                throw;
            }
        }
        public async Task EliminarClienteAsync(int idCliente)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("sp_eliminarCliente", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("p_id_cliente", idCliente);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task EditarClienteAsync(Cliente cliente)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("sp_actualizarCliente", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("p_id_cliente", cliente.Id);
                    command.Parameters.AddWithValue("p_nombre", cliente.Nombre);
                    command.Parameters.AddWithValue("p_direccion", cliente.Direccion);
                    command.Parameters.AddWithValue("p_telefono", cliente.Telefono);
                    command.Parameters.AddWithValue("p_email", cliente.Email);
                        
                    await command.ExecuteNonQueryAsync();

                }
            }
        }

    }

}