using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float speed = 20;
    private float leftBound = -15;

    PlayerController playerScript;

    private void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (playerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            gameObject.SetActive(false);
        }
    }
}
