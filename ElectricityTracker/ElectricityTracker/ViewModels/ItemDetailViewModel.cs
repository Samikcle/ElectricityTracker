using ElectricityTracker.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Microcharts.Forms;
using Microcharts;
using SkiaSharp;
using System.Collections.Generic;

namespace ElectricityTracker.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
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
                var item = await AData.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.ApplianceName;
                Description = item.DeviceName;

                MessagingCenter.Send<string, string>("MyApp", "NotifyMsg", Text);


            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
