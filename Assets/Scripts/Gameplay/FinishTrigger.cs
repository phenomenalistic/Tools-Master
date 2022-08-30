using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public string finishedPerson;

    public static FinishTrigger current;

    public List<ParticleSystem> particleSystems = new List<ParticleSystem> { };

    public float position = 10;
    private void Awake()
    {
        finishedPerson = null;
        current = this;
        position = transform.position.z;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Opponent")
        {
            finishedPerson = finishedPerson == null ? other.tag : finishedPerson;
            EventManager.onOpponentFinished.Invoke();
        }
        else if (other.tag == "Player")
        {
            finishedPerson = finishedPerson == null ? other.tag : finishedPerson;
            PlayerFinished(other.tag);
        }
    }

    void PlayerFinished(string playerTag)
    {
        EventManager.onPlayerFinished.Invoke();
        if (finishedPerson != playerTag)
        {
            EventManager.onPlayerFailed.Invoke();

        }
        else
        {
            PlayCongratulationConfetti();
            StartCoroutine(FountainVibration());
        }
        
    }

    void PlayCongratulationConfetti()
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Play();
        }
    }

    IEnumerator FountainVibration()
    {
        Vibration.Vibrate(10);

        for (int i = 0; i < 6; i++)
        {
            Vibration.Vibrate(2);
            yield return new WaitForSeconds(0.07f);
        }
    }
}
