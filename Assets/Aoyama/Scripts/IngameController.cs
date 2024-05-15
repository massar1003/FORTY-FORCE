using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameController : MonoBehaviour
{
    /// <summary>
    /// スコアのプロパティ
    /// </summary>
    public int Score => _scoreController.Score;

    /// <summary>
    /// ゲームの経過時間のプロパティ
    /// </summary>
    public float GameTime => _timer.GameTime;

    /// <summary>
    /// ゲームのカウントダウンのプロパティ
    /// </summary>
    public float CountDounTime => _timer.CountDounTime;

    [SerializeField]
    private TimeController _timer;

    [SerializeField]
    private ScoreController _scoreController;

    [Header("リザルトシーンの名前")]
    [SerializeField]
    private string _resultSceneName = "";

    [Header("ゲーム開始時に表示するテキスト")]
    [SerializeField]
    private string _startText = "Start!!";

    [Header("ゲーム終了時に表示するテキスト")]
    [SerializeField]
    private string _finishText = "Finish!!";

    [Header("ゲームオーバー時に表示するテキスト")]
    [SerializeField]
    private string _gameoverText = "GameOver!!";

    [Header("シーンの遷移を行うクラス")]
    [SerializeField]
    private SceneChanger _sceneChanger;

    [Header("ゲームの様々な情報を表示するText")]
    [SerializeField]
    private Text _gameText;

    [Header("BGMのオーディオソース")]
    [SerializeField]
    private AudioSource _audioSource;

    [Header("カウントダウン時に鳴らす音")]
    [SerializeField]
    private AudioSource _countAudio;

    [Header("ゲーム開始時になる音")]
    [SerializeField]
    private AudioSource _startAudio;

    /// <summary>
    /// Gameの状態を表すプロパティ
    /// </summary>
    public InGameState State => _timer.State;

    public event Action<int> OnScoreChange;

    // 武器変更時のイベント用フラグ
    private bool _is10count;
    private bool _is20count;
    private bool _is30count;
    private bool _is40count;

    // カウントダウン時のフラグ
    private bool _is3Count;
    private bool _is2Count;
    private bool _is1Count;

    private void Start()
    {
        _timer.Intialized();
        _scoreController.Initialized();

        _timer.OnGameStarted += GameStart;

        _audioSource.Stop();
    }

    private void GameStart()
    {
        _audioSource.Play();
        StartCoroutine(TextChanged());
    }

    private IEnumerator TextChanged()
    {
        _startAudio.Play();
        _gameText.text = _startText;

        yield return new WaitForSeconds(1f);

        _gameText.gameObject.SetActive(false);
    }

    private void Update()
    {
        IngameUpdateByState();

        if (State == InGameState.Finish) return;

        float deltaTime = Time.deltaTime;
        _timer.Update(deltaTime);
    }

    private void IngameUpdateByState()
    {
        if (_gameText != null)
        {
            switch (State)
            {
                case InGameState.WaitStart:
                    _gameText.gameObject.SetActive(true);
                    CountDown();

                    break;
                case InGameState.Game:
                    IngameUpdateGameState();

                    break;
                case InGameState.WaitFinish:
                    _gameText.gameObject.SetActive(true);
                    _gameText.CrossFadeAlpha(1f, 0f, false);
                    _gameText.text = _finishText;

                    break;
                case InGameState.Finish:
                    StartCoroutine(GameFinishCoroutine());

                    break;
            }
        }
        else
        {
            Debug.LogWarning("gameTextがアサインされていません!!");
        }
    }

    private void CountDown()
    {
        string text = _timer.CountDounTime.ToString();
        _gameText.text = text;

        if (text == "3" && !_is3Count)
        {
            _countAudio.Play();
            _is3Count = true;
        }
        else if (text == "2" && !_is2Count)
        {
            _countAudio.Play();
            _is2Count = true;
        }
        else if (text == "1" && !_is1Count)
        {
            _countAudio.Play();
            _is1Count = true;
        }
    }

    private void IngameUpdateGameState()
    {
        float time = _timer.GameTime;

        if (time >= 40 && !_is40count)
        {
            StartCoroutine(ShowCount(40));
            _is40count = true;
        }
        else if (time >= 30 && !_is30count)
        {
            StartCoroutine(ShowCount(30));
            _is30count = true;
        }
        else if (time >= 20 && !_is20count)
        {
            StartCoroutine(ShowCount(20));
            _is20count = true;
        }
        else if (time >= 10 && !_is10count)
        {
            StartCoroutine(ShowCount(10));
            _is10count = true;
        }
    }

    private IEnumerator ShowCount(int count)
    {
        _gameText.gameObject.SetActive(true);
        _gameText.text = count.ToString();
        _gameText.CrossFadeAlpha(0f, 0f, false);
        _gameText.CrossFadeAlpha(1f, 0.2f, false);

        yield return new WaitForSeconds(0.7f);

        _gameText.CrossFadeAlpha(0f, 0.2f, false);

        yield return new WaitForSeconds(0.2f);

        _gameText.gameObject.SetActive(true);
    }

    /// <summary>
    /// ゲーム終了時に行う処理
    /// </summary>
    public void GameFinish()
    {
        StartCoroutine(GameOverCoroutine());
    }

    private IEnumerator GameOverCoroutine()
    {
        _audioSource.Stop();

        _timer.State = InGameState.None;

        // スコアの値を保存する
        ScoreDataCarrier.Score = _scoreController.Score;

        _gameText.gameObject.SetActive(true);
        _gameText.CrossFadeAlpha(1f, 0f, false);
        _gameText.text = _gameoverText;

        yield return new WaitForSeconds(2f);

        if (_sceneChanger != null)
        {
            // Fadeをしながらリザルトシーンに遷移する
            yield return _sceneChanger.SceneChangeCoroutine(_resultSceneName);
        }
        else
        {
            Debug.LogWarning("SceneChangerがアサインされていません!!");
        }
    }

    private IEnumerator GameFinishCoroutine()
    {
        _timer.State = InGameState.None;

        // スコアの値を保存する
        ScoreDataCarrier.Score = _scoreController.Score;

        if (_sceneChanger != null)
        {
            // Fadeをしながらリザルトシーンに遷移する
            yield return _sceneChanger.SceneChangeCoroutine(_resultSceneName);
        }
        else
        {
            Debug.LogWarning("SceneChangerがアサインされていません!!");
        }
    }

    /// <summary>
    /// スコアの加算を行うメソッド
    /// </summary>
    /// <param name="score">加算するスコアの値</param>
    public void AddScore(int score)
    {
        if (State != InGameState.Game) return;

        OnScoreChange?.Invoke(score);
        _scoreController.AddScore(score);
    }
}
