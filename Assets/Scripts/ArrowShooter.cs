using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    [SerializeField] private float shootInterval = 2f; // Temps entre fletxa i fletxa
    [SerializeField] private Vector2 shootDirection = Vector2.left; // Direcció per defecte
    [SerializeField] private Transform firePoint;

    private float _timer;

    void Start()
    {
        _timer = shootInterval;
        if (firePoint == null)
        {
            firePoint = transform; // Si no hay punto de disparo, usa la posición del propio GameObject
            Debug.LogWarning("ArrowShooter: Fire Point no asignado, usando la posición del GameObject.", this);
        }

        // Si la dirección inicial es (0,0), asigna una dirección por defecto para evitar errores
        if (shootDirection == Vector2.zero)
        {
            shootDirection = Vector2.left;
            Debug.LogWarning("ArrowShooter: Shoot Direction es cero, asignado a Vector2.left.", this);
        }
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            ShootArrow();
            _timer = shootInterval;
        }
    }

    void ShootArrow()
    {
        if (ObjectPool.Instance == null)
        {
            Debug.LogError("ArrowShooter: No hay instancia de ObjectPool en la escena.", this);
            return;
        }

        Arrow arrow = ObjectPool.Instance.GetArrow(); // Obtener una flecha del pool
        if (arrow != null)
        {
            arrow.transform.position = firePoint.position; // Posicionar en el punto de disparo
            arrow.SetDirectionAndActivate(shootDirection, ObjectPool.Instance); // Establecer dirección y activarla
        }
    }

    // Opcional: Visualizar la dirección de disparo en el editor
    void OnDrawGizmos()
    {
        if (firePoint != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(firePoint.position, (Vector2)firePoint.position + shootDirection.normalized * 1f);
            Gizmos.DrawSphere((Vector2)firePoint.position + shootDirection.normalized * 1f, 0.1f);
        }
        else
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + shootDirection.normalized * 1f);
            Gizmos.DrawSphere((Vector2)transform.position + shootDirection.normalized * 1f, 0.1f);
        }
    }
}
