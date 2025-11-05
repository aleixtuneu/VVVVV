using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class MoveBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float speed;
    private Vector2 _currentDirection;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //
        // --- DEPURACIÓN: Verifica SpriteRenderer ---
        if (_spriteRenderer == null)
        {
            Debug.LogWarning("MoveBehaviour.Awake(): SpriteRenderer component is NULL on this GameObject!", this);
        }
        //
    }

    public void SetAnimator(Animator animator)
    {
        _animator = animator;
        // --- DEPURACIÓN: Confirma que el Animator fue recibido ---
        if (_animator == null)
        {
            Debug.LogError("MoveBehaviour.SetAnimator(): Received a NULL Animator reference!", this);
        }
        else
        {
            Debug.Log("MoveBehaviour.SetAnimator(): Animator reference successfully received. (Inside MoveBehaviour)", this);
        }
    }

    // Establir direcció del moviment
    public void SetDirection(Vector2 direction)
    {
        _currentDirection = direction;

        if (_animator != null)
        {
            // Si la direcció horitzontal no es 0, el personatge es mou
            bool isRunning = (direction.x != 0);
            _animator.SetBool("IsRunning", isRunning);
            // --- DEPURACIÓN: Muestra el valor de IsRunning ---
            Debug.Log($"MoveBehaviour.SetDirection(): Setting IsRunning to {isRunning} (direction.x: {direction.x})", this);
        }
        else
        {
            // --- DEPURACIÓN: Si el Animator es nulo, reporta ---
            Debug.LogWarning("MoveBehaviour.SetDirection(): Animator is NULL, cannot set 'IsRunning' parameter.", this);
        }

        // Girar sprite segons direccio
        if (_spriteRenderer != null)
        {
            if (direction.x > 0) // Dreta
            {
                _spriteRenderer.flipX = false;
            }
            else if (direction.x < 0) // Esquerra
            {
                _spriteRenderer.flipX = true;
            }
        }
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_currentDirection.x * speed, _rb.linearVelocity.y);
    }

    /*
    public void Move(Vector2 direction)
    {
       _rb.linearVelocity = new Vector2(direction.x * speed, _rb.linearVelocityY);
    }
    */
}
