using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class AlertTextBehaviour : MonoBehaviour
{

    public Text text;
    public float displayTime;
    public float timeLeft;

    private void Awake()
    {
        timeLeft = displayTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if(timeLeft <= 0)
        {
            Destroy(this.gameObject);
        }

        float a = timeLeft / displayTime;
        text.color = new Color(text.color.r, text.color.g, text.color.b, a);
    }
}
