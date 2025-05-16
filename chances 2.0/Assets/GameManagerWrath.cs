using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;


public class GameManagerWrath : MonoBehaviour
{
    public HealthSystem wrathLife;
    public CameraSwitch cameraSwitch;
    public Camera camera;
    public SkillOption skillOption;
    public TimeCode timeCode;
    public StartBlinkingAnim blink;


    [SerializeField] private GameObject playerLife;
    [SerializeField] private GameObject senemyLife;

    [SerializeField] private GameObject game;
    [SerializeField] private GameObject wrathBoss;
    public bool check = false;
    public GameObject[] playerVids;
    public GameObject[] playerAnimations;
    public GameObject[] enemyAnimations;
    public GameObject[] mainUIs;
    public GameObject[] gameplays;
    public GameObject gameover;

    public void Update()
    {
        //enemy dead
        if (wrathLife.health <= 0)
        {
            enemyAnimations[0].SetActive(false);
            enemyAnimations[2].SetActive(true);

            Invoke("PostBattle", 0.8f);
        }
        //player dead
        if (PlayerStats.Instance.PHealth <= 0)
        {
            gameover.SetActive(true);

            PlayerStats.Instance.PlayerLife--;
            PlayerStats.Instance.PHealth = PlayerStats.Instance.MaxPHealth;

            PlayerPrefs.SetInt("PHealth", PlayerStats.Instance.PHealth);
            PlayerPrefs.SetInt("PlayerLife", PlayerStats.Instance.PlayerLife);
            PlayerPrefs.Save();


            Invoke("LoadOverWorld", 1.06f);
        }
    }
    private void PostBattle()
    {

        PlayerStats.Instance.Money += 150;
        PlayerPrefs.SetInt("Money", PlayerStats.Instance.Money);
        PlayerStats.Instance.AllocationStats += 3;
        PlayerPrefs.SetInt("AllocationStats", PlayerStats.Instance.AllocationStats);
        ItemStats.Instance.largeBottle++;
        PlayerPrefs.SetInt("LargeBottle", ItemStats.Instance.largeBottle);
        ItemStats.Instance.largeMedkit++;
        PlayerPrefs.SetInt("LargeMedkit", ItemStats.Instance.largeMedkit);

        PlayerPrefs.Save();
        SceneManager.LoadScene(35);

    }
    private void LoadOverWorld()
    {

        SceneManager.LoadScene(1);
    }

    void OnEnable()
    {
        timeCode.countdownTimer = timeCode.totalTime;
    }
    public void OnClickAttack()
    {

        if (wrathLife.health != 0)
        {

            HideAttack();

            senemyLife.SetActive(false);

            // player animation
            playerVids[0].SetActive(true);
            Invoke("RandomBattleOutcome", 3f);
            //return animation
        }
    }


    private void RandomBattleOutcome()
    {
        int number = Random.value < 0.6f ? 1 : 0;
        playerVids[0].SetActive(false);

        if (number == 0)
        {
            int totalDamage = PlayerStats.Instance.AttackPower;

            if (skillOption != null && skillOption.attack == true)
            {
                totalDamage += PlayerStats.Instance.MagicPower;
                skillOption.attack = false;
            }

            wrathLife.TakeDamage(totalDamage);
            blink.StartBlinking(1);
            Invoke("ReturnAll", 1f);
        }

        else if (number == 1)
        {
            ReturnAnimation();
        }
        senemyLife.SetActive(false);
    }

    public void PlayerBlink()
    {
        blink.StartBlinking(0);

    }
    public void EnemyBlink()
    {
        blink.StartBlinking(1);
    }

    public void ReturnAnimation()
    {
        EnemyAnimAttack();
        Invoke(nameof(ReturnEnemyAnim), 4.5f);
    }
    public void EnemyAnimAttack()
    {
        enemyAnimations[1].SetActive(true);
    }

    public void NormalReturn()
    {
        enemyAnimations[0].SetActive(true);
        enemyAnimations[1].SetActive(false);
    }

    public void ReturnEnemyAnim()
    {
        enemyAnimations[0].SetActive(true);
        enemyAnimations[1].SetActive(false);

        PlayGame();
    }

    public void EnemyTakeDamage()
    {
        int totalDamage = PlayerPrefs.GetInt("AttackPower", PlayerStats.Instance.AttackPower);

        if (skillOption != null && skillOption.attack == true)
        {
            totalDamage += PlayerPrefs.GetInt("MagicPower", PlayerStats.Instance.MagicPower);
            skillOption.attack = false;
        }

        wrathLife.TakeDamage(totalDamage);
        blink.StartBlinking(1);

    }

    public void EnemyTakeDmgSh()
    {
        wrathLife.TakeDamage(10);
        blink.StartBlinking(1);
    }
    public void PlayerTakeDamage()
    {

        int damage = Random.Range(10, 20);
        if (skillOption != null)
        {

            if (skillOption.shield == false)
            {
                PlayerStats.Instance.PHealth -= damage;
                PlayerPrefs.SetInt("PHealth", PlayerStats.Instance.PHealth);

            }
            else
            {
                skillOption.shield = false;
            }
        }
        blink.StartBlinking(0);

    }


    public void PlayGame()
    {
        cameraSwitch.PrideLustCameraMiniGame();
        camera.orthographic = true;
        int rnd = Random.Range(0, gameplays.Length);
        timeCode.countdownTimer = timeCode.initialCountdownDuration;

        gameplays[rnd].SetActive(true);

    }

    #region Basics
    public void ReturnAll()
    {
        skillOption.HideShield();
        cameraSwitch.FightScene();
        // game.SetActive(false);
        gameplays.ToList().ForEach(x =>
        {
            x.SetActive(false);
        });


        timeCode.countdownTimer = timeCode.initialCountdownDuration;

        mainUIs.ToList().ForEach(x =>
        {
            x.SetActive(true);
        });
        camera.orthographic = false;
    }
    private void HideAttack()
    {
        mainUIs.ToList().ForEach(objToHide =>
         {
             objToHide.SetActive(false);
         });
    }
    #endregion
}
