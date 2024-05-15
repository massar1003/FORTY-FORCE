using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon0 : WeaponBase
{
    [SerializeField]
    float rotateSpeed, rotateDist;
    [SerializeField]
    GameObject rotator;
    float curRotation;
    private void Awake()
    {
        curRotation = 0;
        ApplyRotation();
        rotator.transform.position = new Vector3(0, rotateDist);
    }
    protected override void ResetWeapon()
    {
        base.ResetWeapon();
        transform.eulerAngles = Vector3.zero;
    }
    protected override void LateUpdate()
    {
        base.LateUpdate();
        curRotation += rotateSpeed * Time.deltaTime;
        ApplyRotation();
    }
    void ApplyRotation()
    {
        Vector3 rot = new Vector3(0, 0, curRotation);
        transform.eulerAngles = rot;
    }
}
