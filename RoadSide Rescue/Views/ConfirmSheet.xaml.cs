using The49.Maui.BottomSheet;
using Roadside_Rescue.ViewModel;
namespace RoadSide_Rescue.Views;
public partial class ConfirmSheet : BottomSheet
{
    private readonly FirebaseService _firebaseService;
    public ConfirmSheet()
	{
		InitializeComponent();
        _firebaseService = new FirebaseService();
        LoadUserLocations();
        SetHalfHeight();
    }
    private async void LoadUserLocations()
    {
        try
        {
            var locations = await _firebaseService.GetAllUserLocationsAsync();
            LocationsListView.ItemsSource = locations;
        }
        catch (Exception ex)
        {
        }
    }
    private void SetHalfHeight()
    {
        // Get the screen height and set the BottomSheet height to half
        var screenHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
        HeightRequest = screenHeight / 2;
    }
}
public class UserLocation
{
    public string UserEmail { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string LocationDetails => $"Lat: {Latitude}, Lon: {Longitude}";
}

