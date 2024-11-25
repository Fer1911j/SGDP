using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using APPGDP0001.Models;
using APPGDP0001.Services;
using System.Collections.ObjectModel;

namespace APPGDP0001.ViewModels
{
    /// <summary>
    /// ViewModel para gestionar clientes en la aplicación.
    /// Contiene la lógica para cargar, agregar, editar y eliminar clientes.
    /// </summary>
    public partial class ClientesViewModel : ObservableObject
    {
        private readonly DataBaseService _dataBaseService; // Servicio para acceder a la base de datos

        // ObservableCollection para almacenar la lista de clientes
        [ObservableProperty]
        private ObservableCollection<Cliente> _clientes;

        // Texto de búsqueda para filtrar clientes
        [ObservableProperty]
        private string searchText;

        // Cliente actualmente en edición
        [ObservableProperty]
        private Cliente clienteEditado;

        // Propiedades para capturar la información del cliente
        [ObservableProperty]
        private string nombre;

        [ObservableProperty]
        private string direccion;

        [ObservableProperty]
        private string telefono;

        [ObservableProperty]
        private string email;

        // Comandos para manejar acciones de los clientes
        public AsyncRelayCommand<Cliente> DeleteClientCommand { get; }

        public AsyncRelayCommand AgregarClienteCommand { get; }

        public IAsyncRelayCommand LoadClientesCommand { get; }

        public IAsyncRelayCommand<Cliente> EditarClienteCommand { get; }

        /// <summary>
        /// Constructor del ViewModel que inicializa el servicio de base de datos y los comandos.
        /// </summary>
        public ClientesViewModel()
        {
            _dataBaseService = new DataBaseService();
            Clientes = new ObservableCollection<Cliente>(); // Inicializar la lista de clientes

            // Inicialización de comandos
            LoadClientesCommand = new AsyncRelayCommand(LoadClientes);
            AgregarClienteCommand = new AsyncRelayCommand(AgregarClienteAsync);
            DeleteClientCommand = new AsyncRelayCommand<Cliente>(DeleteClient);
            EditarClienteCommand = new AsyncRelayCommand<Cliente>(EditarClienteAsync);

            // Cargar la lista de clientes al iniciar
            LoadClientesCommand.Execute(null);
        }

        /// <summary>
        /// Método que se llama automáticamente cuando SearchText cambia.
        /// Filtra la lista de clientes en función del texto ingresado en el buscador.
        /// </summary>
        partial void OnSearchTextChanged(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                LoadClientes(); // Si el texto está vacío, recarga toda la lista
            }
            else
            {
                // Filtrar la lista de clientes
                var filteredList = _clientes.Where(c => c.Nombre.ToLower().Contains(value.ToLower())).ToList();

                // Limpiar la lista y agregar los resultados filtrados
                _clientes.Clear();
                foreach (var cliente in filteredList)
                {
                    _clientes.Add(cliente);
                }
            }
        }
        

        /// <summary>
        /// Método para editar un cliente en la base de datos.
        /// </summary>
        private async Task EditarClienteAsync(Cliente cliente)
        {
            if (cliente == null || cliente.Id <= 0) return;

            try
            {
                await _dataBaseService.EditarClienteAsync(cliente);
                await LoadClientes(); // Recargar la lista de clientes tras la edición
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al editar el cliente: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo editar el cliente. Inténtelo de nuevo.", "OK");
            }
        }

        /// <summary>
        /// Método para cargar la lista de clientes desde la base de datos.
        /// </summary>
        private async Task LoadClientes()
        {
            try
            {
                var clientesList = await _dataBaseService.GetClientesAsync();
                Clientes.Clear();
                foreach (var cliente in clientesList)
                {
                    Clientes.Add(cliente);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar clientes: {ex.Message}");
            }
        }

        /// <summary>
        /// Método para agregar un nuevo cliente a la base de datos.
        /// </summary>
        public async Task AgregarClienteAsync()
        {
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Direccion) || string.IsNullOrEmpty(Telefono) || string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "Por favor, complete todos los campos.", "OK");
                return; // No proceder si hay campos vacíos
            }

            try
            {
                // Llamar al servicio para agregar el cliente a la base de datos
                await _dataBaseService.AgregarClienteAsync(Nombre, Direccion, Telefono, Email);

                // Recargamos la lista de clientes después de agregar uno nuevo
                await LoadClientes();

                await Application.Current.MainPage.DisplayAlert("Éxito", "Cliente agregado con éxito.", "OK");

                // Limpiar los campos después de agregar el cliente
                Nombre = string.Empty;
                Direccion = string.Empty;
                Telefono = string.Empty;
                Email = string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar cliente: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", "Hubo un error al agregar el cliente.", "OK");
            }
        }

        /// <summary>
        /// Método para eliminar un cliente de la base de datos.
        /// </summary>
        public async Task DeleteClient(Cliente cliente)
        {
            if (cliente == null)
            {
                Console.WriteLine("El cliente es nulo.");
                return; // Salir si el cliente es nulo
            }

            try
            {
                // Llamar al servicio para eliminar el cliente de la base de datos
                await _dataBaseService.EliminarClienteAsync(cliente.Id);
                await LoadClientes(); // Recargar la lista después de eliminar un cliente
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar cliente: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo eliminar el cliente.", "OK");
            }
        }
    }
}



