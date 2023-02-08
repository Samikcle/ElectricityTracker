using ElectricityTracker.Models;
using ElectricityTracker.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ElectricityTracker.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private ApplianceFmt _selectedItem;
        

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<ApplianceFmt> ItemTapped { get; }
        public Command<ApplianceFmt> ATapped { get; }

        public ObservableCollection<TVUsageFmt> TVItems { get; }
        public ObservableCollection<ACUsageFmt> ACItems { get; }
        public ObservableCollection<WHeaterUsageFmt> WHItems { get; }
        public ObservableCollection<ApplianceFmt> AItems { get; }

        private double _PriceTM;
        private string _PriceThisM;
        public string PriceThisM { get => _PriceThisM; set => SetProperty(ref _PriceThisM, value); }

        public ItemsViewModel()
        {
            Title = "Appliance List";
            Items = new ObservableCollection<Item>();

            TVItems = new ObservableCollection<TVUsageFmt>();
            ACItems = new ObservableCollection<ACUsageFmt>();
            WHItems = new ObservableCollection<WHeaterUsageFmt>();
            AItems = new ObservableCollection<ApplianceFmt>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<ApplianceFmt>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
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
                

                ACItems.Clear();
                var acitems = await ACData.GetItemsAsync(true);
                

                WHItems.Clear();
                var whitems = await WHData.GetItemsAsync(true);

                AItems.Clear();
                var aitems = await AData.GetItemsAsync(true);

                foreach (var item in tvitems)
                {
                    TVItems.Add(item);
                    
                }

                foreach (var item in acitems)
                {
                    ACItems.Add(item);
                    
                }

                foreach (var item in whitems)
                {
                    WHItems.Add(item);
                    
                }
                foreach (var item in aitems)
                {
                    AItems.Add(item);

                }

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

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public ApplianceFmt SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(ApplianceFmt item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}