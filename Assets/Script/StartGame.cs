using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartPanel : MonoBehaviour, IPointerDownHandler
{
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if(RedLightManager.instance.started) return;
        RedLightManager.instance.PlayGame();
        DOTween.Kill(RedLightManager.instance.clickStart.transform);
    }
}
