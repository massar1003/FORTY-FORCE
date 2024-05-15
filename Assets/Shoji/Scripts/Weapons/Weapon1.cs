using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1 : WeaponBase
{
    [SerializeField]
    float attackSpeed = 0.2f, attackAngle = 60;
    [SerializeField]
    GameObject attackSprite;
    protected override void ResetWeapon()
    {
        base.ResetWeapon();
        attackSprite.SetActive(false);
    }
    protected override void Attack()
    {
        base.Attack();
        StartCoroutine("AttackAnim");
        attackSprite.SetActive(true);
        source.PlayOneShot(attackClip);
    }
    IEnumerator AttackAnim()
    {
        float dir = transform.eulerAngles.z;
        float prog = 0;
        while (prog < 1)
        {
            prog += 1 / attackSpeed * Time.deltaTime;
            float curWeaponDir = (prog - 0.5f) * attackAngle;
            transform.eulerAngles = new Vector3(0, 0, dir + curWeaponDir);

            yield return null;
        }
        transform.localEulerAngles = Vector3.zero;
        attackSprite.SetActive(false);
    }
}
