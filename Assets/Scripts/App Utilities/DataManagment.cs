using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManagment : MonoBehaviour
{
    public OnAppStart onAppStartScript;
    void Start()
    {
        GameData.Load();
        if (!GameData.HasInt(GameData.Valuse.level)) { GameData.SetInt(GameData.Valuse.level, 1); }

        if (!GameData.HasInt(GameData.Valuse.level)) { GameData.SetInt(GameData.Valuse.gameCount, 1); }

        if (!GameData.HasInt(GameData.Valuse.money)) { GameData.SetInt(GameData.Valuse.money, 10); }

        if (!GameData.HasInt(GameData.Valuse.vibration)) { GameData.SetInt(GameData.Valuse.vibration, 1); }

        if (!GameData.HasInt(GameData.Valuse.sound)) { GameData.SetInt(GameData.Valuse.sound, 1); }

        

        onAppStartScript.GoToGame();
    }
}
