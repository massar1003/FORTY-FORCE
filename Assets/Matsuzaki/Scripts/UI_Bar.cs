using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Bar : MonoBehaviour
{
    public IngameController IC; //����͖{�ԗp
    //public Test T;    //����̓e�X�g�p

    public Slider slider;

    void Update()
    {
        //�ȉ��͖{�ԗp
        float time = IC.GameTime;
        slider.value = time / 50;

        //�ȉ��̓e�X�g�p
        //float time = T.times; 
        //slider.value = time/50;
    }
}
