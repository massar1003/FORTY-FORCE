using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Result_Score : MonoBehaviour
{
    public Text T_RScore;
    void Start()
    {
        int  score = ScoreDataCarrier.Score;
        T_RScore.text = score.ToString("0");
    }
}
