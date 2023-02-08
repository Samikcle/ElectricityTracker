using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Microcharts;
using SkiaSharp;
using Microcharts.Forms;
using System.Collections.Generic;
using ElectricityTracker.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using ElectricityTracker.Services;
using static System.Net.Mime.MediaTypeNames;

namespace ElectricityTracker.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; }
        public ObservableCollection<TVUsageFmt> TVItems { get; }
        public ObservableCollection<ACUsageFmt> ACItems { get; }
        public ObservableCollection<WHeaterUsageFmt> WHItems { get; }
        public ObservableCollection<ApplianceFmt> AItems { get; }
        public Command LoadItemsCommand { get; }
        private double _PriceTM;
        private string _PriceThisM;
        public string PriceThisM { get => _PriceThisM; set => SetProperty(ref _PriceThisM, value); }
                
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));

            TVItems = new ObservableCollection<TVUsageFmt>();
            ACItems = new ObservableCollection<ACUsageFmt>();
            WHItems = new ObservableCollection<WHeaterUsageFmt>();
            AItems = new ObservableCollection<ApplianceFmt>();

            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }

                TVItems.Clear();
                var tvitems = await TVData.GetItemsAsync(true);
                double tvtotaluse = 0;
                
                ACItems.Clear();
                var acitems = await ACData.GetItemsAsync(true);
                double actotaluse = 0;
                
                WHItems.Clear();
                var whitems = await WHData.GetItemsAsync(true);
                double whtotaluse = 0;
                
                foreach (var item in tvitems)
                {
                    TVItems.Add(item);
                    tvtotaluse += item.kw * item.hrUsed;
                }

                foreach (var item in acitems)
                {
                    ACItems.Add(item);
                    actotaluse += item.kw * item.hrUsed;
                }

                foreach (var item in whitems)
                {
                    WHItems.Add(item);
                    whtotaluse += item.kw * item.hrUsed;
                }
                _PriceTM = (tvtotaluse + actotaluse + whtotaluse)*0.43;
                _PriceThisM = (Math.Round(_PriceTM, 2, MidpointRounding.ToEven)).ToString();
                OnPropertyChanged(nameof(_PriceTM));
                OnPropertyChanged(nameof(_PriceThisM));
                OnPropertyChanged(nameof(PriceThisM));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        public ICommand OpenWebCommand { get; }

        private string itemId;
        private string text;
        private string description;
        public string Id { get; set; }

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

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        

        private string priceThisMo;

        public string PriceThisMo { get => priceThisMo; set => SetProperty(ref priceThisMo, value); }

    }
}