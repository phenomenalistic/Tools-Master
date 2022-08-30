using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : MonoBehaviour
{
    public ToolProperies tool;
    public void OnPlayeButtonPress()
    {
        EventManager.onPlayButtonPress.Invoke();
        Player.current.SetTool(tool);
        Vibration.Vibrate(8);
    }
}
