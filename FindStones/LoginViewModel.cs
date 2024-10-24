using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace FindStones
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly ApiServices _apiService;
        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            _apiService = new ApiServices();
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
                string result = await _apiService.LoginUserAsync(Username, Password);

                if (!string.IsNullOrEmpty(result))
                {
                    // Assuming result is some success message or token
                    LoginMessage = "Login successful!";
                    // Here you can navigate to another page or store the token.
                }
                else
                {
                    LoginMessage = "Login failed. Please check your credentials.";
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
