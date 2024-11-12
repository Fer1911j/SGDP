using APPGDP0001.Models;
namespace APPGDP0001.View
{

    public partial class ClientesView : ContentView
    {
        public ClientesView()
        {
            InitializeComponent();
            
        }
        private async void OnAddClientClicked(object sender, EventArgs e)
        {
            // Usar DisplayAlert desde la página principal
            await Application.Current.MainPage.Navigation.PushAsync(new ClientesAgregarPage());
        }

        private void OnEditClientClicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var clienteSeleccionado = (Cliente)imageButton.CommandParameter;

            if (clienteSeleccionado == null)
            {
                // Manejar el caso cuando no hay cliente seleccionado
                return; // Salir del método
            }

            // Verificar que los campos están inicializados
            if (NameEntry != null && AddressEntry != null && PhoneEntry != null && EmailEntry != null)
            {
                NameEntry.Text = clienteSeleccionado.Nombre;
                AddressEntry.Text = clienteSeleccionado.Direccion;
                PhoneEntry.Text = clienteSeleccionado.Telefono;
                EmailEntry.Text = clienteSeleccionado.Email; // Rellenar el campo de email
            }

            // Mostrar el StackLayout de edición
            EditClientOverlay.IsVisible = true;
        }
        private void OnSaveClicked(object sender, EventArgs e)
        {
            // Lógica para guardar los cambios del cliente
            // Aquí tendrías que implementar la lógica necesaria para actualizar el cliente.
            // Luego oculta el StackLayout
            EditClientOverlay.IsVisible = false;
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            // Oculta el StackLayout sin guardar cambios
            EditClientOverlay.IsVisible = false;
        }



    }

}