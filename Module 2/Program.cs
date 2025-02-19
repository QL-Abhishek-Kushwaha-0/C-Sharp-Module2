using System.Security.AccessControl;
using System.Security.Principal;

namespace Module_2
{
    class BankAccount
    {
        string accountHolderName;
        int accountHolderAge;
        readonly long accountNumber;  // To ensure that once a account is created it then cannot be manipulated
        decimal balance;

        //Constructor to initialise the Account with its 11 digit account number and balance as zero
        public BankAccount(string userName, int age)
        {
            accountHolderName = userName;
            accountHolderAge = age;
            Random random = new Random();
            accountNumber = random.NextInt64(10_000_000_000L, 99_999_999_999L);  // Will Create Account Number of 11 Digits
            SetBalance(0.0m);
            Console.WriteLine($"Hey {accountHolderName}, Your account has been created!!");
            //DisplayAccountDetails();
        }

        public BankAccount(long accountNumber, string userName, int userAge)
        {
            this.accountNumber = accountNumber;
            accountHolderName = userName;
            accountHolderAge = userAge;
            SetBalance(0.0m);
        }

        void SaveAccount(long accountNumber, string accountHolderName, int accountHolderAge, decimal balance)
        {
            string accountDetailsStoragePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../AccountsStorage/", accountNumber + ".txt");
            accountDetailsStoragePath = Path.GetFullPath(accountDetailsStoragePath);
            //Console.WriteLine(accountDetailsStoragePath);

            using (StreamWriter stream = new StreamWriter(accountDetailsStoragePath))
            {
                stream.WriteLine($"{accountNumber},{accountHolderName},{accountHolderAge},{balance}");
            }
        }

        protected void SetBalance(decimal initialDeposit)
        {
            balance = initialDeposit;
            SaveAccount(accountNumber, accountHolderName, accountHolderAge, balance);
            //DisplayAccountDetails();
        }

        protected decimal GetBalance()
        {
            return balance;
        }

        // Method to Deposit the Money in Account
        public void Deposit(decimal depositAmount)
        {
            if (depositAmount <= 0)
            {
                Console.WriteLine("Enter a Valid Amount to deposit!!");
            }
            else
            {
                balance += depositAmount;
                DisplayUpdatedBalance("Deposit");
            }
        }

        // Method to Withdraw the Money
        public virtual void Withdraw(decimal withdrawAmount)
        {
            if (withdrawAmount > balance)
            {
                Console.WriteLine("Insufficient Balance!!");
            }
            else
            {
                balance -= withdrawAmount;
                DisplayUpdatedBalance("Withdraw");
            }
        }

        // Method to Display the Current Balance after each transaction
        void DisplayUpdatedBalance(string transactionType)
        {
            Console.WriteLine($"{transactionType} Successfull!!! Your Updated Bank balance is Rs {balance}");
        }

        // Method to display the account Details
        public void DisplayAccountDetails()
        {
            Console.WriteLine("Your Account Number is : {0} & Current Bank Balance is : Rs {1}", accountNumber, balance);
        }
    }

    // --------------------> TASK 3 <--------------------

    class SavingsAccount : BankAccount
    {
        readonly int minSavingsAccountBalance = 500;
        readonly decimal interest = 3;
        public SavingsAccount(string userName, int age, decimal initialDeposit) : base(userName, age)
        {
            SetBalance(initialDeposit);
            DisplayAccountDetails();
        }

        public SavingsAccount(long accountNumber, string userName, int userAge, decimal existingBalance) : base(accountNumber, userName, userAge)
        {
            SetBalance(existingBalance);
        }

        public override void Withdraw(decimal withdrawAmount)
        {
            decimal existingBalance = GetBalance();
            if (existingBalance - withdrawAmount < minSavingsAccountBalance)
            {

                Console.Write($"Minimum Balance of {minSavingsAccountBalance} needs to be Maintained!!! Choose a lesses amount : ");
                while (!decimal.TryParse(Console.ReadLine(), out withdrawAmount) || (existingBalance - withdrawAmount < minSavingsAccountBalance))
                {
                    Console.Write("Enter a Valid Amount (Maintaining Minimum Balance (Rs 500): ");
                }
            }
            SetBalance(existingBalance - withdrawAmount);
            Console.WriteLine($"Your Withdraw is Successfull!!! Your remaining Bank Balance is {GetBalance()}");
        }
        public void InterestCalculation()
        {
            decimal currentBalance = GetBalance();
            decimal applicableInterest = (GetBalance() * interest) / 100;
            SetBalance(applicableInterest + currentBalance);
            currentBalance = GetBalance();
            Console.WriteLine($"Your Bank Balance after adding Applicable Interest is {currentBalance}");
        }
    }

