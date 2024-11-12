using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net; // Para hashear la contraseña
using System.ComponentModel;
using System.Windows.Input;
using MySql.Data.MySqlClient;
using Microsoft.Maui.Controls;
using BCrypt.Net;
using System.Configuration;


namespace APPGDP0001.ViewModels
{
    public class AddUserViewModel : INotifyPropertyChanged
    {
        private string _nombreUsuario;
        private string _email;
        private string _contrasena;
        private string _rol;

        public string NombreUsuario
        { get => _nombreUsuario;
            set
            {
                _nombreUsuario = value;
                OnPropertyChanged(nameof(NombreUsuario));
            }     
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Contrasena
        {
            get => _contrasena;
            set
            {
                _contrasena = value;
                OnPropertyChanged(nameof(Contrasena));
            }
        }
        public string Rol
        {
            get => _rol;
            set
            {
                _rol = value;
                OnPropertyChanged(nameof(Rol));
            }
        }
        public ICommand AddUserCommand { get; }
        public AddUserViewModel()
        {
            AddUserCommand = new Command(async () => await AddUserAsync());
        }
        private async Task AddUserAsync()
        {
            string ConnectionString = "Server=bty5zrienhqxuu2hfxso-mysql.services.clever-cloud.com;Port=3306;Database=bty5zrienhqxuu2hfxso;User=uhmemuoixa55byvg;Password=NcJmIS1hGhAhM0hiQAMG;";
            string hashesdPassword = BCrypt.Net.BCrypt.HashPassword(Contrasena);

            using (var connection= new MySqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (var command= new MySqlCommand("sp_CrearUsuario", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("p_nombre_usuario", NombreUsuario);
                        command.Parameters.AddWithValue("p_email", Email);
                        command.Parameters.AddWithValue("p_contrasena_hash", hashesdPassword);
                        command.Parameters.AddWithValue("p_rol", Rol);

                        await command.ExecuteNonQueryAsync();

                        await Application.Current.MainPage.DisplayAlert("Éxito", "Usuario agregado con exito", "Ok");

                    }
                }
                catch(Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
                }
                
            }
        }





        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
