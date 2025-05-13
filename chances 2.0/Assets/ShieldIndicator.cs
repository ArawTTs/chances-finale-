using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldIndicator : MonoBehaviour
{
    public Renderer playerRenderer;
    private Color originalColor;

    void Start()
    {
        originalColor = playerRenderer.material.color;
    }

    public void FlashGrey()
    {
        playerRenderer.material.color = Color.cyan;
        Invoke(nameof(ReturnToOriginalColor), 2f);

        Debug.Log("CalledSH");
    }

    private void ReturnToOriginalColor()
    {
        playerRenderer.material.color = originalColor;
    }

}
