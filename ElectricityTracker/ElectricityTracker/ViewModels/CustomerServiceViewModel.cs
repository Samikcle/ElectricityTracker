using ElectricityTracker.Views;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ElectricityTracker.ViewModels
{
    public class CustomerServiceViewModel : BaseViewModel
    {
        private string email;
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }
        private string contact;
        public string Contact
        {
            get => contact;
            set => SetProperty(ref contact, value);
        }
        private string elaborate;
        public string Elaborate
        {
            get => elaborate;
            set => SetProperty(ref elaborate, value);
        }

        public Command Submit { get; }
        public CustomerServiceViewModel() 
        {
            Title = "Customer Service";
            Email = "";
            Contact = "";
            Elaborate = "";

            Submit = new Command(async () => await Submitcommand());
        }

        async Task Submitcommand()
        {
            
            Email = "";
            OnPropertyChanged(nameof(Email));
            Contact = "";
            OnPropertyChanged(nameof(Contact));
            Elaborate = "";
            OnPropertyChanged(nameof(Elaborate));
            //await Shell.Current.GoToAsync($"//{nameof(SubmissionReceived)}");
            //wait DisplayAlert("Alert", "You have been alerted", "OK");
        }
    }
}
