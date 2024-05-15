using UnityEngine;

[System.Serializable]
public class ScoreController
{
    private int _score = 0;

    public int Score => _score;

    public void Initialized()
    {
        _score = 0;
    }

    public void AddScore(int score)
    {
        Debug.Log("スコアを加算！！");

        _score += score;
    }
}
