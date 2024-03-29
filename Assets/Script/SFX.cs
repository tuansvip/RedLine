using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public static SFX instance;
    public AudioSource bg;
    public AudioSource click;
    public AudioSource win;
    public AudioSource lose;
    public AudioSource enemy;
    public AudioSource firework;
    public AudioSource shot1;
    public AudioSource shot2;
    public AudioSource femaleScream;
    public AudioSource maleScream;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void PlayBg()
    {
        bg.Play();
    }
    public void PlayClick()
    {
        click.Play();
    }
    public void PlayWin()
    {
        win.Play();
    }
    public void PlayLose()
    {
        lose.Play();
    }
    public void PlayEnemy()
    {
        enemy.Play();
    }
    public void PlayFirework()
    {
        firework.Play();
    }
    public void PlayShot1()
    {
        shot1.Play();
    }
    public void PlayShot2()
    {
        shot2.Play();
    }
    public void PlayFemaleScream()
    {
        femaleScream.Play();
    }
    public void PlayMaleScream()
    {
        maleScream.Play();
    }
    public void StopBg()
    {
        bg.Stop();
    }
    public void StopClick()
    {
        click.Stop();
    }
    public void StopWin()
    {
        win.Stop();
    }
    public void StopLose()
    {
        lose.Stop();
    }
    public void StopEnemy()
    {
        enemy.Stop();
    }
    public void StopFirework()
    {
        firework.Stop();
    }
    public void StopShot1()
    {
        shot1.Stop();
    }
    public void StopShot2()
    {
        shot2.Stop();
    }
    public void StopFemaleScream()
    {
        femaleScream.Stop();
    }
    public void StopMaleScream()
    {
        maleScream.Stop();
    }

    public void MuteAll()
    {
        bg.mute = true;
        click.mute = true;
        win.mute = true;
        lose.mute = true;
        enemy.mute = true;
        firework.mute = true;
        shot1.mute = true;
        shot2.mute = true;
        femaleScream.mute = true;
    }
    public void UnmuteAll()
    {
        bg.mute = false;
        click.mute = false;
        win.mute = false;
        lose.mute = false;
        enemy.mute = false;
        firework.mute = false;
        shot1.mute = false;
        shot2.mute = false;
        femaleScream.mute = false;
    }
}
