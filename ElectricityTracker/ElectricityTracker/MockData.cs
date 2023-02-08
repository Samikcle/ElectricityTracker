using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ElectricityTracker.Models;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;

namespace ElectricityTracker
{
    public class MockData
    {
        public int Fridge = 100;
        public int Microwave = 300;
        public int AC = 200;

        readonly List<Item> items;

        public MockData()
        {
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "F", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "S", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "T", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "F", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "F", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "S", Description="This is an item description." }
            };


        }
    }

    public interface MockData<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }


}
