using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource tap, characterExplosion, rolling, birdsong, uncorkingALid, waterSlap, mowerNoise, winMusic, walk;

    public static SoundManager current;
    public static bool soundOn = true;

    private void Awake()
    {
        //Init();
        Play(birdsong);
        current = this;
        //soundOn = true;
    }

    public void Init()
    {
        soundOn = GameData.GetInt(GameData.Valuse.sound) == 1;
    }

    public void Play(AudioSource sound)
    {
        if (soundOn) { sound.Play(); }
    }

    public void Stop(AudioSource sound)
    {
        if (soundOn) { sound.Stop(); }
    }


}
