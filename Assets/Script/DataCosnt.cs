using System;
using UnityEngine;

public static class DataConst
{
    public const float UpLevelInterval = 10f;
    public const int MaxLevel = 9;
    public static float ScoreRate(int level)
    {
        return 1.5f + (level - 1) * 1f;
    }

    public static float ShooterArriveTime(int level)
    {
        return 90 + 45f * (1.0f / level);
    }

    public static int ShootWaveCount(int level)
    {
        return 3 + (int)((level - 1) * 0.3f);
    }

    public static float ShootWaveInterval(int level)
    {
        switch (level)
        {
            case 1:
                return 3.3f;
            case 2:
                return 3.2f;
            case 3:
                return 3.0f;        
            case 4:
                return 2.8f;
            case 5:
                return 2.6f;
            case 6:
                return 2.4f;
            case 7:
                return 2.2f;
            case 8:
                return 2.1f;
            case 9:
                return 2f;
            default:
                return 2f;
        }
    }

    public static float SinSpeedRate(int level)
    {
        return 1f + (level - 1) * 0.1f;
    }
}