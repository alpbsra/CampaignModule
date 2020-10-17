using CampaignModule.Core.Objects;
using System;
using System.Collections.Generic;

namespace CampaignModule.App
{
    class Program
    {
        #region Used for storage 
        static List<Product> S_ProductList { get; set; }
        static List<Campaign> S_CampaignList { get; set; }
        static List<Order> S_OrderList { get; set; }
        #endregion

        static void Main(string[] args)
        {
            S_ProductList = new List<Product>();
            S_CampaignList = new List<Campaign>();
            S_OrderList = new List<Order>();
                       
            Console.WriteLine("Please enter one of 1, 2, 3 or 4 values for select scenario file");
            string scenarioFileNumber = Console.ReadLine();
            var fileNumbers = new List<string> { "1", "2", "3", "4" };

            if (fileNumbers.Contains(scenarioFileNumber))
            {
                try
                {
                    var timehandler = new TimeHandler { Time = 0 };
                    var CommandList = new List<string>();
                    var opr = new Operations();
                    CommandList = opr.ReadCommandsFromScenarioFile(scenarioFileNumber);

                    if (CommandList.Count == 0)
                        Console.WriteLine("No command in file!");
                    else
                        foreach (string line in CommandList)
                            Console.WriteLine(opr.ExecuteCommand(line, timehandler, S_ProductList, S_CampaignList, S_OrderList));
                }
                catch (Exception exc)
                {
                    Console.WriteLine("Unexpected error occurred. Error detail : " + exc.Message);
                }
            }
            else
                Console.WriteLine("Please enter one of 1, 2, 3 or 4 values for select scenario file.");

            Console.ReadLine();
        }
    }
}
