using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon3 : WeaponBase
{
    [SerializeField]
    Animator anim;
    protected override void Attack()
    {
        base.Attack();
        anim.SetTrigger("Attack");
        source.PlayOneShot(attackClip);
    }
}
