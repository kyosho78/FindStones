namespace FindStones
{
    public partial class MainTabbedPage : TabbedPage
    {
        private readonly ApiServices _apiService;
        public MainTabbedPage()
        {
            InitializeComponent();
            _apiService = new ApiServices();
            LoadHiddenStones();
        }

        public async void LoadHiddenStones()
        {
            var userId = Preferences.Get("UserId", 0);
            var hiddenStones = await _apiService.GetHiddenStonesAsync(userId);
            HiddenStonesCollectionView.ItemsSource = hiddenStones;
            // Implementation of LoadHiddenStones method
        }
    }
}
