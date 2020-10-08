using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    class Gum : ProductInventory
    {
        public Gum(string slot, string name, decimal price)
        {
            Slot = slot;
            Name = name;
            Price = price;
            ProductType = "Gum";
            OutPutMessage = "Chew Chew, Yum!";
        }
    }
}
