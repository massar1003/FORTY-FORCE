using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameController : MonoBehaviour
{
    /// <summary>
    /// �X�R�A�̃v���p�e�B
    /// </summary>
    public int Score => _scoreController.Score;

    /// <summary>
    /// �Q�[���̌o�ߎ��Ԃ̃v���p�e�B
    /// </summary>
    public float GameTime => _timer.GameTime;

    /// <summary>
    /// �Q�[���̃J�E���g�_�E���̃v���p�e�B
    /// </summary>
    public float CountDounTime => _timer.CountDounTime;

    [SerializeField]
    private TimeController _timer;

    [SerializeField]
    private ScoreController _scoreController;

    [Header("���U���g�V�[���̖��O")]
    [SerializeField]
    private string _resultSceneName = "";

    [Header("�Q�[���J�n���ɕ\������e�L�X�g")]
    [SerializeField]
    private string _startText = "Start!!";

    [Header("�Q�[���I�����ɕ\������e�L�X�g")]
    [SerializeField]
    private string _finishText = "Finish!!";

    [Header("�Q�[���I�[�o�[���ɕ\������e�L�X�g")]
    [SerializeField]
    private string _gameoverText = "GameOver!!";

    [Header("�V�[���̑J�ڂ��s���N���X")]
    [SerializeField]
    private SceneChanger _sceneChanger;

    [Header("�Q�[���̗l�X�ȏ���\������Text")]
    [SerializeField]
    private Text _gameText;

    [Header("BGM�̃I�[�f�B�I�\�[�X")]
    [SerializeField]
    private AudioSource _audioSource;

    [Header("�J�E���g�_�E�����ɖ炷��")]
    [SerializeField]
    private AudioSource _countAudio;

    [Header("�Q�[���J�n���ɂȂ鉹")]
    [SerializeField]
    private AudioSource _startAudio;

    /// <summary>
    /// Game�̏�Ԃ�\���v���p�e�B
    /// </summary>
    public InGameState State => _timer.State;

    public event Action<int> OnScoreChange;

    // ����ύX���̃C�x���g�p�t���O
    private bool _is10count;
    private bool _is20count;
    private bool _is30count;
    private bool _is40count;

    // �J�E���g�_�E�����̃t���O
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
            Debug.LogWarning("gameText���A�T�C������Ă��܂���!!");
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
    /// �Q�[���I�����ɍs������
    /// </summary>
    public void GameFinish()
    {
        StartCoroutine(GameOverCoroutine());
    }

    private IEnumerator GameOverCoroutine()
    {
        _audioSource.Stop();

        _timer.State = InGameState.None;

        // �X�R�A�̒l��ۑ�����
        ScoreDataCarrier.Score = _scoreController.Score;

        _gameText.gameObject.SetActive(true);
        _gameText.CrossFadeAlpha(1f, 0f, false);
        _gameText.text = _gameoverText;

        yield return new WaitForSeconds(2f);

        if (_sceneChanger != null)
        {
            // Fade�����Ȃ��烊�U���g�V�[���ɑJ�ڂ���
            yield return _sceneChanger.SceneChangeCoroutine(_resultSceneName);
        }
        else
        {
            Debug.LogWarning("SceneChanger���A�T�C������Ă��܂���!!");
        }
    }

    private IEnumerator GameFinishCoroutine()
    {
        _timer.State = InGameState.None;

        // �X�R�A�̒l��ۑ�����
        ScoreDataCarrier.Score = _scoreController.Score;

        if (_sceneChanger != null)
        {
            // Fade�����Ȃ��烊�U���g�V�[���ɑJ�ڂ���
            yield return _sceneChanger.SceneChangeCoroutine(_resultSceneName);
        }
        else
        {
            Debug.LogWarning("SceneChanger���A�T�C������Ă��܂���!!");
        }
    }

    /// <summary>
    /// �X�R�A�̉��Z���s�����\�b�h
    /// </summary>
    /// <param name="score">���Z����X�R�A�̒l</param>
    public void AddScore(int score)
    {
        if (State != InGameState.Game) return;

        OnScoreChange?.Invoke(score);
        _scoreController.AddScore(score);
    }
}
