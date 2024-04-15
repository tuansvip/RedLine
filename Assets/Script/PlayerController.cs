using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace phamtuan
{

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
        public GameObject joystick;
        public RectTransform joystickBack;
        public RectTransform joystickHandle;
        Animator anim;
        public Transform centerPoint;
        public float joystickmag;
        public float velo;


        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            if (win || lose) return;
            if ( RedLineManager.instance.timer <= 0)
            {
                lose = true;
                StartCoroutine(Lose());
            }
            velo = rb.velocity.magnitude;
    /*        if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        touchPos = joystickBack.position;
                        isMoving = true;
                        break;
                    case TouchPhase.Moved:
                        if ((joystickHandle.position - touchPos).magnitude > 70)
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
            }*/

            rb.interpolation = RigidbodyInterpolation.Interpolate;
        }


        private void FixedUpdate()
        {
            if (lose || win || !RedLineManager.instance.started) return;
            joystickmag = joystick.GetComponent<FloatingJoystick>().Direction.magnitude;
            if (joystick.transform.GetChild(0).gameObject.activeInHierarchy && !win)
            {
                if (joystickmag > 0.3f)
                {
                    touchPos = joystickBack.position;

                    direction = joystick.GetComponent<FloatingJoystick>().Direction;
                }
                else
                {
                    direction = Vector2.up;
                }
                rb.velocity = (new Vector3(direction.x, 0, direction.y)).normalized * speed;            
            } else if (!joystick.transform.GetChild(0).gameObject.activeInHierarchy)
            {
                rb.velocity = Vector3.zero;
            }

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("RedLine"))
            {
                Debug.Log("cc");
                win = true;
                RedLineManager.instance.paused = true;
                RedLineManager.instance.x2SpeedBtn.SetActive(false);
                RedLineManager.instance.gameplayPanel.SetActive(false);
                Debug.Log("You win");
                anim.Play("Win");

                SFX.instance.StopAll();
                SFX.instance.PlayWin();
                transform.DORotate(new Vector3(0, 180, 0), 0.5f);
                StopAllCoroutines();
                StartCoroutine(Win());
                
            }
        }
        public IEnumerator Win()
        {
            yield return new WaitForSeconds(3f);
            RedLineManager.instance.endPanel.ShowPanel(true);
        
        }
        public IEnumerator Lose()
        {
            lose = true;
            anim.Play("Shot");
            if (Random.Range(0, 2) == 1) RedLineManager.instance.guard1.Play("Shot"); else RedLineManager.instance.guard2.Play("Shot");
            if (Random.Range(0, 2) == 1) SFX.instance.PlayShot1(); else SFX.instance.PlayShot2();
            yield return new WaitForSeconds(0.5f);
            RedLineManager.instance.playerCounters--;
            SFX.instance.PlayMaleScream();
            if (!win)
            {
                yield return new WaitForSeconds(3);
                SFX.instance.StopAll();
                SFX.instance.PlayLose();
                RedLineManager.instance.joystick.SetActive(false);
                RedLineManager.instance.gameplayPanel.SetActive(false);
                RedLineManager.instance.x2SpeedBtn.SetActive(false);
                RedLineManager.instance.losePanel.transform
                    .DOMoveY(centerPoint.position.y, 0.5f)
                    .OnComplete(() =>
                    {
                        RedLineManager.instance.paused = true;
                        Time.timeScale = 0;
                        DOTween.KillAll();
                    });
            }
        }

    }
}
