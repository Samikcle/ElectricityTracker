using ElectricityTracker.ViewModels;
using ElectricityTracker.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ElectricityTracker
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(WarrantyDetailPage), typeof(WarrantyDetailPage));
            
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
