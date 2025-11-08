// To exceed the requirements for this assignment, I implemented a DeleteEntry() method that allows removing multiple entries at once using index input.
// I also used a custom delimiter for saving and loading entries to avoid split errors with commas or text content.
// Lastly, I added better user experience elements like formatted dates, validation checks, and clear menu prompts.

using System;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nMenu Options:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Quit");
            Console.WriteLine("6. Delete an entry");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                string prompt = promptGenerator.GetRandomPrompt();
                Console.WriteLine(prompt);
                string response = Console.ReadLine();
                Entry entry = new Entry(prompt, response, DateTime.Now.ToString("dd/MM/yyyy"));
                journal.AddEntry(entry);
            }
            else if (choice == "2")
            {
                journal.DisplayAll();
            }
            else if (choice == "3")
            {
                Console.Write("Enter filename to save: ");
                string filename = Console.ReadLine();
                journal.SaveToFile(filename);
            }
            else if (choice == "4")
            {
                Console.Write("Enter filename to load: ");
                string filename = Console.ReadLine();
                journal.LoadFromFile(filename);
            }
            else if (choice == "5")
            {
                running = false;
            }
            else if (choice == "6")
            {
                journal.DisplayAll();
                Console.Write("Enter the numbers of the entries to delete (e.g. 1,2,3): ");
                string input = Console.ReadLine();
                journal.DeleteEntry(input);
}
            else
            {
                Console.WriteLine("Invalid option.");
            }
        }
    }
}