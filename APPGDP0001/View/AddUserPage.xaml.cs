
using APPGDP0001.ViewModels;
using Microsoft.Maui.Controls;

namespace APPGDP0001.View;

public partial class AddUserPage : ContentPage
{
	public  AddUserPage()
	{
		InitializeComponent();
		BindingContext = new AddUserViewModel();
	}
    

}