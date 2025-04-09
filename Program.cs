using System.Globalization;

namespace Overload_try_except;

class Program
{
    //Create a Calculator class    
    public class Calculator
    {
        //Since we aren't using any custom logic in get/set we'll use C#'s auto-implementation        
        public string? Number1 { get; set; }
        public string? Number2 { get; set; }
        public string? Number3 { get; set; }

        //Create method's with the same name but a different amount or type of parameters == method overload.
        //But if the int version of Add only had two integer parameters it would have been skipped
        //since C# implicitly converts int to decimal types and so would have used the double version
        public int Add(int a, int b, int c)
        {
            return a + b + c;
        }

        public double Add(double a, double b)
        {
            return a + b;
        }
    }//End of class Calculator    

    //Method needs to be static so we can access it in Main
    static private void ReadInput()
    {
        bool programRunning = true;

        //Instantiate our Calc object.
        //We could do this at a higher level (Program class) but it's not really
        //necessary since we only use it in the ReadInput method
        Calculator calc = new Calculator();

        //Fetch local decimal separator for numbers
        string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        int num1;
        int num2;
        int num3;
        double decNum1;
        double decNum2;
        const string decimalMessage = "is a whole number, decimal expected";
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


        //The while loop will continue until the user chooses option 3 (exit) since the loop depends on
        //programRunning being true.
        while (programRunning)
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Add three integer numbers");
            Console.WriteLine($"2. Add two decimal numbers (decimal separator is: '{decimalSeparator}')");
            Console.WriteLine("3. Exit");
            string? input = Console.ReadLine();

            //Choose case based on the main menu choices the user types in (1,2,3)
            switch (input)
            {
                case "1":
                    Console.WriteLine("First number:");
                    calc.Number1 = Console.ReadLine();
                    Console.WriteLine("Second number:");
                    calc.Number2 = Console.ReadLine();
                    Console.WriteLine("Third number:");
                    calc.Number3 = Console.ReadLine();

                    try
                    {
                        //Convert strings to integer in a try/catch block.
                        //Will throw Exception if conversion fails
                        num1 = Convert.ToInt32(calc.Number1);
                        num2 = Convert.ToInt32(calc.Number2);
                        num3 = Convert.ToInt32(calc.Number3);

                        //Display a special message if the sum is the answer to Life, the Universe & Everything :-)
                        if (calc.Add(num1, num2, num3) == dontPanic)
                            Console.WriteLine(dontPanicMessage);
                        else
                            //Display the sum                            
                            Console.WriteLine($"The sum is: {calc.Add(num1, num2, num3)}\n");
                    }
                    catch (Exception error)
                    {
                        //Show Exception error message
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
                        //Convert strings to decimal in a try/catch block.
                        //Will throw Exception if conversion fails.
                        //Since C# implicitly converts int to decimal types, 
                        //pure integer's like 5 will convert as well 
                        decNum1 = Convert.ToDouble(calc.Number1);
                        decNum2 = Convert.ToDouble(calc.Number2);

                        //But we will check if userinput does not contain a dot, i.e. it's not a decimal type
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

                        //Display a special message if the sum is the answer to Life, the Universe & Everything :-)
                        if (calc.Add(decNum1, decNum2) == dontPanic)
                            Console.WriteLine(dontPanicMessage);
                        else
                            //Display the sum
                            Console.WriteLine($"The sum is: {calc.Add(decNum1, decNum2)}\n");
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine($"{error.Message}\n");
                    }
                    break;

                case "3":
                    //If user chooses 3, set programRunning to false ensuring that the while loop
                    //will exit
                    programRunning = false;
                    break;

                default:
                    //Inform user that he must choose 1,2 or 3                    
                    Console.WriteLine("Please choose '1','2' or '3'.\n");
                    break;
            }
        }
    }

    static void Main(string[] args)
    {
        //Call method
        ReadInput();

    }//End of Main
}

