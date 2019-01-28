using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHumanroid : Unit
{
    public Animator animator;

    void Start()
    {
        if (animator == null)
        {
            Debug.LogWarning("Warning, no animator attach with this unit");
        }
    }

    public void ChangeLookDirection(string direction)
    {
        if (direction == DIRECTION.LEFT)
        {
            SetLookDirectionAnimation(1);
        }
        else if (direction == DIRECTION.RIGHT)
        {
            SetLookDirectionAnimation(2);

        }
        else if (direction == DIRECTION.UP)
        {
            SetLookDirectionAnimation(0);
        }
        else if (direction == DIRECTION.DOWN)
        {
            SetLookDirectionAnimation(0);
        }
    }

    public void SetLookDirectionAnimation(float time)
    {
        animator.Play("Unit_Human", -1, (1f / 2) * time);
        animator.Update(0f);
    }
}
