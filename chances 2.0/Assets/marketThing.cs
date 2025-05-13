using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marketThing : MonoBehaviour
{
    public GameObject marketButton;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            marketButton.SetActive(true);
            //Debug.Log("market button show");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        marketButton.SetActive(false);
        //Debug.Log("market gone");
    }
}
