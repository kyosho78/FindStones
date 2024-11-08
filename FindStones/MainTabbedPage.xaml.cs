using System.Windows.Input;
using Microsoft.Maui.ApplicationModel;

namespace FindStones
{
    public partial class MainTabbedPage : TabbedPage
    {
        private readonly ApiServices _apiService;

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


        private async void LoadHiddenStones()
        {
            var userId = Preferences.Get("UserId", 0); // Assumes you are storing UserId in Preferences
            var hiddenStones = await _apiService.GetHiddenStonesAsync(userId);
            HiddenStonesCollectionView.ItemsSource = hiddenStones;
        }
    }
}
