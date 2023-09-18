using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[System.Serializable]
public class PlayerInfo
{
    public int finalScoreBestValue = 0;
}
public class Movement1 : MonoBehaviour
{
    public PlayerInfo pl;
    public static Movement1 Instance;

    [DllImport("__Internal")]
    private static extern void SaveExtern(string date);

    [DllImport("__Internal")]
    private static extern void LoadExtern();

    [DllImport("__Internal")]
    private static extern void SetToLeaderboard(int value);

    [DllImport("__Internal")]
    private static extern void ShowAdv();

    [DllImport("__Internal")]
    private static extern void AddExtraTimeExtern();
    [DllImport("__Internal")]
    private static extern void RateGame();

    public Transform[] arrows;
    public Image[] arrowsImage;
    public KeyCode[] arrowKeys;
    public TextMeshProUGUI[] wrongsText;

    public Transform objCanvas;
    private float moveSpeed = 60f;
    private int directionArrow;

    public TextMeshProUGUI scoreValue;
    public TextMeshProUGUI TimeValue;
    public TextMeshProUGUI scoreMultiValue;
    public TextMeshProUGUI scoreMultiValue1;

    public Image pointingImage;
    public Image movingImage;
    public GameObject gameoverPanel;
    public TextMeshProUGUI finalScore,finalScoreBest;
    public Transform arrowTransform;

    private int score = 0;
    private bool isGameOver = false;
    private int saveInt = 0;
    private int saveIntArrow = 1;
    private int count = 0;
    private bool changeVersion = false;

    private float timer = 30;
    private TimeSpan timespan;
    private Vector3 targetPosition;
    private int xMulti = 1;
    private int xCounter = 0;
    private int xCounterMulti = 2;


    private float Starttimer = 3f;
    private bool Startgame = true;
    public GameObject startPanel;
    public TextMeshProUGUI startTimers;
    public GameObject newRecord;
    public GameObject advButton;

    public AudioSource correctAudio;
    public AudioSource wrongAudio;

    public Image sound;
    public Sprite muteAudio;
    public Sprite unmuteAudio;

    private bool siund = false;
    private bool advClosed = false;

    private int n = 0;

    [Header("Audio")]
    public AudioSource[] audio;

