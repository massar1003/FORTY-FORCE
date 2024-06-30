using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Bar_Break : MonoBehaviour
{
    public IngameController IC; //�{�ԗp
    //public Test T;    //�e�X�g�p
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
            Debug.LogError("inGameControllerGameObject�̃Q�[���I�u�W�F�N�g���݂���܂���B");
        }
    }

    void Update()
    { 
        Text text = this.GetComponent<Text>();

        float time = IC.GameTime;   //�{�ԗp
        //float time = T.times;     //�e�X�g�p
        if (time >= second)
        {
            text.color = new Color(1.0f, 0.0f, 0.0f, 0.4f);
        }
    }
}
