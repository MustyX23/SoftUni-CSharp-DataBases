double number1 = int.Parse(Console.ReadLine());
double number2 = int.Parse(Console.ReadLine());  
string operation = Console.ReadLine();
double calculation = 0;
 
if (operation == "/")
{

    if (number1 == 0)
    {
        Console.WriteLine($"Cannot divide {number2} by zero");
        return;
    }
    if (number2 == 0) {
        Console.WriteLine($"Cannot divide {number1} by zero");
        return;
    }
    calculation = number1 / number2;
    Console.WriteLine($"{number1} / {number2} = {calculation:f2}");
}
else if (operation == "%")
{
    if (number1 == 0)
    {
        Console.WriteLine($"Cannot divide {number2} by zero");
        return;
    }
    if (number2 == 0)
    { 
        Console.WriteLine($"Cannot divide {number1} by zero");
        return;
    }
    calculation = number1 % number2;
    Console.WriteLine($"{number1} % {number2} = {calculation}");

}
else if (operation == "+")
{
    calculation = number1 + number2;
    if (calculation % 2 == 0)
    {
        Console.WriteLine($"{number1} + {number2} = {calculation} - even");
        return;
    }
    else
    {
        Console.WriteLine($"{number1} + {number2} = {calculation} - odd");
        return;
    }
}
else if (operation == "*")
{
    calculation = number1 * number2;
    if (calculation % 2 == 0)
    {
        Console.WriteLine($"{number1} * {number2} = {calculation} - even");
        return;
    }
    else
    {
        Console.WriteLine($"{number1} * {number2} = {calculation} - odd");
        return;
    }
}
else if (operation == "-")
{
    calculation = number1 - number2;
    if (calculation % 2 == 0)
    {
        Console.WriteLine($"{number1} - {number2} = {calculation} - even");
        return;
    }
    else
    {
        Console.WriteLine($"{number1} - {number2} = {calculation} - odd");
        return;
    }
}
