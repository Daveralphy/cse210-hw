using System;
using System.Threading;

public class Activity
{
    private string _name;
    private string _description;
    private int _duration;

    protected string Name => _name;
    protected string Description => _description;
    protected int Duration => _duration;

    protected static readonly Random _rand = new Random();

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
        _duration = 0;
    }

    public void DisplayStartingMessage()
    {
        Console.WriteLine($"Welcome to the {Name}.");
        Console.WriteLine(Description);
        Console.Write("Enter the duration in seconds: ");

        if (!int.TryParse(Console.ReadLine(), out int seconds) || seconds < 0)
        {
            seconds = 0;
        }
        _duration = seconds;

        Console.Clear();
        Console.WriteLine("Get ready...");
        ShowSpinner(3);
    }

    public void DisplayEndingMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!");
        ShowSpinner(2);
        Console.WriteLine($"You completed {Duration} seconds of the {Name}.");
        ShowSpinner(3);
        Console.Clear();
    }

    public void ShowSpinner(int seconds)
    {
        string[] frames = { "|", "/", "-", "\\" };
        DateTime end = DateTime.Now.AddSeconds(seconds);
        int i = 0;

        while (DateTime.Now < end)
        {
            Console.Write(frames[i]);
            Thread.Sleep(200);
            Console.Write("\b");
            i = (i + 1) % frames.Length;
        }
    }

    public void ShowCountDown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            string value = i.ToString();
            Console.Write(value);
            Thread.Sleep(1000);

            for (int k = 0; k < value.Length; k++)
            {
                Console.Write("\b \b");
            }
        }
    }
}