    class CurrentAccount : BankAccount
    {
        readonly int minCurrentAccountBalance = 1000;

        public CurrentAccount(string userName, int age, decimal initialDeposit) : base(userName, age)
        {
            SetBalance(initialDeposit);
            DisplayAccountDetails();
        }

        public CurrentAccount(long accountNumber, string userName, int userAge, decimal existingBalance) : base(accountNumber, userName, userAge)
        {
            SetBalance(existingBalance);
        }

        public override void Withdraw(decimal withdrawAmount)
        {
            decimal existingBalance = GetBalance();
            if (existingBalance - withdrawAmount < minCurrentAccountBalance)
            {

                Console.Write($"Minimum Balance of {minCurrentAccountBalance} needs to be Maintained!!! Choose a lesses amount : ");
                while (!decimal.TryParse(Console.ReadLine(), out withdrawAmount) || (existingBalance - withdrawAmount < minCurrentAccountBalance))
                {
                    Console.Write("Enter a Valid Amount (Maintaining Minimum Balance (Rs 1000): ");
                }
            }
            SetBalance(existingBalance - withdrawAmount);
            Console.WriteLine($"Your Withdraw is Successfull!!! Your remaining Bank Balance is {GetBalance()}");
        }
    }
    class Module2
    {
        // Method OverLoading :- Same Function Name but differ in number of variables, return type or variable types
        static int Sum(int num1, int num2)
        {
            return num1 + num2;
        }
        static int Sum(int num1, int num2, int num3)
        {
            return num1 + num2 + num3;
        }
        static double Sum(double num1, double num2)
        {
            return num1 + num2;
        }

        // Method to print Greet Message
        static void GreetUser(string name)
        {
            Console.WriteLine($"Hello {name}");
        }

        // Method to calculate area of Rectangle
        static double CalculateArea(double length, double width)
        {
            return length * width;
        }

        // --------------------> TASK 1 <--------------------
        static void Task1()
        {
            // Q.1 :- Methods => Greeting Message – Create a method GreetUser() that takes a name as input and prints a greeting message.
            Console.WriteLine("\nGreet Function\n");
            Console.Write("Enter your name : ");
            string userName = Console.ReadLine() ?? "";
            GreetUser($"{userName}, Have a Good Day!!!");

            Console.WriteLine();

            // Q.2 :- Method Parameters -> Calculate Area of a Rectangle – Create a method CalculateArea(int length, int width) that takes two parameters and returns the area of a rectangle.
            Console.WriteLine("Function for Area of Rectangle\n");
            Console.Write("Enter the length of the rectangle : ");
            double rectLength = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter the width of the rectangle : ");
            double rectWidth = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine($"Length : {rectLength} & Width : {rectWidth} => Area of the Rectangle : {CalculateArea(rectLength, rectWidth)}");

            Console.WriteLine();

            /* Q.3 :- Method Overloading
                    a). Sum of Numbers – Create 9an overloaded method Sum() that calculates the sum of:
                    b). Two Integers
                    c). Three Integers
                    d). Two Double Values
             */
            Console.WriteLine("Method Overloading\n");
            int firstIntNum = 12;
            int secondIntNum = 13;
            int thirdIntNum = 14;
            double firstDoubleNum = 12.23;
            double secondDoubleNum = 98.234;

            int twoIntegerSum = Sum(firstIntNum, secondIntNum);
            int threeIntegerSum = Sum(firstIntNum, secondIntNum, thirdIntNum);
            double twoDouobleSum = Sum(firstDoubleNum, secondDoubleNum);

            Console.WriteLine("Two Integers Sum : {0}\nThree Integers Sum : {1}\nTwo Double Values Sum : {2}", twoIntegerSum, threeIntegerSum, twoDouobleSum);
            Console.WriteLine();
        }

