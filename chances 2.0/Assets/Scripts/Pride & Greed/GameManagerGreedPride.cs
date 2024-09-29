using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;


public class GameManagerGreedPride : MonoBehaviour
{
    public HealthSystem GreedLife;
    public HealthSystemPlayer healthSystemPlayer;
    public CameraSwitch cameraSwitch;
    public SkillOption skillOption;
    public TimeCode timeCode;
    public ObjectSpawner2D objectSpawner;


    [SerializeField] private GameObject playerLife;
    [SerializeField] private GameObject senemyLife;
    [SerializeField] private GameObject skillOptionContainer;

    [SerializeField] private GameObject game;
    [SerializeField] private GameObject greedBoss;
    private bool hasDied = false;
    public bool check = false;
    public GameObject[] playerVids;
    public GameObject[] playerAnimations;
    public GameObject[] enemyAnimations;
    public GameObject[] mainUIs;
    public GameObject gameover;

    public void Update()
    {
        if (GreedLife.health == 0)
        {
            enemyAnimations[0].SetActive(false);
            enemyAnimations[3].SetActive(false);
            enemyAnimations[2].SetActive(true);
            enemyAnimations[5].SetActive(true);

            Invoke("LoadOverWorld", 0.8f);
        }
        //player dead
        if (healthSystemPlayer.health == 0)
        {
            gameover.SetActive(true);
            Invoke("LoadOverWorld", 1.06f);
        }

    }

    private void LoadOverWorld()
    {
        SceneManager.LoadScene(1);
    }
    public void OnClickAttack()
    {

        if (GreedLife.health != 0)
        {

            // cameraSwitch.PlayerView();
            HideAttack();

            playerLife.SetActive(false);
            senemyLife.SetActive(false);
            skillOptionContainer.SetActive(false);

            // playerAnimations[0].SetActive(false);
            // playerAnimations[1].SetActive(true);
            // player animation
            playerVids[0].SetActive(true);

            //return animation
            Invoke("ReturnAnimation", 3f);
        }
    }

    public void ReturnAnimation()
    {
        // cameraSwitch.FightScene();
        // playerAnimations[0].SetActive(true);
        // playerAnimations[1].SetActive(false);
        playerVids[0].SetActive(false);

        EnemyAnimAttack();

        int number = Random.Range(0, 2);
        Debug.Log("number: " + number);

        if (number == 0)
        {
            // damage enemy
            GreedLife.TakeDamage(22f);

            Invoke("ReturnAll", 1f);
        }
        else if (number == 1)
        {
            Invoke("PlayGame", 1f);

        }
    }

    public void EnemyAnimAttack()
    {
        cameraSwitch.EnemyPosition();
        enemyAnimations[0].SetActive(false);
        enemyAnimations[3].SetActive(false);

        enemyAnimations[1].SetActive(true);
        enemyAnimations[4].SetActive(true);
    }

    public void ReturnEnemyAnim()
    {
        enemyAnimations[0].SetActive(true);
        enemyAnimations[3].SetActive(true);

        enemyAnimations[1].SetActive(false);
        enemyAnimations[4].SetActive(false);
    }

    public void EnemyTakeDamage()
    {
        int damage = Random.Range(25, 35);
        GreedLife.TakeDamage(15);

        if (skillOption.attack == true)//activate more damage when skill
        {
            GreedLife.TakeDamage(damage);
            skillOption.attack = false;
        }


    }
    public void PlayerTakeDamage()
    {
        int damage = Random.Range(10, 20);
        if (skillOption.shield == false)//immune damage if shielded
        {
            healthSystemPlayer.TakeDamage(damage);

        }
        else if (skillOption.shield == true)
        {
            skillOption.shield = false;
        }
    }


    public void PlayGame()
    {
        cameraSwitch.PrideLustCameraMiniGame();
        game.SetActive(true);
        objectSpawner.SpawnRoutineCour();
        ReturnEnemyAnim();
    }

    #region Basics
    public void ReturnAll()
    {
        skillOption.HideShield();
        cameraSwitch.FightScene();
        game.SetActive(false);


        timeCode.countdownTimer = timeCode.initialCountdownDuration;

        mainUIs.ToList().ForEach(x =>
        {
            x.SetActive(true);
        });
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
