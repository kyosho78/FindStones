using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FindStones
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly ApiServices _apiService;

        private string _itemsData;
        public string ItemsData
        {
            get { return _itemsData; }
            set
            {
                _itemsData = value;
                OnPropertyChanged();
            }
        }

        private string _locationsData;
        public string LocationsData
        {
            get { return _locationsData; }
            set
            {
                _locationsData = value;
                OnPropertyChanged();
            }
        }

        private string _itemHistoriesData;
        public string ItemHistoriesData
        {
            get { return _itemHistoriesData; }
            set
            {
                _itemHistoriesData = value;
                OnPropertyChanged();
            }
        }

        private string _notificationsData;
        public string NotificationsData
        {
            get { return _notificationsData; }
            set
            {
                _notificationsData = value;
                OnPropertyChanged();
            }
        }

        private string _userFoundItemsData;
        public string UserFoundItemsData
        {
            get { return _userFoundItemsData; }
            set
            {
                _userFoundItemsData = value;
                OnPropertyChanged();
            }
        }
        private string _usersData;
        public string UsersData
        {
            get { return _usersData; }
            set
            {
                _usersData = value;
                OnPropertyChanged();
            }
        }

        

        public MainPageViewModel()
        {
            _apiService = new ApiServices();
            LoadDataAsync();  // Call the method to load all data
        }

        public async Task LoadDataAsync()
        {
            ItemsData = await _apiService.GetItemsAsync();
            LocationsData = await _apiService.GetLocationsAsync();
            ItemHistoriesData = await _apiService.GetItemHistoriesAsync();
            NotificationsData = await _apiService.GetNotificationsAsync();
            UsersData = await _apiService.GetUsersAsync();
            UserFoundItemsData = await _apiService.GetUserFoundItemsAsync();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