        // --------------------> TASK 2 <--------------------
        static void BankingOperations(BankAccount myAccount)
        {
            Console.WriteLine();
            while (true)
            {
                Console.Write("Choose from the following type of Transactions : \n1. Deposit\n2. Withdraw\n3. Show Balance\n4. Balance after Applying Interest\nEnter the choice of transaction : ");

                // Options available for transactions
                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice > 4 || choice <= 0)
                {
                    Console.Write("\nEnter a valid Choice : ");
                }

                if (choice == 3)
                {
                    myAccount.DisplayAccountDetails();
                    Console.WriteLine();
                }
                else if (choice == 4)
                {
                    Console.WriteLine();
                    if (myAccount is SavingsAccount savingAccount)
                    {
                        savingAccount.InterestCalculation();
                    }
                    else
                    {
                        Console.WriteLine($"Interest not appplicable on {myAccount.GetType().Name} type of account!!!");
                    }
                }
                else
                {
                    Console.Write((choice == 1) ? "\nEnter the Amount to be deposited : " : "\nEnter the Amount to be withdrawn : ");
                    decimal transactionAmount;
                    while (!decimal.TryParse(Console.ReadLine(), out transactionAmount) || transactionAmount <= 0)
                    {
                        Console.Write("\nEnter a Valid Amount to continue with the transaction!!! : ");
                        continue;
                    }
                    if (choice == 1) { myAccount.Deposit(transactionAmount); }
                    else { myAccount.Withdraw(transactionAmount); }
                }

                Console.Write("\nEnter 1 to continue with another transaction : ");

                if (Console.ReadLine() != "1")
                {
                    Console.WriteLine("Transaaction Ends!!!!");
                    break;
                }
            }
        }

        static void ShowAllAccounts()
        {
            Console.WriteLine();
            string filePath = @"D:\Quokka Labs\Module 2\Module 2\bin\AccountsStorage";

            string[] accountPaths = Directory.GetFiles(filePath); // Gives the path of each file
            string[] accounts = Directory.GetFiles(filePath, "*.txt", SearchOption.AllDirectories);

            Console.WriteLine("Accounts associated with this Bank : ");
            int idx = 1;
            Console.WriteLine($"{"S.No.",-5} {"Name",-25} {"Account Number",-20} {"Age",-5}");
            Console.WriteLine(new string('-', 60));

            foreach (string account in accounts)
            {
                using (StreamReader stream = new StreamReader(account))
                {
                    string accountDetails = stream.ReadToEnd();
                    string[] props = accountDetails.Split(',');

                    Console.WriteLine($"{idx++,-5} {props[1],-25} {props[0],-20} {props[2],-5}");
                }
            }

        }

        static void Bank()
        {
            Console.WriteLine();
            while (true)
            {
                Console.Write("1. Create Bank Account\n2. Create Current or Savings Account\n3. Create or Login Current and Savings Account\n4. List all the Accounts\nChoose the Task to perform or (Press 5 to Exit) : ");
                if (!int.TryParse(Console.ReadLine(), out int taskChoice) || (taskChoice <= 0 && taskChoice > 5))
                {
                    Console.WriteLine("Enter a Valid Choice");
                }
                if(taskChoice == 5)
                {
                    Console.WriteLine("Exited!!!");
                    break;
                }
                switch (taskChoice)
                {
                    case 1:
                        Task2();
                        break;
                    case 2:
                        Task3(2);
                        break;
                    case 3:
                        Task3(3);
                        break;
                    case 4:
                        ShowAllAccounts();
                        break;
                    default:
                        Console.WriteLine("Wrong choice of Task!!");
                        break;
                }
                Console.Write("\nDo You wish to Continue to another Bank Operation?? Enter 1 for Yes : ");
                if (Console.ReadLine() != "1")
                {
                    Console.WriteLine("Exited!!!");
                    break;
                }
                Console.WriteLine();
            }
        }

        static void CurrentAccount(string userName, int userAge)
        {
            decimal initialDeposit;
            Console.Write("Enter the Amount to deposit : ");
            while (!decimal.TryParse(Console.ReadLine(), out initialDeposit) || initialDeposit < 1000)
            {
                Console.WriteLine("To Create and Maintain a Current account a Minimum Balance of 1000 Should be maintained!!!");
                Console.Write("Enter a valid amount to continue : ");
            }
            Console.WriteLine();
            CurrentAccount newCurrentAccount = new CurrentAccount(userName, userAge, initialDeposit);
            BankingOperations(newCurrentAccount);
        }

