using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MySql.Data.MySqlClient;

namespace APPGDP0001.ViewModels
{
    
    public partial  class UserViewModel : ObservableObject
    {
        private bool isAdminSubmenuVisible;
        private bool isInventorySubmenuVisible;
        private bool isDocumentsSubmenuVisible;
        private bool isSettingsSubmenuVisible;
        private bool _isSettingsVisible;
        public ICommand LogoutCommand { get; }

        private string _nombreUsuario;
        private string _rol;

        public string NombreUsuario
        {
            get => _nombreUsuario;
            set
            {
                _nombreUsuario = value;
                OnPropertyChanged(nameof(NombreUsuario));
            }
        }

        public string Rol
        {
            get => _rol;
            set
            {
                _rol = value;
                OnPropertyChanged(nameof(Rol));
            }
        }
        public UserViewModel(string nombreUsuario, string rol)
        {
            NombreUsuario = nombreUsuario;
            Rol = rol;
            LogoutCommand = new Command(OnLogout);
        }

        private void OnLogout()
        {
            Application.Current.MainPage = new NavigationPage(new View.LoginPage());
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [ObservableProperty]
        private bool isSettingsVisible;

        public IRelayCommand ToggleSettingsCommand => new RelayCommand(() => IsSettingsVisible = !IsSettingsVisible);



        [ObservableProperty]
        private ContentView currentView;

        public UserViewModel()
        {
            IsSettingsVisible = false;
            

        }

        public bool IsAdminSubmenuVisible
        {
            get => isAdminSubmenuVisible;
            set => SetProperty(ref isAdminSubmenuVisible, value);
        }

        public bool IsInventorySubmenuVisible
        {
            get => isInventorySubmenuVisible;
            set => SetProperty(ref isInventorySubmenuVisible, value);
        }

        public bool IsDocumentsSubmenuVisible
        {
            get => isDocumentsSubmenuVisible;
            set => SetProperty(ref isDocumentsSubmenuVisible, value);
        }

        public bool IsSettingsSubmenuVisible
        {
            get => isSettingsSubmenuVisible;
            set => SetProperty(ref isSettingsSubmenuVisible, value);
        }

        public ICommand ToggleAdminSubmenuCommand => new RelayCommand(() =>
        {
            IsAdminSubmenuVisible = !IsAdminSubmenuVisible;
            IsInventorySubmenuVisible = false; // Ocultar otros submenús si se abre uno
            IsDocumentsSubmenuVisible = false;
            IsSettingsSubmenuVisible = false;
        });

        public ICommand ToggleInventorySubmenuCommand => new RelayCommand(() =>
        {
            IsInventorySubmenuVisible = !IsInventorySubmenuVisible;
            IsAdminSubmenuVisible = false;
            IsDocumentsSubmenuVisible = false;
            IsSettingsSubmenuVisible = false;
        });

        public ICommand ToggleDocumentsSubmenuCommand => new RelayCommand(() =>
        {
            IsDocumentsSubmenuVisible = !IsDocumentsSubmenuVisible;
            IsAdminSubmenuVisible = false;
            IsInventorySubmenuVisible = false;
            IsSettingsSubmenuVisible = false;
        });

        public ICommand ToggleSettingsSubmenuCommand => new RelayCommand(() =>
        {
            IsSettingsSubmenuVisible = !IsSettingsSubmenuVisible;
            IsAdminSubmenuVisible = false;
            IsInventorySubmenuVisible = false;
            IsDocumentsSubmenuVisible = false;
        });
    }
}