using UnityEngine;

public class ChaserAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectionRange = 5f; // Distancia a la que empieza a perseguirte
    public Transform playerTransform; // Acá vamos a arrastrar al Player

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Si olvidaste arrastrar al Player desde Unity, lo busca solo por el nombre
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
            // distancia entre el ninja y el jugador
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= detectionRange)
            {
                // si estás cerca, persigue directo al jugador
                moveDirection = (playerTransform.position - transform.position).normalized;
            }
            else
            {
                // si estás lejos, se mueve al azar
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
        // se mueve al ninja usando físicas para que respete las paredes igual que el mago
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    void GetNewDirection()
    {
        moveDirection = Random.insideUnitCircle.normalized;
        timer = Random.Range(2f, 4f);
    }
}