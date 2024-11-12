using APPGDP0001.Services;
using APPGDP0001.ViewModels;
using Microsoft.Maui.Controls;

namespace APPGDP0001.View
{

	public partial class ClientesAgregarPage : ContentPage
	{
       
        public ClientesAgregarPage()
		{
			InitializeComponent();

            var dataBaseService = new DataBaseService();
            var viewModel = new ClientesViewModel();

            // Asigna el ViewModel como BindingContext
            BindingContext = viewModel;

        }
	}
}