using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject toolPanel, startPanel, gamePanel, finishPanel;

    public static UIManager current;

    void Awake()
    {
        current = this;
        TurnOnPanel(_toolPanel: true, _startPanel: true);
        EventManager.onPlayButtonPress.AddListener(OnPlayButtonPress);
        EventManager.onPlayerComplete.AddListener(PlayerComplete);
        EventManager.onPlayerFailed.AddListener(PlayerFailed);
        EventManager.onPlayerWin.AddListener(PlayerWin);
    }

    void TurnOnPanel(bool _toolPanel = false, bool _startPanel = false, bool _gamePanel = false, bool _finishPanel = false)
    {
        toolPanel.SetActive(_toolPanel);
        startPanel.SetActive(_startPanel);
        gamePanel.SetActive(_gamePanel);
        finishPanel.SetActive(_finishPanel);
    }

    void OnPlayButtonPress()
    {
        TurnOnPanel(_toolPanel: true, _gamePanel: true);
#if UNITY_EDITOR
        stopWatch = StartCoroutine(StopWatch());
#endif
    }

    void PlayerComplete()
    {
        TurnOnPanel(_finishPanel: true);
        FinishPanel fp = finishPanel.GetComponent<FinishPanel>();
        fp.TurnOnText(fp.completeText);

#if UNITY_EDITOR
        StopCoroutine(stopWatch);
        Debug.Log("Playtime = " + Mathf.Floor(gameLength).ToString());
#endif
    }

    void PlayerFailed()
    {
        TurnOnPanel(_finishPanel: true);
        FinishPanel fp = finishPanel.GetComponent<FinishPanel>();
        fp.TurnOnText(fp.failedText);

#if UNITY_EDITOR
        StopCoroutine(stopWatch);
        Debug.Log("Playtime = " + Mathf.Floor(gameLength).ToString());
#endif
    }

    void PlayerWin()
    {
        TurnOnPanel(_finishPanel: true);
        FinishPanel fp = finishPanel.GetComponent<FinishPanel>();
        fp.TurnOnText(fp.winText);

#if UNITY_EDITOR
        StopCoroutine(stopWatch);
        Debug.Log("Playtime = " + Mathf.Floor(gameLength).ToString());
#endif
    }

#if UNITY_EDITOR
    private float gameLength = 0;
    private Coroutine stopWatch;
    IEnumerator StopWatch()
    {
        while (true)
        {
            gameLength += Time.deltaTime;
            yield return null;
        }
    }
#endif
}
