using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrokeTarget : MonoBehaviour
{

    public TargetBoard TargetBoard {get; set;}
    private bool mouseOver;
    private float remaining = 100.0f;

    [SerializeField]
    private SpriteRenderer radius;

    void Awake(){
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update(){
        if (mouseOver){
            remaining -= Time.deltaTime * 50f;
            radius.material.SetFloat("_Cutoff", Mathf.Max((remaining / 100f), float.Epsilon));
            Debug.Log(remaining);
        }

        if (remaining <= 0.0f){
            TargetBoard.NotifyPointScored();
        }
    }


    void OnMouseEnter(){
        Debug.Log("hello");
        mouseOver = true;
    }

    void OnMouseExit(){
        mouseOver = false;
    }
}
