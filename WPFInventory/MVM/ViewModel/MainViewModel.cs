using System;
using System.ComponentModel;
using WPFInventory.Core;
using WPFInventory.MVM.ViewModel;

namespace WPFInventory.MVM.ViewModel
{
    class MainViewModel : ObeservableObject
    {

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand DiscoveryViewCommand { get; set; }
        public RelayCommand ProductsViewCommand { get; set; }
        public RelayCommand DeliveriesViewCommand { get; set; }
        public RelayCommand UsersViewCommand { get; set; }

        public HomeViewModel HomeVM{ get; set; }

        public DiscoveryViewModel DiscoveryVM { get; set; }

        public ProductsViewModel ProductsVM { get; set; }

        public DeliveriesViewModel DeliveryVM { get; set; }
        public UsersViewModel UsersVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { 
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            DiscoveryVM = new DiscoveryViewModel();
            ProductsVM = new ProductsViewModel();
            DeliveryVM = new DeliveriesViewModel();
            UsersVM = new UsersViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o => 
            {
                CurrentView = HomeVM;
            });

            DiscoveryViewCommand = new RelayCommand(o =>
            {
                CurrentView = DiscoveryVM;
            });

            ProductsViewCommand = new RelayCommand(o =>
            {
                CurrentView = ProductsVM;
            });

            DeliveriesViewCommand = new RelayCommand(o =>
            {
                CurrentView = DeliveryVM;
            });

            UsersViewCommand = new RelayCommand(o =>
            {
                CurrentView = UsersVM;
            });
        }
    }
}
