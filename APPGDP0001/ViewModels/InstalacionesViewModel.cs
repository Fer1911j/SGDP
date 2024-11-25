using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using APPGDP0001.Models;
using APPGDP0001.Services;
using System.Collections.ObjectModel;

namespace APPGDP0001.ViewModels
{
    public partial class InstalacionesViewModel : ObservableObject
    {
        private readonly InstalacionesDataBase _instalacionesDataBase; // Servicio para acceder a la base de datos

        // ObservableCollection para almacenar la lista de clientes
        [ObservableProperty]
        private ObservableCollection<Instalaciones> _instalacion;

        [ObservableProperty]
        private string searchText;

        [ObservableProperty]
        private int id_proyecto;

        [ObservableProperty]
        private string ubicacion;

        [ObservableProperty]
        private string fecha_inicio;

        [ObservableProperty]
        private string fecha_fin;

        [ObservableProperty]
        private string estado_instalacion;

        [ObservableProperty]
        private string coordenadas;

        [ObservableProperty]
        private int id_tecnico;


        public IAsyncRelayCommand LoadInstalacionesCommand { get; }

        public InstalacionesViewModel()
        {
            _instalacionesDataBase = new InstalacionesDataBase();
            Instalacion = new ObservableCollection<Instalaciones>(); // Inicializar la lista de clientes

            // Inicialización de comandos
            LoadInstalacionesCommand = new AsyncRelayCommand(LoadInstalaciones);


            LoadInstalacionesCommand.Execute(null);
        }

        private async Task LoadInstalaciones()
        {
            try
            {
                var clientesList = await _instalacionesDataBase.GetInstalacionesAsync();
                Instalacion.Clear();
                foreach (var instalaciones in clientesList)
                {
                    Instalacion.Add(instalaciones);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar Instalaciones: {ex.Message}");
            }
        }
        
    }
}
