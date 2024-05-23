using Firebase.Auth;
using RoadSide_Rescue.ViewModel;

namespace RoadSide_Rescue.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
        BindingContext = new LoginViewModel(Navigation);
    }
}