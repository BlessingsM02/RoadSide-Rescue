

using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Maps;

namespace RoadSide_Rescue.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
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
            var geolocationRequest = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromMicroseconds(20));
            var location = await Geolocation.GetLocationAsync(geolocationRequest);

            // Clear existing pins
            map.Pins.Clear();

            var pin = new Pin()
            {
                Address = $"{location}",
                Location = location,
                Type = PinType.Place,
                Label = "Current",
            };
            map.Pins.Add(pin);
        }

    }
}
