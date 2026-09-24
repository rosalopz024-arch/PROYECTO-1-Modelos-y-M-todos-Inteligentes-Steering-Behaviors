using UnityEngine;

public class CowardAI : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    public float fleeRange = 5f; // Distancia a la que se asusta y huye
    public Transform playerTransform;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Busca al jugador automáticamente si no se lo asignamos
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                playerTransform = playerObj.transform;
        }

        GetNewDirection();
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // calcula la distancia al jugador
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= fleeRange)
            {
                // si estás cerca, huye en dirección contraria
                moveDirection = (transform.position - playerTransform.position).normalized;
            }
            else
            {
                // si en wander estás lejos, se mueve al azar
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    GetNewDirection();
                }
            }
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    void GetNewDirection()
    {
        moveDirection = Random.insideUnitCircle.normalized;
        timer = Random.Range(2f, 4f);
    }
}