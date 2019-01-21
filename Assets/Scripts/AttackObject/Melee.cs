using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : AttackObject
{
    public float width;
    public float height;

    void Start()
    {
        if (width != 0 && height != 0)
        {
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(width, height);
        }
    }

    public void SetSize(float width, float height)
    {
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(width, height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
