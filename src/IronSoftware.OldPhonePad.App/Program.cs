using System;
using IronSoftware.OldPhonePad;

namespace IronSoftware.OldPhonePad.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Old Phone Pad Challenge - Interactive Mode");
            Console.WriteLine("Type a sequence of numbers ending with '#' to see the output.");
            Console.WriteLine("Type 'exit' to quit.");
            Console.WriteLine("---------------------------------------------------------");

            while (true)
            {
                Console.Write("Input: ");
                string? input = Console.ReadLine();

                if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                try
                {
                    // Ensure input is not null for the library call, although basic validation handles nulls
                    string result = PhonePad.OldPhonePad(input ?? string.Empty);
                    Console.WriteLine($"Output: {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine();
            }
        }
    }
}
