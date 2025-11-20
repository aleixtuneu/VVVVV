using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour, InputSystem_Actions.IUIActions
{
    public static DialogueManager Instance { get; private set; } // Singleton

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText; 
    [SerializeField] private GameObject advanceTextPrompt; 
    [SerializeField] private float typingSpeed = 0.05f; // velocitat de text

    private string[] currentDialogueLines;
    private int currentLineIndex; 
    private bool isTyping; 
    public bool dialogueActive; 

    private InputSystem_Actions _inputActions;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        _inputActions = new InputSystem_Actions();
        _inputActions.UI.SetCallbacks(this); 
    }

    void Start()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        if (advanceTextPrompt != null)
        {
            advanceTextPrompt.SetActive(false);
            dialogueActive = false;
            isTyping = false;
        }
    }

    private void OnEnable()
    {
        _inputActions.UI.Enable();
    }

    private void OnDisable()
    {
        _inputActions.UI.Disable();
    }

    public void OnAdvanceDialogue(InputAction.CallbackContext context)
    {
        if (context.performed && dialogueActive) // si el diàleg está actiu i la acció
        {
            HandleAdvanceDialogue();
        }
    }

    // Iniciar diàleg
    public void StartDialogue(string[] dialogueLines)
    {
        if (dialoguePanel == null || dialogueText == null)
        {
            Debug.LogError("Dialogue UI elements not assigned in DialogueManager!", this);
            return;
        }

        if (dialogueLines == null || dialogueLines.Length == 0)
        {
            Debug.LogWarning("Intentando iniciar diálogo sin líneas de diálogo. Abortando.", this);
            EndDialogue(); // Asegurarse de que el juego no se quede pausado
            return;
        }

        currentDialogueLines = dialogueLines;
        currentLineIndex = 0;
        dialogueActive = true;
        dialoguePanel.SetActive(true); // Mostrar panell de diàleg

        Time.timeScale = 0f; // Pausar

        StartCoroutine(TypeLine()); // Escriure la primera línia
    }

    // Avançar diàleg
    private void HandleAdvanceDialogue()
    {
        if (isTyping)
        {
            // Si està escribint, saltar a línia completa
            StopAllCoroutines();
            dialogueText.text = currentDialogueLines[currentLineIndex];
            isTyping = false;
            if (advanceTextPrompt != null)
            {
                advanceTextPrompt.SetActive(true);
            }                
        }
        else
        {
            // Si el text està complet, seguent línia
            currentLineIndex++;
            if (currentLineIndex < currentDialogueLines.Length)
            {
                StartCoroutine(TypeLine());
            }
            else
            {
                EndDialogue(); // Finalitzar si no hi ha mes línies
            }
        }
    }

    // Corrutina per escriure text lletra per lletra
    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = ""; // Netejar text anterior
        if (advanceTextPrompt != null)
        {
            advanceTextPrompt.SetActive(false); // Ocultar 
        }
            
        foreach (char letter in currentDialogueLines[currentLineIndex].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed); // Funciona amb timescale = 0
        }

        isTyping = false;
        if (advanceTextPrompt != null)
        {
            advanceTextPrompt.SetActive(true); // Mostrar 
        }          
    }

    // Finalitzar diàleg
    private void EndDialogue()
    {
        dialogueActive = false;
        dialoguePanel.SetActive(false); // Ocultar panell

        if (advanceTextPrompt != null)
        {
            advanceTextPrompt.SetActive(false);
        }
 
        Time.timeScale = 1f; // Reanudar
        Debug.Log("Diálogo finalizado.");
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
