using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Plus : MonoBehaviour
{
    public IngameController IC;
    public Text T_Plus;
    private float prevTime = 0;
    private float deltaTime = 0;

    private void Start()
    {
        IC.OnScoreChange += Change;
    }

    private void Awake()
    {
        var inGameControllerGameObject = GameObject.Find("InGameController");
        if (inGameControllerGameObject != null)
        {
            IC = inGameControllerGameObject.GetComponent<IngameController>();
        }
        else
        {
            Debug.LogError("inGameControllerGameObjectのゲームオブジェクトがみつかりません。");
        }

        T_Plus.text = "";
    }

    private void Change(int score)
    {
        StopCoroutine("deltaTimeCoroutine");
        T_Plus.text = "+" + score.ToString("0");
        StartCoroutine("deltaTimeCoroutine");
    }

    IEnumerator deltaTimeCoroutine()
    {
        yield return new WaitForSeconds(1);
        T_Plus.text = "";
    }

    void Update()
    {

    }
}
