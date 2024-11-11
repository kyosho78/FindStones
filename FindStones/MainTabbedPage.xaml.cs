using System.Windows.Input;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;

namespace FindStones
{
    public partial class MainTabbedPage : TabbedPage
    {
        private readonly ApiServices _apiService;
        private readonly HttpClient _httpClient;

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


        public ICommand DeleteStoneCommand => new Command<int>(async (stoneId) =>
        {
            bool isDeleted = await DeleteStoneAsync(stoneId);
            if (isDeleted)
            {
                await RefreshHiddenStones();
                await DisplayAlert("Success", "Stone deleted successfully.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to delete stone.", "OK");
            }
        });


        public MainTabbedPage()
        {
            InitializeComponent();
            _apiService = new ApiServices();
            LoadHiddenStones();
            BindingContext = this;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://findstonesapi.azurewebsites.net/")
            };


        }

        //Method to load hidden stones
        private async void LoadHiddenStones()
        {
            var userId = Preferences.Get("UserId", 0); //Get user ID  
            var hiddenStones = await _apiService.GetHiddenStonesAsync(userId); //Get hidden stones
            HiddenStonesCollectionView.ItemsSource = hiddenStones; //Set hidden stones to collection view
        }

        // Method to refresh data
        private async Task RefreshHiddenStones()
        {
            var userId = Preferences.Get("UserId", 0);
            var hiddenStones = await _apiService.GetHiddenStonesAsync(userId);
            HiddenStonesCollectionView.ItemsSource = hiddenStones;
        }

        private async Task<bool> DeleteStoneAsync(int stoneId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Items/{stoneId}");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Stone with ID {stoneId} deleted successfully.");
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to delete stone. Status code: {response.StatusCode}, Response: {errorContent}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting stone: {ex.Message}");
                return false;
            }
        }
    }
}
