using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using APPGDP0001.Services;
using Microsoft.Maui.Controls;

namespace APPGDP0001.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly DataBaseService _databaseService;
        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        // Comando para el boton de login 
        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            _databaseService =new DataBaseService();
            LoginCommand = new Command(OnLogin);
        }
        private async void OnLogin()
        {
            // Validar las credenciales del usuario
            var isValidUser = await _databaseService.ValidateUser(Username, Password);

            if (isValidUser)
            {
                // Obtener el rol del usuario
                var userRole = await _databaseService.GetUserRole(Username, Password);

                if (userRole != null)
                {
                    // Verificar si el rol es "admin"
                    if (userRole.Equals("admin", StringComparison.OrdinalIgnoreCase))
                    {
                        // Crear una instancia del AdminViewModel con el nombre de usuario y el rol
                        var adminViewModel = new AdminViewModel(Username, userRole);

                        // Asignar la AdminPage con el AdminViewModel como BindingContext
                        Application.Current.MainPage = new NavigationPage(new View.AdminPage { BindingContext = adminViewModel });
                    }
                    else if (userRole.Equals("usuario", StringComparison.OrdinalIgnoreCase))
                    {
                        // Crear la UserPage para el usuario regular
                        Application.Current.MainPage = new NavigationPage(new View.UserPage());
                    }
                    else
                    {
                        // En caso de que el rol no sea ni "admin" ni "usuario"
                        await Application.Current.MainPage.DisplayAlert("Error", "Rol desconocido", "OK");
                    }
                }
                else
                {
                    // Manejar el error si no se puede obtener el rol del usuario
                    await Application.Current.MainPage.DisplayAlert("Error", "No se pudo obtener el rol del usuario", "OK");
                }
            }
            else
            {
                // Mostrar un mensaje de error si la validación de usuario falla
                await Application.Current.MainPage.DisplayAlert("Error", "Usuario o contraseña incorrecta", "OK");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
