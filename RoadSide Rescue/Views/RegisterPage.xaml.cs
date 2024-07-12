using Newtonsoft.Json;
using RoadSide_Rescue.ViewModel;
using System.Reflection;

namespace RoadSide_Rescue.Views;

public partial class RegisterPage : ContentPage
{
	private readonly UserDetailServices _userDetailServices;
	public RegisterPage()
	{
		InitializeComponent();
        BindingContext = new RegisterViewModel(Navigation);
		_userDetailServices = new UserDetailServices();
    }

    private async void Submit_Clicked(object sender, EventArgs e)
    {
        var Username = Email.Text;
        var UName = name.Text;
        var Contacts = Phone.Text;

        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(UName) || string.IsNullOrWhiteSpace(Contacts))
        {
            return;
        }

        try
        {

            var details = new userDetails
            {
                UserEmail = Username,
                Name = UName,
                Contact = Contacts,
            };

            await _userDetailServices.RegisterVehicleAsync(details);

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to register User: {ex.Message}", "OK");
        }
    }
}