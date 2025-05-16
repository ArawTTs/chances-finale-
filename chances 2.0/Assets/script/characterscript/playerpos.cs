using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerpos : MonoBehaviour
{
    private gamemaster gm;

    void Start()
    {
        GameObject gmObject = GameObject.FindGameObjectWithTag("GM");
        if (gmObject != null)
        {
            gm = gmObject.GetComponent<gamemaster>();
            if (gm != null)
            {
                transform.position = gm.lastcheckpointpos;
            }
            else
            {
                Debug.LogWarning("gamemaster component missing on GM object!");
            }
        }
        else
        {
            Debug.LogWarning("No GameObject with tag 'GM' found!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
