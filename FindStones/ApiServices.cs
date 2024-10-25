using FindStonesAPI.Models;
using Newtonsoft.Json;
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


    public async Task<HttpResponseMessage> LoginUserAsync(string username, string password)
    {
        var loginData = new { Username = username, Password = password };
        var jsonContent = JsonConvert.SerializeObject(loginData);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync("api/Users/login", content);
        return response;  // Return the HttpResponseMessage
    }


}
