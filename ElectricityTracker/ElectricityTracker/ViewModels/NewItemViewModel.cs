using ElectricityTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ElectricityTracker.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string text;
        private string description;
        private string date;
        private string month;
        private string year;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public string Month
        {
            get => month;
            set => SetProperty(ref month, value);
        }
        public string Year
        {
            get => year;
            set => SetProperty(ref year, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            ApplianceFmt newItem = new ApplianceFmt()
            {
                Id = Guid.NewGuid().ToString(),
                ApplianceName = Text,
                DeviceName = Description,
                PurchaseDate = Date,
                PurchaseMonth = Month,
                PurchaseYear = Int32.Parse(Year)
            };

            await AData.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
