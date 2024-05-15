using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [SerializeField]
    protected int damage = 1;
    public int Damage { get => damage; }

    [SerializeField,Min(0.016f)]
    protected float attackRate = 1;//ñàïbÇÃçUåÇâÒêî
    protected float curAttackTime;

    [SerializeField]
    protected AudioSource source;
    [SerializeField]
    protected AudioClip attackClip;
    bool isStop=false;

    public void InitWeapon()
    {
        curAttackTime = 1;
    }
    public virtual void StopWeapon()
    {
        isStop = true;
    }
    protected virtual void ResetWeapon()
    {
        curAttackTime = 0;
    }
    protected virtual void LateUpdate()
    {
        if(isStop) return;

        curAttackTime += Time.deltaTime * attackRate;
        if (curAttackTime < 1) return;
        Attack();
    }
    protected virtual void Attack()
    {
        ResetWeapon();
    }
}
