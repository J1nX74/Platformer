using UnityEngine;

public class MaceScript : MonoBehaviour
{
    [SerializeField] int speed = 2;
    Vector2 startPoint;
    Rigidbody2D rb;
    int direction = 1;

    void Start()
    {
        startPoint = gameObject.transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.right * direction * speed * 10, ForceMode2D.Impulse);
    }

    void Update()
    {
        if (Vector2.Distance(startPoint,gameObject.transform.position) < 0.2f)
        {
            print("√де вы были 8 лет");
            direction *= -1;
            rb.AddRelativeForce(Vector2.right * direction * speed, ForceMode2D.Impulse);
        }
    }
}
