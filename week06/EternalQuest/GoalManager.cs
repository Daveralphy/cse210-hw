using System;
using System.IO;
using System.Collections.Generic;

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    private int _level = 1;
    private int _xp = 0;
    private int _streak = 0;
    private DateTime _lastRecordDate = DateTime.MinValue;

    public void Start()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine($"Points: {_score} | Level: {_level} | XP: {_xp}/1000 | Streak: {_streak} days");
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");

            Console.Write("Select a choice: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1": CreateGoal(); break;
                case "2": ListGoalDetails(); break;
                case "3": SaveGoals(); break;
                case "4": LoadGoals(); break;
                case "5": RecordEvent(); break;
                case "6": return;
            }
        }
    }

    public void AutoLoad()
    {
        string file = "autosave.txt";

        if (!File.Exists(file))
            return;

        string[] lines = File.ReadAllLines(file);
        _goals.Clear();

        if (lines.Length < 5)
            return;

        _score = int.Parse(lines[0]);
        _level = int.Parse(lines[1]);
        _xp = int.Parse(lines[2]);
        _streak = int.Parse(lines[3]);

        if (string.IsNullOrWhiteSpace(lines[4]))
        {
            _lastRecordDate = DateTime.MinValue;
        }
        else
        {
            DateTime.TryParse(lines[4], null, System.Globalization.DateTimeStyles.RoundtripKind, out _lastRecordDate);
        }

        for (int i = 5; i < lines.Length; i++)
        {
            string[] p = lines[i].Split("|");
            string type = p[0];

            if (type == "SimpleGoal")
            {
                var g = new SimpleGoal(p[1], p[2], int.Parse(p[3]));
                if (bool.Parse(p[4]))
                    g.RecordEvent();
                _goals.Add(g);
            }
            else if (type == "EternalGoal")
            {
                _goals.Add(new EternalGoal(p[1], p[2], int.Parse(p[3])));
            }
            else if (type == "ChecklistGoal")
            {
                var g = new ChecklistGoal(
                    p[1], p[2], int.Parse(p[3]),
                    int.Parse(p[5]), int.Parse(p[6])
                );

                int completed = int.Parse(p[4]);
                for (int c = 0; c < completed; c++)
                    g.RecordEvent();

                _goals.Add(g);
            }
        }
    }

    public void AutoSave()
    {
        string file = "autosave.txt";

        List<string> lines = new List<string>();
        lines.Add(_score.ToString());
        lines.Add(_level.ToString());
        lines.Add(_xp.ToString());
        lines.Add(_streak.ToString());

        if (_lastRecordDate == DateTime.MinValue)
            lines.Add("");
        else
            lines.Add(_lastRecordDate.ToString("o"));

        foreach (Goal g in _goals)
            lines.Add(g.GetStringRepresentation());

        File.WriteAllLines(file, lines);
    }

    private void CreateGoal()
    {
        Console.WriteLine("Choose the type of goal:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Enter number: ");
        string choice = Console.ReadLine();

        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Description: ");
        string desc = Console.ReadLine();
        Console.Write("Points: ");
        int points = int.Parse(Console.ReadLine());

        if (choice == "1")
        {
            _goals.Add(new SimpleGoal(name, desc, points));
        }
        else if (choice == "2")
        {
            _goals.Add(new EternalGoal(name, desc, points));
        }
        else if (choice == "3")
        {
            Console.Write("Target number: ");
            int target = int.Parse(Console.ReadLine());
            Console.Write("Bonus points: ");
            int bonus = int.Parse(Console.ReadLine());
            _goals.Add(new ChecklistGoal(name, desc, points, target, bonus));
        }
    }

    private void ListGoalDetails()
    {
        Console.WriteLine("Your Goals:");
        int index = 1;
        foreach (var g in _goals)
        {
            Console.WriteLine($"{index}. {g.GetDetailsString()}");
            index++;
        }
    }

    private void RecordEvent()
    {
        Console.WriteLine("Which goal did you accomplish?");
        ListGoalDetails();
        Console.Write("Goal number: ");

        int i = int.Parse(Console.ReadLine()) - 1;

        if (i < 0 || i >= _goals.Count)
        {
            return;
        }

        int points = _goals[i].RecordEvent();
        _score += points;

        _xp += points;

        while (_xp >= 1000)
        {
            _level++;
            _xp -= 1000;
            Console.WriteLine($"*** Level Up! You are now Level {_level}! ***");
        }

        if (_lastRecordDate.Date == DateTime.Today.AddDays(-1))
        {
            _streak++;
        }
        else if (_lastRecordDate == DateTime.MinValue)
        {
            _streak = 1;
        }
        else
        {
            _streak = 1;
        }

        _lastRecordDate = DateTime.Today;

        Console.WriteLine($"You earned {points} points!");
    }

    private void SaveGoals()
    {
        Console.Write("File name? ");
        string file = Console.ReadLine();

        List<string> lines = new List<string>();
        lines.Add(_score.ToString());
        lines.Add(_level.ToString());
        lines.Add(_xp.ToString());
        lines.Add(_streak.ToString());

        if (_lastRecordDate == DateTime.MinValue)
            lines.Add("");
        else
            lines.Add(_lastRecordDate.ToString("o"));

        foreach (Goal g in _goals)
            lines.Add(g.GetStringRepresentation());

        File.WriteAllLines(file, lines);
    }

    private void LoadGoals()
    {
        while (true)
        {
            Console.Write("Enter file name to load (or type 'back' to return to menu): ");
            string file = Console.ReadLine();

            if (file.ToLower() == "back")
            {
                return;
            }

            if (!File.Exists(file))
            {
                Console.WriteLine("File not found. Try again or type 'back' to return.");
                Console.WriteLine();
                continue;
            }

            string[] lines = File.ReadAllLines(file);
            _goals.Clear();

            _score = int.Parse(lines[0]);
            _level = int.Parse(lines[1]);
            _xp = int.Parse(lines[2]);
            _streak = int.Parse(lines[3]);

            if (string.IsNullOrWhiteSpace(lines[4]))
            {
                _lastRecordDate = DateTime.MinValue;
            }
            else
            {
                DateTime.TryParse(lines[4], null, System.Globalization.DateTimeStyles.RoundtripKind, out _lastRecordDate);
            }

            for (int i = 5; i < lines.Length; i++)
            {
                string[] p = lines[i].Split("|");
                string type = p[0];

                if (type == "SimpleGoal")
                {
                    var g = new SimpleGoal(p[1], p[2], int.Parse(p[3]));
                    if (bool.Parse(p[4]))
                        g.RecordEvent();
                    _goals.Add(g);
                }
                else if (type == "EternalGoal")
                {
                    _goals.Add(new EternalGoal(p[1], p[2], int.Parse(p[3])));
                }
                else if (type == "ChecklistGoal")
                {
                    var g = new ChecklistGoal(
                        p[1], p[2], int.Parse(p[3]),
                        int.Parse(p[5]), int.Parse(p[6])
                    );

                    int completed = int.Parse(p[4]);
                    for (int c = 0; c < completed; c++)
                        g.RecordEvent();

                    _goals.Add(g);
                }
            }

            Console.WriteLine("Goals loaded successfully.");
            return;
        }
    }
}