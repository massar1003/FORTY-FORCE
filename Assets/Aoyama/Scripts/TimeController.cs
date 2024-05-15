using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// �^�C���ƁA����ɂ��Q�[���̏�Ԃ̕ύX���s���N���X
/// </summary>
[System.Serializable]
public class TimeController
{
    [Header("�J�E���g�_�E���̕b��")]
    [SerializeField]
    private float _countDown = 3f;

    [Header("�Q�[���̐�������")]
    [SerializeField]
    private float _gameTime = 50f;

    [Header("�I����̃C���^�[�o��")]
    [SerializeField]
    private float _finishInterval = 2f;

    /// <summary>
    /// �J�E���g�_�E���p�ϐ�
    /// </summary>
    private float _countDownTimer = 0f;

    /// <summary>
    /// �Q�[���̌o�ߎ���
    /// </summary>
    private float _gameTimer = 0f;

    /// <summary>
    /// �Q�[���̏I�����̃C���^�[�o���o�ߎ���
    /// </summary>
    private float _finishIntervalTimer = 0f;

    public float GameTime => _gameTimer;

    public float CountDounTime => Mathf.Floor(_countDownTimer);

    private InGameState _state = InGameState.WaitStart;
    public InGameState State
    { 
        get => _state; 
        set => _state = value;
    }

    public event Action OnGameStarted;


    public void Intialized()
    {
        _countDownTimer = _countDown + 0.9f;
        _gameTimer = 0f;
        _finishIntervalTimer = 0f;
        _state = InGameState.WaitStart;
    }

    public void Update(float deltaTime)
    {
        // �Q�[���̏�Ԃɉ����ď��������s
        switch (_state)
        {
            case InGameState.WaitStart:
                WaitStartState(deltaTime);

                break;
            case InGameState.Game:
                GameState(deltaTime);

                break;
            case InGameState.WaitFinish:
                WaitFinishState(deltaTime);

                break;
        }
    }

    private void WaitStartState(float deltaTime)
    {
        _countDownTimer -= deltaTime;

        if (_countDownTimer < 1)
        {
            _state = InGameState.Game;
            OnGameStarted?.Invoke();
        }
    }


    private void GameState(float deltaTime)
    {
        _gameTimer += deltaTime;

        if (_gameTimer > _gameTime)
        {
            _state = InGameState.WaitFinish;
            _gameTimer = _gameTime;
        }
    }

    private void WaitFinishState(float deltaTime)
    {
        _finishIntervalTimer += deltaTime;

        if (_finishIntervalTimer > _finishInterval)
        {
            _state = InGameState.Finish;
        }
    }
}
