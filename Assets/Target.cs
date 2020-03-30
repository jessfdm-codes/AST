using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    protected SpriteRenderer radius;

    public TargetBoard TargetBoard {get; set;}
    protected float remaining = 100.0f;

    void Awake(){
    }
    
    void Start(){
    }
}
