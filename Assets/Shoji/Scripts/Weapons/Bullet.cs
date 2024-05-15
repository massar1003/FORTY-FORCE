using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : WeaponBase
{
    [SerializeField]
    float moveSpeed;
    protected override void LateUpdate()
    {
        base.LateUpdate();
        transform.Translate(0, moveSpeed * Time.deltaTime, 0, Space.Self);
    }
    protected override void Attack()
    {
        Destroy(gameObject);
    }
}
