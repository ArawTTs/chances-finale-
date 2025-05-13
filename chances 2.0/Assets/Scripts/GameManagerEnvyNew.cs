using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManagerEnvyNew : MonoBehaviour
{


    public GameManager gameManager1;
    public HealthSystem envyLife;
    public CameraSwitch cameraSwitch;
    public TimeCode timeCode;
    public SkillOption skillOption;
    public Item item;
    [SerializeField] private GameObject playerBack;
    [SerializeField] private GameObject playerFront;
    //EnvySection
    [SerializeField] private GameObject eenemyLife;
    [SerializeField] private GameObject eenemyAttack;
    [SerializeField] private GameObject eenemyIdle;
    [SerializeField] private GameObject eenemyDeath;
    [SerializeField] private GameObject envyBoss;
    [SerializeField] private GameObject envyGameplay;
    [SerializeField] private GameObject playerLife;
    public StartBlinkingAnim blink;
    public ShieldIndicator shieldInd;
    //SlothSection


    private bool hasDied = false;
    public bool check = false;

    [SerializeField]
    private GameObject[] PVids
;
    [SerializeField] private GameObject[] Animations;
    [SerializeField] private GameObject[] Buttons;
    public GameObject gameover;


    void Update()
    {
        if (envyLife.health <= 0 && !hasDied)
        {
            hasDied = true;
            EnvyDone();

            Reward();
            // Invoke("LoadOverWorld", 0.8f);
            Invoke("PostBattle", 1.06f);

        }

        //player dead
        if (PlayerStats.Instance.PHealth <= 0)
        {
            gameover.SetActive(true);



            PlayerStats.Instance.PHealth = PlayerStats.Instance.MaxPHealth;
            PlayerStats.Instance.PlayerLife--;
            PlayerPrefs.SetInt("PHealth", PlayerStats.Instance.PHealth);
            PlayerPrefs.SetInt("PlayerLife", PlayerStats.Instance.PlayerLife);

            PlayerPrefs.Save();
            Invoke("LoadOverWorld", 1.06f);

        }
    }

    private void LoadOverWorld()
    {
        SceneManager.LoadScene(1);
    }
    private void DemoWorld()
    {

        SceneManager.LoadScene(17);
    }
    private void PostBattle()
    {
        SceneManager.LoadScene(30);
    }
    private void Reward()
    {
        PlayerStats.Instance.Money += 20;
        PlayerPrefs.SetInt("Money", PlayerStats.Instance.Money);
        PlayerStats.Instance.AllocationStats++;
        PlayerPrefs.SetInt("AllocationStats", PlayerStats.Instance.AllocationStats);
        ItemStats.Instance.smallBottle++;
        PlayerPrefs.SetInt("SmallBottle", ItemStats.Instance.smallBottle);
        PlayerPrefs.Save();

    }
    public void EnvyDone()
    {

        if (envyLife.health >= 0)
        {
            envyBoss.SetActive(true);
        }


        hasDied = true;


        // eenemyIdle.SetActive(false);
        eenemyLife.SetActive(false);
        eenemyIdle.SetActive(false);
        eenemyDeath.SetActive(true);

        Invoke("EDeathAnim", 1.5f);

    }


    public void OnClickAttack()
    {
        if (envyLife.health != 0)
        {
            // cameraSwitch.PlayerView();

            HideAttack();
            timeCode.loseIndicator = false;

            PVids[0].SetActive(true);


            Invoke("EAnimatePlayer", 3.8f);

            Invoke("EnvyAttack", 4.3f);
        }

    }

    #region Basics
    public void ReturnAll()
    {
        playerLife.SetActive(true);
        eenemyAttack.SetActive(false);

        if (envyLife.health >= 80)
        {
            timeCode.countdownTimer = 20f;
            timeCode.totalTime = 20f;
        }
        else if (envyLife.health <= 80 && envyLife.health >= 60)
        {
            timeCode.countdownTimer = 15f;
            timeCode.totalTime = 15f;

        }
        else if (envyLife.health <= 60)
        {
            timeCode.countdownTimer = 10f;
            timeCode.totalTime = 10f;
        }

        Buttons.ToList().ForEach(button =>
        {
            button.SetActive(true);
        });
    }
    private void HideAttack()
    {
        Buttons.ToList().ForEach(objToHide =>
         {
             objToHide.SetActive(false);
         });
    }
    #endregion

    #region Envy Actions

    private void EDeathAnim()
    {
        eenemyDeath.SetActive(false);
    }

    public void EnvyAttack()
    {
        // eenemyIdle.SetActive(false);
        // eenemyAttack.SetActive(true);

        // damage enemy 1 or play game 2
        int number = Random.Range(0, 2);
        // int number = 1;
        if (number == 0)
        {
            // damage enemy
            // envyLife.TakeDamage(22f);


            int totalDamage = PlayerPrefs.GetInt("AttackPower", PlayerStats.Instance.AttackPower);

            if (skillOption != null && skillOption.attack == true)
            {
                totalDamage += PlayerPrefs.GetInt("MagicPower", PlayerStats.Instance.MagicPower);
                skillOption.attack = false;
            }

            if (item.itemB == false)
            {
                envyLife.TakeDamage(totalDamage);
                blink.StartBlinking(0);
            }
            item.itemB = false;
            ReturnAll();
        }
        else
        if (number == 1)
        {

            // PVids[1].SetActive(true);
            Invoke("EnvyShow", 1f);
            HideAttack();
        }

    }

    public void StartBlinking0()
    {
        blink.StartBlinking(1);

    }
    public void StartBlinking()
    {
        blink.StartBlinking(0);

    }
    public void EAnimatePlayer()
    {
        PVids[0].SetActive(false);

        // if (skillOption.shield == false)
        // {
        //     blink.StartBlinking(0);
        // }

        // cameraSwitch.FightScene();
        playerBack.SetActive(true);
        playerFront.SetActive(false);
        if (envyLife.health != 0)
        {
            eenemyLife.SetActive(true);
            playerLife.SetActive(true);

        }
        eenemyLife.SetActive(true);
    }
    public void EnvyShow()
    {
        PVids[1].SetActive(true);
        Invoke(nameof(HideEnvyVid), 1.5f); // Calls after 1.5s
    }

    void HideEnvyVid()
    {
        envyGameplay.SetActive(true);
        HideAttack();
        PVids[1].SetActive(false);
    }

    public void EnvyAnimation()
    {
        // HideAttack();
        PVids[1].SetActive(true);
        Invoke(nameof(DisableAnim), 3f); // Calls after 1.5s
    }

    void DisableAnim()
    {
        PVids[1].SetActive(false);
        skillOption.shield = false;
        skillOption.HideShield();

        // ReturnAll();
        Invoke(nameof(ShieldPlayerIndicator), 2f);
    }

    void ShieldPlayerIndicator()
    {
        shieldInd.FlashGrey();

        Invoke(nameof(StartBlinking), .5f);
        envyLife.TakeDamage(10);
    }

    #endregion




}
