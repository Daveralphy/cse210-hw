using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    private int _level = 1;
    private int _xp = 0;
    private int _streak = 0;
    private DateTime _lastRecordDate = DateTime.MinValue;

    public void AutoLoad()
    {
        string file = "autosave.csv";

        if (!File.Exists(file))
            return;

        var lines = File.ReadAllLines(file);
        bool headerFound = lines[0].StartsWith("type,name");
        int start = headerFound ? 1 : 0;

        _goals.Clear();

        for (int i = start; i < lines.Length; i++)
        {
            var row = lines[i].Split(',');

            string type = row[0];
            string name = row[1];
            string desc = row[2];
            int points = int.Parse(row[3]);

            string date = row[4];
            if (!string.IsNullOrWhiteSpace(date))
                DateTime.TryParse(date, out _lastRecordDate);

            if (type == "SimpleGoal")
            {
                bool complete = bool.Parse(row[5]);
                var g = new SimpleGoal(name, desc, points);
                if (complete) g.RecordEvent();
                _goals.Add(g);
            }
            else if (type == "EternalGoal")
            {
                _goals.Add(new EternalGoal(name, desc, points));
            }
            else if (type == "ChecklistGoal")
            {
                int completed = int.Parse(row[6]);
                int target = int.Parse(row[7]);
                int bonus = int.Parse(row[8]);

                var g = new ChecklistGoal(name, desc, points, target, bonus);
                for (int c = 0; c < completed; c++)
                    g.RecordEvent();

                _goals.Add(g);
            }
        }
    }

    public void AutoSave()
    {
        string file = "autosave.csv";

        List<string> lines = new List<string>();
        lines.Add("type,name,description,points,date,complete,amountCompleted,target,bonus");

        string dateString = _lastRecordDate == DateTime.MinValue
            ? ""
            : _lastRecordDate.ToString("yyyy-MM-dd");

        foreach (Goal g in _goals)
        {
            if (g is SimpleGoal sg)
            {
                lines.Add($"SimpleGoal,{sg.GetName()},{sg.GetDescription()},{sg.GetPoints()},{dateString},{sg.IsComplete()},,,");
            }
            else if (g is EternalGoal eg)
            {
                lines.Add($"EternalGoal,{eg.GetName()},{eg.GetDescription()},{eg.GetPoints()},{dateString},,,,");
            }
            else if (g is ChecklistGoal cg)
            {
                var rep = cg.GetStringRepresentation().Split("|");
                int completed = int.Parse(rep[4]);
                int target = int.Parse(rep[5]);
                int bonus = int.Parse(rep[6]);
                lines.Add($"ChecklistGoal,{cg.GetName()},{cg.GetDescription()},{cg.GetPoints()},{dateString},,{completed},{target},{bonus}");
            }
        }

        File.WriteAllLines(file, lines);
    }

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
            return;

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

        bool exists = File.Exists(file);

        if (exists)
        {
            Console.WriteLine("This file already exists.");
            Console.Write("Save to this file? (yes/no): ");
            string choice = Console.ReadLine().ToLower();
            if (choice != "yes")
                return;
        }

        List<string> lines = new List<string>();

        if (!exists)
        {
            lines.Add("type,name,description,points,date,complete,amountCompleted,target,bonus");
        }
        else
        {
            string firstLine = File.ReadLines(file).First();
            if (!firstLine.StartsWith("type,name"))
            {
                lines.Add("type,name,description,points,date,complete,amountCompleted,target,bonus");
            }
            else
            {
                lines.Add(firstLine);
                lines.AddRange(File.ReadAllLines(file).Skip(1));
            }
        }

        string dateString = _lastRecordDate == DateTime.MinValue
            ? ""
            : _lastRecordDate.ToString("yyyy-MM-dd");

        foreach (Goal g in _goals)
        {
            if (g is SimpleGoal sg)
            {
                lines.Add($"SimpleGoal,{sg.GetName()},{sg.GetDescription()},{sg.GetPoints()},{dateString},{sg.IsComplete()},,,");
            }
            else if (g is EternalGoal eg)
            {
                lines.Add($"EternalGoal,{eg.GetName()},{eg.GetDescription()},{eg.GetPoints()},{dateString},,,,");
            }
            else if (g is ChecklistGoal cg)
            {
                var rep = cg.GetStringRepresentation().Split("|");
                int completed = int.Parse(rep[4]);
                int target = int.Parse(rep[5]);
                int bonus = int.Parse(rep[6]);
                lines.Add($"ChecklistGoal,{cg.GetName()},{cg.GetDescription()},{cg.GetPoints()},{dateString},,{completed},{target},{bonus}");
            }
        }

        File.WriteAllLines(file, lines);
        Console.WriteLine("Goals saved to CSV successfully.");
    }

    private void LoadGoals()
    {
        while (true)
        {
            Console.Write("Enter CSV file name to load (or type 'back'): ");
            string file = Console.ReadLine();

            if (file.ToLower() == "back")
                return;

            if (!File.Exists(file))
            {
                Console.WriteLine("File not found. Try again.");
                continue;
            }

            var lines = File.ReadAllLines(file);
            bool headerFound = lines[0].StartsWith("type,name");
            int start = headerFound ? 1 : 0;

            _goals.Clear();

            for (int i = start; i < lines.Length; i++)
            {
                var row = lines[i].Split(',');

                string type = row[0];
                string name = row[1];
                string desc = row[2];
                int points = int.Parse(row[3]);

                string date = row[4];
                if (!string.IsNullOrWhiteSpace(date))
                    DateTime.TryParse(date, out _lastRecordDate);

                if (type == "SimpleGoal")
                {
                    bool complete = bool.Parse(row[5]);
                    var g = new SimpleGoal(name, desc, points);
                    if (complete) g.RecordEvent();
                    _goals.Add(g);
                }
                else if (type == "EternalGoal")
                {
                    _goals.Add(new EternalGoal(name, desc, points));
                }
                else if (type == "ChecklistGoal")
                {
                    int completed = int.Parse(row[6]);
                    int target = int.Parse(row[7]);
                    int bonus = int.Parse(row[8]);

                    var g = new ChecklistGoal(name, desc, points, target, bonus);
                    for (int c = 0; c < completed; c++)
                        g.RecordEvent();

                    _goals.Add(g);
                }
            }

            Console.WriteLine("CSV goals loaded successfully.");
            return;
        }
    }
}