using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoSingleton<SoundSystem>
{
    [SerializeField] private AudioSource mainSource;
    [SerializeField] private AudioClip mainMusic, coin, pipe, upgrade, finish;

    public void MainMusicPlay()
    {
        mainSource.clip = mainMusic;
        mainSource.Play();
        mainSource.volume = 70;
    }

    public void MainMusicStop()
    {
        mainSource.Stop();
        mainSource.volume = 0;
    }

    public void CallCoinSound()
    {
        mainSource.PlayOneShot(coin);
    }
    public void CallPipeSound()
    {
        mainSource.PlayOneShot(pipe);
    }
    public void CallUpgradeSound()
    {
        mainSource.PlayOneShot(upgrade);
    }
    public void CallFinishSound()
    {
        mainSource.PlayOneShot(finish);
    }
}
