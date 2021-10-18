using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleBankSystem
{
    class CreateNewAccount
    {
        public BankAccount bankAccount;
        private static int accountNumberSeed = 200000;
        private int accountNo;
        private string fileName;
        private string fileContent;
        public int CreateAccount (string fName, string lName, string address, int phone, string email)
        {
            //generate a unique account number
            accountNo = GenerateAccNo(accountNumberSeed);
            bankAccount = new BankAccount(fName, lName, address, phone, email, accountNo, 0);
            //file name
            fileName = accountNo.ToString() + ".txt";
            //add the txt file content
            fileContent += "First Name|" + bankAccount.FirstName + "\n";
            fileContent += "Last Name|" + bankAccount.LastName + "\n";
            fileContent += "Address|" + bankAccount.Address + "\n";
            fileContent += "Phone|" + bankAccount.PhoneNo.ToString() + "\n";
            fileContent += "Email|" + bankAccount.Email + "\n";
            fileContent += "AccountNo|" + bankAccount.AccountNo.ToString() + "\n";
            fileContent += "Balance|" + bankAccount.Balance.ToString() + "\n";
            //create a new file and write the content to it, then close it
            System.IO.File.WriteAllText(fileName, fileContent);
            return accountNo;


        }

        //Method to generate unique number, check all the txt file
        public int GenerateAccNo(int number)
        {
            //number is the accountNumberSeed which is 200000
            int accountNo = number;
            do
            {
                string filename = accountNo.ToString() + ".txt";
                if (!File.Exists(filename))
                {
                    break;
                }
                //if the account number existed, add 1 to the seed
                else if (File.Exists(filename))
                {
                    accountNo++;
                }

            } while (true);
            return accountNo;
        }
    }
}
