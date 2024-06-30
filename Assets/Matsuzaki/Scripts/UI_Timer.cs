using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Timer : MonoBehaviour
{
    public IngameController IC; //����͖{�ԗp
    //public Test T;    //����̓e�X�g�p
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
            Debug.LogError("inGameControllerGameObject�̃Q�[���I�u�W�F�N�g���݂���܂���B");
        }

        //Move_Timer = GetComponent<RectTransform>();
    }

    void Update()
    {
        //�ȉ��͖{�ԗp
        float time = IC.GameTime;
        T_Timer.text = time.ToString("f0");
        //Move_Timer.position += new Vector3(0.1666f,0,0);
        //�ȉ��̓e�X�g�p
        //float time = T.times; 
        //T_Timer.text = time.ToString("n2");
    }
}
