using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float standTimer;
    public Vector2 direction;
    public Vector3 touchPos;
    public bool isMoving = false;
    public bool win = false;
    public bool lose = false;
    public Rigidbody rb;
    public float speeder;
    public GameObject joystick;
    public RectTransform joystickBack;
    public RectTransform joystickHandle;
    Animator anim;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        joystick.GetComponent<RectTransform>().anchorMax = Vector2.zero;
        joystick.GetComponent<RectTransform>().anchorMin = Vector2.zero;
    }

    private void Update()
    {
        if (win || lose) return;
        if (!isMoving && RedLightManager.instance.started)
        {
            standTimer += Time.deltaTime;
        }
        if (standTimer > 15 || RedLightManager.instance.timer <= 0)
        {
            lose = true;
            StartCoroutine(Lose());
        }
        if (Input.touchCount > 0)
        {
            standTimer = 0;
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchPos = joystickBack.position;
                    isMoving = true;
                    break;
                case TouchPhase.Moved:
                    if ((joystickHandle.position - touchPos).magnitude> 70 )
                    {
                        direction.x = (joystickHandle.position - touchPos).x;
                        direction.y = (joystickHandle.position - touchPos).y;
                    }
                    direction.Normalize();
                    break;
                case TouchPhase.Ended:
                    isMoving = false;
                    direction = Vector2.up;
                    break;
            }
        }
    }
    private void FixedUpdate()
    {
        if (lose || win || !RedLightManager.instance.started) return;
        speeder = rb.velocity.magnitude;
        if (isMoving && !win)
        {
            if (speeder < 1.5f)
            {
                rb.AddForce(new Vector3(direction.x, 0, direction.y) * speed, ForceMode.Force);
            }
            
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedLine"))
        {
            win = true;
            RedLightManager.instance.paused = true;
            Debug.Log("You win");
            anim.Play("Win");
            SFX.instance.PlayWin();
            transform.DORotate(new Vector3(0, 180, 0), 0.5f);
            transform.GetChild(0).DORotate(new Vector3(0, 0, 0), 0.5f);
            RedLightManager.instance.fxWin.Play();
            StartCoroutine(Win());
            
        }
    }
    public IEnumerator Win()
    {
        yield return new WaitForSeconds(3f);
        RedLightManager.instance.endPanel.ShowPanel(true);
    }
    public IEnumerator Lose()
    {
        if (win) yield break;
        lose = true;
        anim.Play("Shot");
        if (Random.Range(0, 2) == 1) SFX.instance.PlayShot1(); else SFX.instance.PlayShot2();
        yield return new WaitForSeconds(0.5f);
        RedLightManager.instance.playerCounters--;
        SFX.instance.PlayMaleScream();
        yield return new WaitForSeconds(3);
        SFX.instance.PlayLose();
        RedLightManager.instance.joystick.SetActive(false);
        RedLightManager.instance.gameplayPanel.SetActive(false);
        RedLightManager.instance.losePanel.GetComponent<RectTransform>()
            .DOMoveY(GameObject.Find("Center").GetComponent<RectTransform>().position.y, 0.5f)
            .OnComplete(() =>
            {
                RedLightManager.instance.paused = true;
                Time.timeScale = 0;
            });
    }
}
