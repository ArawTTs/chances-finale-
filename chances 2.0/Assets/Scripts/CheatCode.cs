using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class CheatCode : MonoBehaviour
{
    [SerializeField] private GameObject cheat;
    [SerializeField] private TMP_InputField AP;
    [SerializeField] private TMP_InputField MP;
    [SerializeField] private TMP_InputField maxPHealth;
    [SerializeField] private TMP_InputField money;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleCheat();
        }
    }

    void ToggleCheat()
    {
        cheat.SetActive(!cheat.activeSelf);
    }

    public void OnClickUpdateStats()
    {
        PlayerStats.Instance.AttackPower = UpdateStat(AP.text, "AttackPower", PlayerStats.Instance.AttackPower);
        PlayerStats.Instance.MagicPower = UpdateStat(MP.text, "MagicPower", PlayerStats.Instance.MagicPower);
        PlayerStats.Instance.MaxPHealth = UpdateStat(maxPHealth.text, "MaxPHealth", PlayerStats.Instance.MaxPHealth);
        PlayerStats.Instance.Money = UpdateStat(money.text, "Money", PlayerStats.Instance.Money);
    }

    public void OnClickRestoreHnS()
    {
        PlayerStats.Instance.PHealth = PlayerStats.Instance.MaxPHealth;
        PlayerStats.Instance.PSkill = 3;

        PlayerPrefs.SetInt("PHealth", PlayerStats.Instance.PHealth);
        PlayerPrefs.SetInt("PSkill", PlayerStats.Instance.PSkill);
    }

    public void OnClickRestoreItems()
    {
        ItemStats.Instance.smallBottle = 10;
        ItemStats.Instance.largeBottle = 10;
        ItemStats.Instance.smallMedkit = 10;
        ItemStats.Instance.largeMedkit = 10;

        PlayerPrefs.SetInt("SmallBottle", ItemStats.Instance.smallBottle);
        PlayerPrefs.SetInt("LargeBottle", ItemStats.Instance.largeBottle);
        PlayerPrefs.SetInt("SmallMedkit", ItemStats.Instance.smallMedkit);
        PlayerPrefs.SetInt("LargeMedkit", ItemStats.Instance.largeMedkit);
    }

    int UpdateStat(string input, string key, int currentValue)
    {
        string cleaned = Regex.Replace(input, "[^0-9]", "");

        if (!string.IsNullOrEmpty(cleaned))
        {
            int newValue = int.Parse(cleaned);
            PlayerPrefs.SetInt(key, newValue);
            return newValue;
        }
        else
        {
            return currentValue; // Keep old value
        }
    }
}
