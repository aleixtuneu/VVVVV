using UnityEngine;

public class JumpBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float jumpForce;
    private float _originalGravityScale; // Gravetat original (positiva)
    private bool _isGravityInverted = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        if (_rb != null)
        {
            _originalGravityScale = Mathf.Abs(_rb.gravityScale); // Guardar gravetat "normal"
            _rb.gravityScale = _originalGravityScale;
        }
    }

    /*
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    */

    public void Jump()
    {
        if (_rb != null)
        {
            // Si la gravetat està normal, invertir al saltar. Si està invertida, posar normal.
            if (!_isGravityInverted)
            {
                _rb.gravityScale = -_originalGravityScale; // Invertir gravetat
                _isGravityInverted = true;
                _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Força de salt 
            }
            else
            {
                _rb.gravityScale = _originalGravityScale;
                _isGravityInverted = false;
            }

            // Opcional: limpiar la velocidad vertical para un cambio más "limpio"
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0);
        }

    }

    public void ResetGravityToNormal()
    {
        if (_rb != null && _isGravityInverted) // Només si està invertida
        {
            _rb.gravityScale = _originalGravityScale;
            _isGravityInverted = false;
            //Debug.Log("Gravity normal.");
        }
    }
}
