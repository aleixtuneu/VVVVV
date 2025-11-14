using UnityEngine;

public class JumpBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float jumpForce;
    private float _originalGravityScale;
    private bool _isGravityInverted = false;
    private SpriteRenderer _spriteRenderer;
    private Character _character;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _character = GetComponent<Character>();

        if (_rb != null)
        {
            _originalGravityScale = Mathf.Abs(_rb.gravityScale); // Guardar gravetat "normal"
            _rb.gravityScale = _originalGravityScale;
        }
    }

    public void Jump()
    {
        if (_rb != null)
        {
            // Si pot invertir la gravetat
            if (_rb != null && _character != null && _character.CanInvertGravity())
            {
                // Si la gravetat està normal, invertir al saltar. Si està invertida, posar normal.
                if (!_isGravityInverted)
                {
                    _rb.gravityScale = -_originalGravityScale; // Invertir gravetat
                    _isGravityInverted = true;

                    _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Força de salt 

                    // Girar sprite
                    if (_spriteRenderer != null)
                    {
                        _spriteRenderer.flipY = true;
                    }

                    if (_character != null)
                    {
                        _character.SetGravityInvertedState(true);
                        _character.SetCanInvertGravity(false);
                    }
                }
                else
                {
                    _rb.gravityScale = _originalGravityScale;
                    _isGravityInverted = false;

                    // Girar sprite
                    if (_spriteRenderer != null)
                    {
                        _spriteRenderer.flipY = false;
                    }

                    // gravetat normal
                    if (_character != null)
                    {
                        _character.SetGravityInvertedState(false);
                        _character.SetCanInvertGravity(false);
                    }
                }

                // Netejar velocitat vertical
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0);
            }    
        }
    }

    public void ResetGravityToNormal()
    {
        if (_rb != null && _isGravityInverted) // Només si està invertida
        {
            _rb.gravityScale = _originalGravityScale;
            _isGravityInverted = false;

            if (_spriteRenderer != null)
            {
                _spriteRenderer.flipY = false;
            }

            // Resetejar Gravetat normal
            if (_character != null)
            {
                _character.SetGravityInvertedState(false);
            }
        }
    }
}
