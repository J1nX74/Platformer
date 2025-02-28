using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    SpriteRenderer sp;
    Rigidbody2D rb;
    Animator animator;
    [Header("Движение")]
    [SerializeField] float speed = 5f;
    bool isDead = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
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

            
        }
    }
}
