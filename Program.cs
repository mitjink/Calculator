using System;
using System.Globalization;


namespace Program
{
    class Calculator
    {
        private static NumberFormatInfo numberFormat = new NumberFormatInfo()
        {
            NumberDecimalSeparator = "."
        };

        private double memory = 0;
        private double currentValue = 0;

        public void Start()
        {
            Console.WriteLine("Калькулятор запущен. Для завершения работы программы введите 'выход'. Введите выражение:");
            while (true)
            {
                string input = Console.ReadLine();
                if (input != null)
                {
                    input = input.Trim();
                    if (input.ToLower() == "выход")
                    {
                        break;
                    }
                    if (!string.IsNullOrEmpty(input))
                    {
                        ProcessInput(input);
                    }
                }
            }
        }

        private bool TryParseNumber(string str, out double result)
        {
            return double.TryParse(str, NumberStyles.Any, numberFormat, out result);
        }

        public static string ToPointString(double value)
        {
            return value.ToString(numberFormat);
        }

        private void ProcessInput(string input)
        {
            string[] parts = input.Split(' ');
            switch (parts.Length)
            {
                case 1:
                    ProcessSingleInput(parts[0]);
                    break;
                case 2:
                    ProcessingDoubleInput(parts[0], parts[1]);
                    break;
                case 3:
                    ProcessingTripleInput(parts[0], parts[1], parts[2]);
                    break;
                default:
                    Console.WriteLine("Ошибка: неверный формат ввода");
                    break;

            }
        }

        private void ProcessSingleInput(string input)
        {
            switch (input.ToUpper())
            {
                case "M+":
                    memory += currentValue;
                    Console.WriteLine($"Добавлено в память: {ToPointString(currentValue)}");
                    break;
                case "M-":
                    memory -= currentValue;
                    Console.WriteLine($"Вычтено из памяти: {ToPointString(currentValue)}");
                    break;
                case "MR":
                    currentValue = memory;
                    Console.WriteLine($"Загружено из памяти: {ToPointString(memory)}");
                    break;
                default:
                    Console.WriteLine("Ошибка: неизвестная операция");
                    break;
            }
        }
        private void ProcessingDoubleInput(string operation, string numberStr)
        {
            if (operation.ToLower() == "sqrt")
            {
                if (TryParseNumber(numberStr, out double number))
                {
                    if (number < 0)
                    {
                        Console.WriteLine("Ошибка: корень из отрицательного числа");
                        return;
                    }
                    currentValue = Math.Sqrt(number);
                    Console.WriteLine(currentValue);
                }
                else
                {
                    Console.WriteLine("Ошибка: неверное число");
                }
            }
            else
            {
                Console.WriteLine("Ошибка: неизвестная операция");
            }
        }

        private void ProcessingTripleInput(string firstNumber, string operation, string secondNumber)
        {
            if (!TryParseNumber(firstNumber, out double a) || !TryParseNumber(secondNumber, out double b))
            {
                Console.WriteLine("Ошибка: неверные числа");
                return;
            }

            switch (operation)
            {
                case "+":
                    currentValue = a + b;
                    break;
                case "-":
                    currentValue = a - b;
                    break;
                case "*":
                    currentValue = a * b;
                    break;
                case "/":
                    if (b == 0)
                    {
                        Console.WriteLine("Ошибка: деление на ноль");
                        return;
                    }
                    currentValue = a / b;
                    break;
                case "%":
                    if (b == 0)
                    {
                        Console.WriteLine("Ошибка: деление на ноль");
                        return;
                    }
                    currentValue = a % b;
                    break;
                case "^":
                    currentValue = Math.Pow(a, b);
                    break;
                default:
                    Console.WriteLine("Ошибка: неизвестная операция");
                    return;
            }

            Console.WriteLine($"{a} {operation} {b} = {ToPointString(currentValue)}");
        }
    }
    class Program
    {
        public static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            calculator.Start();
        }
    }
}