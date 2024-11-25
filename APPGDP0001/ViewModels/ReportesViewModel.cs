using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using APPGDP0001.Models;
using APPGDP0001.Services;
using System.Collections.ObjectModel;

namespace APPGDP0001.ViewModels
{
    public partial class ReportesViewModel : ObservableObject
    {
        private readonly ReportesDataBase _reportesDataBase; // Servicio para acceder a la base de datos

        // ObservableCollection para almacenar la lista de clientes
        [ObservableProperty]
        private ObservableCollection<Reportes> _reporte;

        [ObservableProperty]
        private string searchText;

        [ObservableProperty]
        private int id_instalacion;

        [ObservableProperty]
        private string descripcion;

        [ObservableProperty]
        private string acciones_tomadas;

        [ObservableProperty]
        private string fecha_reporte;

        [ObservableProperty]
        private string id_usuario;

        

        public IAsyncRelayCommand LoadReportesCommand { get; }

        public ReportesViewModel()
        {
             _reportesDataBase= new ReportesDataBase();
            Reporte = new ObservableCollection<Reportes>(); // Inicializar la lista de clientes

            // Inicialización de comandos
            LoadReportesCommand = new AsyncRelayCommand(LoadReportes);


            LoadReportesCommand.Execute(null);
        }

        private async Task LoadReportes()
        {
            try
            {
                var clientesList = await _reportesDataBase.GetReportesAsync();
               Reporte.Clear();
                foreach (var proyectos in clientesList)
                {
                    Reporte.Add(proyectos);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar Reportes de Incidentes: {ex.Message}");
            }
        }
    }
}
