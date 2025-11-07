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
    //
    [SerializeField] private LayerMask groundLayer; // Capa de terra
    [SerializeField] private Transform groundCheckPoint; // Punt d'origen del raycast
    [SerializeField] private Transform groundCheckPointInverted;
    [SerializeField] protected float raycastLength = 0.3f;

    private bool _isGravityInverted = false;
    //

    protected virtual void Awake()
    {
        _mb = GetComponent<MoveBehaviour>();
        _jb = GetComponent<JumpBehaviour>();
        //

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

    public bool IsGrounded()
    {
        Transform currentCheckPoint = _isGravityInverted ? groundCheckPointInverted : groundCheckPoint; // Seleccionar punt correcte

        // Raycast cap avall, retorna true si detecta algo
        RaycastHit2D hit = Physics2D.Raycast(groundCheckPoint.position, Vector2.down, raycastLength, groundLayer);

        Debug.Log($"Raycast Hit: {hit.collider != null} at {hit.point}");

        return hit.collider != null;
    }

    // Visualitzar raycast
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheckPoint.position, (Vector2)groundCheckPoint.position + Vector2.down * raycastLength);

        if (groundCheckPoint != null)
        {
            Gizmos.color = _isGravityInverted ? Color.gray : Color.red; // Color diferent si no es l'actiu
            Gizmos.DrawLine(groundCheckPoint.position, (Vector2)groundCheckPoint.position + Vector2.down * raycastLength);
        }

        if (groundCheckPointInverted != null)
        {
            Gizmos.color = _isGravityInverted ? Color.red : Color.gray;
            Gizmos.DrawLine(groundCheckPointInverted.position, (Vector2)groundCheckPointInverted.position + Vector2.down * raycastLength);
        }
    }

    public void SetGravityInvertedState(bool inverted)
    {
        _isGravityInverted = inverted;
    }
}
