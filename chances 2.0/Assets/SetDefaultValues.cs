using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDefaultValues : MonoBehaviour
{


    public void Awake()
    {
        // Set default values
        PlayerStats.Instance.MaxPHealth = 100;
        PlayerStats.Instance.PHealth = PlayerStats.Instance.MaxPHealth;
        PlayerStats.Instance.PlayerLife = 3;
        PlayerStats.Instance.PSkill = 3;
        PlayerStats.Instance.AttackPower = 20;
        PlayerStats.Instance.MagicPower = 20;
        PlayerStats.Instance.AllocationStats = 2;
        PlayerStats.Instance.Money = 50;

        // Save to PlayerPrefs
        PlayerPrefs.SetInt("PHealth", PlayerStats.Instance.PHealth);
        PlayerPrefs.SetInt("MaxPHealth", PlayerStats.Instance.MaxPHealth);
        PlayerPrefs.SetInt("PlayerLife", PlayerStats.Instance.PlayerLife);
        PlayerPrefs.SetInt("PSkill", PlayerStats.Instance.PSkill);
        PlayerPrefs.SetInt("AttackPower", PlayerStats.Instance.AttackPower);
        PlayerPrefs.SetInt("MagicPower", PlayerStats.Instance.MagicPower);
        PlayerPrefs.SetInt("AllocationStats", PlayerStats.Instance.AllocationStats);
        PlayerPrefs.SetInt("Money", PlayerStats.Instance.Money);

        ItemStats.Instance.smallBottle = 5;
        ItemStats.Instance.largeBottle = 2;
        ItemStats.Instance.smallMedkit = 5;
        ItemStats.Instance.largeMedkit = 2;


        PlayerPrefs.SetInt("SmallBottle", ItemStats.Instance.smallBottle);
        PlayerPrefs.SetInt("LargeBottle", ItemStats.Instance.largeBottle);
        PlayerPrefs.SetInt("SmallMedkit", ItemStats.Instance.smallMedkit);
        PlayerPrefs.SetInt("LargeMedkit", ItemStats.Instance.largeMedkit);



        PlayerPrefs.Save();
    }

}
