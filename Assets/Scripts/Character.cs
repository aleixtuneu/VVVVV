using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MoveBehaviour))]
[RequireComponent(typeof(JumpBehaviour))]
[RequireComponent(typeof(Animator))]

public class Character : MonoBehaviour
{
    [SerializeField] protected MoveBehaviour _mb;
    [SerializeField] protected JumpBehaviour _jb;
    [SerializeField] protected Animator _animator;

    void Awake()
    {
        _mb = GetComponent<MoveBehaviour>();
        _jb = GetComponent<JumpBehaviour>();
        _animator = GetComponent<Animator>();

        //
        // --- DEPURACIÓN: Verificar si los componentes principales existen ---
        if (_mb == null)
        {
            Debug.LogError("Character.Awake(): MoveBehaviour component is NULL. Check if it's on the GameObject.", this);
            return; // Si no hay MoveBehaviour, no podemos continuar con la animación
        }
        if (_animator == null)
        {
            Debug.LogError("Character.Awake(): Animator component is NULL. Check if it's on the GameObject.", this);
            // Podemos continuar, pero la animación no funcionará
        }
        else
        {
            Debug.Log("Character.Awake(): Animator component found.", this);
        }

        // --- Intentar pasar el Animator a MoveBehaviour ---
        if (_animator != null) // Solo intenta pasar si el Animator existe
        {
            _mb.SetAnimator(_animator);
            Debug.Log("Character.Awake(): Attempted to pass Animator to MoveBehaviour.", this);
        }
        else
        {
            Debug.LogWarning("Character.Awake(): Skipping passing Animator to MoveBehaviour because _animator is NULL.", this);
        }
        //
    }
}
