using System;

class Program
{
    static void Main(string[] args)
    {
        // Console.Write("What is the magic number? ");
        // int magicNumber = int.Parse(Console.ReadLine());
        
        Random randomGenerator = new Random();

        bool playAgain = true;
        while (playAgain)
        {
            int magicNumber = randomGenerator.Next(1, 101);
            int guess = -1;
            int guessCount = 0;

            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out guess))
                {
                    Console.WriteLine("Please enter a whole number.");
                    continue;
                }

                guessCount++;

                if (magicNumber > guess)
                {
                    Console.WriteLine("Higher");
                }
                else if (magicNumber < guess)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                }
            }

            Console.WriteLine($"You guessed the number in {guessCount} guesses.");

            while (true)
            {
                Console.Write("Do you want to play again? (yes/no) ");
                string answer = Console.ReadLine();
                string a = answer == null ? "" : answer.Trim().ToLower();

                if (a == "yes")
                {
                    playAgain = true;
                    break;
                }
                else if (a == "no")
                {
                    playAgain = false;
                    break;
                }

                // Anything else (including "y" or "n") will reprompt
                Console.WriteLine("Please type the full word 'yes' or 'no'.");
            }
        }
    }
}