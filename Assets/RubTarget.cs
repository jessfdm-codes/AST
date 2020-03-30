using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class RubTarget : Target
{
    [SerializeField]
    private float mininumRub = 2f;
    [SerializeField]
    private float maximumRub = 4f;

    private bool rubLeft = true;
    
    private bool mouseOver = false;
    void Update()
    {
        if(!mouseOver){
            return;
        }

        if (Input.GetMouseButtonDown(rubLeft ? 0 : 1)) {
        remaining -= Random.Range(mininumRub, maximumRub);
        radius.material.SetFloat("_Cutoff", Mathf.Max((remaining / 100f), float.Epsilon));
        rubLeft = !rubLeft;
        }
    
        if (remaining <= maximumRub){
            radius.material.SetFloat("_Cutoff", 100f);
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
