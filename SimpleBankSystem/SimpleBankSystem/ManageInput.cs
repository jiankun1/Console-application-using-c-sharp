using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankSystem
{
    class ManageInput
    {
        
        ConsoleKeyInfo key;
        private string userInput;

        //Get user input and limit the length of input
        public string GetInputAndLimitLength(int inputLength)
        {
            userInput = string.Empty;
            //Allow user input until Enter key is pressed
            do
            {
                
                key = Console.ReadKey(true);

                //if press backspace, remove one char
                if (key.Key == ConsoleKey.Backspace && userInput.Length > 0)
                {
                    Console.Write("\b \b");
                    //use substring to remove last char
                    userInput = userInput.Substring(0, userInput.Length - 1);

                }
                //control char will not be add to the string variable
                else if (!char.IsControl(key.KeyChar) && userInput.Length < inputLength)
                {
                    
                    Console.Write(key.KeyChar);
                    userInput += key.KeyChar;
                }
            }while (key.Key != ConsoleKey.Enter);

            return userInput;
        }

        //password masking, reference to https://stackoverflow.com/questions/3404421/password-masking-console-application
        public string GetPassword(int inputLength)
        {
            var pwd = string.Empty;
            //allow enter password unitil enter key is pressed
            do
            {
                key = Console.ReadKey(true);

                //if press backspace, remove one char
                if (key.Key == ConsoleKey.Backspace && pwd.Length > 0)
                {
                    Console.Write("\b \b");
                    
                    //pwd = pwd[0..^1];
                    pwd = pwd.Substring(0, pwd.Length - 1);
                }
                //control char will not be add to the password variable
                else if (!char.IsControl(key.KeyChar) && pwd.Length < inputLength)
                {
                    Console.Write("*");
                    //Console.Write(key.KeyChar);
                    pwd += key.KeyChar;
                }

            } while (key.Key != ConsoleKey.Enter);
            
            return pwd;
        }
    }
}
