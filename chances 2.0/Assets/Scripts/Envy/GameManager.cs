using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Threading;

public class GameManager : MonoBehaviour
{
    public SkillOption skillOption;
    public GameObject youWin;
    public GameObject youLose;
    public GameObject WholeGame;
    public GameObject Envy;
    public GameObject EnemyLife;
    public GameObject Timer;
    public GameObject playerBack;
    public GameObject playerFront;
    public GameObject attackbtn;
    public GameObject EnemyDeath;
    public Item item;

    private bool isRed = false;
    public Sprite bgImage;
    public List<Button> btns = new List<Button>();

    public Sprite[] puzzles;
    public List<Sprite> gamePuzzles = new List<Sprite>();

    private bool firstGuess, secondGuess;
    private int countCorrectGuesses;
    private int gameGuesses;
    private int firstGuessIndex, secondGuessIndex;
    private string firstGuessPuzzle, secondGuessPuzzle;

    public HealthSystem enemyLife;
    public TimeCode timeCode;
    public AddButtons addButtons;
    public CameraSwitch cameraSwitch;
    public GameManagerEnvyNew gameManagerEnvy;

    public GameObject[] toShow;
    public GameObject[] toHide;

    public GameObject[] LosetoShow;
    public GameObject[] LosetoHide;

    [SerializeField] private SpriteRenderer spriteRenderer;
    private Color origColor = new Color(1f, 1f, 1f, 1f);

    public void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("Sprites/1 Envy/puzzles");




        SpriteRenderer spriteRenderer = Envy.GetComponent<SpriteRenderer>();
    }


    void OnEnable()
    {
        timeCode.loseIndicator = false;
        youWin.SetActive(false);

        GetButtons();
        // LevelUpProgression();
        AddListeners(); //button listener
        AddGamePuzzles(); // add game puzezle
        Shuffle(gamePuzzles); // shuffle the game puzzle
        gameGuesses = gamePuzzles.Count / 2; //defines the total guesses

    }

    public void RegisterButton(Button button)
    {
        btns.Add(button);
        button.image.sprite = bgImage;
    }


    void GetButtons()
    {
        if (btns.Count == 0)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Box1");

            for (int i = 0; i < objects.Length; i++)
            {
                btns.Add(objects[i].GetComponent<Button>());
                btns[i].image.sprite = bgImage;
            }
        }
    }

    public void AddGamePuzzles()
    {
        int looper = btns.Count;
        int index = 0;
        if (gamePuzzles.Count > 0)
        {
            gamePuzzles.Clear();
        }
        for (int i = 0; i < looper; i++)
        {
            if (index == looper / 2)
            {
                index = 0;
            }
            gamePuzzles.Add(puzzles[index]);
            index++;
        }
    }

    public void AddListeners()
    {
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() => PickPuzzle());
        }
    }

    public void PickPuzzle()
    {
        if (!firstGuess && UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable)
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
        }
        else if (!secondGuess && UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable && firstGuessIndex != int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name))
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;

            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];

            if (firstGuessPuzzle == secondGuessPuzzle)
            {
            }
            else
            {
            }

            StartCoroutine(checkThePuzleMatch());
        }
    }

    IEnumerator checkThePuzleMatch()
    {
        yield return new WaitForSeconds(0.2f);

        if (firstGuessPuzzle == secondGuessPuzzle)
        {
            yield return new WaitForSeconds(0.2f);

            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            CheckTheGameFinished();
        }
        else
        {
            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;
        }

        yield return new WaitForSeconds(0.2f);

        firstGuess = secondGuess = false;
    }

    public void ResetGame()
    {
        firstGuess = secondGuess = false;
        countCorrectGuesses = 0;

        // Reset buttons
        foreach (var btn in btns)
        {
            btn.interactable = true;
            btn.image.color = Color.white;
            btn.image.sprite = bgImage;
        }
        float percent = enemyLife.health / enemyLife.maxHealth * 100f;

        if (percent >= 80f)
        {
            timeCode.initialCountdownDuration = 20f;
        }
        else if (percent >= 60f && percent <= 80f)
        {
            timeCode.initialCountdownDuration = 15f;
        }
        else if (percent == 40f)
        {
            timeCode.initialCountdownDuration = 15f;
        }
        GetButtons();

        AddListeners();
        AddGamePuzzles();
        gameGuesses = gamePuzzles.Count / 2;

        timeCode.ResetTimer();
        Shuffle(gamePuzzles);

        skillOption.attack = false;
        skillOption.shield = false;
    }

    public void ClearPuzzles()
    {
        gamePuzzles.Clear();
        btns.Clear();

        addButtons.GenerateButtons();

    }
    public void CheckTheGameFinished()
    {
        countCorrectGuesses++;
        if (countCorrectGuesses == gameGuesses)
        {
            skillOption.HideShield();

            gameManagerEnvy.check = true;

            //  Damage Envy *** 

            int totalDamage = PlayerPrefs.GetInt("AttackPower", PlayerStats.Instance.AttackPower);

            if (skillOption != null && skillOption.attack == true)
            {
                totalDamage += PlayerPrefs.GetInt("MagicPower", PlayerStats.Instance.MagicPower);

                skillOption.attack = false;
            }

            if (item.itemB == false)
            {
                enemyLife.TakeDamage(totalDamage);
                gameManagerEnvy.StartBlinking();
            }
            item.itemB = false;

            // ***

            Invoke("CallReturn", 1f);


            EnemyLife.SetActive(false);

            ClearPuzzles();

            if (enemyLife.health <= 0)
            {

                WholeGame.SetActive(false);
                Envy.SetActive(false);
                EnemyLife.SetActive(false);

                youWin.SetActive(true);
                Timer.SetActive(false);
            }

            // LevelUpProgression();
            addButtons.totalBoxes += 2;


            toShow.ToList().ForEach(button =>
            {
                button.SetActive(true);
            });
            toHide.ToList().ForEach(button =>
            {
                button.SetActive(false);
            });

            ResetGame();

        }
    }



    // private void LevelUpProgression()
    // {
    //     if (enemyLife.health > 80 && enemyLife.health <= 90)
    //     {
    //         addButtons.totalBoxes += 2;
    //     }
    //     else if (enemyLife.health >= 60 && enemyLife.health <= 80)
    //     {
    //         addButtons.totalBoxes += 2;
    //     }
    //     else if (enemyLife.health == 40)
    //     {
    //         addButtons.totalBoxes += 2;
    //     }

    // }

    private void CallReturn()
    {
        gameManagerEnvy.ReturnAll();
    }

    void Blink()
    {
        spriteRenderer.color = isRed ? origColor : Color.red;
        isRed = !isRed;
    }

    public void StartBlinking()
    {
        InvokeRepeating("Blink", 0f, 0.1f);
        Invoke("StopBlinking", 1f);
    }

    public void StopBlinking()
    {
        CancelInvoke("Blink");
        spriteRenderer.color = origColor;
    }

    private void AnimatePlayer()
    {
        cameraSwitch.FightScene();
        EnemyLife.SetActive(true);
    }



    public void Shuffle(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
