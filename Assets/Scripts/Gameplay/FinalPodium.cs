using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPodium : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public GameObject PlayerPosition;
    public static FinalPodium current;
    private void Awake()
    {
        current = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") { PlayerEnter(); }
    }

    void PlayerEnter()
    {
        EventManager.onPlayerWin.Invoke();
        SoundManager.current.Play(SoundManager.current.winMusic);
        StartCoroutine(WaitPS());
        StartCoroutine(FountainVibration());
    }

    IEnumerator WaitPS(float waitTime = 0.5f)
    {
        yield return new WaitForSeconds(waitTime);
        particleSystem.Play();
    }

    IEnumerator FountainVibration()
    {
        Vibration.Vibrate(30);

        for (int i = 0; i < 20; i++)
        {
            Vibration.Vibrate(4);
            yield return new WaitForSeconds(0.07f);
        }
    }
}
