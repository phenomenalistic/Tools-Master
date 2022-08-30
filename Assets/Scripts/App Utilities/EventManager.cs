using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent onPlayButtonPress = new UnityEvent();
    public static UnityEvent onPlayerFinished = new UnityEvent();
    public static UnityEvent onOpponentFinished = new UnityEvent();
    public static UnityEvent onPlayerFailed = new UnityEvent();
    public static UnityEvent onPlayerComplete = new UnityEvent();
    public static UnityEvent onPlayerWin = new UnityEvent();
}
