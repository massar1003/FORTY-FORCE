using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Timer : MonoBehaviour
{
    public IngameController IC; //これは本番用
    //public Test T;    //これはテスト用
    RectTransform Move_Timer;

    public Text T_Timer;

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

        //Move_Timer = GetComponent<RectTransform>();
    }

    void Update()
    {
        //以下は本番用
        float time = IC.GameTime;
        T_Timer.text = time.ToString("f0");
        //Move_Timer.position += new Vector3(0.1666f,0,0);
        //以下はテスト用
        //float time = T.times; 
        //T_Timer.text = time.ToString("n2");
    }
}
