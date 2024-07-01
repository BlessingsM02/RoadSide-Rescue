using Microsoft.Maui.Controls.Maps;
using RoadSide_Rescue.Views;
using Microsoft.Maui.Maps;
using Newtonsoft.Json;
using RoadSide_Rescue.ViewModel;
using Microsoft.Maui.Controls;

namespace RoadSide_Rescue.Views
{
    public partial class HomePage : ContentPage
    {
        private readonly VehicleRegistrationService _vehicleRegistrationService;
        public HomePage()
        {
            _vehicleRegistrationService = new VehicleRegistrationService();
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var geolocationRequest = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromMicroseconds(20));
            var location = await Geolocation.GetLocationAsync(geolocationRequest);

            map.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromKilometers(10)));
        }
        
        private async void requestButton_Clicked(object sender, EventArgs e)
        {
            


            try
            {
                var geolocationRequest = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(20));
                var location = await Geolocation.GetLocationAsync(geolocationRequest);

                // Clear existing pins on the map
                map.Pins.Clear();

                // Add a new pin for the current location
                var pin = new Pin
                {
                    Address = $"{location}",
                    Location = location,
                    Type = PinType.Place,
                    Label = "Current",
                };
                map.Pins.Add(pin);

                // Retrieve user email from Firebase token
                var userInfo = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("FreshFirebaseToken", ""));
                string userEmail = userInfo?.User?.Email ?? "Unknown";

                // Check if the user is registered with a vehicle
                var vehicleExists = await _vehicleRegistrationService.CheckVehicleExistenceAsync(userEmail);
                if (!vehicleExists)
                {
                    await App.Current.MainPage.DisplayAlert("Information", "You are not registered with any vehicle. Please register your vehicle.", "OK");
                    await App.Current.MainPage.Navigation.PushAsync(new RegisterVehiclePage());
                }
                else
                {
                    await App.Current.MainPage.Navigation.PushAsync(new RequestPage());

                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
            
        }

        

    }
}
