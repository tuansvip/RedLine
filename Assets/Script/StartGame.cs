using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace phamtuan
{
    public class StartPanel : MonoBehaviour, IPointerDownHandler
    {
     
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if(RedLineManager.instance.started) return;
            RedLineManager.instance.PlayGame();
            DOTween.Kill(RedLineManager.instance.clickStart.transform); 
        }


    }

}
