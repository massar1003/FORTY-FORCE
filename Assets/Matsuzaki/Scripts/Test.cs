using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	public float times = 0;
	public int scores = 0;

	void Update()
	{
		times += Time.deltaTime;
		scores += 1;
	}
}
