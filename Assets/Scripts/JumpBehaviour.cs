using UnityEngine;

public class JumpBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float jumpForce;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void Jump()
    {
        _rb.AddForce(Vector2.up * jumpForce);
    }
}
