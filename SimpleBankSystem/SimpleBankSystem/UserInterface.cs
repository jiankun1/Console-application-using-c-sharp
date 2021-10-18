using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleBankSystem
{
    class UserInterface
    {
        //Follow SetCursorPosition Method on Microsoft Docs
        protected static int originY;
        protected static int originX;

        //Set a array contained the input tags
        private string[] loginTags = { "User Name: ", "Password: " };
        private CheckCredentials checkLogin = new CheckCredentials();
        private ManageInput getUserInput = new ManageInput();

        private string[] mainMenuTags = { "1. Create a new account", "2. Search for an account", "3. Deposit", "4. Withdraw", "5. A/C statement", "6. Delete account", "7. Exit"};
        private string[] createAccTags = { "First Name: ", "Last Name: ", "Address: ", "Phone: ", "Email: " };
        //private string[] newAccountTags = { "First Name", "Last Name", "Address", "Phone", "Email" };
        private CreateNewAccount createAcc = new CreateNewAccount();
        //Array to store the position behind the tags
        private int[,] loginPosition = new int[2, 2];
        string[] loginInputs = new string[2];
        string[,] credentials = new string[3, 2];
        int menuInputPosX;
        int menuInputPosY;
        int menuInput;
        string[] createAccInput = new string[5];
        int[,] createAccPos = new int[5, 2];
        //private static int accountNumberSeed = 200000;
        //search account properties
        int searchInput;
        //account number and y/n
        int[,] searchInputPos = new int[2, 2];
        private GetAccountDetails getAccDetails = new GetAccountDetails();

        //Deposit input field
        int[,] depositFieldPos = new int[2, 2];
        private string[] depositTags = { "Account Number: ", "Amount: $" };

        //Withdraw input field
        int[,] withdrawFieldPos = new int[2, 2];
        private string[] withdrawTags = { "Account Number: ", "Amount: $" };

        //account statement input field position
        int[] accStatementPos = new int[2];

        //Delete account input field position
        int[] delAccountPos = new int[2];

        SendEmail sendStatement = new SendEmail();


        //Print the character at specific position, reference to Microsoft Docs
        protected static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(originX + x, originY + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        //Create the login page
        public void CreateLoginPage(int pageDepth, int pageWidth, int startY, int startX)
        {
            //Clear the screen
            Console.Clear();
            //Copy the original cursor position
            originY = Console.CursorTop;
            originX = Console.CursorLeft;

            //Print the border of the login page
            for (int depth = 0; depth < pageDepth; depth++)
            {
                if (depth == 0 | depth == 2 | depth == (pageDepth - 1))
                {
                    for (int width = 0; width < pageWidth; width++)
                    {
                        WriteAt("=", startX + width, startY + depth);
                    }
                }
                else
                {
                    WriteAt("|", startX, startY + depth);
                    WriteAt("|", startX + pageWidth - 1, startY + depth);
                }
            }
            //Print out the heading and sub-heading in the form
            WriteAt("WELCOME TO SIMPLE BANKING SYSTEM", startX + 10, startY + 1);
            WriteAt("LOGIN TO START", startX + 18, startY + 3);

            // Display the labels of login field
            int item = 0;
            foreach (string tagName in loginTags)
            {
                WriteAt(tagName, startX + 10, startY + 6 + item);
                //store column value
                loginPosition[item, 0] = Console.CursorLeft;
                //store row value
                loginPosition[item, 1] = Console.CursorTop;
                //next item
                item++;
            }

            //do..while loop allow user input username and password in multiple times
            do
            {
                // user inputs, item = 1 because two cursor location were stored in the array
                // do for loop to clear the input value on the sreen
                for (int num = 0; num < item; num++)
                {
                    if (loginInputs[num] != null)
                    {
                        Console.SetCursorPosition(loginPosition[num, 0], loginPosition[num, 1]);
                        //console write space to cover previous value
                        Console.Write(new string(' ', loginInputs[num].Length));
                    }

                }
                //clear the warning message
                WriteAt("                              ", startX, pageDepth + 2);

                // user inputs, item = 1 because two cursor location were stored 
                //use for loop set the cursor position and let user input username and password
                for (int num = 0; num < item; num++)
                {
                    //for user entering password
                    if (num == 1)
                    {
                        //set position
                        Console.SetCursorPosition(loginPosition[num, 0], loginPosition[num, 1]);
                        //call the method Getpassword to get user input and store in loginInputs[1], 15 is the length of user input
                        loginInputs[num] = getUserInput.GetPassword(15);

                    }
                    //User entering username
                    else
                    {
                        Console.SetCursorPosition(loginPosition[num, 0], loginPosition[num, 1]);
                        //the length of user input is 10
                        loginInputs[num] = getUserInput.GetInputAndLimitLength(10);
                    }
                    

                }
                
                //check the users name and password, refer to class CheckCredentials
                if (checkLogin.CheckInfo(loginInputs[0], loginInputs[1]))
                {
                    WriteAt("Valid credentials!...", startX, pageDepth + 2);
                    Console.ReadKey();
                    break;
                }
                else
                {
                    WriteAt("Invalid credentials!...", startX, pageDepth + 2);
                    Console.ReadKey();
                }


            } while (true);
        }

        //Interface for main menu
        public int MainMenu(int pageDepth, int pageWidth, int startY, int startX)
        {
            string inputResult = null;

            //Clear the screen
            Console.Clear();
            //Copy the original cursor position
            originY = Console.CursorTop;
            originX = Console.CursorLeft;

            //Print the form of the main menu
            for (int depth = 0; depth < pageDepth; depth++)
            {
                if (depth == 0 | depth == 2 | depth == 10 | depth == (pageDepth - 1))
                {
                    for (int width = 0; width < pageWidth; width++)
                    {
                        WriteAt("=", startX + width, startY + depth);
                    }
                }
                else
                {
                    WriteAt("|", startX, startY + depth);
                    WriteAt("|", startX + pageWidth - 1, startY + depth);
                }
            }
            //Print out the heading in the form
            WriteAt("WELCOME TO SIMPLE BANKING SYSTEM", startX + 10, startY + 1);

            // Display the labels of main menu
            int item = 0;
            foreach (string tagName in mainMenuTags)
            {
                WriteAt(tagName, startX + 10, startY + 3 + item);
                item++;
            }

            //let user select the menu, collect the input position
            WriteAt("Enter your choice (1-7): ", startX + 8, startY + 11);
            menuInputPosX = Console.CursorLeft;
            menuInputPosY = Console.CursorTop;
            
            //Allow user input for multiple times
            do
            {
                //clear the previous input value
                if (inputResult != null)
                {
                    Console.SetCursorPosition(menuInputPosX, menuInputPosY);
                    Console.Write(new string(' ', inputResult.Length));
                }
                //set the cursor position again and user can only enter 1 integer
                Console.SetCursorPosition(menuInputPosX, menuInputPosY);
                inputResult = getUserInput.GetInputAndLimitLength(1);

                //check if the user input is integer and within the range (1-7)
                bool checkInt = int.TryParse(inputResult, out menuInput);
                if (checkInt == false || menuInput > 7 || menuInput < 1 )
                {
                    WriteAt("Please Enter a number (1-7)", startX, pageDepth + 2);
                    //Console.ReadKey();
                }
                else
                {
                    WriteAt("Press Enter to proceed .......", startX, pageDepth + 2);
                    Console.ReadKey();
                    break;
                }


            } while (true);
            return menuInput;
        }

        //Interface for create account page
        public void CreateNewAccount(int pageDepth, int pageWidth, int startY, int startX)
        {
            //Local variables
            int phone;
            string checkInfo = "n";
            int checkInfoPosX;
            int checkInfoPosY;

            //Clear console
            Console.Clear();
            //Copy the original cursor position
            originY = Console.CursorTop;
            originX = Console.CursorLeft;

            //Print the form of the create account page
            for (int depth = 0; depth < pageDepth; depth++)
            {
                if (depth == 0 | depth == 2 | depth == (pageDepth - 1))
                {
                    for (int width = 0; width < pageWidth; width++)
                    {
                        WriteAt("=", startX + width, startY + depth);
                    }
                }
                else
                {
                    WriteAt("|", startX, startY + depth);
                    WriteAt("|", startX + pageWidth - 1, startY + depth);
                }
            }

            //Print out the heading and sub-heading in the form
            WriteAt("CREATE A NEW ACCOUNT", startX + 15, startY + 1);
            WriteAt("ENTER THE DETAILS", startX + 15, startY + 3);

            
            // Display the labels and record the cursor postition
            int item = 0;
            foreach (string tagName in createAccTags)
            {
                WriteAt(tagName, startX + 2, startY + 5 + item);
                //store column value
                createAccPos[item, 0] = Console.CursorLeft;
                //store row value
                createAccPos[item, 1] = Console.CursorTop;
                //next item
                item++;
            }

            //Allow user enter information for multiple times
            do
            {
                //clear previous input value on the screen
                for (int num = 0; num < item; num++)
                {
                    
                    if (createAccInput[num] != null)
                    {
                        Console.SetCursorPosition(createAccPos[num, 0], createAccPos[num, 1]);
                        Console.Write(new string(' ', createAccInput[num].Length));
                    }
                }
                //clear the warning message
                WriteAt("                                          ", startX, startY + pageDepth + 1);

                //get user input
                for (int num = 0; num < item; num++)
                {
                    Console.SetCursorPosition(createAccPos[num, 0], createAccPos[num, 1]);
                    
                    //if input the phone number
                    if (num == 3)
                    {
                        //keep checking if the input phone number is vaild
                        do
                        {
                            //clear the input field for phone number
                            if (createAccInput[num] != null)
                            {
                                Console.SetCursorPosition(createAccPos[num, 0], createAccPos[num, 1]);
                                Console.Write(new string(' ', createAccInput[num].Length));
                            }
                            Console.SetCursorPosition(createAccPos[num, 0], createAccPos[num, 1]);
                            //limit the maximum length of phone number to 10
                            createAccInput[num] = getUserInput.GetInputAndLimitLength(10);
                            //check if the input is int
                            bool checkInt = int.TryParse(createAccInput[num], out phone);
                            //if phone number format not correct, ask user input again
                            if (checkInt == false || createAccInput[num].Length != 10)
                            {
                                WriteAt("Please Enter 10 digit number", startX, startY + pageDepth + 1);
                                //Console.ReadKey();
                            }
                            else
                            {
                                WriteAt("                             ", startX, startY + pageDepth + 1);
                                break;
                            }

                        } while (true);

                    }
                    //check email format
                    else if (num == 4)
                    {
                        do
                        {
                            //clear the input field for email address
                            if (createAccInput[num] != null)
                            {
                                Console.SetCursorPosition(createAccPos[num, 0], createAccPos[num, 1]);
                                Console.Write(new string(' ', createAccInput[num].Length));
                            }
                            
                            Console.SetCursorPosition(createAccPos[num, 0], createAccPos[num, 1]);
                            //limit the maximum length of email address to 35
                            createAccInput[num] = getUserInput.GetInputAndLimitLength(35);
                            //check if the input contain @
                            if (createAccInput[num].Contains('@'))
                            {
                                string[] word = createAccInput[num].Split('@');
                                //if ensure only has one @
                                if (word.Length > 2)
                                {
                                    WriteAt("Please ensure the email address is correct", startX, startY + pageDepth + 1);
                                }
                                //check if using gmail, outlook or UTS email address
                                else if (word[1].CompareTo("gmail.com") == 0 | word[1].CompareTo("outlook.com") == 0 | word[1].CompareTo("uts.edu.au") == 0)
                                {
                                    WriteAt("                                                     ", startX, startY + pageDepth + 1);
                                    break;
                                }
                                else
                                {
                                    WriteAt("Please use gmail, outlook or UTS email     ", startX, startY + pageDepth + 1);
                                }
                            }
                            //if not contain @
                            else
                            {
                                WriteAt("Please ensure the email address is correct", startX, startY + pageDepth + 1);
                            }

                        } while (true);
                    }
                    else
                    {
                        //Collect user input for other fields
                        createAccInput[num] = getUserInput.GetInputAndLimitLength(35);
                    }

                }
                //confirm message
                WriteAt("Is the information correct (y/n)? ", startX, startY + pageDepth + 1);
                checkInfoPosX = Console.CursorLeft;
                checkInfoPosY = Console.CursorTop;
                //user check the information and input y or n
                do
                {
                    if (checkInfo != null)
                    {
                        Console.SetCursorPosition(checkInfoPosX, checkInfoPosY);
                        Console.Write(new string(' ', checkInfo.Length));
                    }
                    Console.SetCursorPosition(checkInfoPosX, checkInfoPosY);
                    checkInfo = getUserInput.GetInputAndLimitLength(1);
                    if (checkInfo == "y" | checkInfo == "n")
                    {
                        break;
                    }
                } while (true);

            } while (checkInfo == "n");

            WriteAt("Account Created! details will be provided via email.", startX, startY + pageDepth + 4);

            //convert input string to int
            phone = Convert.ToInt32(createAccInput[3]);
            //create a txt file with user details and generate a account number
            int accountNo = createAcc.CreateAccount(createAccInput[0], createAccInput[1], createAccInput[2], phone, createAccInput[4]);
            //account number messasge
            string accountNoMsg = "Account number is: " + accountNo.ToString();
            WriteAt(accountNoMsg, startX, startY + pageDepth + 5);
            //send email to the email address provided
            sendStatement.SendNewAccount(createAccInput, createAccInput[4], createAccInput[0], accountNo);

        }

        //Interface for search account page
        public void SearchAccount (int pageDepth, int pageWidth, int startY, int startX)
        {
            string searchAcc = null;
            //save the input of yes or no
            string checkAnotherAcc = "y";
            string[,] accDetails = new string[7, 2];
            string actualContent;

            //clear the console
            Console.Clear();
            //Copy the original cursor position
            originY = Console.CursorTop;
            originX = Console.CursorLeft;

            //Print the form of the search account form
            for (int depth = 0; depth < pageDepth; depth++)
            {
                if (depth == 0 | depth == 2 | depth == (pageDepth - 1))
                {
                    for (int width = 0; width < pageWidth; width++)
                    {
                        WriteAt("=", startX + width, startY + depth);
                    }
                }
                else
                {
                    WriteAt("|", startX, startY + depth);
                    WriteAt("|", startX + pageWidth - 1, startY + depth);
                }
            }

            //Print out the heading and sub-heading in the form
            WriteAt("SEARCH AN ACCOUNT", startX + 15, startY + 1);
            WriteAt("ENTER THE DETAILS", startX + 15, startY + 3);
            WriteAt("Account Number: ", startX + 8, startY + 6);
            searchInputPos[0,0] = Console.CursorLeft;
            searchInputPos[0,1] = Console.CursorTop;

            //Allow user input account number for multiple times
            do
            {
                //check if the input field is empty
                if (searchAcc != null)
                {
                    Console.SetCursorPosition(searchInputPos[0,0], searchInputPos[0,1]);
                    Console.Write(new string(' ', searchAcc.Length));

                }
                //clear the warning message and previous account statment
                WriteAt("                             ", startX, startY + pageDepth + 1);
                WriteAt("                                    ", startX, startY + pageDepth + 2);
                for (int num = 0; num < 14; num++)
                {
                    WriteAt("                                                  ", startX, startY + pageDepth + 2 + num);
                }

                //let user input 
                Console.SetCursorPosition(searchInputPos[0,0], searchInputPos[0,1]);
                searchAcc = getUserInput.GetInputAndLimitLength(10);
                //firstly put it as file name
                string theFileName = searchAcc + ".txt";
                //check if input is integer
                bool checkInt = int.TryParse(searchAcc, out searchInput);

                //check if the input account number meet is vaild
                if (checkInt == false | searchAcc.Length < 6 )
                {
                    
                    //"account not found" message
                    WriteAt("Account not found!", startX, startY + pageDepth + 1);
                    WriteAt("Check another account (y/n)? ", startX, startY + pageDepth + 2);
                    //receive the input
                    checkAnotherAcc = getUserInput.GetInputAndLimitLength(1);
                    
                }
                else if (!File.Exists(theFileName))
                {
                    //"account not found" message
                    WriteAt("Account not found!", startX, startY + pageDepth + 1);
                    WriteAt("Check another account (y/n)? ", startX, startY + pageDepth + 2);
                    //receive the input
                    checkAnotherAcc = getUserInput.GetInputAndLimitLength(1);
                    
                }
                else if (File.Exists(theFileName))
                {
                    //"account found" message
                    WriteAt("Account found!", startX, startY + pageDepth + 1);
                    //print account details
                    //print the form for the account statement
                    for (int depth = 0; depth < 13; depth++)
                    {
                        if (depth == 0 | depth == 2 | depth == 12)
                        {
                            for (int width = 0; width < pageWidth; width++)
                            {
                                WriteAt("=", startX + width, startY + depth + pageDepth + 2);
                            }
                        }
                        else
                        {
                            WriteAt("|", startX, startY + depth + pageDepth + 2);
                            WriteAt("|", startX + pageWidth - 1, startY + depth + pageDepth + 2);
                        }
                    }
                    WriteAt("ACCOUNT DETAILS", startX + 10, startY + 1 + pageDepth + 2);
                    //Get the account details using method
                    accDetails = getAccDetails.SearchAccDetails(theFileName);
                    //print the information of the account
                    for (int num = 0; num < 2; num++)
                    {
                        if (num == 0)
                        {
                            actualContent = "Account No: " + accDetails[5, 1];
                            WriteAt(actualContent, startX + 5, startY + num + 4 + pageDepth + 2);
                        }
                        else if (num == 1)
                        {
                            actualContent = "Account Balance: $" + accDetails[6, 1];
                            WriteAt(actualContent, startX + 5, startY + num + 4 + pageDepth + 2);
                        }
                    }
                    for (int num = 0; num < 5; num++)
                    {
                        actualContent = accDetails[num, 0] + ": " + accDetails[num, 1];
                        WriteAt(actualContent, startX + 5, startY + num + 6 + pageDepth + 2);
                    }
                    //if check another account, default is yes
                    WriteAt("Check another account (y/n)? ", startX, startY + 13 + pageDepth + 2);
                    checkAnotherAcc = getUserInput.GetInputAndLimitLength(1);
                    
                }
            } while (checkAnotherAcc != "n");
        }

        //Interface for deposit page
        public void DepositPage (int pageDepth, int pageWidth, int startY, int startX)
        {
            //Local variables
            string accountNoInput = null;
            int accountNo;
            string retryInput = "y";
            string theFileName;
            string depositAmountInput = null;
            double depositAmount;
            string actualContent = null;

            Console.Clear();
            //Copy the original cursor position
            originY = Console.CursorTop;
            originX = Console.CursorLeft;

            //Print the form of the deposit page
            for (int depth = 0; depth < pageDepth; depth++)
            {
                if (depth == 0 | depth == 2 | depth == (pageDepth - 1))
                {
                    for (int width = 0; width < pageWidth; width++)
                    {
                        WriteAt("=", startX + width, startY + depth);
                    }
                }
                else
                {
                    WriteAt("|", startX, startY + depth);
                    WriteAt("|", startX + pageWidth - 1, startY + depth);
                }
            }

            //Heading and sub-heading
            WriteAt("DEPOSIT", startX + 20, startY + 1);
            WriteAt("ENTER THE DETAILS", startX + 15, startY + 3);

            //display the label and collect the input field position
            int item = 0;
            foreach (string tageName in depositTags)
            {
                WriteAt(tageName, startX + 10, startY + 6 + item);
                depositFieldPos[item, 0] = Console.CursorLeft;
                depositFieldPos[item, 1] = Console.CursorTop;
                item++;
            }

            //Allow user enter account number multiple times
            do
            {
                //clear input field
                if(accountNoInput != null)
                {
                    Console.SetCursorPosition(depositFieldPos[0, 0], depositFieldPos[0, 1]);
                    Console.Write(new string(' ', accountNoInput.Length));
                }
                //clear message
                WriteAt("                                   ", startX, startY + pageDepth + 1);
                WriteAt("                                   ", startX, startY + pageDepth + 2);

                //get the user input with length 8
                Console.SetCursorPosition(depositFieldPos[0, 0], depositFieldPos[0, 1]);
                accountNoInput = getUserInput.GetInputAndLimitLength(10);
                //Check if user input is integer
                bool checkInt = int.TryParse(accountNoInput, out accountNo);
                theFileName = accountNoInput + ".txt";

                //check the account number
                if (checkInt == false)
                {
                    WriteAt("Please enter integer", startX, startY + pageDepth + 1);
                    WriteAt("Retry (y/n)? ", startX, startY + pageDepth + 2);
                    retryInput = getUserInput.GetInputAndLimitLength(1);

                }
                else if (!File.Exists(theFileName))
                {
                    WriteAt("Account not found!", startX, startY + pageDepth + 1);
                    WriteAt("Retry (y/n)? ", startX, startY + pageDepth + 2);
                    retryInput = getUserInput.GetInputAndLimitLength(1);
                }
                else if (File.Exists(theFileName))
                {
                    WriteAt("Account found! Enter the amount...", startX, startY + pageDepth + 1);
                    retryInput = "y";
                    break;

                }
            } while (retryInput != "n");

            //if user choose n, not to retry, it will not ask user to enter amount and then back to main menu
            if (retryInput == "y")
            {
                //Allow user enter amount in multiple times
                do
                {
                    //clear input field
                    if (depositAmountInput != null)
                    {
                        Console.SetCursorPosition(depositFieldPos[1, 0], depositFieldPos[1, 1]);
                        Console.Write(new string(' ', depositAmountInput.Length));
                    }
                    //clear message
                    WriteAt("                                                                 ", startX, startY + pageDepth + 2);

                    Console.SetCursorPosition(depositFieldPos[1, 0], depositFieldPos[1, 1]);
                    depositAmountInput = getUserInput.GetInputAndLimitLength(10);
                    
                    //check if the amount is integer and larger than 0
                    bool checkDouble = double.TryParse(depositAmountInput, out depositAmount);
                    if(checkDouble == true & depositAmount > 0)
                    {
                        break;
                    }
                    else
                    {
                        WriteAt("Please enter again", startX, startY + pageDepth + 2);
                        Console.ReadKey();
                    }

                } while (true);

                //read the details from txt file
                string[] lines = File.ReadAllLines(theFileName);
                //get the balance
                string[] balanceInfo = lines[6].Split("|");
                //convert to double
                double currentBalance = Convert.ToDouble(balanceInfo[1]);
                //add amount to balance
                currentBalance += Convert.ToDouble(depositAmountInput);

                //write the actual content that shown in the text file
                for (int num = 0; num < lines.Length; num++)
                {
                    //update balance and copy original personal information
                    if(num == 6)
                    {
                        actualContent += balanceInfo[0] + "|" + currentBalance.ToString() + "\n";
                    }
                    else
                    {
                        actualContent += lines[num] + "\n";
                    }
                }
                //add the transaction
                actualContent += DateTime.Now.ToString("dd.MM.yyyy") + "|Deposit|" + depositAmountInput + "|" + currentBalance.ToString() + "\n";
                //save to txt file
                System.IO.File.WriteAllText(theFileName, actualContent);
                WriteAt("Deposit successful!", startX, startY + pageDepth + 2);

            }
        }

        //Interface for withdraw page
        public void WithdrawalPage(int pageDepth, int pageWidth, int startY, int startX)
        {
            //local variable
            string accountNoInput = null;
            int accountNo;
            string retryInput = "y";
            string theFileName;
            string withdrawAmountInput = null;
            double withdrawAmount;
            string actualContent = null;

            //clear the console
            Console.Clear();
            //Copy the original cursor position
            originY = Console.CursorTop;
            originX = Console.CursorLeft;
            
            //Print the form of the withdrawal page
            for (int depth = 0; depth < pageDepth; depth++)
            {
                if (depth == 0 | depth == 2 | depth == (pageDepth - 1))
                {
                    for (int width = 0; width < pageWidth; width++)
                    {
                        WriteAt("=", startX + width, startY + depth);
                    }
                }
                else
                {
                    WriteAt("|", startX, startY + depth);
                    WriteAt("|", startX + pageWidth - 1, startY + depth);
                }
            }
            //Heading and sub-heading
            WriteAt("WITHDRAW", startX + 20, startY + 1);
            WriteAt("ENTER THE DETAILS", startX + 15, startY + 3);

            //display the label and collect the input field position
            int item = 0;
            foreach (string tageName in withdrawTags)
            {
                WriteAt(tageName, startX + 5, startY + 6 + item);
                withdrawFieldPos[item, 0] = Console.CursorLeft;
                withdrawFieldPos[item, 1] = Console.CursorTop;
                item++;
            }

            //Allow user input account number in multiple times
            do
            {
                //clear input field
                if (accountNoInput != null)
                {
                    Console.SetCursorPosition(withdrawFieldPos[0, 0], withdrawFieldPos[0, 1]);
                    Console.Write(new string(' ', accountNoInput.Length));
                }
                //clear message
                WriteAt("                                   ", startX, startY + pageDepth + 1);
                WriteAt("                                   ", startX, startY + pageDepth + 2);
                //get user input
                Console.SetCursorPosition(withdrawFieldPos[0, 0], withdrawFieldPos[0, 1]);
                accountNoInput = getUserInput.GetInputAndLimitLength(10);
                //check if the account number is valid
                bool checkInt = int.TryParse(accountNoInput, out accountNo);
                theFileName = accountNoInput + ".txt";
                //check the account number
                if (checkInt == false)
                {
                    WriteAt("Please enter integer", startX, startY + pageDepth + 1);
                    WriteAt("Retry (y/n)? ", startX, startY + pageDepth + 2);
                    retryInput = getUserInput.GetInputAndLimitLength(1);

                }
                else if (!File.Exists(theFileName))
                {
                    WriteAt("Account not found!", startX, startY + pageDepth + 1);
                    WriteAt("Retry (y/n)? ", startX, startY + pageDepth + 2);
                    retryInput = getUserInput.GetInputAndLimitLength(1);
                }
                else if (File.Exists(theFileName))
                {
                    WriteAt("Account found! Enter the amount...", startX, startY + pageDepth + 1);
                    retryInput = "y";
                    break;
                }
            } while (retryInput != "n");

            //if user enter 'n', it will go back to main menu and not run the following code
            if (retryInput == "y")
            {
                //get information from txt file
                string[] lines = File.ReadAllLines(theFileName);
                //get the balance
                string[] balanceInfo = lines[6].Split("|");
                double currentBalance = Convert.ToDouble(balanceInfo[1]);

                //Allow user enter amount multiple times
                do
                {
                    //clear input field
                    if (withdrawAmountInput != null)
                    {
                        Console.SetCursorPosition(withdrawFieldPos[1, 0], withdrawFieldPos[1, 1]);
                        Console.Write(new string(' ', withdrawAmountInput.Length));
                    }
                    //clear warning message
                    WriteAt("                                                                 ", startX, startY + pageDepth + 2);
                    
                    //get user input
                    Console.SetCursorPosition(withdrawFieldPos[1, 0], withdrawFieldPos[1, 1]);
                    withdrawAmountInput = getUserInput.GetInputAndLimitLength(10);
                    //make sure user input is integer and valid
                    bool checkDouble = double.TryParse(withdrawAmountInput, out withdrawAmount);
                    if(checkDouble == false | withdrawAmount < 0)
                    {
                        WriteAt("Please enter again", startX, startY + pageDepth + 2);
                        Console.ReadKey();
                    }
                    else if(currentBalance < withdrawAmount)
                    {
                        WriteAt("The withdrawal exceeds the current balance, please try again", startX, startY + pageDepth + 2);
                        Console.ReadKey();
                    }
                    else
                    {
                        currentBalance -= withdrawAmount;
                        break;
                    }

                } while (true);

                //write the actual content that shown in the text file
                for (int num = 0; num < lines.Length; num++)
                {
                    //update balance
                    if (num == 6)
                    {
                        actualContent += balanceInfo[0] + "|" + currentBalance.ToString() + "\n";
                    }
                    else
                    {
                        actualContent += lines[num] + "\n";
                    }
                }
                //add the transaction
                actualContent += DateTime.Now.ToString("dd.MM.yyyy") + "|Withdraw|" + withdrawAmountInput + "|" + currentBalance.ToString() + "\n";
                System.IO.File.WriteAllText(theFileName, actualContent);
                WriteAt("Withdraw successful!", startX, startY + pageDepth + 2);

            }
        }

        //Interface for account statement
        public void AccountStatement (int pageDepth, int pageWidth, int startY, int startX)
        {
            //local variables
            string accNumberInput = null;
            int accNumber;
            string theFileName;
            string[,] accDetails = new string[7, 2];
            string actualContent;
            string sendEmail;
            string emailBody = string.Empty;
            string emailAddress;
            string receiverName;
            string retryInput = "y";

            //clear console
            Console.Clear();
            //Copy the original cursor position
            originY = Console.CursorTop;
            originX = Console.CursorLeft;
            //print the boarder
            for (int depth = 0; depth < pageDepth; depth++)
            {
                if (depth == 0 | depth == 2 | depth == (pageDepth - 1))
                {
                    for (int width = 0; width < pageWidth; width++)
                    {
                        WriteAt("=", startX + width, startY + depth);
                    }
                }
                else
                {
                    WriteAt("|", startX, startY + depth);
                    WriteAt("|", startX + pageWidth - 1, startY + depth);
                }
            }
            //Display the titles
            WriteAt("STATEMENT", startX + 20, startY + 1);
            WriteAt("ENTER THE DETAILS", startX + 15, startY + 3);
            //user input field
            WriteAt("Account Number: ", startX + 5, startY + 5);
            accStatementPos[0] = Console.CursorLeft;
            accStatementPos[1] = Console.CursorTop;

            //Allow user re-enter account number
            do
            {
                //clear the input field
                if (accNumberInput != null)
                {
                    Console.SetCursorPosition(accStatementPos[0], accStatementPos[1]);
                    Console.Write(new string(' ', accNumberInput.Length));
                }
                //clear the warning message
                WriteAt("                                                                 ", startX, startY + pageDepth + 1);
                WriteAt("                                                                 ", startX, startY + pageDepth + 2);

                //get user input
                Console.SetCursorPosition(accStatementPos[0], accStatementPos[1]);
                accNumberInput = getUserInput.GetInputAndLimitLength(10);
                //check if input is integer
                bool checkInt = int.TryParse(accNumberInput, out accNumber);
                //check if the file is existed
                theFileName = accNumberInput + ".txt";
                if (checkInt == false)
                {
                    WriteAt("Please enter account number...", startX, startY + pageDepth + 1);
                    //Console.ReadKey();
                    WriteAt("Retry (y/n)? ", startX, startY + pageDepth + 2);
                    retryInput = getUserInput.GetInputAndLimitLength(1);
                }
                else if (!File.Exists(theFileName))
                {
                    WriteAt("Account not found! Please enter the number again...", startX, startY + pageDepth + 1);
                    //Console.ReadKey();
                    WriteAt("Retry (y/n)? ", startX, startY + pageDepth + 2);
                    retryInput = getUserInput.GetInputAndLimitLength(1);
                }
                else if (File.Exists(theFileName))
                {
                    WriteAt("Account found! The statement is displayed below", startX, startY + pageDepth + 1);
                    retryInput = "y";
                    break;
                }

            } while (retryInput != "n");

            //if user enter 'n', it will go back to main menu and not run the following code
            if (retryInput == "y")
            {
                //read data from txt file
                string[] lines = File.ReadAllLines(theFileName);
                //display the form
                for (int depth = 0; depth < 19; depth++)
                {
                    if (depth == 0 | depth == 2 | depth == 18)
                    {
                        for (int width = 0; width < pageWidth; width++)
                        {
                            WriteAt("=", startX + width, startY + depth + pageDepth + 2);
                        }
                    }
                    else
                    {
                        WriteAt("|", startX, startY + depth + pageDepth + 2);
                        WriteAt("|", startX + pageWidth - 1, startY + depth + pageDepth + 2);
                    }
                }
                //display titles
                WriteAt("SIMPLE BANKING SYSTEM", startX + 15, startY + pageDepth + 2 + 1);
                WriteAt("Account statement", startX + 17, startY + pageDepth + 2 + 3);

                //display account details
                accDetails = getAccDetails.SearchAccDetails(theFileName);
                for (int num = 0; num < 2; num++)
                {
                    if (num == 0)
                    {
                        actualContent = "Account No: " + accDetails[5, 1];
                        WriteAt(actualContent, startX + 5, startY + num + 5 + pageDepth + 2);
                        emailBody += actualContent + "\n";
                    }
                    else if (num == 1)
                    {
                        actualContent = "Account Balance: $" + accDetails[6, 1];
                        WriteAt(actualContent, startX + 5, startY + num + 5 + pageDepth + 2);
                        emailBody += actualContent + "\n";
                    }
                }
                for (int num = 0; num < 5; num++)
                {
                    actualContent = accDetails[num, 0] + ": " + accDetails[num, 1];
                    WriteAt(actualContent, startX + 5, startY + num + 7 + pageDepth + 2);
                    emailBody += actualContent + "\n";
                }
                
                //transaction labels
                if (lines.Length > 8) 
                {
                    WriteAt("Date | Action | Amount | Balance ", startX + 5, startY + 12 + pageDepth + 2);
                    emailBody += "Date | Action | Amount | Balance \n";
                }

                //display recent five transactions
                int item = 0;
                //if total lines smaller than 12, print from line 8, else print from line (number of lines - 5)
                if (lines.Length < 12)
                {
                    for (int num = 7; num < lines.Length; num++)
                    {
                        WriteAt(lines[num], startX + 5, startY + item + 13 + pageDepth + 2);
                        item++;
                        emailBody += lines[num] + "\n";
                    }
                }
                else
                {
                    for (int num = lines.Length - 5; num < lines.Length; num++)
                    {
                        WriteAt(lines[num], startX + 5, startY + item + 13 + pageDepth + 2);
                        item++;
                        emailBody += lines[num] + "\n";
                    }
                }
                //collect email address and receiver name (first name)
                emailAddress = accDetails[4, 1];
                receiverName = accDetails[0, 1];
                //if enter 'y' , send the statement
                WriteAt("Email statement (y/n)?", startX, startY + pageDepth + 2 + 19);
                sendEmail = getUserInput.GetInputAndLimitLength(1);
                if (sendEmail == "y")
                {
                    sendStatement.SendAccountStatement(emailBody, emailAddress, receiverName);
                    WriteAt("Email sent successfully!...", startX, startY + pageDepth + 2 + 20);

                }
                else
                {
                    WriteAt("Back to Main menu...", startX, startY + pageDepth + 2 + 20);

                }
                Console.ReadKey();
            }

        }

        //Interface for delete account page
        public void DeleteAccount (int pageDepth, int pageWidth, int startY, int startX)
        {
            //local variables
            string delAccountInput = null;
            int delAccount;
            string theFileName;
            string[,] accDetails = new string[7, 2];
            string actualContent;
            string deleteInput;
            string retryInput = "y";

            //clear console
            Console.Clear();
            //Copy the original cursor position
            originY = Console.CursorTop;
            originX = Console.CursorLeft;
            //print the boarder
            for (int depth = 0; depth < pageDepth; depth++)
            {
                if (depth == 0 | depth == 2 | depth == (pageDepth - 1))
                {
                    for (int width = 0; width < pageWidth; width++)
                    {
                        WriteAt("=", startX + width, startY + depth);
                    }
                }
                else
                {
                    WriteAt("|", startX, startY + depth);
                    WriteAt("|", startX + pageWidth - 1, startY + depth);
                }
            }
            //Display the titles
            WriteAt("DELETE AN ACCOUNT", startX + 16, startY + 1);
            WriteAt("ENTER THE DETAILS", startX + 16, startY + 3);
            //User input field
            WriteAt("Account Number: ", startX + 5, startY + 5);
            delAccountPos[0] = Console.CursorLeft;
            delAccountPos[1] = Console.CursorTop;

            //Allow user re-enter account number
            do
            {
                //clear the input field
                if (delAccountInput != null)
                {
                    Console.SetCursorPosition(delAccountPos[0], delAccountPos[1]);
                    Console.Write(new string(' ', delAccountInput.Length));
                }
                //clear the warning message
                WriteAt("                                                                 ", startX, startY + pageDepth + 1);
                WriteAt("                                                                 ", startX, startY + pageDepth + 2);
                //get user input
                Console.SetCursorPosition(delAccountPos[0], delAccountPos[1]);
                delAccountInput = getUserInput.GetInputAndLimitLength(10);
                //check if input is integer
                bool checkInt = int.TryParse(delAccountInput, out delAccount);
                //check if the account is existed
                theFileName = delAccountInput + ".txt";
                if (checkInt == false)
                {
                    WriteAt("Please enter account number...", startX, startY + pageDepth + 1);
                    //Console.ReadKey();
                    WriteAt("Retry (y/n)? ", startX, startY + pageDepth + 2);
                    retryInput = getUserInput.GetInputAndLimitLength(1);
                }
                else if (!File.Exists(theFileName))
                {
                    WriteAt("Account not found! Please enter the number again...", startX, startY + pageDepth + 1);
                    //Console.ReadKey();
                    WriteAt("Retry (y/n)? ", startX, startY + pageDepth + 2);
                    retryInput = getUserInput.GetInputAndLimitLength(1);
                }
                else if (File.Exists(theFileName))
                {
                    WriteAt("Account found! Details displayed below", startX, startY + pageDepth + 1);
                    retryInput = "y";
                    break;
                }

            } while (retryInput != "n");

            //if user enter 'n', it will go back to main menu and not run the following code
            if (retryInput == "y")
            {
                //display the form
                for (int depth = 0; depth < 12; depth++)
                {
                    if (depth == 0 | depth == 2 | depth == 11)
                    {
                        for (int width = 0; width < pageWidth; width++)
                        {
                            WriteAt("=", startX + width, startY + depth + pageDepth + 2);
                        }
                    }
                    else
                    {
                        WriteAt("|", startX, startY + depth + pageDepth + 2);
                        WriteAt("|", startX + pageWidth - 1, startY + depth + pageDepth + 2);
                    }
                }

                //display titles
                WriteAt("ACCOUNT DETAILS", startX + 10, startY + pageDepth + 2 + 1);

                //display account details
                accDetails = getAccDetails.SearchAccDetails(theFileName);
                for (int num = 0; num < 2; num++)
                {
                    if (num == 0)
                    {
                        actualContent = "Account No: " + accDetails[5, 1];
                        WriteAt(actualContent, startX + 5, startY + num + 4 + pageDepth + 2);
                    }
                    else if (num == 1)
                    {
                        actualContent = "Account Balance: $" + accDetails[6, 1];
                        WriteAt(actualContent, startX + 5, startY + num + 4 + pageDepth + 2);
                    }
                }
                for (int num = 0; num < 5; num++)
                {
                    actualContent = accDetails[num, 0] + ": " + accDetails[num, 1];
                    WriteAt(actualContent, startX + 5, startY + num + 6 + pageDepth + 2);
                }
                //confirm user input
                WriteAt("Delete (y/n)? ", startX, startY + pageDepth + 2 + 12);
                deleteInput = getUserInput.GetInputAndLimitLength(1);
                //delete the account and file if enter 'y'
                if (deleteInput == "y")
                {
                    File.Delete(theFileName);
                    WriteAt("Account Deleted!...", startX, startY + pageDepth + 2 + 13);

                }
                else
                {
                    WriteAt("Account not Deleted!...", startX, startY + pageDepth + 2 + 13);
                }
            }

        }
    }
}
