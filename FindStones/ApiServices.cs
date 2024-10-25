using FindStonesAPI.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

public class ApiServices
{
    private readonly HttpClient _httpClient;

    public ApiServices()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://findstonesapi.azurewebsites.net/")
        };
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    // API call for Item model
    public async Task<string> GetItemsAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("api/Items");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        return null;
    }

    // API call for ItemHistory model
    public async Task<string> GetItemHistoriesAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("api/ItemHistories");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        return null;
    }

    // API call for Location model
    public async Task<string> GetLocationsAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("api/Locations");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        return null;
    }

    // API call for Location model
    public async Task<string> GetNotificationsAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("api/Notifications");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        return null;
    }

    // API call for Location model
    public async Task<string> GetUsersAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("api/Users");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        return null;
    }

    // API call for Location model
    public async Task<string> GetUserFoundItemsAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("api/UserFoundItems");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        return null;
    }

    // POST method to create a new Item
    public async Task<HttpResponseMessage> RegisterUserAsync(User newUser)
    {
        var jsonContent = JsonConvert.SerializeObject(newUser);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // Make the POST request to the registration API
        HttpResponseMessage response = await _httpClient.PostAsync("api/Users/register", content);

        // Return the entire response so the ViewModel can check the status code
        return response;
    }


    public async Task<(bool Success, int UserId, string ErrorMessage)> LoginUserAsync(string username, string password)
    {
        var loginData = new { Username = username, Password = password };
        var jsonContent = JsonConvert.SerializeObject(loginData);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync("api/Users/login", content);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonConvert.DeserializeObject<FindStones.LoginResponse>(responseContent);  // Assuming the response has userId, token, etc.
            return (true, loginResponse.UserId, null);  // Return the userId on success
        }
        else if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return (false, 0, "User not found.");
        }
        else if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return (false, 0, "Wrong password.");
        }
        else
        {
            return (false, 0, "Login failed.");
        }
    }


    // POST method to save a new Item
    public async Task<HttpResponseMessage> SaveItemAsync(Item newItem)
    {
        var jsonContent = JsonConvert.SerializeObject(newItem);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // Make the POST request to the api/items endpoint
        HttpResponseMessage response = await _httpClient.PostAsync("api/Items", content);

        // Return the response so the ViewModel can check if the save was successful
        return response;
    }

    public async Task<int?> SaveLocationAsync(FindStonesAPI.Models.Location newLocation)
    {
        var jsonContent = JsonConvert.SerializeObject(newLocation);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync("api/Locations", content);

        if (response.IsSuccessStatusCode)
        {
            var locationResponse = await response.Content.ReadAsStringAsync();
            var savedLocation = JsonConvert.DeserializeObject<FindStonesAPI.Models.Location>(locationResponse);
            return savedLocation?.LocationId;  // Return the generated location ID
        }

        return null;  // Return null if the location was not saved
    }




}
