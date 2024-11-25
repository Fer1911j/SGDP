using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using APPGDP0001.Models;
using APPGDP0001.Services;
using System.Collections.ObjectModel;
using MySqlX.XDevAPI;
namespace APPGDP0001.ViewModels
{
    public partial class MantenimientoViewModel : ObservableObject
    {
        private readonly MantenimientoDatabase _mantenimientoDatabase; // Servicio para acceder a la base de datos

        // ObservableCollection para almacenar la lista de clientes
        [ObservableProperty]
        private ObservableCollection<Mantenimiento> _mantenimientos;

        [ObservableProperty]
        private string searchText;

        [ObservableProperty]
        private Mantenimiento clienteEditado;

        [ObservableProperty]
        private int id_proyecto;

        [ObservableProperty]
        private int id_tecnico;

        [ObservableProperty]
        private string fecha_programada;

        [ObservableProperty]
        private string descripcion;
        [ObservableProperty]
        private string estado;

        public IAsyncRelayCommand LoadMantenimientoCommand { get; }

        public  MantenimientoViewModel()
        {
            _mantenimientoDatabase = new MantenimientoDatabase();
            Mantenimientos = new ObservableCollection<Mantenimiento>(); // Inicializar la lista de clientes

            // Inicialización de comandos
            LoadMantenimientoCommand = new AsyncRelayCommand(LoadMantenimiento);


            LoadMantenimientoCommand.Execute(null);
        }

        private async Task LoadMantenimiento()
        {
            try
            {
                var clientesList = await _mantenimientoDatabase.GetMantenimientoAsync();
                Mantenimientos.Clear();
                foreach (var cliente in clientesList)
                {
                    Mantenimientos.Add(cliente);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar Mantenimientos: {ex.Message}");
            }
        }
        partial void OnSearchTextChanged(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                LoadMantenimiento(); // Si el texto está vacío, recarga toda la lista
            }
            else
            {
                // Filtrar la lista de clientes
                var filteredList = _mantenimientos.Where(c => c.descriopcion.ToLower().Contains(value.ToLower())).ToList();

                // Limpiar la lista y agregar los resultados filtrados
                _mantenimientos.Clear();
                foreach (var cliente in filteredList)
                {
                    _mantenimientos.Add(cliente);
                }
            }
        }
    }
}
