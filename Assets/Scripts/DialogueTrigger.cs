using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    [TextArea(3, 10)] // Àrea de text gran a l'inspector
    public string[] dialogueLines; // Les línies de diàleg per el trigger
    private bool playerInRange = false; // Detectar si el player està aprop
    private InputSystem_Actions _inputActions;
    private bool _interactButtonPressed = false;

    //
    protected virtual void Awake() 
    {
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
    //

    public void TriggerInteraction()
    {
        // Botó Interactuar
        if (playerInRange)
        {
            if (DialogueManager.Instance != null && !DialogueManager.Instance.dialogueActive)
            {
                DialogueManager.Instance.StartDialogue(dialogueLines);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player en rango para diálogo.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player fuera de rango para diálogo.");
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _interactButtonPressed = true;
        }
    }

    public void Update()
    {
        // Iniciar diàleg
        if (playerInRange && _interactButtonPressed)
        {
            _interactButtonPressed = false;

            if (DialogueManager.Instance != null && !DialogueManager.Instance.dialogueActive)
            {
                DialogueManager.Instance.StartDialogue(dialogueLines);
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context) { }
    public void OnJump(InputAction.CallbackContext context) { }
}
