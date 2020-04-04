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

    void Update()
    {
        if(!mouseOver){
            return;
        }

        if (Input.GetMouseButtonDown(rubLeft ? 0 : 1)) {
        remaining -= Random.Range(mininumRub, maximumRub);
        UpdateRadius();
        rubLeft = !rubLeft;
        }
    
        if (remaining <= maximumRub){
            radius.material.SetFloat("_Cutoff", 100f);
            TargetBoard.NotifyPointScored();
        }
    }
}
