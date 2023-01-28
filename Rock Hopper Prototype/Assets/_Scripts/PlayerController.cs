using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody m_RB;
    private Animator m_Anim;

    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityMultiplier;

    bool IsGrounded = true;
    public bool gameOver = false;

    private void Start()
    {
        m_RB = GetComponent<Rigidbody>();
        m_Anim = GetComponent<Animator>();
        Physics.gravity *= gravityMultiplier;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded && !gameOver)
        {
            m_RB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            IsGrounded = false;
            m_Anim.SetTrigger("Jump_trig");
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
            m_Anim.SetBool("Death_b", true);
            m_Anim.SetInteger("DeathType_int", 1);
        }
    }
}
