using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 20;

    private void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }
}
