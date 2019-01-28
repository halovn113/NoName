using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enum tạm sẽ update trong ScriptableObject của EnumGenerator
public enum AttackType
{
    Slash,
    Thrust,
    Shoot,
    Cast,
}

public enum AttackState
{
    Nope,
    Attacking,
    AfterAttack,
}

public class OnAttack : MonoBehaviour
{

    // public
    public Collider2D attackTrigger; // test collider
    public GameObject weapon; // test melee trước
    private AttackState attackState;

    public Bullet bullet;
    public Melee melee;
    
    public AttackType attackType;
    public float timeAttack = 0.5f; // 0.5 chỉ để test
    public float nextAttack = 0.5f; // 0.5 để test

    // private
    private float attackTimer;
    private float afterATimer;
    private Vector3 aimingPos;
    //private ObjectPooler objPooler; // test

    private Vector3 screenPoint;
    private Vector3 direction;

    public void Init()
    {
        //attackTrigger = GetComponent<Collider2D>();
        attackTimer = 0;
        afterATimer = 0;
        //weapon.SetActive(false);
        //attackTrigger.enabled = false;
        aimingPos = new Vector3(0, 0, 0);
        //objPooler = ObjectPooler.Instance;
    }

    [ContextMenu("Get collider")]
    public void EditorInit()
    {
        attackTrigger = GetComponentInChildren<Collider2D>();        
    }

    public void SetAttackState(AttackState state)
    {
        attackState = state;
        aimingPos = weapon.transform.position;
    }

    public void Attack()
    {
        attackState = AttackState.Attacking;
        attackTimer = timeAttack;
    }

    public AttackState GetAttackState()
    {
        return attackState;
    }


    void FixedUpdate()
    {
        switch (attackState)
        {
            case AttackState.Attacking:
                AttackWork();
                break;
            case AttackState.AfterAttack:
                AfterAttack();
                break;
        }
    }


    public void AttackWork()
    {
        switch (attackType)
        {
            case AttackType.Slash:
                //weapon.transform.position = aimingPos;
                ActiveTrigger();
                AttackSlash();
                break;

            case AttackType.Thrust:
                break;

            case AttackType.Shoot:
                AttackSlash();
                break;

            case AttackType.Cast:
                break;
        }
    }

    void AttackSlash()
    {
        //if (attackTimer > 0)
        //{
        //    attackTimer -= Time.deltaTime;
        //}
        //else
        //{
        //    weapon.SetActive(false);
        //    attackState = AttackState.AfterAttack;
        //}

        if (attackTimer > timeAttack)
        {
            attackTimer = 0;
            weapon.SetActive(false);
            DeactiveTrigger();
            attackState = AttackState.AfterAttack;
        }
        else
        {
            attackTimer += Time.deltaTime;
        }
    }

    void AfterAttack()
    {
        if (afterATimer > nextAttack)
        {
            afterATimer = 0;
            attackState = AttackState.Nope;
        }
        else
        {
            afterATimer += Time.deltaTime;
        }
    }

    public void ActiveTrigger()
    {
        attackTrigger.enabled = true;
    }

    public void DeactiveTrigger()
    {
        attackTrigger.enabled = false;
    }

    public virtual void AttackMelee(Vector3 position)
    {
        Melee _melee = Instantiate(melee);
        _melee.transform.position = position;
    }

    public virtual void AttackRange(Vector3 direction, string dontDestroyWhenMeet = "", string effectWhenMeet = "", Vector3 target = default(Vector3), Vector3 weaponPosition = default(Vector3))
    {
        Vector3 startPos = weaponPosition == default(Vector3) ? transform.position : weaponPosition; 
        Bullet _bullet = Instantiate(bullet);
        bullet.transform.position = startPos;
        if (target != default(Vector3))
        {
            bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, startPos - target);
        }
        bullet.Shoot(direction);

    }
}
