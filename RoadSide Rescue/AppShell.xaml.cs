using RoadSide_Rescue.Views;
namespace RoadSide_Rescue
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        private readonly static Type[] _routablePageTypes =
        [
            typeof(RegisterPage),
            typeof(HistoryPage),
            typeof(ProfilePage),
            typeof(RegisterVehiclePage),
            typeof(RequestPage)

        ];

        private static void RegisterRoutes()
        {
            foreach (var pageType in _routablePageTypes)
            {
                Routing.RegisterRoute(pageType.Name, pageType);
            }
        }
    }
}
