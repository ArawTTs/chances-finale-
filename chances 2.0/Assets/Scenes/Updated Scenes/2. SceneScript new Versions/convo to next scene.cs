using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class convotonextscene : MonoBehaviour
{

    public Animator SceneLoader;
    public int SceneNumber;

    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneNumber);
    }

    public void ActivateTransition()
    {
        SceneLoader.SetTrigger("ChangeScene");
    }
}
