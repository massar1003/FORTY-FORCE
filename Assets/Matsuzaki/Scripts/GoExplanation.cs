using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoExplanation : MonoBehaviour
{
    public SceneChanger SC;
    public void OnClickExplanationButton()
    {
        SceneManager.LoadScene("game");
    }
}
