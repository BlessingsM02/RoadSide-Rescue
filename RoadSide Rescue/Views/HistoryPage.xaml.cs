using Roadside_Rescue.ViewModel;
namespace RoadSide_Rescue.Views
{
    public partial class HistoryPage : ContentPage
    {
        private readonly FirebaseService _firebaseService;
        public HistoryPage()
        {
            InitializeComponent();
            _firebaseService = new FirebaseService();
            LoadUserLocations();
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

    }

}

