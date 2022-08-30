using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolButton : MonoBehaviour
{
    public ToolProperies tool;

    public Button button;
    public Image icon;

    private bool movment = true;
    void Awake()
    {
        EventManager.onPlayButtonPress.AddListener(OnPlayButtonPress);
        EventManager.onPlayerFinished.AddListener(OnPlayerFinished);
        movment = true;
        button.onClick.AddListener(StartGame);
    }

    public void Init(ToolProperies newTool)
    {
        tool = newTool;
        SetValues();
        Opponent.current.tools.Add(tool);
        gameObject.SetActive(true);
    }


    private void SetValues()
    {
        icon.sprite = tool.icon;
    }

    public void OnButtonPress()
    {
        Player.current.SetTool(tool, movment);
        SoundManager.current.Play(SoundManager.current.tap);
        Vibration.Vibrate(8);
    }

    public void StartGame()
    {
        EventManager.onPlayButtonPress.Invoke();
    }

    public void OnPlayButtonPress()
    {
        button.onClick.RemoveListener(StartGame);
    }

    public void OnPlayerFinished()
    {
        movment = false;
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        SetValues();
    }
#endif
}
