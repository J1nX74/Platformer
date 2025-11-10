using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] float targetDistance, speed = 100f;
    SpriteRenderer sr;
    Rigidbody2D rb;
    Vector3 startPoint;
    PlayerController target;


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        target = FindFirstObjectByType<PlayerController>();
        sr.sprite = sprites[1];
    }


    void Update()
    {
        if (targetDistance > Vector2.Distance(gameObject.transform.position, target.transform.position)
            && System.Math.Abs(gameObject.transform.position.y - target.transform.position.y) < 5)
        {
            //print("близко");

            sr.sprite = sprites[0];
            int direction = 1;
            if (target.transform.position.x < gameObject.transform.position.x) direction = -1;
            rb.linearVelocity = new Vector2(speed * direction * Time.deltaTime, rb.linearVelocity.y);
        }
        else if (0.05f > Vector2.Distance(gameObject.transform.position, startPoint))
        {
            //print("далеко, но на стартовой точке");
            rb.linearVelocity = new Vector2(0, 0);
            sr.sprite = sprites[1];
        }
        else
        {
           // print("далеко от всех, возвращаюсь на старт");
            int direction = -1;
            if (gameObject.transform.position.x < startPoint.x)
            {
                direction = 1;
            }
            rb.linearVelocity = new Vector2(speed * direction * Time.deltaTime, rb.linearVelocity.y);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Ground" && startPoint == Vector3.zero)
        {
            startPoint = gameObject.transform.position;
        }
    }
}
