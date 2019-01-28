using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public UnitType unitType;
    public UnitState unitState;
    public float moveSpeed;

    private Vector3 _moveVector;
    private Vector3 _endPos;
    private Vector3 _startPos;

    private float lerpTime = 1f;
    private float currentLerpTime;

    public virtual void HealthApply(float number)
    {
        if (unitType == UnitType.UntouchNPC) return;
        if (health + number <= 0)
        {
            Dead();
            return;
        }
        if (health + number > maxHealth)
        {
            health = maxHealth;
            return;
        }
        health += number;
    }

    public virtual void Dead()
    {
        health = 0;
        unitState = UnitState.Dead;
        Debug.Log("Unit Dead");
    }

    public UnitState GetUnitState()
    {
        return unitState;
    }

    void _unitMove()
    {
        //_startPos = transform.position;
        //_endPos = transform.position + _moveVector.normalized * moveSpeed;
        //if (currentLerpTime > lerpTime)
        //{
        //    currentLerpTime = lerpTime;
        //}
        //float perc = currentLerpTime / lerpTime;
        transform.position += _moveVector.normalized * moveSpeed * Time.deltaTime;
        //transform.position = Vector3.Lerp(_startPos, _endPos, perc);
        //Debug.Log("moving");
    }

    public void Move(Vector3 moveVector)
    {
        _moveVector = moveVector;
    }

    void Update()
    {
        _unitMove();
    }

    public static class DIRECTION
    {
        public static string LEFT = "DIRECTION.LEFT";
        public static string RIGHT = "DIRECTION.RIGHT";
        public static string UP = "DIRECTION.UP";
        public static string DOWN = "DIRECTION.DOWN";

    }
}

public enum UnitType
{
    Player,
    Enemy,
    Allies,
    NPC,
    UntouchNPC,
}

public enum UnitState
{
    Alive,
    Dead,
    Stun,
    Idle,
}
