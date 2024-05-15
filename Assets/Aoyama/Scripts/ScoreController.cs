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
        Debug.Log("�X�R�A�����Z�I�I");

        _score += score;
    }
}
