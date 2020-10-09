using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone
{
    public class VendingMachine
    {
        private const string MAIN_MENU_OPTION_DISPLAY_ITEMS = "Display Vending Machine Items";
        private const string MAIN_MENU_OPTION_PURCHASE = "Purchase";
        private const string MAIN_MENU_OPTION_EXIT = "Exit";
        private const string MAIN_MENU_OPTION_SALES_REPORT = "Hidden Option";
        private readonly string[] MAIN_MENU_OPTIONS = { MAIN_MENU_OPTION_DISPLAY_ITEMS, MAIN_MENU_OPTION_PURCHASE, MAIN_MENU_OPTION_EXIT, MAIN_MENU_OPTION_SALES_REPORT }; //const has to be known at compile time, the array initializer is not const in C#


        private const string PURCHASE_MENU_OPTION_GETBALANCE = "Add Money To Purchase Items(1, 2, 5, 10, 20)";
        private const string PURCHASE_MENU_ITEM = "Select An Item To Purchase";   
        private const string PURCHASE_MENU_OPTION_FINISH_TRANSACTION = "Finish Transaction";
        private readonly string[] PURCHASE_MENU_OPTIONS = { PURCHASE_MENU_OPTION_GETBALANCE, PURCHASE_MENU_ITEM, PURCHASE_MENU_OPTION_FINISH_TRANSACTION };
        private readonly IBasicUserInterface ui = new MenuDrivenCLI();

        private string STRING_OF_SLOTS = "";
        
        public Dictionary<string, int> endOfDaySalesReport = new Dictionary<string, int>();
        VendingMachineAuditReport totalSalesReport = new VendingMachineAuditReport();



        public void Run()
        {

            Dictionary<string, ProductInventory> productList = CreateUpdateVendingMachineInventory();

            bool exit = false;
            while (!exit) //run this is an infinite loop. You'll need a 'finished' option and then you'll break after that option is selected
            {
                string selection = (string)ui.PromptForSelection(MAIN_MENU_OPTIONS);
                if (selection == MAIN_MENU_OPTION_DISPLAY_ITEMS)//presses 1 to display the items
                {
                    DisplayItems(productList);
                }
                else if (selection == MAIN_MENU_OPTION_PURCHASE)//presses 2 to purchase
                {
                    PurchaseMenu(productList);
                }
                else if (selection == MAIN_MENU_OPTION_EXIT)//presses 3 to Exit
                {
                    exit = true;
                }
                else if (selection == MAIN_MENU_OPTION_SALES_REPORT)//presses 4 to purchase
                {
                    

                    totalSalesReport.WriteToTotalSalesReport(endOfDaySalesReport);
                }

            }
        }

        private void PurchaseMenu(Dictionary<string, ProductInventory> productList)
        {
            decimal balance = 0.00M;
            bool isTransactionFinished = false;

            while (!isTransactionFinished)
            {
                Console.WriteLine($"\n{balance,0:C}\n");

                string selection = (string)ui.PromptForSelection(PURCHASE_MENU_OPTIONS);
                if (selection == PURCHASE_MENU_OPTION_GETBALANCE)
                {
                    string[] wholeDollarAmount = { "1", "2", "5", "10", "20" };
                    Console.WriteLine($"{selection}");

                    object depositCheck = null;
                    while (depositCheck == null)
                    {
                        depositCheck = ui.GetChoiceFromUserInput(wholeDollarAmount);//GetChoiceFromUserInput will make sure amount is valid
                    }

                    int deposit = int.Parse(depositCheck.ToString());
                   
                    balance += deposit;

                    VendingMachineAuditReport auditDepositReport = new VendingMachineAuditReport(balance, deposit);
                    auditDepositReport.WriteToAuditLog();

                }
                else if (selection == PURCHASE_MENU_ITEM)
                {
                    Console.WriteLine(selection);
                    string[] array_of_slots = STRING_OF_SLOTS.Split(" ");

                    object validSlotCheck = "";
                    while (validSlotCheck == null || (object)validSlotCheck == "")
                    {
                        validSlotCheck = ui.GetChoiceFromUserInput(array_of_slots);//GetChoiceFromUserInput will make sure product slot selected is valid
                    }
                    string input = validSlotCheck.ToString().ToUpper();

                    if (productList[input].Price > balance)
                    {
                        Console.WriteLine("\nInsufficient Funds; Please Deposit Additional Funds");

                    }
                    else if (productList[input].AmountOfProduct < 0)
                    {
                        Console.WriteLine("SOLD OUT");
                    }
                    else
                    {
                        
                        Console.WriteLine(productList[input].OutPutMessage);
                        balance -= productList[input].Price;
                        productList[input].AmountOfProduct--;
                        endOfDaySalesReport[productList[input].Name]++;


                        totalSalesReport.TotalSales += productList[input].Price;
                        VendingMachineAuditReport auditProductPurchaseReport = new VendingMachineAuditReport(productList[input], balance);
                        auditProductPurchaseReport.WriteToAuditLog();
                    }
                    
                    
                }//end of transaction
                else if (selection == PURCHASE_MENU_OPTION_FINISH_TRANSACTION)
                {
                    string changeCount = GetChange(balance);

                    Console.WriteLine("You are getting back a " + balance + " in change in the following coin amounts: \n" +
                        changeCount);
                    VendingMachineAuditReport auditPurchaseCompleteReport = new VendingMachineAuditReport(balance);
                    auditPurchaseCompleteReport.WriteToAuditLog();

                    isTransactionFinished = true;
                }
            }
        }

        private string GetChange(decimal balance)
        {
            //Gets the change in the lowest coins possible
            decimal quarter = 0.25M;
            decimal dime = 0.10M;
            decimal nickel = 0.05M;
            decimal penny = 0.01M;

            int amountOfCoins = 0;

            decimal originalBalance = balance;

            string coinCount = "";

            if (balance / quarter >= 1)
            {
                amountOfCoins = (int)(balance / quarter);
                coinCount += "Quarter: " + amountOfCoins;
                balance -= (int)(balance / quarter) * quarter;
            }
            if (balance / dime >= 1)
            {
                amountOfCoins = (int)(balance / dime);
                coinCount = coinCount + " Dime: " + amountOfCoins;
                balance -= (int)(balance / dime) * dime;
            }
            if (balance / nickel >= 1)
            {
                amountOfCoins = (int)(balance / nickel);
                coinCount = coinCount + " Nickel: " + amountOfCoins;
                balance -= (int)(balance / nickel) * nickel;

            }

            if (balance / penny >= 1)
            {
                amountOfCoins = (int)(balance / penny);
                coinCount = coinCount + " Penny: " + amountOfCoins;
                balance -= (int)(balance / penny) * penny;
            }

            return coinCount;
        }



        //As long as the inventory > 0 Print the item
        private void DisplayItems(Dictionary<string, ProductInventory> productList)
        {
            string slot = "Slot";
            string name = "Name";
            string purchasePrice = "Purchase Price";
            Console.WriteLine($"{slot,5} {name,0}".PadRight(22) + $"{purchasePrice}");
            foreach (KeyValuePair<string, ProductInventory> product in productList)
            {
                if (product.Value.AmountOfProduct == 0)
                {
                    Console.WriteLine("SOLD OUT");
                    continue;
                }
                Console.WriteLine($"{product.Value.Slot,5} {product.Value.Name,0}".PadRight(22) + $" {product.Value.Price,0:c}");

            }
        }

        private Dictionary<string, ProductInventory> CreateUpdateVendingMachineInventory()
        {
            Dictionary<string, ProductInventory> inventoryList = new Dictionary<string, ProductInventory>();

            string fullPath = @"C:\Users\Student\workspace\module1-capstone-c-team-7\Example Files\Inventory.txt";
            using (StreamReader sr = new StreamReader(fullPath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] productArray = line.Split("|");
                    string type = productArray[3];
                    decimal price = decimal.Parse(productArray[2]);
                    string name = productArray[1];
                    string slot = productArray[0];
                    STRING_OF_SLOTS = $"{slot} {STRING_OF_SLOTS}";
                    endOfDaySalesReport[name] = 0;

                    switch (type)
                    {
                        case "Chip":
                            inventoryList[slot] = new Chips(slot, name, price);                            
                            break;
                        case "Gum":
                            inventoryList[slot] = new Gum(slot, name, price);                            
                            break;
                        case "Drink":
                            inventoryList[slot] = new Beverage(slot, name, price);                            
                            break;
                        case "Candy":
                            inventoryList[slot] = new Candy(slot, name, price);                            
                            break;
                        default:
                            break;
                    }


                }

                return inventoryList;
            }


        }
    }
}

    

