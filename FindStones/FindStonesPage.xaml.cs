using Microsoft.Maui.ApplicationModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FindStones;

public partial class FindStonesPage : ContentPage
{
    // Assuming ApiServices is already set up to fetch stone locations
    private readonly ApiServices _apiService;

    public ICommand ShowAllStonesOnMapCommand => new Command(async () => await ShowAllStonesOnMap());


    public FindStonesPage()
    {
        InitializeComponent();
        _apiService = new ApiServices();
        BindingContext = this;
    }

    // Command to show all stones on Google Maps
    private async Task ShowAllStonesOnMap()
    {
        // Retrieve userId from Preferences
        int userId = Preferences.Get("UserId", 0); // 0 is the default if not found

        if (userId == 0)
        {
            await DisplayAlert("Error", "User ID is not available.", "OK");
            return;
        }

        var stones = await _apiService.GetHiddenStonesAsync(userId);

        if (stones == null || stones.Count == 0)
        {
            await DisplayAlert("No Stones", "There are no stones to display on the map.", "OK");
            return;
        }

        var destinationCoordinates = new List<string>();
        foreach (var stone in stones)
        {
            if (!string.IsNullOrWhiteSpace(stone.LastSeenLocation))
            {
                destinationCoordinates.Add(stone.LastSeenLocation);
            }
        }

        if (destinationCoordinates.Count == 0)
        {
            await DisplayAlert("No Coordinates", "No valid coordinates found for stones.", "OK");
            return;
        }

        string baseUrl = "https://www.google.com/maps/dir/?api=1";
        string waypoints = string.Join("|", destinationCoordinates.Take(destinationCoordinates.Count - 1));
        string completeUrl = $"{baseUrl}&destination={destinationCoordinates.Last()}&waypoints={waypoints}";

        await Launcher.OpenAsync(completeUrl);
    }


}
