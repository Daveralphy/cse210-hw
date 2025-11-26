/*
I exceeded the requirements in several ways in this project.
I added fully animated spinners and countdown timers instead of simple pauses.
I also implemented complete logic for all three activities instead of partial functionality.
I also made the random selection more polished by using a shared Random instance to avoid repetition.
I also added cleaner structure, better encapsulation, and improved user experience.
I did this to push the project beyond the minimum and make it feel more like a real mindfulness tool.
*/

using System;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Start Breathing Activity");
            Console.WriteLine("2. Start Reflecting Activity");
            Console.WriteLine("3. Start Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Select a choice: ");

            string input = Console.ReadLine();
            Console.Clear();

            if (input == "1")
            {
                BreathingActivity a = new BreathingActivity();
                a.Run();
            }
            else if (input == "2")
            {
                ReflectingActivity a = new ReflectingActivity();
                a.Run();
            }
            else if (input == "3")
            {
                ListingActivity a = new ListingActivity();
                a.Run();
            }
            else if (input == "4")
            {
                Console.WriteLine("Goodbye.");
                return;
            }
            else
            {
                Console.WriteLine("Invalid choice.");
                Console.WriteLine();
            }
        }
    }
}