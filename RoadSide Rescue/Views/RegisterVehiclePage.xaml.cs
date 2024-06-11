using Newtonsoft.Json;
using RoadSide_Rescue.ViewModel;

namespace RoadSide_Rescue.Views;

public partial class RegisterVehiclePage : ContentPage
{
    private readonly VehicleRegistrationService _vehicleRegistrationService;
    public RegisterVehiclePage()
	{
		InitializeComponent();
        _vehicleRegistrationService = new VehicleRegistrationService();
    }

    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {

        var make = MakeEntry.Text;
        var model = ModelEntry.Text;
        var color = ColorEntry.Text;

      
        if (string.IsNullOrWhiteSpace(make) || string.IsNullOrWhiteSpace(model) || string.IsNullOrWhiteSpace(color))
        {
            await DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }

        try
        {
            var userInfo = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("FreshFirebaseToken", ""));
            string userEmail = userInfo?.User?.Email ?? "Unknown";

            var vehicle = new Vehicle
            {
                UserEmail = userEmail,
                Make = make,
                Model = model,
                Color = color
            };

            await _vehicleRegistrationService.RegisterVehicleAsync(vehicle);
            await DisplayAlert("Success", "Vehicle registered successfully", "OK");
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to register vehicle: {ex.Message}", "OK");
        }
    }
}
