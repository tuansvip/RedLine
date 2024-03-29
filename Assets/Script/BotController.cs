using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    public bool die;
    public bool win;
    public bool isMoving;
    public Vector3 dir;
    public float speed;
    public Rigidbody rb;
    public enum Gender
    {
        Male,
        Female
    }
    public Gender gender;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public IEnumerator Move()
    {
        dir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(5, 10));
        dir.Normalize();
        speed = Random.Range(12, 20);
        isMoving = true;
        float timeToMove = 5 / RedLightManager.instance.enemy.a;
        yield return new WaitForSeconds(Random.Range(timeToMove - 1, timeToMove));
        isMoving = false;
    }
    private void FixedUpdate()
    {
        if (RedLightManager.instance.paused || die || win) return;
        if (isMoving)
        {
            if (rb.velocity.magnitude < 1.8f)
                rb.AddForce(dir * speed, ForceMode.Force);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedLine"))
        {
            win = true;
            Debug.Log("Bot win");
            transform.DORotate(new Vector3(0, 180, 0), 0.5f);
            Animator animator = GetComponent<Animator>();
            animator.Play("Win");
        }
    }
    public IEnumerator Died()
    {
        if (!win)
        {
            yield return new WaitForSeconds(Random.Range(0.2f,1f));
            if (Random.Range(0, 2) == 1) SFX.instance.PlayShot1(); else SFX.instance.PlayShot2();
            yield return new WaitForSeconds(0.5f);
            RedLightManager.instance.playerCounters--;
            if (gender == Gender.Male)  SFX.instance.PlayMaleScream(); else SFX.instance.PlayFemaleScream();
            Animator anim = GetComponent<Animator>();
            anim.Play("Shot");
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }
    }
}
