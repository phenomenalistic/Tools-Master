using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public static class UnityAnaliticsManager
{
    public static void EnterTime(int hour)
    {
        Analytics.CustomEvent("PlayerAppEvents", new Dictionary<string, object>()
        {
            {"EnterTime", hour }
        });
    }

    public static void LevelWin(int level)
    {
        Analytics.CustomEvent("PlayerAppEvents", new Dictionary<string, object>()
        {
            {"LevelWin", level }
        });
    }

    public static void LevelFailed(int level)
    {
        Analytics.CustomEvent("PlayerAppEvents", new Dictionary<string, object>()
        {
            {"LevelFailed", level }
        });
    }

    public static void LevelComplete(int level)
    {
        Analytics.CustomEvent("PlayerAppEvents", new Dictionary<string, object>()
        {
            {"LevelComplete", level }
        });
    }
}
