using System;
using System.Collections.Generic;
using System.Text;

namespace ElectricityTracker.Models
{
    public class ACUsageFmt
    {
        public string Id { get; set; }
        public int Date { get; set; }
        public double kw { get; set; }
        public double hrUsed { get; set; }
        public double totalKwh() 
        {
            double a = kw * hrUsed;
            return a;
        }
    }
}
