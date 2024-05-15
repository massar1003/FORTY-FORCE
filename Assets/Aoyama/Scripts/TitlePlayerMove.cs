using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TitlePlayerMove : MonoBehaviour
{
    [SerializeField]
    private float _pushPower = 10f;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(_pushPower, _pushPower));
    }
}
