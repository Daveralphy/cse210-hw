using System;
using System.Collections.Generic;
using System.IO;

class Journal
{
    private List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries found.");
            return;
        }

        for (int i = 0; i < _entries.Count; i++)
        {
            Console.WriteLine($"{i + 1}.");
            _entries[i].DisplayEntry();
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (Entry entry in _entries)
            {
                outputFile.WriteLine($"{entry._date}~|~{entry._prompt}~|~{entry._response}");
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        string[] lines = File.ReadAllLines(filename);
        _entries.Clear();

        foreach (string line in lines)
        {
            string[] parts = line.Split("~|~");
            Entry entry = new Entry(parts[1], parts[2], parts[0]);
            _entries.Add(entry);
        }
    }

    public void DeleteEntry(string input)
    {
        string[] parts = input.Split(',');
        List<int> indexes = new List<int>();

        foreach (string part in parts)
        {
            if (int.TryParse(part.Trim(), out int index))
            {
                indexes.Add(index);
            }
        }

        indexes.Sort();
        indexes.Reverse();

        foreach (int index in indexes)
        {
            if (index > 0 && index <= _entries.Count)
            {
                _entries.RemoveAt(index - 1);
            }
        }

        Console.WriteLine("Selected entries deleted successfully.");
    }
}
