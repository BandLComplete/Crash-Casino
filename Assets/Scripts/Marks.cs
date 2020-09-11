using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Marks
{
    public static Dictionary<string, int> StartMarks()
    {
        var levelMarks = new Dictionary<string, int>();
        foreach (var level in Distributions.Dictionary.Keys)
        {
            levelMarks[level] = 0;
        }

        return levelMarks;
    }
    
    public static Dictionary<string, int> GetFromString(string input)
    {
        var levelMarks = new Dictionary<string, int>();
        foreach (var record in input.Split(' ').Select(s => s.Split(':')))
        {
            if (int.TryParse(record[1], out var mark))
                levelMarks[record[0]] = mark;
            else
            {
                //Debug.Log("wrong mark record");
            }
        }

        return levelMarks;
    }
}