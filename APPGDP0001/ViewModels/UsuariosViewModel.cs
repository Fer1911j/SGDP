using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using APPGDP0001.Models;
using APPGDP0001.Services;
using System.Collections.ObjectModel;
using MySqlX.XDevAPI;

namespace APPGDP0001.ViewModels
{
    public partial class UsuariosViewModel : ObservableObject
    {
        private readonly UsuariosDatabaseService _usuariosDataBaseService;

        [ObservableProperty]
        private ObservableCollection<Usuarios> _usuario;

        [ObservableProperty]
        private string searchText;

        [ObservableProperty]
        private string id;

        [ObservableProperty]
        private string nombre;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string contrasena;

        [ObservableProperty]
        private string rol;

        [ObservableProperty]
        private string estado;

        public IAsyncRelayCommand LoadUsuariosCommand { get; }
        public AsyncRelayCommand<Usuarios> DeleteUsuarioCommand { get; }


        public UsuariosViewModel()
        {
            _usuariosDataBaseService = new UsuariosDatabaseService();
            Usuario = new ObservableCollection<Usuarios>();

            LoadUsuariosCommand = new AsyncRelayCommand(LoadUsuarios);
            DeleteUsuarioCommand = new AsyncRelayCommand<Usuarios>(DeleteUsuario);


            LoadUsuariosCommand.Execute(null);
        }

        private async Task LoadUsuarios()
        {
            try
            {
                var usuariosList = await _usuariosDataBaseService.GetUsuariosAsync();
                Usuario.Clear();
                foreach (var usuario in usuariosList)
                {
                    Usuario.Add(usuario);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar Usuarios: {ex.Message}");
            }
        }
        public async Task DeleteUsuario(Usuarios usuario)
        {
            if (usuario == null)
            {
                Console.WriteLine("El cliente es nulo.");
                return; // Salir si el cliente es nulo
            }

            try
            {
                // Llamar al servicio para eliminar el cliente de la base de datos
                await _usuariosDataBaseService.EliminarUsuarioAsync(usuario.Id);
                await LoadUsuarios(); // Recargar la lista después de eliminar un cliente
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar cliente: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo eliminar el cliente.", "OK");
            }
        }
        partial void OnSearchTextChanged(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                LoadUsuarios(); // Si el texto está vacío, recarga toda la lista
            }
            else
            {
                // Filtrar la lista de clientes
                var filteredList = _usuario.Where(c => c.Nombre.ToLower().Contains(value.ToLower())).ToList();

                // Limpiar la lista y agregar los resultados filtrados
                _usuario.Clear();
                foreach (var cliente in filteredList)
                {
                    _usuario.Add(cliente);
                }
            }
        }
    }
}
