using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
public class TimeCode : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public SkillOption skillOption;
    public GameManager gameManager;
    public GameManagerEnvyNew gameManagerEnvy;
    public GameManagerSloth gameManagerSloth;
    public GameManagerGreedPride gameManagerGreedPride;
    public GameManagerWrath gameManagerWrath;
    public Item item;
    public HealthSystem miniGameLife;
    public Image bar;
    public float initialCountdownDuration = 20f; // Initial countdown duration
    public float countdownTimer;
    public float currentCountdownDuration;
    public GameObject lose;
    public GameObject hideGameplay;
    public float totalTime;
    public bool loseIndicator;
    public GameObject[] gameplays;


    void Start()
    {
        countdownTimer = initialCountdownDuration;
        currentCountdownDuration = initialCountdownDuration;
    }

    void Update()
    {
        // Update countdown timer
        countdownTimer -= Time.deltaTime;
        countdownTimer = Mathf.Max(0, countdownTimer);

        // Update UI
        int secondsLeft = Mathf.CeilToInt(countdownTimer);
        countdownText.text = secondsLeft.ToString();
        bar.fillAmount = countdownTimer / totalTime;
        if (miniGameLife.health <= 0)//GnP
        {
            gameManagerGreedPride.PlayerTakeDamage();
            gameManagerGreedPride.ReturnAll();

        }
        //lose
        if (countdownTimer == 0)
        {

            //Player Life Damage
            int rndm = Random.Range(10, 25);

            skillOption.HideShield();
            //Shield

            //Envy
            if (gameManagerEnvy != null)
            {
                if (skillOption.shield == false)
                {

                    PlayerStats.Instance.PHealth -= rndm;
                    PlayerPrefs.SetInt("PHealth", PlayerStats.Instance.PHealth);
                    gameManagerEnvy.StartBlinking0();

                }
                else if (skillOption.shield == true)
                {
                    skillOption.shield = false;
                }

                gameManagerEnvy.ReturnAll();
                gameManager.ClearPuzzles();
                gameManager.ResetGame();
                hideGameplay.SetActive(false);

            }


            //Sloth
            if (gameManagerSloth != null)
            {
                if (skillOption.shield == false)
                {
                    PlayerStats.Instance.PHealth -= rndm;
                    PlayerPrefs.SetInt("PHealth", PlayerStats.Instance.PHealth);

                    gameManagerSloth.PlayerBlinkAnim();
                }
                else if (skillOption.shield == true)
                {
                    skillOption.shield = false;
                }
                gameManagerSloth.ReturnAll();
                gameManagerSloth.check = false;
            }

            //Pride & Greed
            if (gameManagerGreedPride != null)
            {
                if (miniGameLife.health > 0)
                {
                    if (item.itemB == false)
                    {
                        gameManagerGreedPride.EnemyTakeDamage();
                    }
                    item.itemB = false;

                }
                else
                {
                    gameManagerGreedPride.PlayerTakeDamage();

                }
                gameManagerGreedPride.ReturnAll();

            }
            if (gameManagerWrath != null)
            {
                PlayerStats.Instance.PHealth -= rndm + 5;
                PlayerPrefs.SetInt("PHealth", PlayerStats.Instance.PHealth);
                gameManagerWrath.PlayerBlink();
                gameManagerWrath.ReturnAll();
            }


            gameplays.ToList().ForEach(gameplay =>
            {
                gameplay.SetActive(false);
            });

            ResetTimer();
        }


    }

    public void ResetTimer()
    {
        countdownTimer = initialCountdownDuration;
        currentCountdownDuration = initialCountdownDuration;
    }
}
