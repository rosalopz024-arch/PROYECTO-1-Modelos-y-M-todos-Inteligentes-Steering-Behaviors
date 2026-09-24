using UnityEngine;

public class WandererAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetNewDirection();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            GetNewDirection();
        }
    }

    void FixedUpdate()
    {
        // Mueve al personaje usando físicas, lo que hace que respete las paredes y colliders
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    void GetNewDirection()
    {
        moveDirection = Random.insideUnitCircle.normalized;
        timer = Random.Range(2f, 4f);
    }
}