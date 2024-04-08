using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartPanel : MonoBehaviour, IPointerDownHandler
{
     
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if(RedLineManager.instance.started) return;
        RedLineManager.instance.PlayGame();
        DOTween.Kill(RedLineManager.instance.clickStart.transform);
        if (RedLineManager.instance.player.hasShield)
        {
            DOVirtual.DelayedCall(7, () =>
            {
                
                StartCoroutine(BlinkShield());
            });
        }  
    }

    private IEnumerator BlinkShield()
    {
        for (int i = 0; i < 5; i++) 
        { 
            DOTween.Pause(RedLineManager.instance.player.shield.transform);
            RedLineManager.instance.player.shield.transform.GetChild(0).gameObject.SetActive(false);
            RedLineManager.instance.player.shield.transform.GetChild(1).gameObject.SetActive(false);
            RedLineManager.instance.player.shield.transform.GetChild(2).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            DOTween.Play(RedLineManager.instance.player.shield.transform);
            RedLineManager.instance.player.shield.transform.GetChild(0).gameObject.SetActive(true);
            RedLineManager.instance.player.shield.transform.GetChild(2).gameObject.SetActive(true);
            RedLineManager.instance.player.shield.transform.GetChild(1).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }
        RedLineManager.instance.player.hasShield = false;
        DOTween.Kill(RedLineManager.instance.player.shield.transform);
        RedLineManager.instance.player.shield.SetActive(false);
    }
}
