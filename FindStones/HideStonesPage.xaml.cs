using FindStonesAPI.Models;
using Microsoft.Maui.Media;
using System;

namespace FindStones
{
    public partial class HideStonesPage : ContentPage
    {
        private readonly ApiServices _apiService;
        private FileResult photoResult;
        private Microsoft.Maui.Devices.Sensors.Location currentLocation;

        public HideStonesPage()
        {
            InitializeComponent();
            _apiService = new ApiServices();  // Initialize ApiServices
        }

        private async void OnTakePictureClicked(object sender, EventArgs e)
        {
            try
            {
                photoResult = await MediaPicker.CapturePhotoAsync();
                if (photoResult != null)
                {
                    var stream = await photoResult.OpenReadAsync();
                    CapturedImage.Source = ImageSource.FromStream(() => stream);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to capture photo: {ex.Message}", "OK");
            }
        }

        private async void OnGetLocationClicked(object sender, EventArgs e)
        {
            try
            {
                currentLocation = await Geolocation.Default.GetLocationAsync();
                if (currentLocation != null)
                {
                    LocationLabel.Text = $"Location: {currentLocation.Latitude}, {currentLocation.Longitude}";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to get location: {ex.Message}", "OK");
            }
        }

        private async void OnHideStoneClicked(object sender, EventArgs e)
        {
            if (photoResult == null || currentLocation == null)
            {
                await DisplayAlert("Error", "Please take a picture and capture the location before hiding the stone.", "OK");
                return;
            }

            var currentUserId = Preferences.Get("UserId", 0);  // Retrieve user ID from preferences

            if (currentUserId == 0)
            {
                // Handle case where user ID is not available (e.g., redirect to login)
                await DisplayAlert("Error", "User not logged in.", "OK");
                return;
            }

            var newLocation = new FindStonesAPI.Models.Location
            {
                UserId = currentUserId,  // Use the stored user ID
                Latitude = (decimal)currentLocation.Latitude,
                Longitude = (decimal)currentLocation.Longitude,
                Description = "Description of the hiding place"
            };

            // Save the location and check for success
            int? locationId = await _apiService.SaveLocationAsync(newLocation);
            if (locationId == null)
            {
                await DisplayAlert("Error", "Failed to save location.", "OK");
                return;
            }

            var newItem = new Item
            {
                ItemName = "Hidden Stone",
                ImageUrl = photoResult.FullPath,
                LastSeenLocation = $"{currentLocation.Latitude}, {currentLocation.Longitude}",
                CreatedAt = DateTime.UtcNow,
                LocationId = locationId.Value,  // Use the obtained location ID
                CreatorId = currentUserId  // Use the stored user ID
            };

            // Save the item via API and handle the response
            HttpResponseMessage response = await _apiService.SaveItemAsync(newItem);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Success", "The stone has been hidden successfully.", "OK");

                // Refresh the hidden stones list in MainTabbedPage
                var mainPage = (MainTabbedPage)Application.Current.MainPage;
                mainPage.LoadHiddenStones();

                // Optionally switch to the Hidden Stones tab
                mainPage.CurrentPage = mainPage.Children.FirstOrDefault(p => p.Title == "Hidden Stones");
            }
            else
            {
                await DisplayAlert("Error", "Failed to hide the stone. Please try again.", "OK");
            }

        }
    }
}
