using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBlinkingAnim : MonoBehaviour
{
    [SerializeField] private GameObject[] boss;
    private SpriteRenderer[] spriteRenderers;
    private bool[] isRed;
    private Color origColor = new Color(1f, 1f, 1f, 1f);

    private void Awake()
    {
        spriteRenderers = new SpriteRenderer[boss.Length];
        isRed = new bool[boss.Length];

        for (int i = 0; i < boss.Length; i++)
        {
            spriteRenderers[i] = boss[i].GetComponent<SpriteRenderer>();
        }
    }

    void Blink(int index)
    {
        if (spriteRenderers[index] != null)
        {
            spriteRenderers[index].color = isRed[index] ? origColor : Color.red;
            isRed[index] = !isRed[index];
        }
    }

    public void StartBlinking(int index)
    {
        Debug.Log("called");
        InvokeRepeating("Blink_" + index, 0f, 0.1f);
        Invoke("StopBlinking_" + index, 1f);
    }

    void StopBlinking(int index)
    {
        CancelInvoke("Blink_" + index);
        spriteRenderers[index].color = origColor;
    }

    // Separate Blink and StopBlinking per object
    void Blink_0() { Blink(0); }
    void Blink_1() { Blink(1); }
    void Blink_2() { Blink(2); }
    void Blink_3() { Blink(3); }


    void StopBlinking_0() { StopBlinking(0); }
    void StopBlinking_1() { StopBlinking(1); }
    void StopBlinking_2() { StopBlinking(2); }
    void StopBlinking_3() { StopBlinking(3); }
}
