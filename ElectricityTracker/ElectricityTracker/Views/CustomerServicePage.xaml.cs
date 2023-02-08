using ElectricityTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ElectricityTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerServicePage : ContentPage
    {
        CustomerServiceViewModel _viewModel;
        public CustomerServicePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CustomerServiceViewModel();
        }
    }
}