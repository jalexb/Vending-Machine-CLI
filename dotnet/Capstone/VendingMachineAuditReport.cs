using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone
{
    public class VendingMachineAuditReport
    {
        public decimal Balance { get; private set; }
        public DateTime CurrentDate { get; } = DateTime.Now;

        public string AuditString {get; private set;}
        public decimal TotalSales { get; set; } = 0;

        public VendingMachineAuditReport()
        {}

        public VendingMachineAuditReport(decimal balance) //Finished transaction Audit
        {
            //01/01/2019 12:01:35 PM GIVE CHANGE: $6.75 $0.00
            
            decimal finalBalance = 0.00m;
            AuditString = $"{CurrentDate} GIVE CHANGE: {balance,0:C} {finalBalance,0:C}";
        }

        public VendingMachineAuditReport(decimal balance, int deposit) //Balance deposit audit
        {
            //01/01/2019 12:00:00 PM FEED MONEY: $5.00 $5.00 
            AuditString = $"{CurrentDate} FEED MONEY: {deposit,0:C} {balance,0:C}";
        }

        public VendingMachineAuditReport(ProductInventory purchasedProduct, decimal balance) //Purchased item audit
        {
            TotalSales += purchasedProduct.Price;
            AuditString = $"{CurrentDate} {purchasedProduct.Name} {purchasedProduct.Slot} {purchasedProduct.Price,0:C} {balance,0:C}"; 
        }

        public void WriteToAuditLog()
        {

            string fullPath = @"C:\Users\Student\workspace\module1-capstone-c-team-7\Example Files\Log1.txt";

            using (StreamWriter sw = new StreamWriter(fullPath, true)) //finishTransaction = new VendingMachineAuditReport(balance)  finishTransaction.writeToAuditLog()
            {
                sw.WriteLine(AuditString);
            }
        }

        public void WriteToTotalSalesReport(Dictionary<string,int>endOfDaySalesReport)
        {
            string fullPath = @"C:\Users\Student\workspace\module1-capstone-c-team-7\Example Files\SalesReport1.txt";

            using (StreamWriter sw = new StreamWriter(fullPath, false))
            {
                foreach(KeyValuePair<string,int> countProductSold in endOfDaySalesReport)
                {
                    sw.WriteLine(countProductSold.Key + " | " + countProductSold.Value);
                }
                sw.WriteLine($"\n **TOTAL SALES** {TotalSales,0:C}");

            }
        }

    }
}
