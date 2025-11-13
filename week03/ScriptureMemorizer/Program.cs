// To exceed this assignment's requirements, I added a feature that lets the user choose how many 
// words to hide each round and reveal the full scripture afterward, making it more interactive 
// and effective for memorization practice.

using System;

class Program
{
    static void Main(string[] args)
    {
        Reference reference = new Reference("Proverbs", 3, 5, 6);
        string text = "Trust in the Lord with all thine heart and lean not unto thine own understanding";
        Scripture scripture = new Scripture(reference, text);

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\nPress Enter to hide words, type a number to hide that many words, type 'reveal' to show the full scripture, or type 'quit' to end.");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit" || scripture.IsCompletelyHidden())
                break;

            if (input.ToLower() == "reveal")
            {
                Console.Clear();
                scripture.RevealAllWords();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine("\nPress Enter to end.");
                Console.ReadLine();
                break;
            }

            int count;
            if (int.TryParse(input, out count) && count > 0)
                scripture.HideRandomWords(count);
            else
                scripture.HideRandomWords(3);
        }
    }
}
