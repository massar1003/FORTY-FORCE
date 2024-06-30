using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Bar_Break : MonoBehaviour
{
    public IngameController IC; //本番用
    //public Test T;    //テスト用
    public float second;

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
    }

    void Update()
    { 
        Text text = this.GetComponent<Text>();

        float time = IC.GameTime;   //本番用
        //float time = T.times;     //テスト用
        if (time >= second)
        {
            text.color = new Color(1.0f, 0.0f, 0.0f, 0.4f);
        }
    }
}
