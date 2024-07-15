using Newtonsoft.Json;
using RoadSide_Rescue.ViewModel;
using System.Reflection;

namespace RoadSide_Rescue.Views;

public partial class RegisterPage : ContentPage
{
	
	public RegisterPage()
	{
		InitializeComponent();
        BindingContext = new RegisterViewModel(Navigation);
		
    }

    
}