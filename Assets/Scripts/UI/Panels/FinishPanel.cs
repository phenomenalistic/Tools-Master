using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPanel : MonoBehaviour
{
    public GameObject upLine, completeText, winText, failedText;

    void Start()
    {
        //Init();
    }

    void Init()
    {
        if (FinishTrigger.current.finishedPerson == "Player")
        {
            TurnOnText(completeText);
        }
        else
        {
            TurnOnText(failedText);
        }
    }

    public void TurnOnText(params GameObject[] texts)
    {
        completeText.SetActive(false);
        winText.SetActive(false);
        failedText.SetActive(false);

        foreach (GameObject text in texts) { text.SetActive(true); }
    }
}
