using Prism;
using Prism.Ioc;
using Reminder.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Autofac;
using Reminder.PlatformDependent;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Reminder
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }
        
        public App(IPlatformInitializer initializer) : base(initializer) {
            
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<RegistrationPage>();
            containerRegistry.RegisterForNavigation<DashboardMasterDetailPage>();
            containerRegistry.RegisterForNavigation<AddNewPage>();
            containerRegistry.RegisterForNavigation<EditExistingPage>();
            containerRegistry.RegisterForNavigation<ViewCalendarPage>();
        }

        protected override void OnStart()
        {
            base.OnStart();
            DependencyService.Get<IEventKitHandler>().CreateService();
        }
    }
}
