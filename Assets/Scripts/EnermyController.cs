using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyController : MonoBehaviour
{
    public float speed = 3f;

    private Vector2 moveDirection;

    public bool isVertical;

    private float changeTime = 2f;

    private float changeTimer;

    private Rigidbody2D rbody;

    private Animator animator;

    public ParticleSystem brokenEffect;

    private bool isFixed;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        moveDirection = isVertical ? Vector2.up : Vector2.right;

        changeTimer = changeTime;

        isFixed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFixed) return;

        changeTimer -= Time.deltaTime;
        if(changeTimer < 0)
        {
            moveDirection *= -1;
            changeTimer = changeTime;
        }
        Vector2 position = rbody.position;
        position.x += moveDirection.x * speed * Time.deltaTime;
        position.y += moveDirection.y * speed * Time.deltaTime;
        rbody.MovePosition(position);
        animator.SetFloat("moveX", moveDirection.x);
        animator.SetFloat("moveY", moveDirection.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.changeHealth(-1);
        }
    }

    public void Fixed()
    {
        isFixed = true;
        if (brokenEffect.isPlaying)
        {
            brokenEffect.Stop();
        }
        rbody.simulated = false;
        animator.SetTrigger("fixed");
    }
}
