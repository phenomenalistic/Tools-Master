using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameSceneManager
{
    public static int levelCount = 20, trainingLevelCount = 2; 
    public static void LoadNextScene()
    {
        LoadScene(GetScene());
    }

    public static void LoadNextSceneAsync()
    {
        LoadSceneAsync(GetScene());
    }

    static string GetScene()
    {
        int level = GameData.GetInt(GameData.Valuse.level);

        string sceneName;

        if (level <= trainingLevelCount) { sceneName = "Training level " + level.ToString(); }
        else { sceneName = ((level % levelCount) + 1).ToString(); }

        return sceneName;
    }

    static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    static void LoadSceneAsync(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
