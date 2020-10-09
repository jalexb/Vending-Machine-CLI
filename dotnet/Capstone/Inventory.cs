using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class ProductInventory
    {
        public string Slot { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ProductType { get; set; }
        public string OutPutMessage { get; set; }
        public int AmountOfProduct { get; set; } = 5;

        public ProductInventory() { }
    }
}
