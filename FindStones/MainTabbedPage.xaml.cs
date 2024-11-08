using System.Windows.Input;
using Microsoft.Maui.ApplicationModel;

namespace FindStones
{
    public partial class MainTabbedPage : TabbedPage
    {
        private readonly ApiServices _apiService;

        public ICommand OpenMapsCommand => new Command<string>(async (url) =>
        {
            Console.WriteLine($"Opening URL: {url}");
            await Launcher.OpenAsync(url);
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
