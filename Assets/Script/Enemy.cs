using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using System.Transactions;

public class Enemy : MonoBehaviour
{
    public bool Scanning = false;
    public float scanTime;
    public float a = 1;

    private void Update()
    {
        if (RedLightManager.instance.paused || !RedLightManager.instance.started || RedLightManager.instance.timer <=0 || RedLightManager.instance.player.win) return;
        if (!Scanning && !SFX.instance.enemy.isPlaying)
        {
            Scanning = true;

            StartCoroutine(Scan());

        }
        if (Scanning)
        {
            //Scan event
            if (RedLightManager.instance.player.rb.velocity.magnitude > 0.3f && !RedLightManager.instance.player.lose)
            {
                StartCoroutine(RedLightManager.instance.player.Lose());
            }
            foreach (GameObject bot in GameObject.FindGameObjectsWithTag("Bot"))
            {
                if (bot.GetComponent<BotController>().rb.velocity.magnitude > 0.3f && !bot.GetComponent<BotController>().die  && !bot.GetComponent<BotController>().win)
                {
                    bot.GetComponent<BotController>().die = true;
                    StartCoroutine(bot.GetComponent<BotController>().Died());
                }
            }
        }
    }

    public IEnumerator Scan()
    {
        if (RedLightManager.instance.timer > 0)
        {
            transform.DORotate(new Vector3(0, 180, 0), 1);
            if (a < 2)
            {
                a += 0.2f;
           
            }
            RedLightManager.instance.green.SetActive(false);
            RedLightManager.instance.red.SetActive(true);
            yield return new WaitForSeconds(scanTime/a - 1); 
            transform.DORotate(new Vector3(0, 1, 0), 1).OnComplete(() =>
            {
                SFX.instance.enemy.pitch = a;
                SFX.instance.PlayEnemy();
                Scanning = false;
                RedLightManager.instance.green.SetActive(true);
                RedLightManager.instance.red.SetActive(false);
                foreach (GameObject bot in GameObject.FindGameObjectsWithTag("Bot"))
                {
                    StartCoroutine(bot.GetComponent<BotController>().Move());
                }
            });
        }
    }
}
