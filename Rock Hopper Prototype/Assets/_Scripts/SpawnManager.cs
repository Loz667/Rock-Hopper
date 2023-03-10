using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private ObjectPool[] obstaclePools;

    private Vector3 spawnPos = new Vector3 (25, 0, 0);

    private float startDelay = 2;
    private float repeatRate = 1;

    PlayerController playerScript;

    private void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void SpawnObstacle()
    {
        if (playerScript.gameOver == false)
        {
            int randomPool = Random.Range(0, obstaclePools.Length);
            GameObject obstacle = obstaclePools[randomPool].EnableObjectInPool(spawnPos);

            if (obstacle == null)
            {
                foreach (ObjectPool pool in obstaclePools)
                {
                    obstacle = pool.EnableObjectInPool(spawnPos);
                    if (obstacle != null) break;
                }
            }
        }
    }
}
