using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using APPGDP0001.Models;
using APPGDP0001.Services;
using System.Collections.ObjectModel;
namespace APPGDP0001.ViewModels
{
    public partial class ProyectosViewModel : ObservableObject
    { 
        private readonly ProyectosDataBase _proyectosDataBase; // Servicio para acceder a la base de datos

        // ObservableCollection para almacenar la lista de clientes
        [ObservableProperty]
        private ObservableCollection<Proyectos> _proyecto;

        [ObservableProperty]
        private string searchText;

        [ObservableProperty]
        private int id_cliente;

        [ObservableProperty]
        private string nombre_proyecto;

        [ObservableProperty]
        private string fecha_inico;

        [ObservableProperty]
        private string fecha_fin;

        [ObservableProperty]
        private string estado;

        [ObservableProperty]
        private string descripcion;

        [ObservableProperty]
        private int id_equipo;

        public IAsyncRelayCommand LoadProyectosCommand { get; }

        public ProyectosViewModel()
        {
            _proyectosDataBase = new ProyectosDataBase();
            Proyecto = new ObservableCollection<Proyectos>(); // Inicializar la lista de clientes

            // Inicialización de comandos
            LoadProyectosCommand = new AsyncRelayCommand(LoadProyectos);


            LoadProyectosCommand.Execute(null);
        }

        private async Task LoadProyectos()
        {
            try
            {
                var clientesList = await _proyectosDataBase.GetProyectosAsync();
                Proyecto.Clear();
                foreach (var proyectos in clientesList)
                {
                    Proyecto.Add(proyectos);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar Proyectos: {ex.Message}");
            }
        }

    }
}
