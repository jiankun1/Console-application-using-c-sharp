using System;

namespace SimpleBankSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            int selection;
            UserInterface showUI = new UserInterface();
            showUI.CreateLoginPage(10, 50, 2, 20);
            
            //Do..while loop allow user back to the main menu after user completed other actions, press 7 to exit the application
            do
            {
                //user input the number of option
                selection = showUI.MainMenu(13, 50, 2, 20);
                switch (selection)
                {
                    case 1:
                        showUI.CreateNewAccount(12, 50, 2, 20);
                        Console.ReadKey();
                        break;
                    case 2:
                        showUI.SearchAccount(8, 50, 2, 20);
                        Console.ReadKey();
                        break;
                    case 3:
                        showUI.DepositPage(9, 50, 2, 20);
                        Console.ReadKey();
                        break;
                    case 4:
                        showUI.WithdrawalPage(9, 50, 2, 20);
                        Console.ReadKey();
                        break;
                    case 5:
                        showUI.AccountStatement(8, 50, 2, 20);
                        Console.ReadKey();
                        break;
                    case 6:
                        showUI.DeleteAccount(8, 50, 2, 20);
                        Console.ReadKey();
                        break;
                    case 7:
                        Environment.Exit(0);
                        break;
                }

            } while (true);
            
        }
    }

}
