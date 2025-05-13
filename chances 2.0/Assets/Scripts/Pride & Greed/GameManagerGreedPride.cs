using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameManagerGreedPride : MonoBehaviour
{
    public HealthSystem GreedLife;
    public HealthSystemPlayer healthSystemPlayer;
    public CameraSwitch cameraSwitch;
    public SkillOption skillOption;
    public TimeCode timeCode;
    public ObjectSpawner2D objectSpawner;
    public StartBlinkingAnim blink;
    public Item item;

    [SerializeField] private GameObject playerLife;
    [SerializeField] private GameObject senemyLife;
    [SerializeField] private GameObject skillOptionContainer;
    [SerializeField] private GameObject game;
    [SerializeField] private GameObject greedBoss;

    public bool check = false;
    private bool hasDied = false;

    public GameObject[] playerVids;
    public GameObject[] enemyVids;
    public GameObject[] playerAnimations;
    public GameObject[] enemyAnimations;
    public GameObject[] mainUIs;
    public GameObject gameover;

    private void Update()
    {
        if (GreedLife.health <= 0)
        {
            enemyAnimations[0].SetActive(false);
            enemyAnimations[3].SetActive(false);
            enemyAnimations[2].SetActive(true);
            enemyAnimations[5].SetActive(true);
            Invoke("PostBattle", 0.8f);
        }

        if (PlayerStats.Instance.PHealth <= 0)
        {
            PlayerStats.Instance.PHealth = PlayerStats.Instance.MaxPHealth;
            PlayerStats.Instance.PlayerLife--;
            PlayerPrefs.SetInt("PHealth", PlayerStats.Instance.PHealth);
            PlayerPrefs.SetInt("PlayerLife", PlayerStats.Instance.PlayerLife);
            PlayerPrefs.Save();
            Invoke("LoadOverWorld", 1.06f);
        }
    }

    private void PostBattle()
    {
        PlayerStats.Instance.Money += 80;
        PlayerPrefs.SetInt("Money", PlayerStats.Instance.Money);
        PlayerStats.Instance.AllocationStats += 1;
        PlayerPrefs.SetInt("AllocationStats", PlayerStats.Instance.AllocationStats);
        ItemStats.Instance.largeMedkit++;
        PlayerPrefs.SetInt("LargeMedkit", ItemStats.Instance.largeMedkit);
        PlayerPrefs.Save();
        SceneManager.LoadScene(33);
    }

    private void LoadOverWorld()
    {
        SceneManager.LoadScene(1);
    }

    private void Onable()
    {
        timeCode.totalTime = timeCode.initialCountdownDuration;
    }

    public void OnClickAttack()
    {
        if (GreedLife.health != 0)
        {
            // HideAttack();
            // playerLife.SetActive(false);
            // senemyLife.SetActive(false);
            // skillOptionContainer.SetActive(false);
            HideAttack();
            playerVids[0].SetActive(true);
            Invoke("ReturnAnimation", 2.5f);
        }
    }

    public void ReturnAnimation()
    {
        playerVids[0].SetActive(false);

        int number = Random.value < 0.6f ? 1 : 0;

        if (number == 0)
        {
            EnemyTakeDamage();
            Invoke("ReturnAll", 1f);
        }
        else
        {
            EnemyAnimation();
        }
    }

    public void PlainEnemyAnim()
    {
        enemyVids[0].SetActive(true);
        VideoPlayer vp = enemyVids[0].GetComponent<VideoPlayer>();
        vp.loopPointReached += OnEnemyVideoEnd1;
    }
    void OnEnemyVideoEnd1(VideoPlayer vp)
    {
        vp.loopPointReached -= OnEnemyVideoEnd1;
        enemyVids[0].SetActive(false);

    }
    public void EnemyAnimation()
    {
        enemyVids[0].SetActive(true);
        VideoPlayer vp = enemyVids[0].GetComponent<VideoPlayer>();
        vp.loopPointReached += OnEnemyVideoEnd;
    }

    void OnEnemyVideoEnd(VideoPlayer vp)
    {
        vp.loopPointReached -= OnEnemyVideoEnd;
        PlayGame();
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
        int totalDamage = PlayerPrefs.GetInt("AttackPower", PlayerStats.Instance.AttackPower);

        if (skillOption != null && skillOption.attack)
        {
            totalDamage += PlayerPrefs.GetInt("MagicPower", PlayerStats.Instance.MagicPower);
            skillOption.attack = false;
        }

        GreedLife.TakeDamage(totalDamage);
        Invoke("WaitBlink", 1f);
        objectSpawner.speed += 2f;
    }

    public void DeflectAttacktoEnemy()
    {
        GreedLife.TakeDamage(10);
        Invoke(nameof(WaitBlink), 6f);
    }

    private void WaitBlink()
    {
        blink.StartBlinking(0);
        blink.StartBlinking(1);
    }

    public void PlayerTakeDamage()
    {
        int damage = Random.Range(10, 25);

        if (!skillOption.shield)
        {

            PlayerStats.Instance.PHealth -= damage;
            PlayerPrefs.SetInt("PHealth", PlayerStats.Instance.PHealth);
            blink.StartBlinking(2);
            Debug.Log("ItemB: " + item.itemB);
            item.itemB = false;
        }
        else
        {
            skillOption.shield = false;
        }
    }

    public void PlayGame()
    {
        enemyVids[0].SetActive(false);

        cameraSwitch.PrideLustCameraMiniGame();
        game.SetActive(true);

        Camera.main.orthographic = true;
        objectSpawner.SpawnRoutineCour();
        // ReturnEnemyAnim();
    }

    public void ReturnAll()
    {
        skillOption.HideShield();
        cameraSwitch.FightScene();
        game.SetActive(false);
        Camera.main.orthographic = false;
        timeCode.countdownTimer = timeCode.initialCountdownDuration;

        mainUIs.ToList().ForEach(x => x.SetActive(true));
    }

    public void HideAttack()
    {
        mainUIs.ToList().ForEach(obj => obj.SetActive(false));
    }
}
