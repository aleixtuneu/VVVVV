using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    private InputSystem_Actions _inputActions;
    [SerializeField] private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
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
        _rb.linearVelocity = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
