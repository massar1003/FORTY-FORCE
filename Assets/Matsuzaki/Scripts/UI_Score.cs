using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Score : MonoBehaviour
{
    public IngameController IC; //これは本番用
    //public Test T;    //これはテスト用

    public Text T_Score;
    int curScore;
    float addition;
    Vector2 defaultPosition;

    private void Awake()
    {
        defaultPosition = T_Score.rectTransform.anchoredPosition;
    }

    void Update()
    {
        //以下は本番用
        int tarScore = IC.Score;

        T_Score.rectTransform.anchoredPosition = defaultPosition;
        T_Score.rectTransform.localScale = Vector2.one;
        if (curScore == tarScore) return;

        addition += ((tarScore - curScore) / 10f + 1) * Time.deltaTime * 40;
        if (addition >= 1)
        {
            int fl = Mathf.FloorToInt(addition);
            addition -= fl;
            curScore = Mathf.Min(curScore + fl, tarScore);
            RandomMove(tarScore - curScore);
        }

        T_Score.text = curScore.ToString();
    }
    void RandomMove(float delta)
    {
        float angle = Random.Range(-180f, 180f);
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        float dist = Mathf.Min(delta, 5);
        T_Score.rectTransform.anchoredPosition = defaultPosition + dir * dist;
        T_Score.rectTransform.localScale = Vector2.one + dir * Mathf.Min(dist * 0.002f, 0.08f);
    }
}
