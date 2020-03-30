using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrokeTarget : Target
{
    private bool mouseOver;

    void Update()
    {
        if (mouseOver){
            remaining -= Time.deltaTime * 50f;
            radius.material.SetFloat("_Cutoff", Mathf.Max((remaining / 100f), float.Epsilon));
        }

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
