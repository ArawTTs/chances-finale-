using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerRhythm : MonoBehaviour
{
    public AudioClip soundDrop;
    public AudioClip soundWin;
    private AudioSource drop;
    private AudioSource win;

    public bool startPlaying;
    public BeatScroller beatScroller;
    public DisableRhythmHealth disableRhythmHealth;
    public GameFlowManagerLust gameFlowManagerLust;
    public static GameManagerRhythm instance;

    public int hitNote = 100;
    public int goodNote = 150;
    public int perfectNote = 200;
    public float missedNote = 1.5f;

    public int currentScore;
    public int currMultiplier;
    public int trackMultiplier;
    public int[] multiplierThreshold;

    public Text scoreText;
    public Text multiText;

    public HealthSystem gameplayLife;
    public GameObject musicAnalyzer1GO;
    public GameObject musicAnalyzer2GO;
    public GameObject musicAnalyzer3GO;

    private MusicAnalyzer activeMusicAnalyzer;
    private int consecutiveMissCount;
    public bool stopGame = false;
    public bool hasWon = false;

    void Start()
    {
        drop = gameObject.AddComponent<AudioSource>();
        win = gameObject.AddComponent<AudioSource>();

        drop.clip = soundDrop;
        win.clip = soundWin;

        instance = this;
        scoreText.text = "Score : 0";
        currMultiplier = 1;
        stopGame = false;


    }

    void Update()
    {
        if (musicAnalyzer1GO.activeSelf)
        {
            activeMusicAnalyzer = musicAnalyzer1GO.GetComponent<MusicAnalyzer>();

        }
        else if (musicAnalyzer2GO.activeSelf)
        {
            activeMusicAnalyzer = musicAnalyzer2GO.GetComponent<MusicAnalyzer>();

        }
        else if (musicAnalyzer3GO.activeSelf)
        {
            activeMusicAnalyzer = musicAnalyzer3GO.GetComponent<MusicAnalyzer>();

        }


        if (gameplayLife.health <= 0 && !hasWon)
        {
            win.Play();
            gameFlowManagerLust.WinLevel();
            activeMusicAnalyzer.StopMusicAnalyzer();

            stopGame = true;
            hasWon = true;

        }

        if (stopGame)
        {
            return;
        }
    }

    public void NoteHit()
    {
        consecutiveMissCount = 0;

        if (currMultiplier - 1 < multiplierThreshold.Length)
        {
            trackMultiplier++;
            if (multiplierThreshold[currMultiplier - 1] <= trackMultiplier)
            {
                trackMultiplier = 0;
                currMultiplier++;
            }
        }
        multiText.text = "Multiplier : x" + currMultiplier;
        scoreText.text = "Score : " + currentScore;
    }

    public void NormalHit()
    {
        currentScore += hitNote * currMultiplier;
        DealDamage(hitNote * currMultiplier);
        NoteHit();
    }

    public void GoodHit()
    {
        currentScore += goodNote * currMultiplier;
        DealDamage(goodNote * currMultiplier);
        NoteHit();
    }

    public void PerfectHit()
    {
        currentScore += perfectNote * currMultiplier;
        DealDamage(perfectNote * currMultiplier);
        NoteHit();
    }

    public void NoteMissed()
    {
        currMultiplier = 1;
        trackMultiplier = 0;
        gameplayLife.Heal((int)missedNote);

        multiText.text = "Multiplier : x" + currMultiplier;

        consecutiveMissCount++;
        if (consecutiveMissCount >= 5 && !stopGame)
        {

            // stopGame = true;
            consecutiveMissCount = 0;
            gameFlowManagerLust.LoseLevel();
            // activeMusicAnalyzer.Reset();
            // activeMusicAnalyzer.shouldStop = true;


        }
    }

    void DealDamage(int score)
    {
        int damage = (int)((float)score / activeMusicAnalyzer.damageRatio);
        gameplayLife.TakeDamage(damage);
    }
}
