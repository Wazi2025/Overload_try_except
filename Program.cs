using System.Globalization;

namespace Overload_try_except;

class Program
{
    //create a Calculator class    
    public class Calculator
    {
        //since we aren't using any custom logic in get/set we'll use C#'s auto-implementation
        public string? Number1 { get; set; }
        public string? Number2 { get; set; }

        //create method's with the same name but a different amount or type of parameters == method overload
        public int Add(int a, int b)
        {
            return a + b;
        }

        public double Add(double a, double b)
        {
            return a + b;
        }

        public bool IsANumber(string? value)
        {
            //note: not in use at the moment
            //
            //check if string can be converted to number                
            if (int.TryParse(value, out int number) || decimal.TryParse(value, out decimal numberDec))
                return true;
            else
                return false;
        }
    }//end of class Calculator

    //instantiate our calc object at the Program class level since we will need to access it
    //in the ReadInput method
    static Calculator calc = new Calculator();

    //method needs to be static so we can access our calc object
    static private void ReadInput()
    {
        bool programRunning = true;

        //fetch local decimal separator for numbers
        string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        int num1 = 0;
        int num2 = 0;
        double decNum1;
        double decNum2;
        const string decimalMessage = "is a whole number. Decimal expected";
        const int dontPanic = 42;
        const string dontPanicMessage = @"
      ____                     __   __        ____                               
     /\  _`\                  /\ \ /\ \__    /\  _`\                __           
     \ \ \/\ \    ___     ___ \ \/ \ \ ,_\   \ \ \L\ \ __      ___ /\_\    ___   
      \ \ \ \ \  / __`\ /' _ `\\/   \ \ \/    \ \ ,__/'__`\  /' _ `\/\ \  /'___\ 
       \ \ \_\ \/\ \L\ \/\ \/\ \     \ \ \_    \ \ \/\ \L\.\_/\ \/\ \ \ \/\ \__/ 
        \ \____/\ \____/\ \_\ \_\     \ \__\    \ \_\ \__/.\_\ \_\ \_\ \_\ \____\
         \/___/  \/___/  \/_/\/_/      \/__/     \/_/\/__/\/_/\/_/\/_/\/_/\/____/
                                                                                 ";


        //the while loop will continue until the user chooses option 3 (exit) since the loop depends on
        //programRunning being true.
        while (programRunning)
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Add two integer numbers");
            Console.WriteLine($"2. Add two decimal numbers (decimal separator is: '{decimalSeparator}')");
            Console.WriteLine("3. Exit");
            string? input = Console.ReadLine();

            //choose case based on the main menu choices the user types in (1,2,3)
            switch (input)
            {
                case "1":
                    Console.WriteLine("First number:");
                    calc.Number1 = Console.ReadLine();
                    Console.WriteLine("Second number:");
                    calc.Number2 = Console.ReadLine();

                    try
                    {
                        //convert strings to integer in a try/catch block
                        //will throw Exception if conversion fails
                        num1 = Convert.ToInt32(calc.Number1);
                        num2 = Convert.ToInt32(calc.Number2);

                        //display a special message if the sum is the answer to Life, the Universe & Everything :-)
                        if (calc.Add(num1, num2) == dontPanic)
                            Console.WriteLine(dontPanicMessage);
                        else
                            //display the sum
                            Console.WriteLine($"The sum is: {calc.Add(num1, num2)}\n");
                    }
                    catch (Exception error)
                    {
                        //show Exception error message
                        Console.WriteLine($"{error.Message}\n");
                    }
                    break;

                case "2":
                    Console.WriteLine("First decimal number:");
                    calc.Number1 = Console.ReadLine();
                    Console.WriteLine("Second decimal number:");
                    calc.Number2 = Console.ReadLine();

                    try
                    {
                        //convert strings to decimal in a try/catch block
                        //will throw Exception if conversion fails
                        //since C# implicitly converts int to decimal types, 
                        //pure int's like 5 will convert as well 
                        decNum1 = Convert.ToDouble(calc.Number1);
                        decNum2 = Convert.ToDouble(calc.Number2);

                        //but we will check if userinput does not contain a dot, i.e. it's a decimal type
                        //and inform user that a decimal is expected
                        if (!calc.Number1.Contains(decimalSeparator) && !calc.Number2.Contains(decimalSeparator))
                        {
                            Console.WriteLine($"'{calc.Number1}' {decimalMessage}.");
                            Console.WriteLine($"'{calc.Number2}' {decimalMessage}.\n");
                        }
                        else if (!calc.Number1.Contains(decimalSeparator))
                        {
                            Console.WriteLine($"'{calc.Number1}' {decimalMessage}.\n");
                        }
                        else if (!calc.Number2.Contains(decimalSeparator))
                        {
                            Console.WriteLine($"'{calc.Number2}' {decimalMessage}.\n");
                        }

                        //display a special message if the sum is the answer to Life, the Universe & Everything :-)
                        if (calc.Add(decNum1, decNum2) == dontPanic)
                            Console.WriteLine(dontPanicMessage);
                        else
                            //display the sum
                            Console.WriteLine($"The sum is: {calc.Add(decNum1, decNum2)}\n");
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine($"{error.Message}\n");
                    }
                    break;

                case "3":
                    //if user chooses 3, set programRunning to false ensuring that the while loop
                    //will exit
                    programRunning = false;
                    break;

                default:
                    //inform user that he must choose 1,2 or 3                    
                    Console.WriteLine("Please choose '1','2' or '3'.\n");
                    break;
            }
        }
    }

    static void Main(string[] args)
    {
        //call method
        ReadInput();

    }//end of Main
}

