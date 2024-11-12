using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace APPGDP0001.View
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            // Simulación de un inicio de sesión exitoso
            bool isLoginSuccessful = true;

            if (isLoginSuccessful)
            {
                // Navegar a la AdminPage
                await Application.Current.MainPage.Navigation.PushAsync(new AdminPage());
            }
            else
            {
                // Mostrar un mensaje de error
                await DisplayAlert("Error", "Credenciales incorrectas", "OK");
            }
        }

    }
}
