
using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class HighScoresObject
{
    public string DataPath => Path.Combine(Application.persistentDataPath, "hiscore.json");
    public List<SingleHighScore> highScores;
    

    public static HighScoresObject DeserializeHighScore(string data)
    {
        return JsonConvert.DeserializeObject<HighScoresObject>(data);
    }

    //Create - Read - Update - Delete

    public bool TryCreateDataFile()
    {
        if (!File.Exists(DataPath))
        {
            File.CreateText(DataPath);
            return true;
        }
        else
            return false;
    }

    public bool TryReadDataFile(out string result)
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

    public bool TryUpdateDataFile(SingleHighScore newHighScore)
    {
        highScores.Add(newHighScore);
        string test = JsonConvert.SerializeObject(this);
        File.WriteAllText(DataPath, test);
        return true;
    }
}
