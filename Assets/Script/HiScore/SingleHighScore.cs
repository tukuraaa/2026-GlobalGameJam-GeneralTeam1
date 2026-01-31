
using System;

[Serializable]
public class SingleHighScore
{
    public string name;
    public int score;

    public new string ToString() => $"High Score: {name}, {score:G2}";
}
