using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimerGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Image timerFillImage;
    [SerializeField] private GameplayHealth gameplayHealth;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private AttackGluttony attackGluttony;
    [SerializeField] private StartBlinkingAnim blink;
    [SerializeField] private SkillOption skillOption;
    [SerializeField] private ShieldIndicator shieldIndicator;
    [SerializeField] private Item item;
    [SerializeField] private int health;

    public float spawnTimer = 10f;

    private bool hasEnded = false;
    private float currentTime;

    void OnEnable()
    {
        hasEnded = false;
        currentTime = spawnTimer;
        gameplayHealth.health = 100;
    }

    void Update()
    {
        if (gameplayHealth.health <= 0)
        {
            int rnd = Random.Range(10, 25);
            PlayerStats.Instance.PHealth -= rnd;
            PlayerPrefs.SetInt("PHealth", PlayerStats.Instance.PHealth);

            attackGluttony.ReturnAll();
            blink.StartBlinking(1);
        }
        health = gameplayHealth.health;
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            if (!hasEnded)
            {
                hasEnded = true;

                if (health > 0)
                {
                    if (item.itemB == true)
                    {
                        item.itemB = false;

                    }
                    else
                    {

                        // damage enemy
                        int totalDamage = PlayerPrefs.GetInt("AttackPower", PlayerStats.Instance.AttackPower);

                        if (skillOption != null && skillOption.attack == true)
                        {
                            totalDamage += PlayerPrefs.GetInt("MagicPower", PlayerStats.Instance.MagicPower);
                            skillOption.attack = false;
                        }

                        healthSystem.TakeDamage(totalDamage);
                        Invoke(nameof(DelayBlink), 3f);
                    }
                }
                else
                {
                    if (skillOption != null && skillOption.shield == true)
                    {
                        skillOption.shield = false;
                        Debug.Log("wot2");
                        skillOption.HideShield();
                        shieldIndicator.FlashGrey();
                        attackGluttony.ReturnAll();
                        healthSystem.TakeDamage(10);


                    }
                    else
                    {
                        // dmg player
                        int rnd = Random.Range(10, 25);
                        PlayerStats.Instance.PHealth -= rnd;
                        PlayerPrefs.SetInt("PHealth", PlayerStats.Instance.PHealth);

                        attackGluttony.ReturnAll();
                        blink.StartBlinking(1);
                    }



                }
            }
        }
    }

    private void DelayBlink()
    {
        blink.StartBlinking(0);

    }
    void UpdateTimerUI()
    {
        timerText.text = Mathf.Ceil(currentTime).ToString();

        timerFillImage.fillAmount = currentTime / spawnTimer;
    }
}
