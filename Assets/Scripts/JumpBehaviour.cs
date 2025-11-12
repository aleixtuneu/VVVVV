using UnityEngine;

public class JumpBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float jumpForce;
    private float _originalGravityScale;
    private bool _isGravityInverted = false;
    private SpriteRenderer _spriteRenderer;
    //
    private Character _character;
    //

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //
        _character = GetComponent<Character>();

        if (_character == null)
        {
            Debug.LogError("[JumpBehaviour.Awake] ¡ERROR! No se pudo encontrar el componente Character en el mismo GameObject.", this);
        }
        //

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
            // Si la gravetat està normal, invertir al saltar. Si està invertida, posar normal.
            if (!_isGravityInverted)
            {
                Debug.Log("[JumpBehaviour.Jump] Saltando e invirtiendo gravedad.");

                _rb.gravityScale = -_originalGravityScale; // Invertir gravetat
                _isGravityInverted = true;

                _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Força de salt 

                // Girar sprite
                if (_spriteRenderer != null)
                {
                    _spriteRenderer.flipY = true;
                }

                //
                if (_character != null)
                {
                    _character.SetGravityInvertedState(true); // <--- ESTO DEBE SER LLAMADO
                    Debug.Log("[JumpBehaviour.Jump] Llamando a Character.SetGravityInvertedState(true)");
                }
                //
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
                    //
                    Debug.Log("[JumpBehaviour.Jump] Llamando a Character.SetGravityInvertedState(false)");
                    //
                }
            }

            // Netejar velocitat vertical
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

            if (_spriteRenderer != null)
            {
                _spriteRenderer.flipY = false;
            }

            // Gravetat normal
            if (_character != null)
            {
                _character.SetGravityInvertedState(false);
                //
                Debug.Log("[JumpBehaviour.ResetGravityToNormal] Llamando a Character.SetGravityInvertedState(false)");
                //
            }
        }
    }
}
