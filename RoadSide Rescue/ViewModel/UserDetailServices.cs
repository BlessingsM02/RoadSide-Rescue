using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadSide_Rescue.ViewModel
{
    internal class UserDetailServices
    {
        private readonly FirebaseClient _firebaseClient;

        public UserDetailServices()
        {
            _firebaseClient = new FirebaseClient("https://trying-219a9-default-rtdb.firebaseio.com/");
        }

        public async Task RegisterVehicleAsync(userDetails details)
        {
            var detail = await _firebaseClient.Child("details").OnceAsync<userDetails>();

            var existingDetail = detail.FirstOrDefault(v => v.Object.UserEmail == details.UserEmail);

            if (existingDetail != null)
            {
                await _firebaseClient.Child("details").Child(existingDetail.Key).PutAsync(details);
            }
            else
            {
                await _firebaseClient.Child("details").PostAsync(details);
            }
        }

        public async Task DeleteVehicleAsync(string userEmail)
        {
            var detail = await _firebaseClient.Child("details").OnceAsync<Vehicle>();
            var existingDetail = detail.FirstOrDefault(v => v.Object.UserEmail == userEmail);
            if (existingDetail != null)
            {
                await _firebaseClient.Child("details").Child(existingDetail.Key).DeleteAsync();
            }
        }


        public async Task<bool> CheckVehicleExistenceAsync(string userEmail)
        {
            try
            {
                var detail = await _firebaseClient.Child("details").OnceAsync<userDetails>();

                // Check if any vehicle exists with the given user email
                var existingDetail = detail.FirstOrDefault(v => v.Object.UserEmail == userEmail);

                // If a vehicle exists for the user, return true; otherwise, return false
                return existingDetail != null;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

    }
}
public class userDetails
{
    public string UserEmail { get; set; }
    public string Name { get; set; }
    public string Contact { get; set; }
}
