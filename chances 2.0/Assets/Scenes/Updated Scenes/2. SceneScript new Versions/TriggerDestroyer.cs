using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDestroyer : MonoBehaviour
{
    public string objectID; // Unique ID for saving state

    void Awake()
    {
        // Check if the object was already destroyed
        // if (PlayerPrefs.GetInt(objectID, 0) == 1)
        // {
        //     Destroy(gameObject);
        // }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetInt(objectID, 1); // Mark as destroyed
            PlayerPrefs.Save();              // Save to disk
            Destroy(gameObject);
        }
    }

    public void destroy()
    {
        PlayerPrefs.SetInt(objectID, 1); // Mark as destroyed
        PlayerPrefs.Save();              // Save to disk
        Destroy(gameObject);
    }
}
