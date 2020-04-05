using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MainMenu : MonoBehaviour
{
    private Animator animator;

    // TRIGGERS
    private string _T_GOTO_MAIN = "GoTo_Main";
    private string _T_GOTO_MODESELECT = "GoTo_ModeSelect";
    private string _T_EXIT = "Exit";

    void Awake(){
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)){
            Button_Stroke();
        }
    }

    public void Button_Stroke(){
        animator.SetTrigger(_T_GOTO_MODESELECT);
        animator.SetTrigger(_T_EXIT);
    }

    public void Button_StrokeTogether(){
        animator.SetTrigger(_T_GOTO_MAIN);
        animator.SetTrigger(_T_EXIT);
    }

    public void Button_StrokeAlone(){
        animator.SetTrigger(_T_GOTO_MAIN);
        animator.SetTrigger(_T_EXIT);
    }
}
