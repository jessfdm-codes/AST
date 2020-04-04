using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[Serializable]
public class Target : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    protected Image radius;

    public TargetBoard TargetBoard {get; set;}
    public float remaining = 100.0f;
    public bool mouseOver = false;

    void Awake(){
    }
    
    protected void Start(){
        UpdateRadius();
    }

    protected void UpdateRadius(){
        if (TargetBoard.gameOver) {
            return;
        }
        var r = remaining / 100f;
        Debug.Log(r);
        var v = Mathf.Max(r, float.Epsilon);
        radius.material.SetFloat("_Cutoff", v);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse enter");
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exit");
        mouseOver = false;
    }
}
