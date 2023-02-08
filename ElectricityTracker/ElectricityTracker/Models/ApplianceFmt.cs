using System;
using System.Collections.Generic;
using System.Text;

namespace ElectricityTracker.Models
{
    public class ApplianceFmt
    {
        public string Id { get; set; }
        public string ApplianceName { get; set; }
        public string DeviceName { get; set; }
        public string PurchaseDate { get; set; }
        public string PurchaseMonth { get; set; }
        public int PurchaseYear { get; set; }
    }
}
