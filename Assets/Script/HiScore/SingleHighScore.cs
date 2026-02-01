
using System;

[Serializable]
public class SingleHighScore : IComparable<SingleHighScore>
{
    public string name;
    public int score;

    public string ToString()
    {
        return $"High Score: {name}, {score:G2}";
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
