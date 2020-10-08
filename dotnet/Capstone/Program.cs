using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ConstrainedExecution;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine p = new VendingMachine();
            p.Run();
        }   
    }
}