        static void SavingsAccount(string userName, int userAge)
        {
            decimal initialDeposit;
            Console.Write("Enter the Amount to deposit : ");
            while (!decimal.TryParse(Console.ReadLine(), out initialDeposit) || initialDeposit < 500)
            {
                Console.WriteLine("To Create and Maintain a Savings account a Minimum Balance of 500 Should be maintained!!!");
                Console.Write("Enter a valid amount to continue : ");
            }
            Console.WriteLine();
            SavingsAccount newSavingAccount = new SavingsAccount(userName, userAge, initialDeposit);
            BankingOperations(newSavingAccount);
        }

        static void NewAccount(int choice)
        {
            Console.WriteLine();

            Console.Write("Enter your Name : ");
            string userName = Console.ReadLine() ?? "";

            while (string.IsNullOrEmpty(userName) || userName.Length < 3)
            {
                Console.Write("Enter a Valid Name (Name should contain at least 3 letters) !! : ");
                userName = Console.ReadLine();
            }

            Console.Write("Enter your Age : ");
            int userAge;

            while (!int.TryParse(Console.ReadLine(), out userAge) || userAge < 18)
            {
                Console.Write("Enter Valid Age (Age >= 18): ");
            }


            if (userName.Length >= 3 && userAge >= 18)
            {
                switch (choice)
                {
                    case 0:
                        BankAccount myAccount = new BankAccount(userName, userAge);
                        myAccount.DisplayAccountDetails();
                        BankingOperations(myAccount);
                        break;
                    case 1:
                        CurrentAccount(userName, userAge);
                        break;
                    case 2:
                        SavingsAccount(userName, userAge);
                        break;
                    default:
                        Console.WriteLine("Choose a Valid Account type : ");
                        break;
                }
            }
            Console.WriteLine();
        }
        static void Task2()
        {
            while (true)
            {
                Console.Write("\nWelcome to ABC Bank!!\n1. New Account\n2. Existing Account\nChoose your Account Type or (Press 3 to Exit...) : ");
                if (!int.TryParse(Console.ReadLine(), out int choice) || (choice != 1 && choice != 2 && choice != 3))
                {
                    Console.WriteLine("Enter a Valid Choice");
                }
                if (choice == 3)
                {
                    Console.WriteLine("Thank you for Visiting the Bank!!!");
                    break;
                }
                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        NewAccount(0);
                        break;
                    case 2:
                        Console.WriteLine("You don't have any existing Account!!");
                        break;
                    default:
                        Console.WriteLine("Choose a Valid Account type!!!");
                        break;
                }

                Console.Write("\nEnter 1 to continue with another account : ");

                if (Console.ReadLine() != "1")
                {
                    Console.WriteLine("Thanks for visiting us!!!!");
                    break;
                }
            }
        }

        static void ExistingAccount()
        {
            Console.WriteLine();
            Console.Write("1. Current Account\n2. Savings Account\nChoose your Existing Account type : ");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
            {
                Console.WriteLine("Wrong Choice!!!");
                Console.Write("Choose your Account type (1 or 2) or (3 to Exit..) : ");
            }

            Console.WriteLine();
            Console.Write("Enter your Account Number : ");
            long accountNumber;
            while (!long.TryParse(Console.ReadLine(), out accountNumber) || (accountNumber.ToString().Length != 11))
            {
                Console.WriteLine("Invalid Account Number!!");
                Console.WriteLine("Account Number should be Integer and of 11 Digits!!!");
                Console.Write("Enter the Account Number Again : ");
            }

            string accountDetailsStoragePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../AccountsStorage/", accountNumber + ".txt");
            accountDetailsStoragePath = Path.GetFullPath(accountDetailsStoragePath);

            if (choice == 1)
            {
                CurrentAccount myExistingAccount;

                if (File.Exists(accountDetailsStoragePath))
                {
                    using (StreamReader stream = new StreamReader(accountDetailsStoragePath))
                    {
                        string accountDetails = stream.ReadToEnd();

                        stream.Close();

                        string[] props = accountDetails.Split(',');

                        //string accountType = props[4];
                        long currentAccountNumber = Convert.ToInt64(props[0]);
                        string currentAccountHolderName = props[1];
                        int currentAccountHolderAge = Convert.ToInt32(props[2]);
                        decimal currentBankBalance = Convert.ToDecimal(props[3]);

                        myExistingAccount = new CurrentAccount(currentAccountNumber, currentAccountHolderName, currentAccountHolderAge, currentBankBalance);

                        Console.WriteLine("\nWelcome Back!!!");

                        BankingOperations(myExistingAccount);
                    }
                }
                else
                {
                    Console.WriteLine("This Account Number is not assosicated with our Bank!!!");
                }
            }
            else
            {
                SavingsAccount myExistingAccount;

                if (File.Exists(accountDetailsStoragePath))
                {
                    using (StreamReader stream = new StreamReader(accountDetailsStoragePath))
                    {
                        string accountDetails = stream.ReadToEnd();
                        string[] props = accountDetails.Split(',');

                        long currentAccountNumber = Convert.ToInt64(props[0]);
                        string currentAccountHolderName = props[1];
                        int currentAccountHolderAge = Convert.ToInt32(props[2]);
                        decimal currentBankBalance = Convert.ToDecimal(props[3]);

                        stream.Close();

                        myExistingAccount = new SavingsAccount(currentAccountNumber, currentAccountHolderName, currentAccountHolderAge, currentBankBalance);

                        Console.WriteLine("\nWelcome Back!!!");

                        BankingOperations(myExistingAccount);
                    }
                }
                else
                {
                    Console.WriteLine("This Account Number is not assosicated with our Bank!!!");
                }
            }

        }

        static void Task3(int taskChoice)
        {
            int choice;
            while (true)
            {
                Console.Write("\nWelcome to ABC Bank!!\n1. New Account\n2. Existing Account\nChoose your Account Type or (Press 3 to exit... : ");
                if (!int.TryParse(Console.ReadLine(), out choice) && (choice != 1 || choice != 2 || choice != 3))
                {
                    Console.WriteLine("Enter a Valid Choice");
                }
                if(choice == 3)
                {
                    Console.WriteLine("Thank You for visiting the Bank!!!");
                    break;
                }
                Console.WriteLine();
                switch (choice)
                {
                    case 1:
                        Console.Write("Choose the type of Account you want to open -: \n1. Current Account\n2. Savings Account\nEnter your Choice of Account (1 or 2) : ");
                        while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
                        {
                            Console.Write("Enter a Valid Choice : ");
                        }
                        NewAccount(choice);
                        break;
                    case 2:
                        if (taskChoice == 2)
                        {
                            Console.WriteLine("You cannot use an Existing Account!!");
                        }
                        else
                        {
                            ExistingAccount();
                        }
                        break;
                    default:
                        Console.WriteLine("Choose a Valid Account type!!!");
                        break;
                }

                Console.Write("Enter 1 to continue with another account : ");

                if (Console.ReadLine() != "1")
                {
                    Console.WriteLine("Thanks for visiting us!!!!");
                    break;
                }
            }
        }
        static void Main()
        {
            while (true)
            {
                Console.Write("1. Basic Functionalities of OOPS\n2. Enter the Bank\n\nChoose the Task to perform or (Press 3 to exit...) : ");
                if (!int.TryParse(Console.ReadLine(), out int taskChoice) || (taskChoice != 1 && taskChoice != 2 && taskChoice != 3))
                {
                    Console.WriteLine("Enter a Valid Choice");
                }
                if (taskChoice == 3)
                {
                    Console.WriteLine("Exited!!!");
                    break;
                }
                switch (taskChoice)
                {
                    case 1:
                        Task1();
                        break;
                    case 2:
                        Bank();
                        break;
                    default:
                        Console.WriteLine("Wrong choice of Task!!");
                        break;
                }
                Console.Write("\nDo You wish to Continue to another Task?? Enter 1 for Yes : ");
                if (Console.ReadLine() != "1")
                {
                    Console.WriteLine("Exited!!!");
                    break;
                }
                Console.WriteLine();
            }

        }
    }
}
