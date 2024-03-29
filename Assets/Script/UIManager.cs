using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public enum UIState
    {
        Timer,
        PlayerCounters
    }

    public static UIManager instance;

    public UIState currentState;

    private void Awake()
    {
        instance = this;
    }
    private void FixedUpdate()
    {
        switch (currentState)
        {
            case UIState.Timer:
                GetComponent<TextMeshPro>().text = RedLightManager.instance.timer.ToString("F1");
                break;
            case UIState.PlayerCounters:
                GetComponent<TextMeshProUGUI>().text = RedLightManager.instance.playerCounters + " players remaining";
                break;
        }
    }
    
}
