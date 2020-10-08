using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Chips : ProductInventory
    {
        public Chips(string slot, string name, decimal price)
        {
            Slot = slot;
            Name = name;
            Price = price;
            ProductType = "Chips";
            OutPutMessage = "Crunch Crunch, Yum!";
        }
    }
}
