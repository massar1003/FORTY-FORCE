using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaker : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;

    [SerializeField]
    int E_Level=1;

    [SerializeField]
    float E_born_time =2;

    [SerializeField]
    IngameController ingameController;

    public Vector3 GetRandomPosition()
    {
        float posX = Random.Range(-15f, 15f);
        float posY = Random.Range(-15f, 15f);
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            posX = posX < 0 ? -15 : 15;
        }
        else
        {
            posY = posY < 0 ? -15 : 15;
        }

        return new Vector3(posX,posY)+Camera.main.transform.position;
    }



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartEvent");
    }
    IEnumerator StartEvent()
    {
        yield return new WaitUntil(() => ingameController.State == InGameState.Game);
        StartCoroutine("SpawnEnemy");
    }

    // Update is called once per frame
    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            if (ingameController.State != InGameState.Game) break;
            Instantiate(enemy, GetRandomPosition(), Quaternion.identity).GetComponent<Enemy>().Initialize(ingameController,this);
            yield return new WaitForSeconds(E_born_time);
        }
    }
}
