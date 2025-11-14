using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character, InputSystem_Actions.IPlayerActions
{
    private InputSystem_Actions _inputActions;
    //
    public int _bananaCount = 0;
    //

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Comprovar si l'objecte de colisio es una banana
        if (collision.CompareTag("Banana"))
        {
            _bananaCount++;
            // Destruir el GameObject de la banana
            Destroy(collision.gameObject);
        }
    }

    // Contador de bananes
    public void DisplayBananaCount()
    {

    }
}
