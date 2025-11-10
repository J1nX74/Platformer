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
    float radiusLegs = 0.1f;
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

        isDead = true;
        transform.position = startPoint;
        GameManager.instance.RemoveLive();
        animator.SetBool("IsRun", false);
        animator.SetBool("IsJump", false);
        rb.linearVelocity = new Vector2(0, 0);

        StartCoroutine(DeadPause());
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

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump(jumpDistance);
            }

            
        }
    }

    private void FixedUpdate()
    {
        OnGround(CheckGround());
    }

    private bool CheckGround()
    {
        float edgeOffset = 0.3f;

        Vector2 leftEdge = legs.position + Vector3.left * edgeOffset;
        Vector2 rightEdge = legs.position + Vector3.right * edgeOffset;

        RaycastHit2D hitLeft = Physics2D.Raycast(leftEdge, Vector2.down, 0.5f, maskGround);
        RaycastHit2D hitRight = Physics2D.Raycast(rightEdge, Vector2.down, 0.5f, maskGround);

        bool isValidGroundLeft = hitLeft.collider != null && Vector2.Angle(hitLeft.normal, Vector2.up) < 46f;
        bool isValidGroundRight = hitRight.collider != null && Vector2.Angle(hitRight.normal, Vector2.up) < 46f;

        bool isOnGround = isValidGroundLeft || isValidGroundRight;
        return isOnGround;
    }

    private void OnDrawGizmos()
    {
        if (legs != null)
        {
            float edgeOffset = 0.3f;
            Vector2 leftEdge = legs.position + Vector3.left * edgeOffset;
            Vector2 rightEdge = legs.position + Vector3.right * edgeOffset;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(leftEdge, leftEdge + Vector2.down * 0.5f);
            Gizmos.DrawLine(rightEdge, rightEdge + Vector2.down * 0.5f);
        }
    }

    void OnGround(bool onGround)
    {
        isJump = !onGround;  
        animator.SetBool("IsJump", !onGround);
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
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        Color startColor = Color.white;
        Color blinkColor = new Color(0.3f, 0.4f, 0.6f,0.3f);

        for (int i = 0; i < 3; i++)
        {
            sp.color = blinkColor;
            yield return new WaitForSeconds(0.2f);
            sp.color = startColor;
            yield return new WaitForSeconds(0.2f);
        }

        isDead = false;
        GameManager.instance.HideInfoText();  
    }
}

