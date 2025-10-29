using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MoveBehaviour))]
[RequireComponent(typeof(JumpBehaviour))]

public class Character : MonoBehaviour
{
    [SerializeField] protected MoveBehaviour _mb;
    [SerializeField] protected JumpBehaviour _jb;

    void Awake()
    {
        _mb = GetComponent<MoveBehaviour>();
        _jb = GetComponent<JumpBehaviour>();
    }
}
