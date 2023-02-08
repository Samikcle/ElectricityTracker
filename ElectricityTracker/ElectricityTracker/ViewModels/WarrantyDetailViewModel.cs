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
    [QueryProperty(nameof(DeviceId), nameof(DeviceId))]
    public class WarrantyDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        public string Id { get; set; }
        private string _Purchased;
        private string _endWarranty;
        private string _warrantystatus;



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

        public string DeviceId
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

        public string Purchased
        {
            get => _Purchased;
            set => SetProperty(ref _Purchased, value);
        }
        public string endWarranty
        {
            get => _endWarranty;
            set => SetProperty(ref _endWarranty, value);
        }
        public string warrantystatus
        {
            get => _warrantystatus;
            set => SetProperty(ref _warrantystatus, value);
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await AData.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.ApplianceName;
                Description = item.DeviceName;
                Purchased = item.PurchaseDate + "/" + item.PurchaseMonth + "/" + item.PurchaseYear.ToString();
                int WarrantyED = item.PurchaseYear + 1;
                endWarranty = item.PurchaseDate + "/" + item.PurchaseMonth + "/" + WarrantyED.ToString();
                DateTime timenow = DateTime.Now;
                int purchasedmonth = Int32.Parse(item.PurchaseMonth);
                int purchaseddate = Int32.Parse(item.PurchaseDate);
                if (timenow.Year < item.PurchaseYear + 1)
                {
                    warrantystatus = "Warranty not ended";
                }
                else if (timenow.Year > item.PurchaseYear + 1)
                {
                    warrantystatus = "Warranty ended";
                }
                else if (timenow.Year == item.PurchaseYear + 1)
                {
                    if (timenow.Month < purchasedmonth)
                    {
                        warrantystatus = "Warranty not ended";
                    }
                    else if(timenow.Month > purchasedmonth)
                    {
                        warrantystatus = "Warranty ended";
                    }
                    else if (timenow.Month == purchasedmonth)
                    {
                        if (timenow.Day < purchaseddate)
                        {
                            warrantystatus = "Warranty not ended";
                        }
                        else if (timenow.Day > purchaseddate)
                        {
                            warrantystatus = "Warranty ended";
                        }
                        else if (timenow.Day > purchaseddate)
                        {
                            warrantystatus = "Warranty not ended";
                        }
                    }
                }

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
