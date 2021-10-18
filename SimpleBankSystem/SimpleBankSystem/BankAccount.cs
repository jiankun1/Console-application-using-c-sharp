using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankSystem
{
    class BankAccount
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int PhoneNo { get; set; }
        public string Email { get; set; }
        public int AccountNo { get; set; }
        public double Balance { get; set; }
        //public float Transaction { get; set; }

        //Constructor
        public BankAccount(string fName, string lName, string address, int phoneNo, string email, int accountNo, double balance)
        {
            this.FirstName = fName;
            this.LastName = lName;
            this.Address = address;
            this.PhoneNo = phoneNo;
            this.Email = email;
            this.AccountNo = accountNo;
            this.Balance = balance;
            //this.Transaction = transaction;
        }
    }
}
