using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Skill : MonoBehaviour
{
    #region Dependencies
    public CameraSwitch cameraSwitch;
    public HealthSystem cockroachLife;
    public GameManagerSloth gameManagerSloth;
    public GameManagerEnvyNew gameManagerEnvyNew;
    public GameFlowManagerLust gameFlowManagerLust;
    public GameManagerGreedPride gameManagerGreedPride;
    public AttackGluttony attackGluttony;
    public GameManagerWrath gameManagerWrath;
    public SkillManager skillManager;
    public SkillOption skillOption;
    public ShieldIndicator shieldIndicator;
    #endregion
    #region Serialized Fields
    [SerializeField] private GameObject playerBack;
    [SerializeField] private GameObject playerSkill;
    [SerializeField] private GameObject playerShield;
    [SerializeField] private GameObject enemyLife;
    [SerializeField] private GameObject ckenemyLife;
    [SerializeField] private GameObject skillOpt;
    public GameObject[] PSkills;
    public GameObject[] BtnsToShow;
    public GameObject[] ToHide;
    #endregion


    #region Skill Logic
    void Start()
    {
        PlayerStats.Instance.PSkill = PlayerPrefs.GetInt("PSkill", PlayerStats.Instance.PSkill);
    }

    public void OnClickSkill()
    {
        if (PlayerStats.Instance.PHealth != 0 && PlayerStats.Instance.PSkill != 0)
        {
            HideAttack();
            PlayerStats.Instance.PSkill--;
            PlayerPrefs.SetInt("PSkill", PlayerStats.Instance.PSkill);
            skillOpt.SetActive(true);
        }
    }

    public void AnimateSkill()
    {
        PSkills[0].SetActive(true);
        enemyLife.SetActive(false);
        ToHide[3].SetActive(false);
    }

    public void AnimateShield()
    {
        PSkills[1].SetActive(true);
        enemyLife.SetActive(false);
        ToHide[3].SetActive(false);
    }

    public void SkillAttack()
    {
        playerBack.SetActive(true);
        PSkills[0].SetActive(false);
        enemyLife.SetActive(true);

        ExecuteManagerActions();
        // ReturnAll();
    }

    public void SkillShield()
    {
        playerBack.SetActive(true);
        PSkills[1].SetActive(false);
        enemyLife.SetActive(true);
        ReturnAll();

    }
    #endregion

    #region Helper Methods

    public void ShieldAction()
    {
        if (gameManagerEnvyNew != null)
        {
            // gameManagerEnvyNew.EAnimatePlayer();
            gameManagerEnvyNew.EnvyAnimation();
            Invoke(nameof(AnimateShield), 2f);
            Invoke(nameof(SkillShield), 5f);
        }

        if (gameManagerSloth != null)
        {
            if (cockroachLife.health != 0)
            {
                gameManagerSloth.AttackCk();
                Invoke(nameof(AnimateShield), 2f);
                Invoke(nameof(SkillShield), 5f);
                Invoke(nameof(DelayCKAttack), 4f);
            }
            else
            {
                gameManagerSloth.AnimationSloth();
                //player shield 
                Invoke(nameof(AnimateShield), 2f);
                Invoke(nameof(SkillShield), 5f);
                skillOption.shield = false;
                skillOption.HideShield();
                //deflect damage

                gameManagerSloth.TakeDamageEnemy();

            }
        }

    }

    private void DelayCKAttack()
    {
        gameManagerSloth.AnimateCKAttack();
        shieldIndicator.FlashGrey();
    }

    private void ExecuteManagerActions()
    {

        if (gameManagerEnvyNew != null)
        {
            // gameManagerEnvyNew.EAnimatePlayer();
            gameManagerEnvyNew.EnvyShow();
        }

        if (gameManagerSloth != null)
        {
            if (cockroachLife.health != 0)
            {
                // gameManagerSloth.OnClickAttack();
                int totalDamage = PlayerPrefs.GetInt("AttackPower", PlayerStats.Instance.AttackPower);

                if (skillOption != null && skillOption.attack == true)
                {
                    totalDamage += PlayerPrefs.GetInt("MagicPower", PlayerStats.Instance.MagicPower);
                    skillOption.attack = false;
                }

                cockroachLife.TakeDamage(Random.Range(totalDamage, totalDamage + 10));
                gameManagerSloth.AttackCk();
                gameManagerSloth.AnimateCKAttack();
            }
            else
            {
                gameManagerSloth.SlothAttack();
            }
        }
        if (gameManagerGreedPride != null)
        {
            gameManagerGreedPride.ReturnAnimation();
        }
        if (attackGluttony != null)
        {
            attackGluttony.EnemyAnimAttack();

            Invoke("GluttonyPlayGame", 1f);
        }

        if (gameFlowManagerLust != null)
        {
            gameFlowManagerLust.PlayGame();
        }
        if (gameManagerWrath != null)
        {
            gameManagerWrath.ReturnAnimation();
        }

    }
    private void GluttonyPlayGame()
    {
        attackGluttony.PlayGame();
    }
    public void ReturnAll()
    {
        if (ckenemyLife != null)
            ckenemyLife?.SetActive(true);

        BtnsToShow.ToList().ForEach(button => button.SetActive(true));
    }

    public void HideAttack()
    {
        ToHide.ToList().ForEach(objToHide => objToHide.SetActive(false));
    }
    #endregion
}
