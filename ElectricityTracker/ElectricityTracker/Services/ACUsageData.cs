using ElectricityTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityTracker.Services
{
    public class ACUsageData : IDataStore<ACUsageFmt>
    {
        readonly List<ACUsageFmt> acUsageData;

        public ACUsageData()
        {
            acUsageData = new List<ACUsageFmt>()
            {
                new ACUsageFmt { Id = Guid.NewGuid().ToString(), Date = 1, kw = 2, hrUsed = 6 },
                new ACUsageFmt { Id = Guid.NewGuid().ToString(), Date = 2, kw = 2, hrUsed = 8 },
                new ACUsageFmt { Id = Guid.NewGuid().ToString(), Date = 3, kw = 2, hrUsed = 6 },
                new ACUsageFmt { Id = Guid.NewGuid().ToString(), Date = 4, kw = 2, hrUsed = 8 },
                new ACUsageFmt { Id = Guid.NewGuid().ToString(), Date = 5, kw = 2, hrUsed = 7 },
                new ACUsageFmt { Id = Guid.NewGuid().ToString(), Date = 6, kw = 2, hrUsed = 8 }

            };
        }

        public async Task<bool> AddItemAsync(ACUsageFmt ACusageData)
        {
            acUsageData.Add(ACusageData);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(ACUsageFmt ACusageData)
        {
            var oldItem = acUsageData.Where((ACUsageFmt arg) => arg.Id == ACusageData.Id).FirstOrDefault();
            acUsageData.Remove(oldItem);
            acUsageData.Add(ACusageData);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = acUsageData.Where((ACUsageFmt arg) => arg.Id == id).FirstOrDefault();
            acUsageData.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<ACUsageFmt> GetItemAsync(string id)
        {
            return await Task.FromResult(acUsageData.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<ACUsageFmt>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(acUsageData);
        }
    }
}
