using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using APPGDP0001.Services;
using APPGDP0001.Models;
using MySqlX.XDevAPI;
using System.Data;
using System.Windows.Input;
using APPGDP0001.View;
using Microsoft.Maui.Controls;

namespace APPGDP0001.ViewModels
{
    public partial class ClientesViewModel : ObservableObject
    {


        private readonly DataBaseService _dataBaseService;



        [ObservableProperty]
        private ObservableCollection<Cliente> _clientes;

        [ObservableProperty]
        private string nombre;

        [ObservableProperty]
        private string direccion;

        [ObservableProperty]
        private string telefono;

        [ObservableProperty]
        private string email;

        public AsyncRelayCommand<Cliente> DeleteClientCommand { get; }

        public AsyncRelayCommand AgregarClienteCommand { get; }
        

        public IAsyncRelayCommand LoadClientesCommand { get; }

        public IAsyncRelayCommand<Cliente> EditarClienteCommand { get; }
        public ClientesViewModel()
        {
            _dataBaseService = new DataBaseService();
            Clientes = new ObservableCollection<Cliente>();

            LoadClientesCommand = new AsyncRelayCommand(LoadClientes);
            AgregarClienteCommand = new AsyncRelayCommand(AgregarClienteAsync);
            DeleteClientCommand = new AsyncRelayCommand<Cliente>(DeleteClient);
            EditarClienteCommand = new AsyncRelayCommand<Cliente>(EditarClienteAsync);


            LoadClientesCommand.Execute(null);


        }
        
        private async Task EditarClienteAsync(Cliente cliente)
        {
            if (cliente == null)
            {
                Console.WriteLine("El cliente es nulo.");
                return;
            }
            if (cliente != null)
            {
                // Navegar a la página de edición y pasar el cliente
                var navigationParameter = new Dictionary<string, object>
        {
            { "Cliente", cliente }
        };

                await Shell.Current.GoToAsync("clientesEditar", navigationParameter);
            }
        }




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
        public async Task AgregarClienteAsync()
        {
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Direccion) || string.IsNullOrEmpty(Telefono) || string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "Por favor, complete todos los campos.", "OK");
                return;
            }

            try
            {
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
        private async Task DeleteClient(Cliente cliente)
        {
            if (cliente == null)
            {
                Console.WriteLine("El cliente es nulo.");
                return;
            }

            try
            {
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
