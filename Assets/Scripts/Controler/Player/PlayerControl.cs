using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    enum Dash
    {
        Nope,
        Dashing,
        AfterDash,

    }

    Dash dashCon;
    AttackState attackCon;

    public float maxSpeed;
    public KeyBinding keys;

    // test fields
    public OnPlayerAttack onAttack;
    public bool melee;

    public Vector3 attackDirection;

    private Vector3 mousePos;
    private OnUnitLook onUnitLook;
    private float _dashTime;
    // Use this for initialization

    private Vector3 moveVector;
    private bool _facingRight;
    private float _afterDash;

    private float normalSpeed;
    private float currentSpeed;
    private float _mouseAngle;


    public void Init()
    {
        keys = GetComponent<KeyBinding>();
        keys.Init();
        moveVector = new Vector3(0, 0, 0);
        //normalSpeed = gameObject.GetComponent<PlayerStats>().moveSpeed;
        currentSpeed = normalSpeed;
        onUnitLook = gameObject.GetComponent<OnUnitLook>();
        onUnitLook.SetTarget(mousePos);
        _dashTime = 0;
        _afterDash = 0;
        dashCon = Dash.Nope;
        onAttack.Init();
        attackCon = onAttack.GetAttackState();
    }

    void Start ()
    {
        
    }

    void LateUpdate()
    {
        Control();
        Move();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        onUnitLook.RotationUpdate(mousePos);
        RotationControl();
    }

    void Control()
    {
        if (Input.GetKey(keys.GetKey("Up")) && onAttack.GetAttackState() != AttackState.Attacking)
        {
            moveVector.y = 1;
        }
        else if (Input.GetKey(keys.GetKey("Down")) && onAttack.GetAttackState() != AttackState.Attacking)
        {
            moveVector.y = -1;
        }
        else
        {
            moveVector.y = 0;
        }

        if (Input.GetKey(keys.GetKey("Left")) && onAttack.GetAttackState() != AttackState.Attacking)
        {
            moveVector.x = -1;
        }
        else if (Input.GetKey(keys.GetKey("Right")) && onAttack.GetAttackState() != AttackState.Attacking)
        {
            moveVector.x = 1;
        }
        else
        {
            moveVector.x = 0;
        }

        if (Input.GetKey(keys.GetKey("Dash")) && dashCon == Dash.Nope)
        {
            dashCon = Dash.Dashing;
        }

        if (Input.GetKey(keys.GetKey("Attack1")) && onAttack.GetAttackState() == AttackState.Nope)
        {
            if (melee)
            {
                onAttack.AttackMelee(attackDirection);
            }
            //onAttack.SetAttackState(AttackState.Attacking);

        }
    }

    void Move()
    {
        //transform.position += moveVector.normalized * currentSpeed * Time.deltaTime;
        gameObject.GetComponent<Player>().Move(moveVector);

        switch (dashCon)
        {
            case Dash.Dashing:
                //transform.position += moveVector.normalized * currentSpeed * 3 * Time.deltaTime;
                //gameObject.GetComponent<Rigidbody2D>().velocity += moveVector.normalized * 100 * currentSpeed * Time.deltaTime;

                break;

            case Dash.AfterDash:
                if (_afterDash > 0.5f)
                {
                    _afterDash = 0;
                    dashCon = Dash.Nope;
                }
                else
                {
                    _afterDash += Time.deltaTime;
                }
                break;
        }

        DashCondition();

    }

    void DashCondition()
    {
        if (_dashTime > 0.3f)
        {
            _dashTime = 0;
            dashCon = Dash.AfterDash;
        }
        else
        {
            _dashTime += Time.deltaTime;
        }
    }

    void RotationControl()
    {
        if (transform.localScale.x > 0) // nhìn về bên phải 
        {
            if (mousePos.x > transform.position.x)
            {
                _facingRight = true;
            }
        }
        else
        {
            if (mousePos.x < transform.position.x)
            {
                _facingRight = false;
            }
        }

        _mouseAngle = Utilities.GetAngleBetween(gameObject, mousePos);
        if (_mouseAngle >= 315 || _mouseAngle <= 45) // right
        {
            //Debug.Log("right");
            gameObject.GetComponent<Player>().ChangeLookDirection(Unit.DIRECTION.RIGHT);
        }
        else if (_mouseAngle > 45 && _mouseAngle <= 135) // up
        {
            //Debug.Log("up");
            gameObject.GetComponent<Player>().ChangeLookDirection(Unit.DIRECTION.UP);
        }
        else if (_mouseAngle > 135 && _mouseAngle <= 225) // left
        {
            //Debug.Log("Left");
            gameObject.GetComponent<Player>().ChangeLookDirection(Unit.DIRECTION.LEFT);
        }
        else if (_mouseAngle > 225 && _mouseAngle < 315) // down
        {
            //Debug.Log("down");
            gameObject.GetComponent<Player>().ChangeLookDirection(Unit.DIRECTION.DOWN);
        }
    }

}
