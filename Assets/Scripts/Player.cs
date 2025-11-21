using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class Player : Character, InputSystem_Actions.IPlayerActions
{
    private InputSystem_Actions _inputActions;
    public int _bananaCount = 0;

    [SerializeField] private int totalBananasInLevel = 1;
    [SerializeField] private TextMeshProUGUI bananaCountText;

    private bool _interactButtonPressed = false;

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

        // Interacció (diàleg)
        if (collision.CompareTag("DialogueTrigger"))
        {

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

        StartCoroutine(DieSequence());
    }

    // Corrutina per l'animació de die
    private IEnumerator DieSequence()
    {
        // Reproduir animació
        if (_animator != null)
        {
            _animator.SetTrigger("TakeDamage");
            yield return null; // Espera un frame
        }

        float animationDuration = 0.9f;

        // Esperar temps d'animació
        yield return new WaitForSeconds(animationDuration);

        // Pausar joc i mostrar menú gameOver
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }

        // Destruir jugador
        //Destroy(gameObject);
    }

    // Contador de bananes
    public void DisplayBananaCount()
    {
        if (bananaCountText != null)
        {
            bananaCountText.text = "Bananas: " + _bananaCount + "/" + totalBananasInLevel;
        }
    }

    void Update()
    {
        base.Update();

        if (_interactButtonPressed)
        {
            // reiniciar
            _interactButtonPressed = false;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _interactButtonPressed = true;
        }
    }
}
