using CampaignModule.Core.Factory;
using CampaignModule.Core.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CampaignModule.App
{
    public class Operations
    {
        public List<string> ReadCommandsFromScenarioFile(string scenarioFileNumber)
        {
            List<string> CommandList = new List<string>();
            try
            {
                string mPath = string.Format("Scenarios\\SCN{0}", scenarioFileNumber);

                if (File.Exists(mPath))
                {
                    StreamReader sr = new StreamReader(mPath);
                    var line = sr.ReadLine();

                    while (!string.IsNullOrEmpty(line))
                    {
                        CommandList.Add(line);
                        line = sr.ReadLine();
                    }
                    sr.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return CommandList;
        }
        
        public string ExecuteCommand(string command, TimeHandler timeHandler, List<Product> productList, List<Campaign> campaignList, List<Order> orderList)
        {
            string commandResult = string.Empty;
            char[] seperators = { ' ' };

            List<string> cmdArr = command.Split(seperators).ToList().Where(x => !string.IsNullOrEmpty(x)).ToList();
            string commandName = cmdArr[0]; // first parameter is always command name.. like create_product

            if (commandName == "increase_time")
            {
                timeHandler.IncreaseTime(Convert.ToInt32(cmdArr[1]));
                commandResult = string.Format("Time is {0}:00", timeHandler.Time.ToString().PadLeft(2, '0'));
            }
            else
            {                
                cmdArr.RemoveAt(0); // the remaining array elements are parameters of the command

                FactoryCreator CommandCreator = new FactoryCreator();
                BaseCommands commandModel = CommandCreator.CommandFactory(commandName);

                if (commandModel != null)
                {
                    string checkResult = commandModel.CheckParameters(cmdArr, productList, campaignList, orderList);

                    if (string.IsNullOrEmpty(checkResult))
                        commandResult = commandModel.GetCommandResult(cmdArr, productList, campaignList, orderList, timeHandler);
                    else
                        commandResult = checkResult;
                }
                else
                {
                    commandResult = "The command is not defined in the system";
                }
            }

            return commandResult;
        }
    }
}
