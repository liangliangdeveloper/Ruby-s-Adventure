using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制角色移动、生命、动画等
/// </summary>
public class PlayerController : MonoBehaviour
{

    public float speed = 5f;

    private int maxHealth = 5;

    private int currentHealth;

    public int myMaxHealth { get { return maxHealth; } }

    public int myCurrentHealth { get { return currentHealth; } }

    private float invincibleTime = 2f; //无敌时间

    private float invincibleTimer; //无敌计时器

    private bool isInvincible;  //是否无敌

    private Vector2 faceDirection = new Vector2(1,0);

    private Animator animator;

    public GameObject BulletPrefeb;

    Rigidbody2D rbody;

    public AudioClip hitClip;

    public AudioClip launchClip;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 3;
        invincibleTimer = 0;
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        UImanager.instance.updateHealthbar(currentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 moveDirection = new Vector2(moveX, moveY);
        if (moveDirection.x != 0 || moveDirection.y != 0)
        {
            faceDirection = moveDirection;
        }

        animator.SetFloat("Look X", faceDirection.x);
        animator.SetFloat("Look Y", faceDirection.y);
        animator.SetFloat("Speed", moveDirection.magnitude);

        Vector2 position = rbody.position;
        position.x += moveX * speed * Time.deltaTime;
        position.y += moveY * speed * Time.deltaTime;
        rbody.MovePosition(position);

        //无敌
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("Launch");
            GameObject bullet = Instantiate(BulletPrefeb, rbody.position, Quaternion.identity);
            BulletController bc = bullet.GetComponent<BulletController>();
            if (bc != null)
            {
                bc.moveBullet(faceDirection, 300);
                Audiomanager.instance.audioPlay(launchClip);
            }
        }
    }

    public void changeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            Audiomanager.instance.audioPlay(hitClip);
            animator.SetTrigger("Hit");
            invincibleTimer = invincibleTime;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UImanager.instance.updateHealthbar(currentHealth, maxHealth);
    }
}
