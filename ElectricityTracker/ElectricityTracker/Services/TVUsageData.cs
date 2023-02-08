using ElectricityTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityTracker.Services
{
    public class TVUsageData : IDataStore<TVUsageFmt>
    {
        readonly List<TVUsageFmt> tvUsageData;
        
        public TVUsageData()
        {
            tvUsageData = new List<TVUsageFmt>()
            {
                new TVUsageFmt { Id = Guid.NewGuid().ToString(), Date = 1, kw = 0.15, hrUsed = 5 },
                new TVUsageFmt { Id = Guid.NewGuid().ToString(), Date = 2, kw = 0.15, hrUsed = 7 },
                new TVUsageFmt { Id = Guid.NewGuid().ToString(), Date = 3, kw = 0.15, hrUsed = 6 },
                new TVUsageFmt { Id = Guid.NewGuid().ToString(), Date = 4, kw = 0.15, hrUsed = 8 },
                new TVUsageFmt { Id = Guid.NewGuid().ToString(), Date = 5, kw = 0.15, hrUsed = 6 },
                new TVUsageFmt { Id = Guid.NewGuid().ToString(), Date = 6, kw = 0.15, hrUsed = 4 }

            };
        }

        public async Task<bool> AddItemAsync(TVUsageFmt TVusageData)
        {
            tvUsageData.Add(TVusageData);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(TVUsageFmt TVusageData)
        {
            var oldItem = tvUsageData.Where((TVUsageFmt arg) => arg.Id == TVusageData.Id).FirstOrDefault();
            tvUsageData.Remove(oldItem);
            tvUsageData.Add(TVusageData);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = tvUsageData.Where((TVUsageFmt arg) => arg.Id == id).FirstOrDefault();
            tvUsageData.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<TVUsageFmt> GetItemAsync(string id)
        {
            return await Task.FromResult(tvUsageData.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<TVUsageFmt>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(tvUsageData);
        }
    }
}
