using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character, InputSystem_Actions.IPlayerActions
{
    private InputSystem_Actions _inputActions;
    public int _bananaCount = 0;

    protected override void Awake()
    {
        base.Awake(); // Inicialitzar _mb i _jb
        _inputActions = new InputSystem_Actions();
        _inputActions.Player.SetCallbacks(this);
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Guardar la direcció quan es presiona o es deixa la tecla
        Vector2 direction = context.ReadValue<Vector2>();
        direction.y = 0;
        _mb.SetDirection(direction);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _jb.Jump();
        }
    }

    // Col·lisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si l'objecte de colisio es una banana
        if (collision.CompareTag("Banana"))
        {
            _bananaCount++;
            // Destruir el GameObject de la banana
            Destroy(collision.gameObject);
        }

        // Si l'objecte de colisio son punxes
        if (collision.CompareTag("Spikes"))
        {
            // si no es invulnerable, fer mal
            Die();
        }
    }

    // Morirse
    private void Die()
    {
        // Desactivar controls
        if (_inputActions != null)
        {
            _inputActions.Player.Disable();
        }

        // Animación de mort
        if (_animator != null)
        {
            _animator.SetTrigger("TakeDamage");
        }

        // Destruir el GameObject del jugador després d'un temps
        Destroy(gameObject, 0.9f);
    }

    // Contador de bananes
    public void DisplayBananaCount()
    {

    }
}
