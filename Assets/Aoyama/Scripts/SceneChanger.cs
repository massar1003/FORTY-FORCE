using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private Image _fadePanel;

    [SerializeField]
    private float _fadeTime = 0.3f;

    private void Start()
    {
        StartCoroutine(Fadein());
    }

    private IEnumerator Fadein()
    {
        if (_fadePanel == null) yield break;

        _fadePanel.gameObject.SetActive(true);
        _fadePanel.CrossFadeAlpha(0f, _fadeTime, false);

        yield return new WaitForSeconds(_fadeTime);

        _fadePanel.gameObject.SetActive(false);
    }

    public void SceneChange(string sceneName)
    {
        StartCoroutine(SceneChangeCoroutine(sceneName));
    }

    public IEnumerator SceneChangeCoroutine(string sceneName)
    {
        if (_fadePanel != null)
        {
            _fadePanel.gameObject.SetActive(true);
            _fadePanel.CrossFadeAlpha(1f, _fadeTime, false);
        }

        //フェードイン、アウトの処理を入れる
        yield return new WaitForSeconds(_fadeTime);

        Debug.Log($"{sceneName} にシーン遷移");
        SceneManager.LoadScene(sceneName);
    }
}
