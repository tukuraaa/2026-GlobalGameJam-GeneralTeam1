
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class HighScoresObject
{
    //macos example: /Users/ivan/Library/Application Support/DefaultCompany/2026-GGJ-GeneralTeam1/hiscore.json
    public static string DataPath => Path.Combine(Application.persistentDataPath, "hiscore.json");
    public List<SingleHighScore> highScores;


    public HighScoresObject()
    {
        highScores = new();
    }

    public static HighScoresObject LoadHighScore()
    {
        Debug.Log($"path {DataPath}");
        if(TryReadDataFile(out string data))
        {
            return DeserializeHighScore(data);
        }
        else
        {
            HighScoresObject newObj = new();
            TryCreateDataFile();
            TryUpdateAll(newObj);
            return newObj;
        }
    }    

    public static HighScoresObject DeserializeHighScore(string data)
    {
        return JsonConvert.DeserializeObject<HighScoresObject>(data);
    }

    //Create - Read - Update - Delete

    public static bool TryCreateDataFile(string content = "")
    {
        if (!File.Exists(DataPath))
        {
            StreamWriter fileStream = File.CreateText(DataPath);
            if (!string.IsNullOrEmpty(content))
            {
                fileStream.Write(content);
            }
            fileStream.DisposeAsync();
            fileStream.Close();
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

    public static bool TryUpdateAll(HighScoresObject obj)
    {
        string data = JsonConvert.SerializeObject(obj);
        File.WriteAllText(DataPath, data);
        return true;
    }

    public static bool TryUpdateDataFile(SingleHighScore newHighScore)
    {
        HighScoresObject obj = LoadHighScore();
        if(obj == null)
        {
            obj = new();
        }
        obj.highScores.Add(newHighScore);
        string test = JsonConvert.SerializeObject(obj);
        File.WriteAllText(DataPath, test);
        return true;
    }

}
