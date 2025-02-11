namespace Module_2
{
    class Module2
    {
        // Method OverLoading :- Same Function Name but differ in number of variables, return type or variable types
        static int Sum(int num1, int num2) { 
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
        static int CalculateArea(int length, int width)
        {
            int area = length * width;
            return area;
        }
        static void Main()
        {
            // --------------------> TASK 1 <--------------------

            // Q.1 :- Methods => Greeting Message – Create a method GreetUser() that takes a name as input and prints a greeting message.
            Console.Write("Enter your name : ");
            string name = Console.ReadLine() ?? "";
            GreetUser(name);

            // Q.2 :- Method Parameters -> Calculate Area of a Rectangle – Create a method CalculateArea(int length, int width) that takes two parameters and returns the area of a rectangle.
            Console.Write("Enter the length of the rectangle : ");
            int rectLength = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the width of the rectangle : ");
            int rectWidth = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Length : {rectLength} & Width : {rectWidth} => Area of the Rectangle : {CalculateArea(rectLength, rectWidth)}");

            /* Q.3 :- Method Overloading
                    a). Sum of Numbers – Create an overloaded method Sum() that calculates the sum of:
                    b). Two Integers
                    c). Three Integers
                    d). Two Double Values
             */
            int firstIntNum = 12;
            int secondIntNum = 13;
            int thirdIntNum = 14;
            double firstDoubleNum = 12.23;
            double secondDoubleNum = 98.234;

            int twoIntegerSum = Sum(firstIntNum, secondIntNum);
            int threeIntegerSum = Sum(firstIntNum, secondIntNum, thirdIntNum);
            double twoDouobleSum = Sum(firstDoubleNum, secondDoubleNum);

            Console.WriteLine("Two Integers Sum : {0}\nThree Integers Sum : {1}\nTwo Double Values Sum : {2}", twoIntegerSum, threeIntegerSum, twoDouobleSum);


        }
    }
}
