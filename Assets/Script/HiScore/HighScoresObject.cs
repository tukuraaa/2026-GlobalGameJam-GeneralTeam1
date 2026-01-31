
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class HighScoresObject
{
    public static string DataPath => Path.Combine(Application.persistentDataPath, "hiscore.json");
    public List<SingleHighScore> highScores;


    public HighScoresObject()
    {
        highScores = new();
    }

    public static HighScoresObject LoadHighScore()
    {
        if(TryReadDataFile(out string data))
        {
            return DeserializeHighScore(data);
        }
        else
        {
            TryCreateDataFile();
            return new();
        }
    }    

    public static HighScoresObject DeserializeHighScore(string data)
    {
        return JsonConvert.DeserializeObject<HighScoresObject>(data);
    }

    //Create - Read - Update - Delete

    public static bool TryCreateDataFile()
    {
        if (!File.Exists(DataPath))
        {
            File.CreateText(DataPath);
            return true;
        }
        else
            return false;
    }

    public static bool TryReadDataFile(out string result)
    {
        if (!File.Exists(DataPath))
        {
            result = "";
            return false;
        }
        else
        {
            result = File.ReadAllText(DataPath);
            return true;
        }
    }

    public static bool TryUpdateDataFile(SingleHighScore newHighScore)
    {
        HighScoresObject obj = LoadHighScore();
        obj.highScores.Add(newHighScore);
        string test = JsonConvert.SerializeObject(obj);
        File.WriteAllText(DataPath, test);
        return true;
    }

}
