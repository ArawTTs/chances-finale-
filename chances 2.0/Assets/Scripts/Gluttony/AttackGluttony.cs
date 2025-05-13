using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AttackGluttony : MonoBehaviour
{
    [SerializeField] private CameraSwitch cameraSwitch;
    [SerializeField] private HealthSystemPlayer pHealth;
    [SerializeField] private HealthSystem eHealth;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject[] pAnimations;
    [SerializeField] private GameObject[] eAnimations;
    [SerializeField] private GameObject enemyParent;
    [SerializeField] private GameObject gameplay;
    [SerializeField] private StartBlinkingAnim blink;


    public SkillOption skillOption;

    private bool hasLoaded = false;




    void Update()
    {
        if (hasLoaded) return;

        if (eHealth.health <= 0)
        {
            hasLoaded = true;
            Reward();
            Invoke("PostBattle", 1.06f);
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
    private void LoadOverWorld()
    {
        SceneManager.LoadScene(1);
    }

    private void Reward()
    {
        PlayerStats.Instance.Money += 60;
        PlayerPrefs.SetInt("Money", PlayerStats.Instance.Money);
        PlayerStats.Instance.AllocationStats += 1;
        PlayerPrefs.SetInt("AllocationStats", PlayerStats.Instance.AllocationStats);
        ItemStats.Instance.largeBottle++;
        PlayerPrefs.SetInt("LargeBottle", ItemStats.Instance.largeBottle);

        PlayerPrefs.Save();
    }
    private void PostBattle()
    {

        SceneManager.LoadScene(32);
    }
    public void OnClickAttack()
    {
        if (eHealth.health != 0)
        {
            //animate attack
            HideButtons();
            //attack anim
            PlayerAnimAttack();

        }

    }

    public void Attack()
    {
        int number = Random.value < 0.6f ? 1 : 0;
        //1 appear 60% and 0 appear 40%

        if (number == 0)
        {

            pAnimations[0].SetActive(true);
            int totalDamage = PlayerPrefs.GetInt("AttackPower", PlayerStats.Instance.AttackPower);

            if (skillOption != null && skillOption.attack == true)
            {
                totalDamage += PlayerPrefs.GetInt("MagicPower", PlayerStats.Instance.MagicPower);
                skillOption.attack = false;
            }

            eHealth.TakeDamage(totalDamage);

            Invoke("CallBlink", 2.8f);
            Invoke("ReturnAll", 2.8f);
        }
        else if (number == 1)
        {

            Invoke("EnemyAnimAttack", 2.8f);

            //camera switch
            Invoke("PlayGame", 5f);
        }
    }

    void OnEnable()
    {
        hasLoaded = false;
    }

    public void DamageEnemy()
    {
        eHealth.TakeDamage(10);
        Invoke(nameof(CallBlink), 5f);
    }

    public void PlayGame()
    {
        buttons[3].SetActive(false);
        cameraSwitch.SlothGame();
        Camera.main.orthographic = true;

        gameplay.SetActive(true);
    }

    #region Animations
    public void EnemyAnimAttack()
    {
        eAnimations[0].SetActive(true);
        Invoke("ReturnAnimation", 2.8f);
    }
    public void PlayerAnimAttack()
    {
        HideButtons();
        pAnimations[0].SetActive(true);
        Invoke("ReturnAnimation", 2.8f);
        // Invoke("Attack", 2.8f);

        Attack();
    }
    #endregion


    #region Basics
    public void HandleSpawningStatus(bool isSpawning)
    {
        if (!isSpawning)
        {
            cameraSwitch.FightScene();
            Camera.main.orthographic = false;
            ReturnButtons();
            ReturnAnimation();
            gameplay.SetActive(false);

        }
    }
    public void HideButtons()
    {
        foreach (var objToHide in buttons)
        {
            objToHide.SetActive(false);
        }
    }

    public void ReturnAll()
    {
        ReturnButtons();
        ReturnAnimation();
        cameraSwitch.FightScene();
        Camera.main.orthographic = false;
        gameplay.SetActive(false);

    }

    private void CallBlink()
    {
        blink.StartBlinking(0);
    }
    private void ReturnButtons()
    {
        foreach (var objToHide in buttons)
        {
            objToHide.SetActive(true);
        }
        Camera.main.orthographic = false;
        buttons[3].SetActive(true);
    }

    private void ReturnAnimation()
    {
        pAnimations[0].SetActive(false);
        eAnimations[0].SetActive(false);

    }
    #endregion

}
