using RoadSide_Rescue.ViewModel;

namespace RoadSide_Rescue.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
        BindingContext = new RegisterViewModel(Navigation);
    }
}