using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public static class GameAnaliticsManager
{
    public static void Init()
    {
#if !UNITY_EDITOR
        GameAnalytics.Initialize();
#endif
    }

    public static void LevelStart(int level)
    {
#if !UNITY_EDITOR
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level " + level);
#endif
    }
    public static void LevelComplete(int level)
    {
#if !UNITY_EDITOR
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level " + level);
#endif
    }

    public static void LevelFailed(int level)
    {
#if !UNITY_EDITOR
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level " + level);
#endif
    }
}
