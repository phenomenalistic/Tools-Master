using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class OnAppStart : MonoBehaviour
{
    int targetResolution = 1980;
    void Start()
    {
        Application.targetFrameRate = 60;
        if (Screen.height >= targetResolution)
        {
            Screen.SetResolution(Mathf.FloorToInt(((float)Screen.width / (float)Screen.height) * targetResolution), targetResolution, true);
        }
        else
        {
            Screen.SetResolution(Mathf.FloorToInt(((float)Screen.width / (float)Screen.height) * ((float)Screen.height * 0.95f)), (int)((float)Screen.height * 0.95f), true);
        }
        //GameAnaliticsManager.Init();
        //InitFB();
        //Notifications.Init();
        Vibration.Init();
    }

    public void GoToGame()
    {
        GameSceneManager.LoadNextSceneAsync();
    }
    void InitFB()
    {
        /**/
        if (!FB.IsInitialized)
        {
            //Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
        
    }

    private void InitCallback()
    {
        /**/
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
        
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

}
