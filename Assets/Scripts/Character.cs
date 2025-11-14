using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MoveBehaviour))]
[RequireComponent(typeof(JumpBehaviour))]
[RequireComponent(typeof(Animator))]

public class Character : MonoBehaviour
{
    [SerializeField] protected MoveBehaviour _mb;
    [SerializeField] protected JumpBehaviour _jb;
    [SerializeField] protected Animator _animator;
    [SerializeField] private LayerMask groundLayer; // Capa de terra
    [SerializeField] private Transform groundCheckPoint; // Punt d'origen del raycast
    [SerializeField] protected float raycastLength = 0.3f;

    private bool _isGravityInverted = false;

    private SpriteRenderer _charSpriteRenderer;
    private float _groundCheckPointOriginalLocalY;
    //
    private bool _canInvertGravity = true;
    //

    protected virtual void Awake()
    {
        _mb = GetComponent<MoveBehaviour>();
        _jb = GetComponent<JumpBehaviour>();
        //
        _charSpriteRenderer = GetComponent<SpriteRenderer>();

        if (groundCheckPoint != null)
        {
            _groundCheckPointOriginalLocalY = groundCheckPoint.localPosition.y;
        }
        //
    }

    public void Start()
    {
        _animator = GetComponent<Animator>();

        if (_animator != null)
        {
            _mb.SetAnimator(_animator);
        }
    }

    public void Update()
    {
        // Actualitzar "IsGrounded" al MoveBehaviour
        bool currentlyGrounded = IsGrounded();
        _mb.SetGroundedState(currentlyGrounded);

        // Actualitzar "IsJumping" al Animator
        _animator.SetBool("IsJumping", !currentlyGrounded);
    }

    public void SetGravityInvertedState(bool inverted)
    {
        _isGravityInverted = inverted;

        if (groundCheckPoint != null)
        {
            Vector3 localPos = groundCheckPoint.localPosition;

            if (_isGravityInverted)
            {
                // Posició superior
                localPos.y = -_groundCheckPointOriginalLocalY;
            }
            else // Gravedad normal
            {
                // Posició inferior
                localPos.y = _groundCheckPointOriginalLocalY;
            }
            groundCheckPoint.localPosition = localPos;
        }
    }

    public bool IsGrounded()
    {
        Vector2 raycastDirection = _isGravityInverted ? Vector2.up : Vector2.down; // Seleccionar punt correcte

        // Raycast cap avall, retorna true si detecta algo
        RaycastHit2D hit = Physics2D.Raycast(groundCheckPoint.position, raycastDirection, raycastLength, groundLayer);

        Debug.Log($"Raycast Hit: {hit.collider != null} at {hit.point}");

        // Si detecta terra, resetejar salt
        if (hit.collider != null)
        {
            _canInvertGravity = true;
        }

        return hit.collider != null;
    }

    public bool CanInvertGravity()
    {
        return _canInvertGravity;
    }

    public void SetCanInvertGravity(bool canInvert)
    {
        _canInvertGravity = canInvert;
    }

    // Visualitzar raycast
    private void OnDrawGizmos()
    {
        Vector2 gizmoDirection = _isGravityInverted ? Vector2.up : Vector2.down;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheckPoint.position, (Vector2)groundCheckPoint.position + gizmoDirection * raycastLength);
    }  
}
