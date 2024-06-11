using Firebase.Database;
using Firebase.Database.Query;

namespace RoadSide_Rescue.ViewModel
{
    internal class VehicleRegistrationService
    {
        private readonly FirebaseClient _firebaseClient;

        public VehicleRegistrationService()
        {
            _firebaseClient = new FirebaseClient("https://trying-74dd0-default-rtdb.firebaseio.com/");
        }

        public async Task RegisterVehicleAsync(Vehicle vehicle)
        {
            var vehicles = await _firebaseClient.Child("vehicles").OnceAsync<Vehicle>();

            var existingVehicle = vehicles.FirstOrDefault(v => v.Object.UserEmail == vehicle.UserEmail);

            if (existingVehicle != null)
            {
                await _firebaseClient.Child("vehicles").Child(existingVehicle.Key).PutAsync(vehicle);
            }
            else
            {
                await _firebaseClient.Child("vehicles").PostAsync(vehicle);
            }
        }

        public async Task DeleteVehicleAsync(string userEmail)
        {
            var vehicles = await _firebaseClient.Child("vehicles").OnceAsync<Vehicle>();
            var existingVehicle = vehicles.FirstOrDefault(v => v.Object.UserEmail == userEmail);
            if (existingVehicle != null)
            {
                await _firebaseClient.Child("vehicles").Child(existingVehicle.Key).DeleteAsync();
            }
        }

       
        public async Task<bool> CheckVehicleExistenceAsync(string userEmail)
        {
            try
            {
                var vehicles = await _firebaseClient.Child("vehicles").OnceAsync<Vehicle>();

                // Check if any vehicle exists with the given user email
                var existingVehicle = vehicles.FirstOrDefault(v => v.Object.UserEmail == userEmail);

                // If a vehicle exists for the user, return true; otherwise, return false
                return existingVehicle != null;
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }

    }
}
public class Vehicle
{
    public string UserEmail { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
}
