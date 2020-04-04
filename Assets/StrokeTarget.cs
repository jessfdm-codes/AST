using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrokeTarget : Target
{

    void Update()
    {
        if (mouseOver){
            remaining -= Time.deltaTime * 50f;
            UpdateRadius();
        }

        if (remaining <= 0.0f){
            TargetBoard.NotifyPointScored();
        }
    }
}
