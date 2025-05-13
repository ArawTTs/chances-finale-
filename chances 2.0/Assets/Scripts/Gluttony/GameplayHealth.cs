using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayHealth : MonoBehaviour
{
    public Image healthBar;
    public int health = 100;

    private HealthSystemPlayer pHealth;
    public AttackGluttony attackGluttony;
    [SerializeField] private StartBlinkingAnim blink;
    [SerializeField] private SkillOption skillOption;
    [SerializeField] private GameObject game;

    public int damagePerSecond = 3;
    private float damageTimer = 0f;


    void Start()
    {
        if (pHealth == null)
            pHealth = FindObjectOfType<HealthSystemPlayer>();
    }
    void OnEnable()
    {
        health = 100;
    }
    void Update()
    {
        healthBar.fillAmount = health / 100f;

        if (health > 0)
        {
            damageTimer += Time.deltaTime;

            if (damageTimer >= 1f)
            {
                TakeDamage(damagePerSecond);
                damageTimer = 0f;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, 100);
    }

    public void Heal(int heal)
    {
        health += heal;
        health = Mathf.Clamp(health, 0, 100);

    }
}
