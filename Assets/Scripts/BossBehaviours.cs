using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossBehaviours : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] private float speed;

    [Header("Attack Settings")]
    [SerializeField] private Transform weapon;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float coolDown;

    [Header("State Settings")]
    [SerializeField] private float waitTimeIdle;
    [SerializeField] private float waitTimeRoll;
    [SerializeField] private float waitTimeShoot;
    [SerializeField] private float waitTimeWait;

    public enum State
    {
        Idle,
        Roll,
        Shoot          
    }

    public State currentState;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(Actions());
    }

    void Update()
    {
        if (currentState == State.Roll)
        {
            Roll();
        }
        
     

    }

    void Roll()
    {
        transform.Translate(Vector2.left *  speed * Time.deltaTime);
    }

    void Turn()
    {
        if(transform.position.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
        else if (transform.position.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
    }

    IEnumerator Shooting()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(bullet, weapon.position, weapon.rotation);
            yield return new WaitForSeconds(coolDown);
        }        
    }


    IEnumerator Actions()
    {
        Turn();
        currentState = State.Idle;        
        yield return new WaitForSeconds(waitTimeIdle);

        StartCoroutine(Shooting());
        yield return new WaitForSeconds(waitTimeShoot);

        currentState = State.Roll;
        yield return new WaitForSeconds(waitTimeRoll);

        StartCoroutine(Actions());                        
    }



}
