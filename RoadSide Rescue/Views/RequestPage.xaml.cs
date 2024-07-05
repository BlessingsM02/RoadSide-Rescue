using Roadside_Rescue.ViewModel;
using System.Collections.ObjectModel;

namespace RoadSide_Rescue.Views;

public partial class RequestPage : ContentPage
{
    private readonly FirebaseService _firebaseService;
    public ObservableCollection<UserLocation> NearbyUserLocations { get; set; } = new ObservableCollection<UserLocation>();
    public RequestPage()
	{
        InitializeComponent();
        _firebaseService = new FirebaseService();
        LoadUserLocations();
    }
    private async void LoadUserLocations()
    {
        try
        {
            // Get the current location
            var geolocationRequest = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(10));
            var currentLocation = await Geolocation.GetLocationAsync(geolocationRequest);

            if (currentLocation == null)
            {
                await DisplayAlert("Error", "Unable to determine current location.", "OK");
                return;
            }

            // Get all user locations
            var allLocations = await _firebaseService.GetAllUserLocationsAsync();

            // Calculate distance and filter nearby locations within 10 kilometers
            var nearbyLocations = allLocations.Where(location => CalculateDistance(currentLocation, location) <= 10).ToList();

            NearbyUserLocations.Clear();
            foreach (var location in nearbyLocations)
            {
                NearbyUserLocations.Add(location);
            }

            LocationsListView.ItemsSource = NearbyUserLocations;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Unable to load locations: {ex.Message}", "OK");
        }
    }

    private double CalculateDistance(Location currentLocation, UserLocation userLocation)
    {
        var userLocationCoords = new Location(userLocation.Latitude, userLocation.Longitude);
        return currentLocation.CalculateDistance(userLocationCoords, DistanceUnits.Kilometers);
    }


}