using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Vector2 _direction;
    private Rigidbody2D _rb;

    private ObjectPool _objectPool;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Activar fletxa
    public void SetDirectionAndActivate(Vector2 direction, ObjectPool pool)
    {
        _direction = direction.normalized; // Vector normalitzat
        _objectPool = pool;
        gameObject.SetActive(true); // Activar 
        // Rotar sprite de la fletxa perque apunti en la direcció de movimient
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void FixedUpdate()
    {
        if (_rb != null)
        {
            _rb.MovePosition(_rb.position + _direction * speed * Time.fixedDeltaTime);
        }
    }

    // Colisions
    public void OnTriggerEnter2D(Collider2D other)
    {
        // Si colisiona con el jugador
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Die(); // Eliminar jugador
            }
            ReturnToPool(); // Retorna al pool
        }
        // Si colisiona amb l'entorn
        else if (!other.CompareTag("Banana") && !other.CompareTag("Spikes")) // Ignorar bananas i punxes
        {
            ReturnToPool(); // Retorna al pool
        }
    }

    public void ReturnToPool()
    {
        if (_objectPool != null)
        {
            _objectPool.ReturnArrow(this);
        }
    }
}
