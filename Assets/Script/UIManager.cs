using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace phamtuan
{

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
                    GetComponent<Text>().text = RedLineManager.instance.timer.ToString("F1");
                    break;
                case UIState.PlayerCounters:
                    GetComponent<TextMeshProUGUI>().text = RedLineManager.instance.playerCounters + "/35";
                    break;
            }
        }
    
    }
}
