using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using APPGDP0001.Models;
using APPGDP0001.Services;
using System.Collections.ObjectModel;
using MySqlX.XDevAPI;

namespace APPGDP0001.ViewModels
{
    public partial class EquiposViewModel : ObservableObject
    {
        private readonly EquiposDatabaseService _equipoDatabase; // Servicio para acceder a la base de datos

        // ObservableCollection para almacenar la lista de clientes
        [ObservableProperty]
        private ObservableCollection<Equipos> _equipo;

        // Texto de búsqueda para filtrar clientes
        [ObservableProperty]
        private string searchText;

        [ObservableProperty]
        private string nombre_equipo;

        [ObservableProperty]
        private string descripcion;

        public IAsyncRelayCommand LoadEquiposCommand { get; }

        public EquiposViewModel()
        {
            _equipoDatabase = new EquiposDatabaseService();
            Equipo = new ObservableCollection<Equipos>();

            LoadEquiposCommand = new AsyncRelayCommand(LoadEquipos);
            LoadEquiposCommand.Execute(null);
        }


        private async Task LoadEquipos()
        {
            try
            {
                var clientesList = await _equipoDatabase.GetEquiposAsync();
                Equipo.Clear();
                foreach (var equipo in clientesList)
                {
                    Equipo.Add(equipo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar Equipos: {ex.Message}");
            }
        }
        partial void OnSearchTextChanged(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                LoadEquipos(); // Si el texto está vacío, recarga toda la lista
            }
            else
            {
                // Filtrar la lista de clientes
                var filteredList = _equipo.Where(c => c.NombreEquipo.ToLower().Contains(value.ToLower())).ToList();

                // Limpiar la lista y agregar los resultados filtrados
                _equipo.Clear();
                foreach (var cliente in filteredList)
                {
                    _equipo.Add(cliente);
                }
            }
        }
    }
}
