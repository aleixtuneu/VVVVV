using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class MoveBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float speed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        _rb.linearVelocity = new Vector2(direction.x * speed, _rb.linearVelocityY);
    }
}
