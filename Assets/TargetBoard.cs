using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[Serializable]
public class TargetBoard : MonoBehaviour
{
    //Set in editor
    [SerializeField]
    private GameObject[] targetPrefabs;
    public GameObject alertPrefab;
    [SerializeField]
    private Text nextBehaviourText;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text timerText;
    public float targetScore;

    private DogController source;
    public int gameLengthSeconds;
    public bool gameOver;
    private bool isVisible = false;
    private Target currTarget;
    private float score;
    private float timeLeft;    

    private void Awake()
    {
        gameOver = true;
    }

    void Start()
    {        
    }

    void Update()
    {
        UpdateVisibility();

        if (!gameOver)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = MakeSimpleTimerString();

            if (currTarget is null)
            {
                if (score < targetScore) SpawnTarget();
            }

            if (score >= targetScore)
            {
                EndGame(true);
            }
            else if (timeLeft <= 0f)
            {
                EndGame(false);                
            }                
        }        
    }

    public void StartGame(DogController source)
    {
        this.source = source;
        score = 0;
        timeLeft = (float)gameLengthSeconds;
        gameOver = false;
        isVisible = true;
    }

    void EndGame(bool success)
    {
        source.GameOver(success);
        gameOver = true;
        isVisible = false;
        if (currTarget != null)
        {
            Destroy(currTarget.gameObject);
            currTarget = null;
        }
        var msg = Instantiate(alertPrefab, this.transform);
        var txt = msg.GetComponent<Text>();        
        if (success)
        {
            txt.text = "You befriended the dog!";
            txt.color = Color.green;
        }
        else
        {
            txt.text = "The dog got bored...";
            txt.color = Color.red;
        }
    }

    private void UpdateVisibility()
    {
        nextBehaviourText.enabled = isVisible;
        scoreText.enabled = isVisible;
        timerText.enabled = isVisible;
    }

    public void NotifyPointScored(){
        score = Mathf.Min(targetScore, score + Random.Range(1f, 2.5f));
        scoreText.text = $"{score.ToString()}/{targetScore.ToString()}"; // plan to use a progress bar in future

        Destroy(currTarget.gameObject);
        currTarget = null;       
    }

    private void SpawnTarget(){
        Vector3[] corners = new Vector3[4];
        ((RectTransform) transform).GetWorldCorners(corners);

        //Get random point
        float x = Mathf.Lerp(corners[0].x, corners[3].x, Random.Range(0f,1f));
        float y = Mathf.Lerp(corners[0].y, corners[1].y, Random.Range(0f,1f));

        var newGo = Instantiate(targetPrefabs[Random.Range(0, targetPrefabs.Length)],transform);
        (newGo.transform as RectTransform).position = new Vector2(x, y);
        currTarget = newGo.GetComponent<Target>();
        currTarget.TargetBoard = this;

        switch (newGo.name) {
            case "RubTarget(Clone)":
                nextBehaviourText.text = "Rub!";
                break;
            case "SpinTarget(Clone)":
                nextBehaviourText.text = "Spin!";
                break;
            case "StrokeTarget(Clone)":
                nextBehaviourText.text = "Stroke!";
                break;
            case "TickleTarget(Clone)":
                nextBehaviourText.text = "Tickle!";
                break;
            default:
                break;
        }
    }

    // if we're happy showing time just as seconds (e.g. 120), use this
    private string MakeSimpleTimerString() =>
        Mathf.CeilToInt(timeLeft).ToString();

    // if we want minutes (e.g. 2:00), use this
    private string MakeTimerString() {
        int secondsLeft = Mathf.CeilToInt(timeLeft % 60);

        string seconds = secondsLeft < 10
            ? $"0{secondsLeft.ToString()}"
            : (secondsLeft == 60
                ? "00"
                : secondsLeft.ToString());

        int minutesLeft = Mathf.FloorToInt((timeLeft + 1) / 60f);

        return $"{minutesLeft.ToString()}:{seconds}";
    }
}
