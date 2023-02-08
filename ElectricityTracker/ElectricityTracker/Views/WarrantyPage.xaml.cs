using ElectricityTracker.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ElectricityTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WarrantyPage : ContentPage
    {
        WarrantyViewModel _viewModel;

        public WarrantyPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new WarrantyViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
