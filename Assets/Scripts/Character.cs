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

    public void Awake()
    {
        _mb = GetComponent<MoveBehaviour>();
        _jb = GetComponent<JumpBehaviour>();
        //_animator = GetComponent<Animator>();   
    }

    public void Start()
    {
        _animator = GetComponent<Animator>();

        if (_animator != null)
        {
            _mb.SetAnimator(_animator);
        }
    }
}
