using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Bosstaur : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    private Rigidbody2D rigidbody2D;
    private Vector2 direction;
    private readonly Vector2 initialPosition = Vector2.zero;
    private Transform player;
    private Animator animator;
    [SerializeField] public bool attack = false;


    private void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        transform.position = initialPosition;
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        AnimationManager();
        if (player != null)
        {
            FollowPlayer();
            attack = true;
        }

        else
        {
            ReturnToInitialPosition();
        }

    }


    private void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime);

        if (player.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector2(0f, 180f);
        }
        else if (player.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector2(0f, 0f);
        }
    }

    private void ReturnToInitialPosition()
    {
        attack = false;
        transform.position = Vector2.MoveTowards(
            transform.position,
            initialPosition,
            speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerBehaviour playerBehaviour = other.GetComponentInParent<PlayerBehaviour>();

        if (playerBehaviour != null)
        {
            player = playerBehaviour.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerBehaviour playerBehaviour = other.GetComponentInParent<PlayerBehaviour>();

        if (playerBehaviour != null && player == playerBehaviour.transform)
        {
            player = null;
        }
    }
    void AnimationManager()
    {
        animator.SetInteger("pMove", (int)(Mathf.Abs(direction.x) + Mathf.Abs(direction.y)));
        animator.SetBool("pAttack", attack);
    }
    void OnMove()
    {
        rigidbody2D.linearVelocity = direction.normalized * speed;
    }
}

