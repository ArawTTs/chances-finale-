
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    //Health
    public int _pHealth = 100;
    public int PHealth
    {
        get => _pHealth;
        set => _pHealth = Mathf.Clamp(value, 0, MaxPHealth);
    }

    public int MaxPHealth = 100;
    private int _playerLife = 3;
    public int PlayerLife
    {
        get => _playerLife;
        set => _playerLife = Mathf.Clamp(value, 0, 3);
    }
    //Skill
    public int _pSkill = 3;
    public int PSkill
    {
        get => _pSkill;
        set => _pSkill = Mathf.Clamp(value, 0, 3);
    }

    //Attack Power 
    public int AttackPower = 10;
    //Magic Power 
    public int MagicPower = 10;
    public int Heal = 10;
    public int _allocationStats = 5;
    public int AllocationStats
    {
        get => _allocationStats;
        set => _allocationStats = Mathf.Clamp(value, 0, 10);
    }
    public int Money = 100;

    void Update()
    {
        if (PlayerLife <= 0 && PHealth <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        { // to prevent dupl.
            Destroy(gameObject);
        }
        GetPlayerPrefs();
        // SetPlayerPrefs();
    }
    // void LoadPlayerData()
    // {
    //     // Check if each key exists in PlayerPrefs before loading
    //     if (PlayerPrefs.HasKey("PHealth"))
    //         PHealth = PlayerPrefs.GetInt("PHealth");
    //     if (PlayerPrefs.HasKey("MaxPHealth"))
    //         MaxPHealth = PlayerPrefs.GetInt("MaxPHealth");
    //     if (PlayerPrefs.HasKey("PlayerLife"))
    //         PlayerLife = PlayerPrefs.GetInt("PlayerLife");
    //     if (PlayerPrefs.HasKey("PSkill"))
    //         PSkill = PlayerPrefs.GetInt("PSkill");
    //     if (PlayerPrefs.HasKey("AttackPower"))
    //         AttackPower = PlayerPrefs.GetInt("AttackPower");
    //     if (PlayerPrefs.HasKey("MagicPower"))
    //         MagicPower = PlayerPrefs.GetInt("MagicPower");
    //     if (PlayerPrefs.HasKey("AllocationStats"))
    //         AllocationStats = PlayerPrefs.GetInt("AllocationStats");
    // }

    void SetPlayerPrefs()
    {
        PlayerPrefs.SetInt("PHealth", PHealth);
        PlayerPrefs.SetInt("MaxPHealth", MaxPHealth);
        PlayerPrefs.SetInt("PlayerLife", PlayerLife);
        PlayerPrefs.SetInt("PSkill", PSkill);
        PlayerPrefs.SetInt("AttackPower", AttackPower);
        PlayerPrefs.SetInt("MagicPower", MagicPower);
        PlayerPrefs.SetInt("AllocationStats", AllocationStats);
        PlayerPrefs.SetInt("Money", Money);

        PlayerPrefs.Save();
    }

    void GetPlayerPrefs()
    {
        PHealth = PlayerPrefs.GetInt("PHealth", PHealth);
        MaxPHealth = PlayerPrefs.GetInt("MaxPHealth", MaxPHealth);
        PlayerLife = PlayerPrefs.GetInt("PlayerLife", PlayerLife);
        PSkill = PlayerPrefs.GetInt("PSkill", PSkill);
        AttackPower = PlayerPrefs.GetInt("AttackPower", AttackPower);
        MagicPower = PlayerPrefs.GetInt("MagicPower", MagicPower);
        AllocationStats = PlayerPrefs.GetInt("AllocationStats", AllocationStats);
        Money = PlayerPrefs.GetInt("Money", Money);

    }

    public void PrintStats()
    {
        Debug.Log(
            $"HP: {PHealth}/{MaxPHealth} | Life: {PlayerLife} | Skill: {PSkill} | " +
            $"AP: {AttackPower} | MP: {MagicPower} | Heal: {Heal} | " +
            $"Alloc: {AllocationStats}/10 | $: {Money}"
        );
    }


}
