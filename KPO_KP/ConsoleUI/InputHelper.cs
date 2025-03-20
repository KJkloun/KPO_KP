using System;
using System.Globalization;

namespace ConsoleUI
{
    /// <summary>
    /// Вспомогательный класс для считывания и валидации пользовательского ввода.
    /// </summary>
    public static class InputHelper
    {
        /// <summary>
        /// Считывает целое число не меньше minValue.
        /// </summary>
        public static int ReadInt(string prompt, int minValue = 0)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out int result))
                {
                    if (result >= minValue)
                    {
                        return result;
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка: число не может быть меньше {minValue}.");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка: введите целое число.");
                }
            }
        }

        /// <summary>
        /// Считывает десятичное число не меньше minValue.
        /// </summary>
        public static decimal ReadDecimal(string prompt, decimal minValue = 0)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal result))
                {
                    if (result >= minValue)
                    {
                        return result;
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка: число не может быть меньше {minValue}.");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка: введите десятичное число.");
                }
            }
        }

        /// <summary>
        /// Считывает непустую строку.
        /// </summary>
        public static string ReadNonEmptyString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Ошибка: строка не может быть пустой.");
                }
            }
        }

        /// <summary>
        /// Считывает дату в формате yyyy-MM-dd.
        /// </summary>
        public static DateTime ReadDate(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime date))
                {
                    return date;
                }
                else
                {
                    Console.WriteLine("Ошибка: введите дату в формате yyyy-MM-dd.");
                }
            }
        }

        /// <summary>
        /// Считывает формат файла: 1=JSON, 2=CSV, 3=YAML.
        /// </summary>
        public static int ReadFileFormat(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                Console.WriteLine(" (1=JSON, 2=CSV, 3=YAML)");
                string input = Console.ReadLine();
                if (input == "1" || input == "2" || input == "3")
                {
                    return int.Parse(input);
                }
                else
                {
                    Console.WriteLine("Ошибка: введите 1, 2 или 3.");
                }
            }
        }
    }
}