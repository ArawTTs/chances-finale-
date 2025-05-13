using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SkillOption : MonoBehaviour
{
    public Skill skill;
    public bool attack = false;
    public bool shield = false;
    public float timeDelaySkill = 1f;
    public float timeDelayShield = 1f;

    [SerializeField] private GameObject[] shieldIcon;


    public void SkillAttackButton()
    {
        attack = true;

        skill.AnimateSkill();

        Invoke("DelaySkill", timeDelaySkill);
    }

    public void SkillShieldButton()
    {
        shield = true;

        // skill.AnimateShield();
        shieldIcon.ToList().ForEach(elements =>
        {
            elements.SetActive(true);
        });

        skill.HideAttack();
        skill.ShieldAction();
    }

    private void DelaySkill()
    {
        skill.SkillAttack();
    }
    private void DelayShield()
    {
        skill.ShieldAction();
    }

    public void HideShield()
    {
        shieldIcon.ToList().ForEach(elements =>
        {
            elements.SetActive(false);
        });
    }

}
