using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    public float timeLive;
    public bool autoDestroy;
    public string dontDestroyWhenMeet;
    public string effectTagName;

    void Start()
    {
        if (autoDestroy)
        {
            Destroy(gameObject, timeLive);
        }
    }

    public virtual void BeforeDestroy(Collision2D col)
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        BeforeDestroy(col);
        if (col.collider.tag == dontDestroyWhenMeet)
        {
            return;
        }
        Destroy(gameObject);
    }
    
}
