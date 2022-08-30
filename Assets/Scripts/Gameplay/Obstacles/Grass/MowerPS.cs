using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MowerPS : MonoBehaviour
{
    public ParticleSystem ps;
    private ParticleSystem.MainModule main;

    private bool usePlayer = false;

    private void Awake()
    {
        main = ps.main;
        usePlayer = transform.position.x > -1;
        
    }

    void Start()
    {
        if (usePlayer)
        {
            SoundManager.current.mowerNoise.pitch = notWorkMowerPitch;
            SoundManager.current.mowerNoise.volume = notWorkMowerVolume;
            SoundManager.current.Play(SoundManager.current.mowerNoise);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Grass")
        {

            SetPSColors(other.GetComponent<Grass>());
            Play();

        }
    }

    private void SetPSColors(Grass grass)
    {
        main.startColor = new ParticleSystem.MinMaxGradient(grass.wm.oneColor, grass.wm.twoColor);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Grass")
        {
            Stop();
        }
    }

    private float workMowerPitchStart = 1.5f, workMowerPitchEnd = 3f, notWorkMowerPitch = 1, workMowerVolume = 0.5f, notWorkMowerVolume = 0.2f;
    public void Play()
    {
        ps.Play();

        if (usePlayer)
        {
            SoundManager.current.mowerNoise.volume = workMowerVolume;
            if (improvingPitch != null) { StopCoroutine(improvingPitch); }
            improvingPitch = StartCoroutine(ImprovingPitch(SoundManager.current.mowerNoise));

            if (mowGrassVibro != null) { StopCoroutine(mowGrassVibro); }
            mowGrassVibro = StartCoroutine(MowGrassVibro());
            //SoundManager.current.Play(SoundManager.current.mowerNoise);
        }
    }

    public void Stop()
    {
        ps.Stop();
        if (usePlayer)
        {
            StopCoroutine(improvingPitch);
            StopCoroutine(mowGrassVibro);
            SoundManager.current.mowerNoise.pitch = notWorkMowerPitch;
            SoundManager.current.mowerNoise.volume = notWorkMowerVolume;
            
            //SoundManager.current.Stop(SoundManager.current.mowerNoise);
        }
    }

    Coroutine improvingPitch;

    float startPitch = 1.2f, endPitch = 3;
    IEnumerator ImprovingPitch(AudioSource sound)
    {
        float pointer = 0, speed = 0.1f;

        while (pointer < 1)
        {
            pointer += Time.deltaTime * speed;
            sound.pitch = Mathf.Lerp(workMowerPitchStart, workMowerPitchEnd, pointer);
            yield return null;
        }
    }

    private void OnDestroy()
    {
        if (usePlayer && SoundManager.current != null)
        {
            SoundManager.current.mowerNoise.pitch = notWorkMowerPitch;
            SoundManager.current.mowerNoise.volume = notWorkMowerVolume;
            SoundManager.current.Stop(SoundManager.current.mowerNoise);

            StopAllCoroutines();
        }
    }

    Coroutine mowGrassVibro;
    IEnumerator MowGrassVibro()
    {
        while (true)
        {
            Vibration.Vibrate(7);
            yield return new WaitForSeconds(0.07f);
            Vibration.Vibrate(5);
            yield return new WaitForSeconds(0.07f);
        }
    }

}
