using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedLightManager : MonoBehaviour
{
    public static RedLightManager instance;
    public PlayerController player;
    public Enemy enemy;
    public bool paused = false;
    public bool started = false;
    public float timer;
    public int coin = 0;
    public int playerCounters = 15;
    public UIEnd endPanel;
    public ParticleSystem fxWin;

    [Header("UI")]
    public GameObject joystick;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject startPanel;
    public GameObject gameplayPanel;

    [Header("UI Objects")]
    public GameObject green;
    public GameObject red;
    public GameObject clickStart;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        ClickStart();
    }

    private void Update()
    {
        if (paused || !started) return;
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 0; 
        }
    }

    public void PlayGame()
    {
        gameplayPanel.SetActive(true);
        startPanel.SetActive(false);
        green.SetActive(true);
        red.SetActive(false);
        started = true;
        SFX.instance.StopBg();
        SFX.instance.PlayEnemy();
        foreach (GameObject bot in GameObject.FindGameObjectsWithTag("Bot"))
        {
            StartCoroutine(bot.GetComponent<BotController>().Move());
        }
    }
    public void ClickStart()
    {
        clickStart.transform.DORotate(new Vector3(0, 0, 40), 1f).OnComplete(() => clickStart.transform.DORotate(new Vector3(0, 0, 20), 1f).OnComplete(()=> ClickStart()));
    }
    public void Restart()
    {
        DOTween.KillAll();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
