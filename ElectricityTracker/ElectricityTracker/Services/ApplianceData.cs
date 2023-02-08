using ElectricityTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityTracker.Services
{
    public class ApplianceData : IDataStore<ApplianceFmt>
    {
        readonly List<ApplianceFmt> applianceData;

        public ApplianceData()
        {
            applianceData = new List<ApplianceFmt>()
            {
                new ApplianceFmt { Id = Guid.NewGuid().ToString(), ApplianceName = "Air Conditioner", DeviceName = "A100001", PurchaseDate = "31",PurchaseMonth ="2",PurchaseYear=2021 },
                new ApplianceFmt { Id = Guid.NewGuid().ToString(), ApplianceName = "Television", DeviceName = "A100002", PurchaseDate = "2",PurchaseMonth ="7",PurchaseYear=2021 },
                new ApplianceFmt { Id = Guid.NewGuid().ToString(), ApplianceName = "Water Heater", DeviceName = "A100003", PurchaseDate = "17",PurchaseMonth ="1",PurchaseYear=2022 }

            };
        }

        public async Task<bool> AddItemAsync(ApplianceFmt ApplianceData)
        {
            applianceData.Add(ApplianceData);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(ApplianceFmt ApplianceData)
        {
            var oldItem = applianceData.Where((ApplianceFmt arg) => arg.Id == ApplianceData.Id).FirstOrDefault();
            applianceData.Remove(oldItem);
            applianceData.Add(ApplianceData);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = applianceData.Where((ApplianceFmt arg) => arg.Id == id).FirstOrDefault();
            applianceData.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<ApplianceFmt> GetItemAsync(string id)
        {
            return await Task.FromResult(applianceData.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<ApplianceFmt>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(applianceData);
        }
    }
}
