using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckifActive : MonoBehaviour
{
    public GameObject backgroundMusic;

    void OnEnable()
    {
        backgroundMusic.SetActive(false);
    }

    void OnDisable()
    {
        backgroundMusic.SetActive(true);
    }

}
