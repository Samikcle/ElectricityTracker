using ElectricityTracker.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;
using ElectricityTracker.ViewModels;
using System.Threading.Tasks;
using System.Diagnostics;
using System;
using System.Security.Cryptography;
using ElectricityTracker.Models;
using ElectricityTracker.Services;
using Microcharts;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SkiaSharp;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace ElectricityTracker.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public string applianceName { get; set; }

        public IDataStore<TVUsageFmt> TVData => DependencyService.Get<IDataStore<TVUsageFmt>>();
        public ObservableCollection<TVUsageFmt> TVItems;
        public IDataStore<ACUsageFmt> ACData => DependencyService.Get<IDataStore<ACUsageFmt>>();
        public ObservableCollection<ACUsageFmt> ACItems;
        public IDataStore<WHeaterUsageFmt> WHData => DependencyService.Get<IDataStore<WHeaterUsageFmt>>();
        public ObservableCollection<WHeaterUsageFmt> WHItems;

        List<ChartEntry> entry = new List<ChartEntry>();

        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
                                    
            A1.Text = applianceName;
            Cost.Text = "No Data";
            CostPrediction.Text = "";
            Savable.Text = "";
            kWh.Text = "No Data";
            kWhPrediction.Text = "";
            Costday.Text = "No Data";
            kWhday.Text = "No Data";
            AvgkW.Text = "No Data";
            ComparekW.Text = "";
            MessagingCenter.Subscribe<string, string>("MyApp", "NotifyMsg", (sender, arg) =>
            {
                applianceName = arg;
                A1.Text = applianceName;
                OnPropertyChanged(nameof(A1));
                var acitems = ACData.GetItemsAsync(true);
                if (arg == "Air Conditioner")
                {
                    Task.Run(async () => { await LoadAC(); });                                        
                }
                else if (arg == "Television")
                {
                    Task.Run(async () => { await LoadTV(); });
                }
                else if (arg == "Water Heater")
                {
                    Task.Run(async () => { await LoadWH(); });
                }
            });
                       
            BarChart.Chart = new BarChart { Entries = entry, LabelTextSize = 30 }; 
        }

        public async Task LoadAC()
        {
            
            var acitems = await ACData.GetItemsAsync(true);
            double actotaluse = 0;
            double ackwtotaluse = 0;
            double acnumber = 0;
            double acsavable = 0;

            foreach (var item in acitems)
            {
                
                entry.Add(new ChartEntry(Convert.ToInt32(Math.Round(item.hrUsed, MidpointRounding.AwayFromZero))) { Label = "Day " + (item.Date).ToString(), ValueLabel = (item.hrUsed).ToString(), Color = SKColor.Parse("#96d1ff") });
                actotaluse += item.kw * item.hrUsed;
                ackwtotaluse += item.kw;
                acnumber += 1;
                acsavable += item.kw * (item.hrUsed - 1);
            }
            Cost.Text = "RM " + (Math.Round((actotaluse * 0.43), 2, MidpointRounding.ToEven)).ToString();
            CostPrediction.Text = "Predicted to cost RM " + (Math.Round((actotaluse * 0.43 * 5), 2, MidpointRounding.ToEven)).ToString() + " for this Month";
            Savable.Text = "You can save RM " + (Math.Round(((actotaluse * 0.43 * 5) - (acsavable * 0.43 * 5)), 2, MidpointRounding.ToEven)).ToString() + " a month by reducing an hour of usage everyday";
            kWh.Text = (Math.Round((actotaluse), 2, MidpointRounding.ToEven)).ToString() + " kWh";
            kWhPrediction.Text = "Predicted to use " + (Math.Round((actotaluse * 5), 2, MidpointRounding.ToEven)).ToString() + " kWh for this month";
            Costday.Text = "RM " + (Math.Round(((actotaluse * 0.43)/acnumber), 2, MidpointRounding.ToEven)).ToString();
            kWhday.Text = (Math.Round((actotaluse/acnumber), 2, MidpointRounding.ToEven)).ToString() + " kWh";
            AvgkW.Text = (Math.Round((ackwtotaluse/acnumber), 2, MidpointRounding.ToEven)).ToString() + " kW";
            if ((ackwtotaluse / acnumber) > 1.8)
            {
                ComparekW.Text = "Your Air Conditioner uses " + ((ackwtotaluse / acnumber) - 1.8) + "kW more kilowatt per hour than the average";
            }
            else if ((ackwtotaluse / acnumber) < 1.8)
            {
                ComparekW.Text = "Your Air Conditioner uses " + (1.8 - (ackwtotaluse / acnumber)) + "kW less kilowatt per hour than the average";
            }
            else if ((ackwtotaluse / acnumber) == 1.8)
            {
                ComparekW.Text = "Your Air Conditioner uses similar kilowatt per hour compared to the average";
            }
            
        }

        public async Task LoadTV()
        {

            var tvitems = await TVData.GetItemsAsync(true);
            double tvtotaluse = 0;
            double tvkwtotaluse = 0;
            double tvnumber = 0;
            double tvsavable = 0;

            foreach (var item in tvitems)
            {

                entry.Add(new ChartEntry(Convert.ToInt32(Math.Round(item.hrUsed, MidpointRounding.AwayFromZero))) { Label = "Day " + (item.Date).ToString(), ValueLabel = (item.hrUsed).ToString(), Color = SKColor.Parse("#96d1ff") });
                tvtotaluse += item.kw * item.hrUsed;
                tvkwtotaluse += item.kw;
                tvnumber += 1;
                tvsavable += item.kw * (item.hrUsed - 1);
            }
            Cost.Text = "RM " + (Math.Round((tvtotaluse * 0.43), 2, MidpointRounding.ToEven)).ToString();
            CostPrediction.Text = "Predicted to cost RM " + (Math.Round((tvtotaluse * 0.43 * 5), 2, MidpointRounding.ToEven)).ToString() + " for this Month";
            Savable.Text = "You can save RM " + (Math.Round(((tvtotaluse * 0.43 * 5) - (tvsavable * 0.43 * 5)), 2, MidpointRounding.ToEven)).ToString() + " a month by reducing an hour of usage everyday";
            kWh.Text = (Math.Round((tvtotaluse), 2, MidpointRounding.ToEven)).ToString() + " kWh";
            kWhPrediction.Text = "Predicted to use " + (Math.Round((tvtotaluse * 5), 2, MidpointRounding.ToEven)).ToString() + " kWh for this month";
            Costday.Text = "RM " + (Math.Round(((tvtotaluse * 0.43) / tvnumber), 2, MidpointRounding.ToEven)).ToString();
            kWhday.Text = (Math.Round((tvtotaluse / tvnumber), 2, MidpointRounding.ToEven)).ToString() + " kWh";
            AvgkW.Text = (Math.Round((tvkwtotaluse / tvnumber), 2, MidpointRounding.ToEven)).ToString() + " kW";
            if ((tvkwtotaluse / tvnumber) > 0.1)
            {
                ComparekW.Text = "Your Television uses " + ((tvkwtotaluse / tvnumber)-0.1) + "kW more kilowatt per hour than the average";
            }
            else if ((tvkwtotaluse / tvnumber) < 0.1)
            {
                ComparekW.Text = "Your Television uses " + (0.1-(tvkwtotaluse / tvnumber)) + "kW less kilowatt per hour than the average";
            }
            else if((tvkwtotaluse / tvnumber)==0.1)
            {
                ComparekW.Text = "Your Television uses similar kilowatt per hour compared to the average";
            }
            
        }

        public async Task LoadWH()
        {

            var whitems = await WHData.GetItemsAsync(true);
            double whtotaluse = 0;
            double whkwtotaluse = 0;
            double whnumber = 0;
            double whsavable = 0;

            foreach (var item in whitems)
            {

                entry.Add(new ChartEntry(Convert.ToInt32(Math.Round(item.hrUsed, MidpointRounding.AwayFromZero))) { Label = "Day " + (item.Date).ToString(), ValueLabel = (item.hrUsed).ToString(), Color = SKColor.Parse("#96d1ff") });
                whtotaluse += item.kw * item.hrUsed;
                whkwtotaluse += item.kw;
                whnumber += 1;
                whsavable += item.kw * (item.hrUsed - 1);
            }
            Cost.Text = "RM " + (Math.Round((whtotaluse * 0.43), 2, MidpointRounding.ToEven)).ToString();
            CostPrediction.Text = "Predicted to cost RM " + (Math.Round((whtotaluse * 0.43 * 5), 2, MidpointRounding.ToEven)).ToString() + " for this Month";
            Savable.Text = "You can save RM " + (Math.Round(((whtotaluse * 0.43 * 5) - (whsavable * 0.43 * 5)), 2, MidpointRounding.ToEven)).ToString() + " a month by reducing an hour of usage everyday";
            kWh.Text = (Math.Round((whtotaluse), 2, MidpointRounding.ToEven)).ToString() + " kWh";
            kWhPrediction.Text = "Predicted to use " + (Math.Round((whtotaluse * 5), 2, MidpointRounding.ToEven)).ToString() + " kWh for this month";
            Costday.Text = "RM " + (Math.Round(((whtotaluse * 0.43) / whnumber), 2, MidpointRounding.ToEven)).ToString();
            kWhday.Text = (Math.Round((whtotaluse / whnumber), 2, MidpointRounding.ToEven)).ToString() + " kWh";
            AvgkW.Text = (Math.Round((whkwtotaluse / whnumber), 2, MidpointRounding.ToEven)).ToString() + " kW";
            if ((whkwtotaluse / whnumber) > 4)
            {
                ComparekW.Text = "Your Water Heater uses " + ((whkwtotaluse / whnumber) - 4) + "kW more kilowatt per hour than the average";
            }
            else if ((whkwtotaluse / whnumber) < 4)
            {
                ComparekW.Text = "Your Water Heater uses " + (4 - (whkwtotaluse / whnumber)) + "kW less kilowatt per hour than the average";
            }
            else if ((whkwtotaluse / whnumber) == 4)
            {
                ComparekW.Text = "Your Water Heater uses similar kilowatt per hour compared to the average";
            }
            
        }
    }
}