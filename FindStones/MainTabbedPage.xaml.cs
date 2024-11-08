using System.Windows.Input;
using Microsoft.Maui.ApplicationModel;

namespace FindStones
{
    public partial class MainTabbedPage : TabbedPage
    {
        private readonly ApiServices _apiService;

        // Refreshing property
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        // Command to refresh data
        public ICommand RefreshCommand => new Command(async () =>
        {
            IsRefreshing = true;
            await RefreshHiddenStones();
            IsRefreshing = false;
        });

        // Command to open maps
        public ICommand OpenMapsCommand => new Command<string>(async (location) =>
        {
            if (!string.IsNullOrWhiteSpace(location))
            {
                var url = $"https://www.google.com/maps/search/?api=1&query={location}";
                await Launcher.OpenAsync(url);
            }
            else
            {
                Console.WriteLine("Location is missing or invalid.");
            }
        });

        // Command to show full screen image
        public ICommand ShowFullScreenImageCommand => new Command<string>(async (imageUrl) =>
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                await Navigation.PushModalAsync(new ImagePopupPage(imageUrl));
            }
        });


        public MainTabbedPage()
        {
            InitializeComponent();
            _apiService = new ApiServices();
            LoadHiddenStones();
            BindingContext = this;


        }

        //Method to load hidden stones
        private async void LoadHiddenStones()
        {
            var userId = Preferences.Get("UserId", 0);
            var hiddenStones = await _apiService.GetHiddenStonesAsync(userId);
            HiddenStonesCollectionView.ItemsSource = hiddenStones;
        }

        // Method to refresh data
        private async Task RefreshHiddenStones()
        {
            var userId = Preferences.Get("UserId", 0);
            var hiddenStones = await _apiService.GetHiddenStonesAsync(userId);
            HiddenStonesCollectionView.ItemsSource = hiddenStones;
        }
    }
}
