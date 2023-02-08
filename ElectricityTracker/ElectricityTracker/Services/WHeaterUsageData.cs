using ElectricityTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityTracker.Services
{
    public class WHeaterUsageData : IDataStore<WHeaterUsageFmt>
    {
        readonly List<WHeaterUsageFmt> wheaterUsageData;

        public WHeaterUsageData()
        {
            wheaterUsageData = new List<WHeaterUsageFmt>()
            {
                new WHeaterUsageFmt { Id = Guid.NewGuid().ToString(), Date = 1, kw = 4.5, hrUsed = 2 },
                new WHeaterUsageFmt { Id = Guid.NewGuid().ToString(), Date = 2, kw = 4.5, hrUsed = 2 },
                new WHeaterUsageFmt { Id = Guid.NewGuid().ToString(), Date = 3, kw = 4.5, hrUsed = 2 },
                new WHeaterUsageFmt { Id = Guid.NewGuid().ToString(), Date = 4, kw = 4.5, hrUsed = 2 },
                new WHeaterUsageFmt { Id = Guid.NewGuid().ToString(), Date = 5, kw = 4.5, hrUsed = 2 },
                new WHeaterUsageFmt { Id = Guid.NewGuid().ToString(), Date = 6, kw = 4.5, hrUsed = 2 }

            };
        }

        public async Task<bool> AddItemAsync(WHeaterUsageFmt WHeaterusageData)
        {
            wheaterUsageData.Add(WHeaterusageData);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(WHeaterUsageFmt WHeaterusageData)
        {
            var oldItem = wheaterUsageData.Where((WHeaterUsageFmt arg) => arg.Id == WHeaterusageData.Id).FirstOrDefault();
            wheaterUsageData.Remove(oldItem);
            wheaterUsageData.Add(WHeaterusageData);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = wheaterUsageData.Where((WHeaterUsageFmt arg) => arg.Id == id).FirstOrDefault();
            wheaterUsageData.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<WHeaterUsageFmt> GetItemAsync(string id)
        {
            return await Task.FromResult(wheaterUsageData.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<WHeaterUsageFmt>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(wheaterUsageData);
        }
    }
}
