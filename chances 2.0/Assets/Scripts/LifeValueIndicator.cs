using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeValueIndicator : MonoBehaviour
{
    public TextMeshProUGUI health;

    void Update()
    {
        int current = PlayerStats.Instance.PHealth;
        int max = PlayerStats.Instance.MaxPHealth;
        health.text = current + "/" + max;
    }
}
