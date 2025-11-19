using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f; // Velocitat de moviment de l'enemic
    [SerializeField] private Transform patrolPointA; // Punt A
    [SerializeField] private Transform patrolPointB; // Punt B
    [SerializeField] private float arrivalThreshold = 0.1f; // Distancia per considerar que ha arribat al punt

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;

    private Vector2 _worldPatrolPointA; // Posició global punt A
    private Vector2 _worldPatrolPointB; // Posició global punt B
    private Vector2 _targetPoint; // Punt on es dirigeix
    private bool _movingRight; // Direcció de moviment

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        // Obtenir posicions
        _worldPatrolPointA = patrolPointA.position;
        _worldPatrolPointB = patrolPointB.position;

        // Comprovar que el punt A està a l'esquerra del punt B
        if (_worldPatrolPointA.x > _worldPatrolPointB.x)
        {
            // Intercanviar
            Vector2 temp = _worldPatrolPointA;
            _worldPatrolPointA = _worldPatrolPointB;
            _worldPatrolPointB = temp;
        }

        _targetPoint = _worldPatrolPointA;
        // Colocar enemic en la posició inicial
        transform.position = _worldPatrolPointA;
        UpdateDirection(); // Direcció inicial de l'sprite
    }

    void FixedUpdate()
    {
        // Moure cap al punt objectiu
        Vector2 direction = (_targetPoint - (Vector2)transform.position).normalized;
        _rb.linearVelocity = new Vector2(direction.x * speed, _rb.linearVelocity.y);

        // Comprobar si ha arribat al punt
        if (Vector2.Distance(transform.position, _targetPoint) < arrivalThreshold)
        {
            // Canviar a l'altre punt
            if (_targetPoint == _worldPatrolPointA)
            {
                _targetPoint = _worldPatrolPointB;
            }
            else
            {
                _targetPoint = _worldPatrolPointA;
            }

            // Si està a la dreta de la posició actual, _movingRight = true
            _movingRight = (_targetPoint.x > transform.position.x);

            UpdateDirection(); // Actualizar direcció de l'sprite
        }
    }

    // Actualizar direcció de l'sprite
    void UpdateDirection()
    {
        if (_spriteRenderer != null)
        {
            _spriteRenderer.flipX = _movingRight;
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

    // Visualizar punts i direcció
    void OnDrawGizmos()
    {
        Vector2 gizmoPointA = (Application.isPlaying && patrolPointA != null) ? _worldPatrolPointA : (patrolPointA != null ? (Vector2)patrolPointA.position : (Vector2)transform.position - Vector2.right);
        Vector2 gizmoPointB = (Application.isPlaying && patrolPointB != null) ? _worldPatrolPointB : (patrolPointB != null ? (Vector2)patrolPointB.position : (Vector2)transform.position + Vector2.right);

        if (patrolPointA != null && patrolPointB != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(gizmoPointA, gizmoPointB);
            Gizmos.DrawWireSphere(gizmoPointA, 0.2f);
            Gizmos.DrawWireSphere(gizmoPointB, 0.2f);
        }

        if (_rb != null && _spriteRenderer != null)
        {
            Gizmos.color = Color.red;

            Vector2 forwardDir = _spriteRenderer.flipX ? Vector2.left : Vector2.right;
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + forwardDir * 0.5f);
        }
    }
}
