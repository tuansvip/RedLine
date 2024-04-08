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
        if (RedLineManager.instance.paused || !RedLineManager.instance.started || RedLineManager.instance.timer <=0 || RedLineManager.instance.player.win) return;
        if (!Scanning && !SFX.instance.enemy.isPlaying)
        {
            Scanning = true;

            StartCoroutine(Scan());
        }
        if (Scanning)
        {
            //Scan event
            if (RedLineManager.instance.player.rb.velocity.magnitude > 0.3f && !RedLineManager.instance.player.win && !RedLineManager.instance.player.lose && !RedLineManager.instance.player.hasShield)
            {
                StartCoroutine(RedLineManager.instance.player.Lose());
            }
            foreach (BotController bot in RedLineManager.instance.botCon.GetComponentsInChildren<BotController>())
            {
                if (bot.rb.velocity.magnitude > 0.3f && !bot.die  && !bot.win)
                {
                    bot.die = true;
                    StartCoroutine(bot.Died());
                }
            }
        }
        
    }

    public IEnumerator Scan()
    {
        if (RedLineManager.instance.timer > 0)
        {
            transform.DORotate(new Vector3(0, 180, 0), 1);
            SFX.instance.PlayEnemRotate();
            if (a < 2)
            {
                a += 0.2f;
           
            }
            RedLineManager.instance.green.SetActive(false);
            RedLineManager.instance.red.SetActive(true);
            Debug.LogWarning("Scanning");
            RedLineManager.instance.guard1.Play("Aim");
            RedLineManager.instance.guard2.Play("Aim");
            yield return new WaitForSeconds(scanTime/a - 1);
            SFX.instance.enemy.pitch = a;
            SFX.instance.PlayEnemy();
            Scanning = false;
            RedLineManager.instance.green.SetActive(true);
            RedLineManager.instance.red.SetActive(false);
            RedLineManager.instance.guard1.Play("Idle");
            RedLineManager.instance.guard2.Play("Idle");
            foreach (GameObject bot in GameObject.FindGameObjectsWithTag("Bot"))
            {
                StartCoroutine(bot.GetComponent<BotController>().Move());
            }
            SFX.instance.PlayEnemRotate();

            transform.DORotate(new Vector3(0, 1, 0), 1);

        }
    }
}
