using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoGrid : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("AutoGridPosition");
    }
    IEnumerator AutoGridPosition()
    {
        while(true)
        {
            yield return new WaitForSecondsRealtime(5);

            Vector3 tarPos = Camera.main.transform.position;
            transform.position = tarPos - new Vector3(tarPos.x % 2.56f, tarPos.y % 2.56f, -10);
        }
    }
}
