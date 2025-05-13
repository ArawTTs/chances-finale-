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
        bool isActive = cheat.activeSelf;
        cheat.SetActive(!isActive);
    }

    public void OnClickUpdateStats()
    {
        UpdateStat(AP.text, "AttackPower", out int attackPower);
        PlayerStats.Instance.AttackPower = attackPower;

        UpdateStat(MP.text, "MagicPower", out int magicPower);
        PlayerStats.Instance.MagicPower = magicPower;

        UpdateStat(maxPHealth.text, "MaxPHealth", out int maxHealth);
        PlayerStats.Instance.MaxPHealth = maxHealth;

        UpdateStat(money.text, "Money", out int playerMoney);
        PlayerStats.Instance.Money = playerMoney;
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
    void UpdateStat(string input, string key, out int statField)
    {
        string cleaned = Regex.Replace(input, "[^0-9]", "");

        if (!string.IsNullOrEmpty(cleaned))
        {
            int.TryParse(cleaned, out statField);
            PlayerPrefs.SetInt(key, statField);
        }
        else
        {
            statField = PlayerPrefs.GetInt(key, 0); // Keep old value
        }
    }

}
