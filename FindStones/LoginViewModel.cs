using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;


namespace FindStones
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly ApiServices _apiService;
        private readonly INavigation _navigation;
        public ICommand LoginCommand { get; }

        public LoginViewModel(INavigation navigation)
        {
            _apiService = new ApiServices();
            _navigation = navigation;
            LoginCommand = new Command(async () => await LoginUserAsync());
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        private string _loginMessage;
        public string LoginMessage
        {
            get { return _loginMessage; }
            set
            {
                _loginMessage = value;
                OnPropertyChanged();
            }
        }

        public async Task LoginUserAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                LoginMessage = "Please fill in both fields.";
                return;
            }

            try
            {
                HttpResponseMessage response = await _apiService.LoginUserAsync(Username, Password);

                if (response.IsSuccessStatusCode)
                {
                    // Save the login state
                    Preferences.Set("isLoggedIn", true);

                    // Set MainTabbedPage as the new root of the application to remove back button
                    Application.Current.MainPage = new MainTabbedPage();  // Reset the root page to MainTabbedPage
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    LoginMessage = "User not found. Please check the username.";
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    LoginMessage = "Wrong password. Please try again.";
                }
                else
                {
                    LoginMessage = "Login failed. Please try again.";
                }
            }
            catch (Exception ex)
            {
                LoginMessage = $"Error: {ex.Message}";
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
