using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class MoveBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float speed;
    private Vector2 _currentDirection;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    //
    private bool _isGrounded;
    //

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetAnimator(Animator animator)
    {
        _animator = animator;
    }

    public void SetGroundedState(bool grounded)
    {
        _isGrounded = grounded;
    }

    public bool IsGrounded() 
    {
        return _isGrounded;
    }

    // Establir direcció del moviment
    public void SetDirection(Vector2 direction)
    {
        _currentDirection = direction;

        if (_animator != null)
        {
            // Si la direcció horitzontal no es 0, el personatge es mou
            bool isRunning = (direction.x != 0) && _isGrounded;
            _animator.SetBool("IsRunning", isRunning);        
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
