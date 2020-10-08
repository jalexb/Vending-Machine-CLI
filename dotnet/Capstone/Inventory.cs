using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
       
    
    //Shows the correct items for purchase every time

    
    //
    //When the vending machine is started: starting is the VendingMachine.Run() method
   
    //Given an item is in the inventory file
    //When the vending machine is started and the inventory is updated when VendingMachine.Run() Method is called, 
    //Then the slot indicated is filled with five of that product.

    
    //The Vending machine has 2 user interfaces: a customer and an owner user interface

    //Calling the MenuDrivenCli.PromptForSelectin() Displays the options the user can have
    //The customer and the owner need differeny options.
    //The owner accesses UpdateVendingMachineInventory() to update each item to 5


    //The customer can purchase item 1 at a time, displaying sold/available, when then slot is ReadLine() -> reducing the inventory by one and charges customer, and gives back lowest amount of coins

    //The owner overrides some methods that the customer inherits for the user interface 
    //Vending machine Class : Gets the Inventory Item List, Print the ItemsAvailable(), starts the vending machine with Run() ->



  


    //Given an item is available
    //When a customer displays items
    //Then the item is displayed with the name, slot identifier and purchase price
    //Given an item is sold out or otherwise not available
    //When a customer displays items
    //Then the item line is displayed as SOLD OUT



    //    Given a customer is on the purchase menu and has not deposited money(current balance is $0)
    //    When they try to select a product
    //    Then they are given an error message that they must deposit money before making a selection
    //Given a customer is in the purchase menu
    //Then they are able to deposit money in whole dollar amounts
    //Given a customer is in the purchase menu
    //When they deposit money
    //Then his/her current balance is updated and displayed
    //Given a customer is on the purchase menu
    //When they select an item for purchase that is available
    //Then : 1) the customer's current balance is updated based on item cost 2) the inventory and balance
    //of the vending machine are updated 3) the item is dispensed and the user receives a message based
    //on the item type: All chip items will print “Crunch Crunch, Yum!” All candy items will print “Munch
    //Munch, Yum!” All drink items will print “Glug Glug, Yum!” All gum items will print “Chew Chew,
    //Yum!” 4) the customer is returned to the purchase menu
    //Givena customer is on the purchase menu
    //When the customer attempts to purchase an item is sold out or otherwise not available
    //Then they are given an error message and returned to the purchase menu
    //Given a customer is on the purchase menu
    //When they choose Finish Transaction
    //Then the customer recieves(a message with) their change using nickels, dimes and quarters using
    //the smallest amount of coins possible and the current balance is updated to $0. 


    public class ProductInventory
    {

        //Contains the list of items formatted correctly
        
        //Inventory Item class : constructs the name, price, the output when purchased, and Dictionary<string:{A1,A2,A3}, Item Object{Potato Crisps, 3.05, Chip}> ItemCode
        //Subclasses : Chip, Candy, Drink, Gum
        //Then inventory is created/updated(Read/Write) what items are in each slot (item dictionary key) based on the contents (item dictionary object) of the file.  module1-capstone-c-team-7\Example Files\Inventory.txt

        public string Slot { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ProductType { get; set; }
        public string OutPutMessage { get; set; }
        public int AmountOfProduct { get; set; } = 5;

        public ProductInventory() { }
    }
}
