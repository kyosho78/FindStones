using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using FindStonesAPI.Models;
using Microsoft.Maui.Controls;


namespace FindStones
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private readonly ApiServices _apiService;
        private readonly INavigation _navigation; 

        public ICommand RegisterCommand { get; }

        public RegisterViewModel(INavigation navigation)
        {
            _apiService = new ApiServices();
            _navigation = navigation;  // Store the navigation object
            RegisterCommand = new Command(async () => await RegisterUserAsync());
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

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
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

        private string _confirmationMessage;
        public string ConfirmationMessage
        {
            get { return _confirmationMessage; }
            set
            {
                _confirmationMessage = value;
                OnPropertyChanged();
            }
        }

        public async Task RegisterUserAsync()
        {
            var newUser = new User
            {
                Username = this.Username,
                Email = this.Email,
                PasswordHash = this.Password
            };

            // Make the API call to register the user
            HttpResponseMessage response = await _apiService.RegisterUserAsync(newUser);

            // Check the response status
            if (response.IsSuccessStatusCode)
            {
                // Success: Registration completed
                ConfirmationMessage = "Registration successful! Redirecting to login...";

                // Delay to show the success message before navigating
                await Task.Delay(2000);

                // Navigate to LoginPage
                await _navigation.PushAsync(new LoginPage());
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                // Conflict: Email or username already exists
                ConfirmationMessage = "Email or username already exists.";
            }
            else
            {
                // Other failures
                ConfirmationMessage = "Registration failed. Please try again.";
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
