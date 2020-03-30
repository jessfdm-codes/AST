using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubTarget : Target
{
    [SerializeField]
    private float mininumRub = 2f;
    [SerializeField]
    private float maximumRub = 4f;

    private bool rubLeft = true;

    void Update()
    {
        if (Input.GetKeyDown(rubLeft ? KeyCode.LeftArrow : KeyCode.RightArrow)) {
        remaining -= Random.Range(mininumRub, maximumRub);
        radius.material.SetFloat("_Cutoff", Mathf.Max((remaining / 100f), float.Epsilon));
        rubLeft = !rubLeft;
        Debug.Log(remaining);
        }
    
        if (remaining <= maximumRub){
            radius.material.SetFloat("_Cutoff", 100f);
            TargetBoard.NotifyPointScored();
        }
    }
}
