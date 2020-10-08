using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Candy : ProductInventory
    {

        public Candy(string slot, string name, decimal price)
        {
            Slot = slot;
            Name = name;
            Price = price;
            ProductType = "Candy";
            OutPutMessage = "Munch Munch, Yum!";
        }
    }
}
