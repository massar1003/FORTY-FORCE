using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Bar : MonoBehaviour
{
    public IngameController IC; //これは本番用
    //public Test T;    //これはテスト用

    public Slider slider;

    void Update()
    {
        //以下は本番用
        float time = IC.GameTime;
        slider.value = time / 50;

        //以下はテスト用
        //float time = T.times; 
        //slider.value = time/50;
    }
}
