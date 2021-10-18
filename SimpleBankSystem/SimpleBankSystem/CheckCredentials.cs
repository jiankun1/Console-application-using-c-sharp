using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleBankSystem
{
    class CheckCredentials
    {
        string[,] credentials = new string[3, 2];
        
        public bool CheckInfo (string username, string password)
        {
            //read the credentials from login text file
            try
            {
                //read the credentials into a string array
                string[] lines = System.IO.File.ReadAllLines("login.txt");

                int user = 0;
                foreach (string line in lines)
                {
                    //split the lines
                    string[] words = line.Split("|");
                    credentials[user, 0] = words[0];
                    credentials[user, 1] = words[1];
                    user++;
                }


            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }

            //check the users name and password
            for (int num = 0; num < credentials.GetLength(0); num++)
            {

                if (username.CompareTo(credentials[num, 0]) == 0 && password.CompareTo(credentials[num, 1]) == 0)
                {
                    return true;
                }
                /*else if (num == 2)
                {
                    return false;
                }*/
            }
            return false;
        }
    }
}
