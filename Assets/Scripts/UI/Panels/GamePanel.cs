using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    public RectTransform scale, playerPoint, opponentPoint;
    public Text currentLevel, nextLevel;

    Coroutine updateScale;

    private void Awake()
    {
        EventManager.onPlayerFinished.AddListener(OnPlayerFinised);
    }
    private void Start()
    {
        updateScale = StartCoroutine(UpdateScale());
        currentLevel.text = GameData.GetInt(GameData.Valuse.level).ToString();
        nextLevel.text = (GameData.GetInt(GameData.Valuse.level) + 1).ToString();
    }

    IEnumerator UpdateScale()
    {
        while (true)
        {
            playerPoint.anchoredPosition = new Vector2(scale.rect.width * Mathf.InverseLerp(0, FinishTrigger.current.position, Player.current.transform.position.z), 0);
            opponentPoint.anchoredPosition = new Vector2(scale.rect.width * Mathf.InverseLerp(0, FinishTrigger.current.position, Opponent.current.transform.position.z), 0);
            yield return null;
        }
    }

    public void OnPlayerFinised()
    {
        StopCoroutine(updateScale);
    }
}
