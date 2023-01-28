using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UnityEvent playerConnected;

    IEnumerator Start()
    {
        bool connected = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("Error starting LootLocker session");
                return;
            }
            Debug.Log("Successfully connected to LootLocker");
            connected = true;
        });
        yield return new WaitUntil(() => connected);
        playerConnected.Invoke();
    }
}
