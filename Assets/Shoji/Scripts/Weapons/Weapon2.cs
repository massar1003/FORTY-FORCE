using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon2 : WeaponBase
{
    [SerializeField]
    float attackSpeed = 0.2f, attackDist = 60;
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
            transform.eulerAngles = new Vector3(0, 0, dir);
            prog += 1 / attackSpeed * Time.deltaTime;
            float curDist = Mathf.Sin(prog * Mathf.PI) * attackDist + attackSprite.transform.localScale.y;
            attackSprite.transform.localPosition = new Vector3(0, curDist);

            yield return new WaitForFixedUpdate();
        }
        transform.localEulerAngles = Vector3.zero;
        attackSprite.SetActive(false);
    }
}
