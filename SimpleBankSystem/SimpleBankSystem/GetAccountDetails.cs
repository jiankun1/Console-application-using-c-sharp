using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleBankSystem
{
    class GetAccountDetails
    {
        private string[,] accountDetails = new string[7, 2];

        //Method to collect information of specific lines in the txt file
        public string[,] SearchAccDetails(string filename)
        {
            
            try
            {
                //read the credentials into a string array
                string[] lines = System.IO.File.ReadAllLines(filename);

                //the first 7 lines in the txt file
                for (int num = 0; num < 7; num++)
                {
                    string[] word = lines[num].Split("|");
                    accountDetails[num, 0] = word[0];
                    accountDetails[num, 1] = word[1];
                }


            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }

            return accountDetails;
        }
    }
}
