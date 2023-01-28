using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody m_RB;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityMultiplier;

    bool IsGrounded = true;
    public bool gameOver = false;

    private void Start()
    {
        m_RB = GetComponent<Rigidbody>();
        Physics.gravity *= gravityMultiplier;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            m_RB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            IsGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            IsGrounded = true;
        }
        else if (other.collider.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
        }
    }
}
