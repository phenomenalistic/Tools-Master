using UnityEngine;

public static class Vibration
{
#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
#endif

    private static float endOfLastCall = 0;

    public static bool vibration = true;

    public static void Init()
    {
        vibration = GameData.GetInt(GameData.Valuse.vibration) == 1;
    }
    public static void Vibrate(long milliseconds = 250, bool cancel = false)
    {
        if (IsAndroid())
        {
            if (!IsVibrate() || cancel)
            {
                if (cancel) { Cancel(); }
                if (vibration) { vibrator.Call("vibrate", milliseconds); }
                endOfLastCall = Time.time + (milliseconds * 0.001f);
            }
            
        }
        else
        {
            Handheld.Vibrate();
        }
        
    }

    public static void Cancel()
    {
        if (IsAndroid())
        {
            vibrator.Call("cancel");
        }
    }

    private static bool IsVibrate() // кол-во закончилась ли последная вызванная вибрация
    {
        return endOfLastCall > Time.time;
    }

    public static bool IsAndroid()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
    return true;
#else
        return false;
#endif
    }
}
