using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetBoard : MonoBehaviour
{
    [SerializeField]
    private GameObject[] targetPrefabs;
    [SerializeField]
    private Text nextBehaviourText;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text timerText;

    public float targetScore;
    public int gameLengthSeconds;
    public bool gameOver { get; private set; }

    private Target currTarget;
    private float score = 0;
    private float timeLeft;

    void Start()
    {
        timeLeft = (float) gameLengthSeconds;
        SpawnTarget();
    }

    void Update()
    {
        if (gameOver) {
            return;
        }

        if (score >= targetScore) {
            gameOver = true;
            nextBehaviourText.text = "You befriended the dog!";
            return;
        }

        timeLeft -= Time.deltaTime;
        timerText.text = MakeSimpleTimerString();

        if (timeLeft <= 0f) {
            gameOver = true;
            nextBehaviourText.text = "The dog got bored...";
        }
    }

    public void NotifyPointScored(){
        score = Mathf.Min(targetScore, score + Random.Range(1f, 2.5f));
        scoreText.text = $"{score.ToString()}/{targetScore.ToString()}"; // plan to use a progress bar in future

        Destroy(currTarget.gameObject);
        currTarget = null;

        if (score < targetScore) SpawnTarget();
    }

    private void SpawnTarget(){
        Vector3[] corners = new Vector3[4];
        ((RectTransform) transform).GetWorldCorners(corners);

        //Get random point
        float x = Mathf.Lerp(corners[0].x, corners[3].x, Random.Range(0f,1f));
        float y = Mathf.Lerp(corners[0].y, corners[1].y, Random.Range(0f,1f));

        var newGo = Instantiate(targetPrefabs[Random.Range(0, targetPrefabs.Length)]);
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
