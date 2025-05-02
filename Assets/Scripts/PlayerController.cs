using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    SpriteRenderer sp;
    Rigidbody2D rb;
    Animator animator;
    [Header("Движение")]
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpDistance = 1.5f;
    [SerializeField] Transform legs;
    [SerializeField] LayerMask maskGround;
    Vector2 startPoint;
    float radiusLegs = 0.04f;
    bool isDead = false;
    bool isJump = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        startPoint = transform.position;
    }

 
    void GetDamage()
    {
        transform.position = startPoint;
        GameManager.instance.RemoveLive();
        animator.SetBool("IsRun", false);
        animator.SetBool("IsJump", false);
        rb.linearVelocity = new Vector2(0, 0);
    }

    void Update()
    {
        if (!isDead)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                animator.SetBool("IsRun", true);
                sp.flipX = true;

                rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
            }

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                animator.SetBool("IsRun", true);
                sp.flipX = false;

                rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
            {
                animator.SetBool("IsRun", false);
                rb.linearVelocity = Vector2.zero;
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                Jump(jumpDistance);
            }

            
        }
    }

    private void FixedUpdate()
    {
       OnGround(!Physics2D.OverlapCircle(legs.position,radiusLegs,maskGround));
    }

    void OnGround(bool onGround)
    {
        isJump = onGround;
        animator.SetBool("IsJump", onGround);   
    }

    void Jump(float distance)
    {
        if (!isJump)
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(Vector2.up * speed * jumpDistance, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            GameManager.instance.AddGold();
            Destroy(collision.gameObject);
        }


        if (collision.CompareTag("Enemy"))
        {
            GetDamage();
        }
    }

    IEnumerator DeadPause()
    {

    }
}

