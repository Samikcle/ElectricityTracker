using ElectricityTracker.Services;
using ElectricityTracker.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ElectricityTracker
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<TVUsageData>();
            DependencyService.Register<ACUsageData>();
            DependencyService.Register<WHeaterUsageData>();
            DependencyService.Register<ApplianceData>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
