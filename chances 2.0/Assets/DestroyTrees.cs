using UnityEngine;

public class DestroyTrees : MonoBehaviour
{
    public GameObject trees;

    void Start()
    {
        // Check if trees were already destroyed
        if (PlayerPrefs.GetInt("TreesDestroyed", 0) == 1)
        {
            trees.SetActive(false); // Hide trees
        }
    }

    public void destroyTressAct()
    {
        Destroy(trees);
        PlayerPrefs.SetInt("TreesDestroyed", 1); // Remember destroyed
    }
}
