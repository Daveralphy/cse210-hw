using System;

class Program
{
    static void Main(string[] args)
    {
        int percent;
        while (true)
        {
            Console.Write("What is your grade percentage? ");
            string answer = Console.ReadLine();

            if (int.TryParse(answer, out percent))
            {
                break;
            }

            Console.WriteLine("Invalid input. Please enter a whole number (for example: 87). Try again.");
        }

        percent = Math.Clamp(percent, 0, 100);

        string letter = "";

        if (percent >= 90)
        {
            letter = "A";
        }
        else if (percent >= 80)
        {
            letter = "B";
        }
        else if (percent >= 70)
        {
            letter = "C";
        }
        else if (percent >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        string modifier = "";
        if (letter != "F")
        {
            int lastDigit = percent % 10;

            if (percent == 100)
            {
                modifier = "";
            }
            else if (letter == "A")
            {
                if (lastDigit < 3)
                {
                    modifier = "-";
                }
            }
            else
            {
                if (lastDigit >= 7)
                {
                    modifier = "+";
                }
                else if (lastDigit <= 3)
                {
                    modifier = "-";
                }
            }
        }

        Console.WriteLine($"Your grade is: {letter}{modifier}");

        if (percent >= 70)
        {
            Console.WriteLine("You passed!");
        }
        else
        {
            Console.WriteLine("Better luck next time!");
        }
    }
}