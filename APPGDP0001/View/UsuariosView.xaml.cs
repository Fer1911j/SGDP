namespace APPGDP0001.View;

public partial class UsuariosView : ContentView
{
	public UsuariosView()
	{
		InitializeComponent();
	}
    private async void OnAddClientClicked(object sender, EventArgs e)
    {
        // Usar DisplayAlert desde la p�gina principal
        await Application.Current.MainPage.Navigation.PushAsync(new AddUserPage());
    }

}