using Firebase.Database;
using Firebase.Database.Query;




namespace Roadside_Rescue.ViewModel
{
    public class FirebaseService
    {
        private readonly FirebaseClient _firebaseClient;

        public FirebaseService()
        {
            // Initialize FirebaseClient with your Firebase database URL
            _firebaseClient = new FirebaseClient("https://trying-74dd0-default-rtdb.firebaseio.com/");
        }

        public async Task SendCoordinatesAsync(string userEmail, double latitude, double longitude)
        {
            var coordinate = new UserLocation
            {
                UserEmail = userEmail,
                Latitude = latitude,
                Longitude = longitude,
                Timestamp = DateTime.UtcNow
            };

            var locations = await _firebaseClient.Child("locations")
                .OnceAsync<UserLocation>();

            // Check if user already exists
            var existingLocation = locations.FirstOrDefault(l => l.Object.UserEmail == userEmail);
            if (existingLocation != null)
            {
                await _firebaseClient.Child("locations").Child(existingLocation.Key).PutAsync(coordinate);
            }
            else
            {
                await _firebaseClient.Child("locations").PostAsync(coordinate);
            }
        }

        public async Task DeleteLocationAsync(string userEmail)
        {
            var locations = await _firebaseClient.Child("locations").OnceAsync<UserLocation>();
            var existingLocation = locations.FirstOrDefault(l => l.Object.UserEmail == userEmail);
            if (existingLocation != null)
            {
                await _firebaseClient.Child("locations").Child(existingLocation.Key).DeleteAsync();
            }
        }

        public async Task<List<UserLocation>> GetAllUserLocationsAsync()
        {
            var locations = await _firebaseClient.Child("locations").OnceAsync<UserLocation>();
            return locations.Select(l => l.Object).ToList();
        }
    }
}

    public class UserLocation
    {
        public string UserEmail { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }
    }

