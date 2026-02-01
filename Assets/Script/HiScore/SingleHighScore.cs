
using System;

[Serializable]
public class SingleHighScore : IComparable<SingleHighScore>
{
    public string name;
    public int score;

    public new string ToString()
    {
        return $"High Score: {name}, {score}";
    }

    public int CompareTo(SingleHighScore other)
    {
        return other.score - score;
    }

    public SingleHighScore(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}
