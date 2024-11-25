using APPGDP0001.Models;
using APPGDP0001.ViewModels;
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
                // Manejar el caso en que no hay cliente seleccionado
                return; // Salir del método.
            }

            // Asignar el cliente seleccionado a la propiedad ClienteEditado en el ViewModel
            if (BindingContext is ClientesViewModel viewModel)
            {
                viewModel.ClienteEditado = clienteSeleccionado; // Esto debería funcionar si es correcto
            }

            // Mostrar el overlay de edición
            EditClientOverlay.IsVisible = true;
        }
        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (!EditClientOverlay.IsVisible)
                return;

            // Verificar que el ClienteEditado no sea nulo
            if (BindingContext is ClientesViewModel viewModel && viewModel.ClienteEditado != null)
            {
                await viewModel.EditarClienteCommand.ExecuteAsync(viewModel.ClienteEditado);
            }

            // Ocultar el overlay de edición
            EditClientOverlay.IsVisible = false;
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            // Oculta el StackLayout sin guardar cambios
            EditClientOverlay.IsVisible = false;
        }

    }

}