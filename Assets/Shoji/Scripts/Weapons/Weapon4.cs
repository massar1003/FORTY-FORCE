using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon4 : WeaponBase
{
    [SerializeField]
    GameObject bullet;
    [SerializeField,Min(0)]
    int bulletCount;
    protected override void Attack()
    {
        base.Attack();
        source.PlayOneShot(attackClip);
        for (int i = 0; i < bulletCount; i++)
        {
            float calc = (float)i / bulletCount;
            Vector3 bulletDir = new Vector3(0, 0, calc * 360) + transform.eulerAngles;
            Vector3 bulletAdjust = new Vector3(-Mathf.Sin(bulletDir.z * Mathf.Deg2Rad), Mathf.Cos(bulletDir.z * Mathf.Deg2Rad));
            Instantiate(bullet, transform.position + bulletAdjust, Quaternion.Euler(bulletDir));
        }
    }
}
