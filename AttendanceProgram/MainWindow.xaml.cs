using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace AttendanceProgram
{
    public partial class MainWindow : Window
    {
        private const string AuthApiUrl = "https://localhost:7159/api/Auth/login";
        private const string AttendanceApiUrl = "https://localhost:7159/api/Attendance";
        private const string RememberMeFile = "rememberme.txt";
        private string _employeeId; // Store fetched employee ID
        private string _checkInEventTypeId;
        private string _checkOutEventTypeId;

        public MainWindow()
        {
            InitializeComponent();

            // Auto-login if Remember Me is enabled
            if (File.Exists(RememberMeFile))
            {
                var credentials = File.ReadAllText(RememberMeFile).Split(',');
                if (credentials.Length == 2)
                {
                    AutoLogin(credentials[0], credentials[1]);
                }
            }
        }

        private async void AutoLogin(string username, string password)
        {
            bool isAuthenticated = await AuthenticateUser(username, password, showMessage: false);
            if (isAuthenticated)
            {
                ShowAttendanceView();
            }
        }
        private async Task InitializeAttendance()
        {
            _employeeId = await FetchEmployeeId();
            await FetchEventTypeIds();
            bool isCheckedIn = await IsCheckedIn(_employeeId);

            CheckInOutToggleButton.Content = isCheckedIn ? "Check-Out" : "Check-In";
        }

        private async Task<string> FetchEmployeeId()
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };
                using (HttpClient client = new HttpClient(clientHandler))
                {
                    var response = await client.GetAsync("https://localhost:7159/api/Employees");
                    if (response.IsSuccessStatusCode)
                    {
                        var employees = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
                        return employees[0].employee_id; // Fetch the first employee ID dynamically (modify as needed)
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching employee: {ex.Message}");
            }
            return string.Empty;
        }

        private async Task FetchEventTypeIds()
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                using (HttpClient client = new HttpClient(clientHandler))
                {
                    var response = await client.GetAsync("https://localhost:7159/api/EventType");
                    if (response.IsSuccessStatusCode)
                    {
                        var eventTypes = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
                        foreach (var eventType in eventTypes)
                        {
                            if (eventType.event_type_name == "Check-In")
                                _checkInEventTypeId = eventType.event_type_id.ToString();
                            else if (eventType.event_type_name == "Check-Out")
                                _checkOutEventTypeId = eventType.event_type_id.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching event types: {ex.Message}");
            }
        }

        private async void CheckInOutToggleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string eventTypeId = CheckInOutToggleButton.Content.ToString() == "Check-In" ? _checkInEventTypeId : _checkOutEventTypeId;
                var payload = new
                {
                    employee_id = _employeeId,
                    event_date = DateTime.UtcNow,
                    event_time = DateTime.UtcNow.TimeOfDay,
                    event_type_id = eventTypeId,
                    created_by = _employeeId
                };

                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                using (HttpClient client = new HttpClient(clientHandler))
                {
                    var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(AttendanceApiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        bool isCheckIn = CheckInOutToggleButton.Content.ToString() == "Check-In";
                        CheckInOutToggleButton.Content = isCheckIn ? "Check-Out" : "Check-In";
                        MessageBox.Show(isCheckIn ? "Checked In successfully!" : "Checked Out successfully!", "Success");
                    }
                    else
                    {
                        MessageBox.Show("Failed to log attendance.", "Error");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error");
            }
        }

        private void ShowLoginView()
        {
            LoginView.Visibility = Visibility.Visible;
            AttendanceView.Visibility = Visibility.Collapsed;
        }

        private async void ShowAttendanceView()
        {
            LoginView.Visibility = Visibility.Collapsed;
            AttendanceView.Visibility = Visibility.Visible;
            DateTimeText.Text = $"Current Date/Time: {DateTime.Now}";

            // Replace "YourEmployeeIdHere" with actual logic to get the employee ID
            string employeeId = "YourEmployeeIdHere";
            bool isCheckedIn = await IsCheckedIn(employeeId);

            CheckInOutToggleButton.Content = isCheckedIn ? "Check-Out" : "Check-In";
            await InitializeAttendance(); // Ensure initialization logic runs
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool isAuthenticated = await AuthenticateUser(username, password);
            if (isAuthenticated)
            {
                if (RememberMeCheckBox.IsChecked == true)
                {
                    File.WriteAllText(RememberMeFile, $"{username},{password}");
                }
                ShowAttendanceView();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<bool> AuthenticateUser(string username, string password, bool showMessage = true)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                using (HttpClient client = new HttpClient(clientHandler))
                {
                    var payload = new { username, password };
                    var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(AuthApiUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        return true; // Login successful
                    }
                }
            }
            catch (Exception ex)
            {
                if (showMessage)
                    MessageBox.Show($"Connection Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return false;
        }

        private async Task<bool> IsCheckedIn(string employeeId)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                using (HttpClient client = new HttpClient(clientHandler))
                {
                    HttpResponseMessage response = await client.GetAsync($"{AttendanceApiUrl}?employeeId={employeeId}");
                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
                        return result != null && result.checkedIn; // Assuming API returns "checkedIn"
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking attendance status: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return false;
        }

       

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(RememberMeFile))
            {
                File.Delete(RememberMeFile);
            }
            ShowLoginView();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Cleanup actions on window close
        }
    }
}