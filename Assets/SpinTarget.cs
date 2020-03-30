using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class SpinTarget : Target
{
    private bool entered = false;
    private float prevAngle;

    private bool clockwise = false;

    // Update is called once per frame
    void Update()
    {
        var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var diff = point - this.transform.position;

        if (diff.magnitude > 300){
            return;
        }

        var angle = Mathf.Atan2(diff.y, diff.x);

        //Check mouse movement
        if (clockwise){
            if (angle < prevAngle){
                prevAngle = angle;
            } else if (prevAngle < -3f && angle > 0f){
                prevAngle = angle;
                remaining -= 20;
            }
        } else {
            if (angle > prevAngle){
                prevAngle = angle;
            } else if (prevAngle > 3f && angle < 0f){
                prevAngle = angle;
                remaining -= 20;
            }
        }
        UpdateRadius();

        if (remaining <= 0){
            TargetBoard.NotifyPointScored();
        }
    }

    void OnMouseEnter(){
        entered = true;
    }
}
