using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlothSetActiveChecker : MonoBehaviour
{

    // public GameObject[] levels;
    public GameObject[] box;
    public GameObject player;
    public Transform playerT;
    public Transform[] boxT;
    public GameManagerSloth gameManagerSloth;


    void OnEnable()
    {
        Conditions();
        gameManagerSloth.LevelChecker();
    }

    void Conditions()
    {
        player.transform.position = playerT.position;
        box[0].SetActive(true);
        box[0].transform.position = boxT[0].position;
        box[1].SetActive(true);
        box[1].transform.position = boxT[1].position;
    }
}
