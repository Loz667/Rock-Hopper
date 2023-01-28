using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject obstacle;
    [SerializeField] int poolSize;

    private GameObject[] pool;

    private void Awake()
    {
        PopulatePool();
    }

    public GameObject EnableObjectInPool(Vector3 position)
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].transform.position = position;
                pool[i].SetActive(true);
                return pool[i];
            }
        }
        return null;
    }

    public void DisableObjectInPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(obstacle, transform);
            pool[i].SetActive(false);
        }
    }
}
