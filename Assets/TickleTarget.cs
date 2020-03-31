using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickleTarget : Target
{
    [SerializeField]
    private int minRepetitions = 4;
    [SerializeField]
    private int maxRepetitions = 6;
    private string[] keys = { "a", "s", "d", "f" };
    private Stack<string> nextKey = new Stack<string>();
    private int repetitions;
    private bool mouseOver = false;

    void Start()
    {
        keys = keys.Reverse().ToArray();
        repetitions = Random.Range(minRepetitions, maxRepetitions);

        for (int i = 0; i < repetitions; i++) {
            foreach (string key in keys) nextKey.Push(key);
        }
    }

    private void Update()
    {
        if(!mouseOver) {
            return;
        }

        switch (nextKey.Peek().ToUpper()) {
            case "A":
                if (Input.GetKeyDown(KeyCode.A)) IncreaseDoggyHappiness();
                break;
            case "S":
                if (Input.GetKeyDown(KeyCode.S)) IncreaseDoggyHappiness();
                break;
            case "D":
                if (Input.GetKeyDown(KeyCode.D)) IncreaseDoggyHappiness();
                break;
            case "F":
                if (Input.GetKeyDown(KeyCode.F)) IncreaseDoggyHappiness();
                break;
            default:
                break;
        }
    }

    private void IncreaseDoggyHappiness() {
        nextKey.Pop();

        remaining -= (100f / (repetitions * keys.Length));
        UpdateRadius();

        if (remaining <= 0.0f){
            TargetBoard.NotifyPointScored();
        }
    }

    void OnMouseEnter(){
        mouseOver = true;
    }

    void OnMouseExit(){
        mouseOver = false;
    }
}
