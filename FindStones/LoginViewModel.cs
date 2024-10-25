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
                // Call the API service to login, which returns a tuple (bool Success, int UserId, string ErrorMessage)
                var (Success, UserId, ErrorMessage) = await _apiService.LoginUserAsync(Username, Password);

                if (Success)
                {
                    // Save the login state and user ID
                    Preferences.Set("isLoggedIn", true);
                    Preferences.Set("UserId", UserId);

                    // Set MainTabbedPage as the new root of the application to remove the back button
                    Application.Current.MainPage = new MainTabbedPage();
                }
                else
                {
                    // Display error message returned from the API
                    LoginMessage = ErrorMessage;
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