    void Awake()
    {
        if (Instance == null)
        {
            transform.parent = null;
            Instance = this;

            LoadExtern();
            ShowAdv();
            PauseAudi();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        StartRound2();
        StartRound1();
    }
    void Update()
    {
        if (advClosed)
        {
            int roundedValue = Mathf.RoundToInt(Starttimer);
            if (Language.Instance.CurrentLanguage == "en")
            {
                startTimers.text = "Game starts in " + roundedValue + " seconds";
            }
            else if (Language.Instance.CurrentLanguage == "ru")
            {
                startTimers.text = "Игра начнётся через " + roundedValue + " секунды";
            }

            if (Startgame)
            {
                Starttimer -= 1 * Time.deltaTime;

                if (Starttimer <= 0)
                {
                    Startgame = false;
                    startPanel.SetActive(false);
                }
            }
            if (!isGameOver && !Startgame)
            {
                //scoreMultiValue1.text = pl.finalScoreBestValue.ToString();
                if (timer <= 0)
                {
                    GameOver();
                }
                arrowTransform.Translate(targetPosition * moveSpeed * Time.deltaTime);
                timer -= 1 * Time.deltaTime;

                if (count == 5)
                {
                    changeVersion = !changeVersion;
                    count = 0;
                }

                if (!changeVersion)
                {
                    pointingImage.color = Color.green;
                    movingImage.color = Color.white;
                    foreach (Image arrow in arrowsImage)
                    {
                        arrow.color = Color.white;
                    }
                }
                else
                {
                    movingImage.color = new Color(255 / 255f, 150 / 255f, 0, 255 / 255f); ;
                    pointingImage.color = Color.white;
                    foreach (Image arrow in arrowsImage)
                    {
                        arrow.color = new Color(255 / 255f, 150 / 255f, 0, 255 / 255f);
                    }
                }

                if (Input.GetKeyDown(arrowKeys[0]))
                {
                    if (changeVersion)
                    {
                        if (targetPosition == Vector3.left)
                        {
                            ScorePoint();
                            count++;
                            correctAudio.Play();
                        }
                        else
                        {
                            wrongAudio.Play();
                            xMulti = 1;
                            xCounterMulti = 2;
                            xCounter = 0;
                            count++;
                            StartRound2();
                            StartRound1();
                        }
                    }
                    else
                    {
                        if (0 == directionArrow)
                        {
                            ScorePoint();
                            count++;
                            correctAudio.Play();
                        }
                        else
                        {
                            wrongAudio.Play();
                            xMulti = 1;
                            xCounterMulti = 2;
                            xCounter = 0;
                            count++;
                            StartRound2();
                            StartRound1();
                        }
                    }
                }
                else if (Input.GetKeyDown(arrowKeys[1]))
                {
                    if (changeVersion)
                    {
                        if (targetPosition == Vector3.right)
                        {
                            ScorePoint();
                            count++;
                            correctAudio.Play();
                        }
                        else
                        {
                            wrongAudio.Play();
                            xMulti = 1;
                            xCounterMulti = 2;
                            xCounter = 0;
                            count++;
                            StartRound2();
                            StartRound1();
                        }
                    }
                    else
                    {
                        if (1 == directionArrow)
                        {
                            ScorePoint();
                            count++;
                            correctAudio.Play();
                        }
                        else
                        {
                            wrongAudio.Play();
                            xMulti = 1;
                            xCounterMulti = 2;
                            xCounter = 0;
                            count++;
                            StartRound2();
                            StartRound1();
                        }
                    }
                }
                else if (Input.GetKeyDown(arrowKeys[2]))
                {

                    if (changeVersion)
                    {
                        if (targetPosition == Vector3.up)
                        {
                            ScorePoint();
                            count++;
                            correctAudio.Play();
                        }
                        else
                        {
                            wrongAudio.Play();
                            xMulti = 1;
                            xCounterMulti = 2;
                            xCounter = 0;
                            count++;
                            StartRound2();
                            StartRound1();
                        }
                    }
                    else
                    {
                        if (2 == directionArrow)
                        {
                            ScorePoint();
                            count++;
                            correctAudio.Play();
                        }
                        else
                        {
                            wrongAudio.Play();
                            xMulti = 1;
                            xCounterMulti = 2;
                            xCounter = 0;
                            count++;
                            StartRound2();
                            StartRound1();
                        }
                    }
                }
                else if (Input.GetKeyDown(arrowKeys[3]))
                {

                    if (changeVersion)
                    {
                        if (targetPosition == Vector3.down)
                        {
                            ScorePoint();
                            count++;
                            correctAudio.Play();
                        }
                        else
                        {
                            wrongAudio.Play();
                            xMulti = 1;
                            xCounterMulti = 2;
                            xCounter = 0;
                            count++;
                            StartRound2();
                            StartRound1();
                        }
                    }
                    else
                    {
                        if (3 == directionArrow)
                        {
                            ScorePoint();
                            count++;
                            correctAudio.Play();
                        }
                        else
                        {
                            wrongAudio.Play();
                            xMulti = 1;
                            xCounterMulti = 2;
                            xCounter = 0;
                            count++;
                            StartRound2();
                            StartRound1();
                        }
                    }
                }
            }

            scoreValue.text = score.ToString();
            scoreMultiValue.text = "x " + xMulti.ToString();
            timespan = TimeSpan.FromSeconds(timer);
            string formattedTime = string.Format("{0:D2}:{1:D2}", timespan.Minutes, timespan.Seconds);

            TimeValue.text = formattedTime;

            if (xCounter >= xCounterMulti)
            {
                xMulti++;
                xCounter = 0;
                xCounterMulti *= 2;
            }
        }

    }

    public void AdvClosedMethod()
    {
        advClosed = true;
    }
    private void GameOver()
    {
        if (score >= pl.finalScoreBestValue)
        {
            pl.finalScoreBestValue = score;
            newRecord.SetActive(true);
        }
        gameoverPanel.SetActive(true);
        finalScore.text = score.ToString();
        finalScoreBest.text = pl.finalScoreBestValue.ToString();

        Save();
        isGameOver = true;
        if (n == 1)
        {
            advButton.SetActive(false);
        }
        else
        {
            advButton.SetActive(true);
        }
    }
    public void Save()
    {
        string jsonString = JsonUtility.ToJson(pl);
        SaveExtern(jsonString);
    }

    public void Leaderboard()
    {
        int finalScore = (int)pl.finalScoreBestValue;
        SetToLeaderboard(finalScore);
    }

    public void SetPlayerInfo(string value)
    {
        pl = JsonUtility.FromJson<PlayerInfo>(value);
    }
    public void PauseAudi()
    {
        foreach (AudioSource aui in audio)
        {
            aui.mute = true;
        }
    }
    public void UnPauseAudi()
    {
        foreach (AudioSource aui in audio)
        {
            aui.mute = false;
        }

    }

    public void AdvExtraTime()
    {
        PauseAudi();
        AddExtraTimeExtern(); 
    }

    public void level()
    {
        n = 0;
        Leaderboard();
        SceneManager.LoadScene(1);
    }
    public void AddTime()
    {
        timer += 30f;
        isGameOver = false;
        gameoverPanel.SetActive(false);
        n += 1;    
    }
    private void StartRound1()
    {
        int randi = UnityEngine.Random.Range(0, 4);
        while (randi == saveInt)
        {
            randi = UnityEngine.Random.Range(0, 4);
        }
        saveInt = randi;

        switch (randi)
        {
            case 0:
                targetPosition = Vector3.left;
                break;
            case 1:
                targetPosition = Vector3.right;
                break;
            case 2:
                targetPosition = Vector3.up;
                break;
            case 3:
                targetPosition = Vector3.down;
                break;
        }
    }
    private void StartRound2()
    {
        int randi = UnityEngine.Random.Range(0, 4);
        while (randi == saveIntArrow)
        {
            randi = UnityEngine.Random.Range(0, 4);
        }
        saveIntArrow = randi;

        foreach (Transform arrow in arrows)
        {
            switch (randi)
            {
                case 0:
                    arrow.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                    directionArrow = 0;
                    break;
                case 1:
                    arrow.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 270f));
                    directionArrow = 1;
                    break;
                case 2:
                    arrow.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    directionArrow = 2;
                    break;
                case 3:
                    arrow.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
                    directionArrow = 3;
                    break;
            }
        }
    }
    private void ScorePoint()
    {
        xCounter++;

        score += 50 * xMulti;
        StartRound2();
        StartRound1();
        
    }
    public void RateGameButton()
    {
        RateGame();
    }

    public void unmuteAndMuteAudio()
    {
        if (!siund)
        {
            sound.sprite = muteAudio;
            siund = true;
            PauseAudi();
        }
        else
        {
            sound.sprite = unmuteAudio;
            siund = false;
            UnPauseAudi();
        }
        
    }
}