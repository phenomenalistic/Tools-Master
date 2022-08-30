using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyPanel : MonoBehaviour
{
    public Text moneyText;

    public RectTransform iconRT;

    private int moneyCount = 0;
    void Awake()
    {
        moneyCount = GameData.GetInt(GameData.Valuse.money);
        UpdateMoneyPanel();
    }

    public void UpdateMoneyPanel()
    {
        moneyText.text = moneyCount.ToString();
        moneyText.SetAllDirty();
    }

    public void AddMoney(int count, float waitTime = 0)
    {
        int newValue = GameData.GetInt(GameData.Valuse.money) + count;
        if (newValue < 0) { newValue = 0; }
        GameData.SetInt(GameData.Valuse.money, newValue);
        StartCoroutine(StartAddAnimation(waitTime, count));
    }

    Coroutine addAnimation;
    public IEnumerator StartAddAnimation(float waitTime, int addCount)
    {
        if (waitTime == 0)
        {
            moneyCount += addCount;
            UpdateMoneyPanel();
        }
        yield return new WaitForSeconds(waitTime);
        if (addAnimation != null) { StopCoroutine(addAnimation); }
        addAnimation = StartCoroutine(AddAnimation());

        if (waitTime != 0)
        {
            moneyCount += addCount;
            UpdateMoneyPanel();
        }
    }

    IEnumerator AddAnimation()
    {
        float pointer = 0, speed = 10;

        while (pointer < 1)
        {
            pointer += Time.deltaTime * speed;
            iconRT.localScale = Vector3.one * Mathf.Lerp(1, 1.2f, pointer);
            yield return null;
        }
        while (pointer > 0)
        {
            pointer -= Time.deltaTime * speed;
            iconRT.localScale = Vector3.one * Mathf.Lerp(1, 1.2f, pointer);
            yield return null;
        }
    }

    Coroutine addAnimationForMany;
    public IEnumerator AddMany()
    {
        addAnimationForMany = StartCoroutine(AddAnimation());

        yield return new WaitForSeconds(0.05f);
        for (int i = 0; i < 5; i++)
        {
            StopCoroutine(addAnimationForMany);
            addAnimationForMany = StartCoroutine(AddAnimation());
            Vibration.Vibrate(4);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
