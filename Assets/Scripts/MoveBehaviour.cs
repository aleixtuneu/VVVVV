using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class MoveBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float speed;
    private Vector2 _currentDirection;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private bool _isGrounded;
    //
    private float _lastInputX = 0f; // Última direcció horitzontal
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
        //
        bool wasGrounded = _isGrounded;
        //
        _isGrounded = grounded;

        if (!wasGrounded && _isGrounded)
        {
            UpdateRunningAnimation();
        }
    }

    public bool IsGrounded() 
    {
        return _isGrounded;
    }

    // Establir direcció del moviment
    public void SetDirection(Vector2 direction)
    {
        _currentDirection = direction;
        //
        _lastInputX = direction.x; // Últim input horitzontal

        if (_lastInputX != 0 && _spriteRenderer != null)
        {
            if (_lastInputX > 0) // Dreta
            {
                _spriteRenderer.flipX = false;
            }
            else if (_lastInputX < 0) // Esquerra
            {
                _spriteRenderer.flipX = true;
            }
        }

        UpdateRunningAnimation();
        //

        /*
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
        */
    }
    //
    private void UpdateRunningAnimation()
    {
        if (_animator != null)
        {
            bool isRunning = (Mathf.Abs(_lastInputX) > 0.01f) && _isGrounded;
            _animator.SetBool("IsRunning", isRunning);
        }
    }
    //
    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_currentDirection.x * speed, _rb.linearVelocity.y);
    }
}
