using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeEnemyValue : MonoBehaviour
{
    public TextMeshProUGUI health;
    public HealthSystem enemyHealth;

    void Update()
    {
        health.text = enemyHealth.health.ToString() + "/100";
    }
}
