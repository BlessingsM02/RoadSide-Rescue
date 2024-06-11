using Microsoft.Maui.Controls;
using Newtonsoft.Json;

namespace RoadSide_Rescue.Views;

public partial class ProfilePage : ContentPage
{

    public ProfilePage()
    {
        InitializeComponent();
        GetProfileInfo();
    }
   

    private void GetProfileInfo()
    {
        var userInfo = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("FreshFirebaseToken", ""));
        UserEmail.Text = userInfo.User.Email;
    }

    private async void LogoutButton_Clicked(object sender, EventArgs e)
    {
        Preferences.Remove("FreshFirebaseToken");
        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }

}