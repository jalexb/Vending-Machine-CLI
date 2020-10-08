using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Beverage : ProductInventory
    {
        public Beverage(string slot, string name, decimal price)
        {
            Slot = slot;
            Name = name;
            Price = price;
            ProductType = "Beverage";
            OutPutMessage = "Glug, Glug, Yum!";
        }
    }
}
