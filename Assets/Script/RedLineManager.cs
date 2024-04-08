using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedLineManager : MonoBehaviour
{
    public static RedLineManager instance;

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    [Header("Game Stat")]
    public Enemy enemy;
    public bool paused = false;
    public bool started = false;
    public float timer;
    public int coin = 0;
    public int playerCounters = 15;
    public Difficulty difficulty;

    public PlayerController player;
    public UIEnd endPanel;
    public GameObject botCon;
    public Animator guard1, guard2;

    [Header("Spawn Pos")]
    public Transform[] spawnPos;

    [Header("UI")]
    public GameObject joystick;
    public GameObject losePanel;
    public GameObject startPanel;
    public GameObject gameplayPanel;
    public GameObject boosterPanel;

    [Header("UI Objects")]
    public GameObject green;
    public GameObject red;
    public GameObject clickStart;

    private void Awake()
    {
        Application.targetFrameRate = 120;
        if (instance == null)
        {
            instance = this;
        }

        switch (difficulty)
        {
            case Difficulty.Easy:
                timer = 60;
                break;
            case Difficulty.Medium:
                timer = 45;
                break;
            case Difficulty.Hard:
                timer = 30;
                break;
        }

        Vector3 camoffset = Camera.main.transform.position - player.transform.position;
        player.transform.position = spawnPos[Random.Range(0, 10)].position;
        Camera.main.transform.position = player.transform.position + camoffset;
        CursorAnim();
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
    public void CursorAnim()
    {
        clickStart.transform.DORotate(new Vector3(0, 0, 40), 1f).OnComplete(() => clickStart.transform.DORotate(new Vector3(0, 0, 20), 1f).OnComplete(()=> CursorAnim()));
    }
    public void Restart()
    {
        DOTween.KillAll();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void CloseBoosterPanel()
    {
        boosterPanel.SetActive(false);
    }
    public void Adrenaline()
    {
        player.speed *= 2;
        boosterPanel.SetActive(false);
    }
    public void Shield()
    {
       
        boosterPanel.SetActive(false);
        player.hasShield = true;
        player.shield.SetActive(true);
        player.ShieldRotate();
    }
}
