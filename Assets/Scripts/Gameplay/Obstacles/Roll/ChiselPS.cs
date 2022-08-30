using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChiselPS : MonoBehaviour
{
    public List<ParticleSystem> ps;
    private ParticleSystem.MainModule main1, main2;
    public bool usePlayer = false;

    private void Awake()
    {
        if (transform.position.x > -1) { usePlayer = true; }
        else { usePlayer = false; }
        main1 = ps[0].main;
        main2 = ps[1].main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Spiral")
        {
            SetPSColors(other.GetComponent<RollTrigger>());
            Play();
        }
    }

    private void SetPSColors(RollTrigger rt)
    {
        main1.startColor = new ParticleSystem.MinMaxGradient(rt.rm.oneColor, rt.rm.twoColor);
        main2.startColor = new ParticleSystem.MinMaxGradient(rt.rm.oneColor, rt.rm.twoColor);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Spiral")
        {
            Stop();
        }
    }

    public void Play()
    {
        if (usePlayer)
        {
            if (improvingPitch != null) { StopCoroutine(improvingPitch); }
            improvingPitch = StartCoroutine(ImprovingPitch(SoundManager.current.rolling));

            SoundManager.current.rolling.pitch = startPitch;
            StartCoroutine(PlaySoundWithDelay(SoundManager.current.rolling));

            if (rollingVibro != null) { StopCoroutine(rollingVibro); }
            rollingVibro = StartCoroutine(RollingVibro());
        }
        foreach (ParticleSystem p in ps)
        {
            p.Play();
        }
    }

    public void Stop()
    {
        if (usePlayer)
        {
            SoundManager.current.Stop(SoundManager.current.rolling);
            SoundManager.current.Play(SoundManager.current.uncorkingALid);
            Vibration.Vibrate(20);
            StopCoroutine(improvingPitch);
            StopCoroutine(rollingVibro);
        }
        
        foreach (ParticleSystem p in ps)
        {
            p.Stop();
        }
    }

    IEnumerator PlaySoundWithDelay(AudioSource sound, float delay = 0.01f)
    {
        yield return null;
        SoundManager.current.Play(sound);
    }

    IEnumerator StopSoundWithDelay(AudioSource sound, float delay = 0.01f)
    {
        yield return new WaitForSeconds(delay);
        SoundManager.current.Stop(sound);
    }

    Coroutine improvingPitch;

    float startPitch = 1.2f, endPitch = 3;
    IEnumerator ImprovingPitch(AudioSource sound)
    {
        float pointer = 0, speed = 0.1f;

        while (pointer < 1)
        {
            pointer += Time.deltaTime * speed;
            sound.pitch = Mathf.Lerp(startPitch, endPitch, pointer);
            yield return null;
        }
    }

    private void OnDestroy()
    {
        if (usePlayer && SoundManager.current != null)
        {
            StopAllCoroutines();
            SoundManager.current.Stop(SoundManager.current.rolling);
            SoundManager.current.rolling.pitch = startPitch;
        }
    }

    IEnumerator FountainVibration()
    {
        Vibration.Vibrate(20);

        for (int i = 0; i < 10; i++)
        {
            Vibration.Vibrate(3);
            yield return new WaitForSeconds(0.07f);
        }
    }

    Coroutine rollingVibro;
    IEnumerator RollingVibro()
    {
        while (true)
        {
            Vibration.Vibrate(3);
            yield return new WaitForSeconds(0.07f);
        }
    }
}
