using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private Rigidbody m_RB;
    private Animator m_Anim;

    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityMultiplier;

    [SerializeField] ParticleSystem impactEffect;
    [SerializeField] ParticleSystem dirtParticle;

    private AudioSource m_Audio;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip crashSound;

    bool IsGrounded = true;
    public bool gameOver = false;

    private float scoreMultiplier = 10f;
    private float score = 0f;

    [SerializeField] private UnityEvent<int> gameOverEvent;
    [SerializeField] private UnityEvent<int> scoreUpdateEvent;

    private void Start()
    {
        m_RB = GetComponent<Rigidbody>();
        m_Anim = GetComponent<Animator>();
        m_Audio = GetComponent<AudioSource>();
        Physics.gravity *= gravityMultiplier;
    }

    private void Update()
    {
        score += scoreMultiplier * Time.deltaTime;
        scoreUpdateEvent.Invoke((int)score);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded && !gameOver)
        {
            m_RB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            IsGrounded = false;
            m_Anim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            m_Audio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void GameOver()
    {
        gameOverEvent.Invoke((int)score);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            IsGrounded = true;
            dirtParticle.Play();
        }
        else if (other.collider.CompareTag("Obstacle"))
        {
            dirtParticle.Stop();
            impactEffect.Play();
            m_Audio.PlayOneShot(crashSound, 1.0f);
            gameOver = true;
            m_Anim.SetBool("Death_b", true);
            m_Anim.SetInteger("DeathType_int", 1);
        }
    }
}
