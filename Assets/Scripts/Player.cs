using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Player : Character, InputSystem_Actions.IPlayerActions
{
    private InputSystem_Actions _inputActions;
    public int _bananaCount = 0;

    [SerializeField] private int totalBananasInLevel = 1;
    [SerializeField] private TextMeshProUGUI bananaCountText;

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

    public void Start()
    {
        DisplayBananaCount();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (Time.timeScale > 0)
        {
            // Guardar la direcció quan es presiona o es deixa la tecla
            Vector2 direction = context.ReadValue<Vector2>();
            direction.y = 0;
            _mb.SetDirection(direction);
        }
        else
        {
            _mb.SetDirection(Vector2.zero);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (Time.timeScale > 0 && context.performed)
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

            DisplayBananaCount();

            if (_bananaCount >= totalBananasInLevel)
            {
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.WinGame();
                    _inputActions.Player.Disable();
                }
            }
        }

        // Si l'objecte de colisio son punxes
        if (collision.CompareTag("Spikes"))
        {
            // si no es invulnerable, fer mal
            Die();
        }
    }

    // Morirse
    public void Die()
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

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver(); // Esto pausará el juego y mostrará el menú de Game Over
        }

        // Destruir el GameObject del jugador després d'un temps
        Destroy(gameObject, 0.9f);
    }

    // Contador de bananes
    public void DisplayBananaCount()
    {
        if (bananaCountText != null)
        {
            bananaCountText.text = "Bananas: " + _bananaCount + "/" + totalBananasInLevel;
        }
        else
        {
            Debug.LogWarning("BananaCountText no está asignado en el Inspector de Player.cs.");
        }
    }
}
