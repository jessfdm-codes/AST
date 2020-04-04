using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CircleCollider2D))]
public class SpinTarget : Target
{
    private float prevAngle;
    public float theta_to_prog_conv;
    private bool clockwise = false;

    // Update is called once per frame
    void Update()
    {
        if (mouseOver)
        {
            var point = Input.mousePosition;
            var diff = point - this.transform.position;

            var angle = Mathf.Atan2(diff.y, diff.x);

            //Check mouse movement
            if (clockwise)
            {
                //TODO
            }
            else
            {
                if (angle <= prevAngle)
                {

                }
                else if (angle > prevAngle || prevAngle > 3f && angle < 0f)
                {
                    remaining -= Mathf.Abs(angle - prevAngle) * theta_to_prog_conv;
                }
            }
            prevAngle = angle;
            UpdateRadius();

            if (remaining <= 0)
            {
                TargetBoard.NotifyPointScored();
            }
        }
    }
}
