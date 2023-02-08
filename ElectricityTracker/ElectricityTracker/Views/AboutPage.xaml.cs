using Microcharts.Forms;
using Microcharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using System.Globalization;
using ElectricityTracker.Models;
using ElectricityTracker.Services;
using ElectricityTracker.ViewModels;
using System.Collections.ObjectModel;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace ElectricityTracker.Views
{
    public partial class AboutPage : ContentPage
    {
        ItemsViewModel _viewModel;
        public IDataStore<TVUsageFmt> TVData => DependencyService.Get<IDataStore<TVUsageFmt>>();
        public ObservableCollection<TVUsageFmt> TVItems;
        public IDataStore<ACUsageFmt> ACData => DependencyService.Get<IDataStore<ACUsageFmt>>();
        public ObservableCollection<ACUsageFmt> ACItems;
        public IDataStore<WHeaterUsageFmt> WHData => DependencyService.Get<IDataStore<WHeaterUsageFmt>>();
        public ObservableCollection<WHeaterUsageFmt> WHItems;

        List<ChartEntry> entry = new List<ChartEntry>();

        private double _PriceTM;
        private string _PriceThisM;
        private double _EnergyTM;
        private string _EnergyThisM;

        public AboutPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();

            TVItems = new ObservableCollection<TVUsageFmt>();
            ACItems = new ObservableCollection<ACUsageFmt>();
            WHItems = new ObservableCollection<WHeaterUsageFmt>();
            PriceThisM.Text = "";
            EnergyThisM.Text = "";
            PredictedCost.Text = "";
            PredictedEnergy.Text = "";
            Savableac.Text = "";
            Savabletv.Text = "";
            Savablewh.Text = "";
            Task.Run(async () => { await ExecuteLoadItemsCommand(); });
                                    
            chartViewPie.Chart = new DonutChart { Entries = entry, LabelTextSize = 30 };
            

        }
        public async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                TVItems.Clear();
                var tvitems = await TVData.GetItemsAsync(true);
                double tvtotaluse = 0;
                double tvpercentage = 0;
                double tvsavable = 0;

                ACItems.Clear();
                var acitems = await ACData.GetItemsAsync(true);
                double actotaluse = 0;
                double acpercentage = 0;
                double acsavable = 0;

                WHItems.Clear();
                var whitems = await WHData.GetItemsAsync(true);
                double whtotaluse = 0;
                double whpercentage = 0;
                double whsavable = 0;

                foreach (var item in tvitems)
                {
                    TVItems.Add(item);
                    tvtotaluse += item.kw * item.hrUsed;
                    tvsavable += item.kw * (item.hrUsed-1);
                }
                

                foreach (var item in acitems)
                {
                    ACItems.Add(item);
                    actotaluse += item.kw * item.hrUsed;
                    acsavable += item.kw * (item.hrUsed - 1);
                }
                

                foreach (var item in whitems)
                {
                    WHItems.Add(item);
                    whtotaluse += item.kw * item.hrUsed;
                    whsavable += item.kw * (item.hrUsed - 1);
                }
                _PriceTM = (tvtotaluse + actotaluse + whtotaluse) * 0.43;
                _PriceThisM = (Math.Round(_PriceTM, 2, MidpointRounding.ToEven)).ToString();
                PriceThisM.Text = "RM " + _PriceThisM;
                _EnergyTM = (tvtotaluse + actotaluse + whtotaluse);
                _EnergyThisM = (Math.Round(_EnergyTM, 2, MidpointRounding.ToEven)).ToString();
                EnergyThisM.Text = "Energy used: " + _EnergyThisM + " kWh";
                PredictedCost.Text = "Predicted cost this month: RM" + (Math.Round((((tvtotaluse + actotaluse + whtotaluse) * 0.43) * 5), 2, MidpointRounding.ToEven)).ToString();
                PredictedEnergy.Text = "Predicted Energy for this month: " + (Math.Round(((tvtotaluse + actotaluse + whtotaluse) * 5), 2, MidpointRounding.ToEven)).ToString() + " kWh";
                Savableac.Text = "You can save RM " + (Math.Round(((actotaluse * 0.43 * 5) -(acsavable * 0.43 * 5) ), 2, MidpointRounding.ToEven)).ToString() + " a month by reducing use of Air Conditioner by an hour everyday";
                Savabletv.Text = "You can save RM " + (Math.Round(((tvtotaluse * 0.43 * 5) - (tvsavable * 0.43 * 5)), 2, MidpointRounding.ToEven)).ToString() + " a month by reducing use of Television by an hour everyday";
                Savablewh.Text = "You can save RM " + (Math.Round(((whtotaluse * 0.43 * 5) - (whsavable * 0.43 * 5)), 2, MidpointRounding.ToEven)).ToString() + " a month by reducing use of Water Heater by an hour everyday";

                tvpercentage = Math.Round(((tvtotaluse / (tvtotaluse + actotaluse + whtotaluse)) * 100), 2 ,MidpointRounding.ToEven);
                acpercentage = Math.Round(((actotaluse / (tvtotaluse + actotaluse + whtotaluse)) * 100), 2, MidpointRounding.ToEven);
                whpercentage = Math.Round(((whtotaluse / (tvtotaluse + actotaluse + whtotaluse)) * 100), 2, MidpointRounding.ToEven);

                entry.Add(new ChartEntry(Convert.ToInt32(Math.Round(tvtotaluse, MidpointRounding.AwayFromZero))) { Label = "Television", ValueLabel = tvpercentage.ToString() + "%", Color = SKColor.Parse("#32a852"), ValueLabelColor = SKColor.Parse("#32a852") });
                entry.Add(new ChartEntry(Convert.ToInt32(Math.Round(actotaluse, MidpointRounding.AwayFromZero))) { Label = "Air Conditioner", ValueLabel = acpercentage.ToString() + "%", Color = SKColor.Parse("#ff0aad"), ValueLabelColor = SKColor.Parse("#ff0aad") });
                entry.Add(new ChartEntry(Convert.ToInt32(Math.Round(whtotaluse, MidpointRounding.AwayFromZero))) { Label = "Water Heater", ValueLabel = whpercentage.ToString() + "%", Color = SKColor.Parse("#ff540a"), ValueLabelColor = SKColor.Parse("#ff540a") });
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
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}