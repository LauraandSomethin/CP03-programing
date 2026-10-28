using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerBehaviour : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float invencibleTime;

    private Vector2 direction;
    private bool rolling;
    private float currentInvencibleTime;
    private bool attacking;

    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Collider2D collider2D;

    [SerializeField] private GameObject ball1;
    [SerializeField] private GameObject ball2;
    [SerializeField] private GameObject ball3;
    [SerializeField] private GameObject ball4;
    [SerializeField] private GameObject ball5;

    private float life = 5;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();
    }


    void Start()
    {
        rigidbody2D.freezeRotation = true;
    }


    void Update()
    {
        Move();
        Roll();
        Attack();

        AnimationManager();
    }


    void FixedUpdate()
    {
        OnMove();
    }


    void Move()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 20f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 10f;
        }

        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0f, 180f);
        }
        else if (direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0f, 0f);
        }
    }

    void sortofattackwhilerolling()

        //eu cansei nesta parte
    {
        if (rolling == true && attacking == true)
        {
            if (life == 5)
            {
                Destroy(ball1);
            }
            if (life == 4)
            {
                Destroy(ball2);
            }
            if (life == 3)
            {
                Destroy(ball3);
            }
            if (life == 2)
            {
                Destroy(ball4);
            }
            if (life == 1)
            {
                Destroy(ball5);
            }
        }
    }

        void OnMove()
    {
        rigidbody2D.linearVelocity = direction.normalized * speed;
    }


    void Roll()
    {
        rolling = Input.GetButtonDown("Fire2") && direction.x != 0;

        if (rolling == true && currentInvencibleTime == 0)
        {
            currentInvencibleTime = invencibleTime;
            collider2D.enabled = false;
        }
        else if (currentInvencibleTime > 0)
        {
            currentInvencibleTime -= Time.deltaTime;
        }
        else if (currentInvencibleTime < 0.01f)
        {
            currentInvencibleTime = 0;
            collider2D.enabled = true;
        }
    }


    void Attack()
    {
        attacking = Input.GetButtonDown("Fire1");
    }


    void AnimationManager()
    {
        animator.SetInteger("pMove", (int)(Mathf.Abs(direction.x) + Mathf.Abs(direction.y)));
        animator.SetBool("pRoll", rolling);
        animator.SetBool("pAttack", attacking);
    }


}