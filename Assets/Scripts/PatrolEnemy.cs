using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f; // Velocitat de moviment de l'enemic
    [SerializeField] private Transform patrolPointA; // Primer punt de patrulla
    [SerializeField] private Transform patrolPointB; // Segon punt de patrulla
    [SerializeField] private float arrivalThreshold = 0.1f; // Distancia per considerar que ha arribat al punt

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;

    private Vector2 _targetPoint; // punt al que es dirigeix l'enemic
    private bool _movingRight = true; // direcci� de moviment

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        // Inicialitzar al punt A
        _targetPoint = patrolPointA.position;
        transform.position = patrolPointA.position;
        UpdateDirection(); // Establir direcci�n inicial de l'sprite
    }

    void FixedUpdate()
    {
        // Moure l'enemic fins el punt objectiu
        Vector2 direction = (_targetPoint - (Vector2)transform.position).normalized;
        _rb.linearVelocity = new Vector2(direction.x * speed, _rb.linearVelocity.y);

        // Comprovar si ha arribat al punt
        if (Vector2.Distance(transform.position, _targetPoint) < arrivalThreshold)
        {
            // Canviar punt
            if (_targetPoint == (Vector2)patrolPointA.position)
            {
                _targetPoint = patrolPointB.position;
                _movingRight = true;
            }
            else
            {
                _targetPoint = patrolPointA.position;
                _movingRight = false;
            }
            UpdateDirection(); // Actualizar sprite perqu� miri a la nova direcci�
        }
    }

    // Actualitza la direcci� de l'sprite segons _movingRight
    void UpdateDirection()
    {
        if (_spriteRenderer != null)
        {
            if (_movingRight)
            {
                _spriteRenderer.flipX = false; // Dreta
            }
            else
            {
                _spriteRenderer.flipX = true; // Esquerra
            }
        }
    }

    // Colisions
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.Die(); // Eliminar jugador
            }
        }
    }

    // Visualitzar punts de patrulla i direcci� a l'editor
    void OnDrawGizmos()
    {
        if (patrolPointA != null && patrolPointB != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(patrolPointA.position, patrolPointB.position);
            Gizmos.DrawWireSphere(patrolPointA.position, 0.2f);
            Gizmos.DrawWireSphere(patrolPointB.position, 0.2f);
        }

        if (_rb != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * (_spriteRenderer.flipX ? -1 : 1) * 0.5f);
        }
    }
}
