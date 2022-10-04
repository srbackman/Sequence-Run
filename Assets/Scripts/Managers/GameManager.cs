using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using TMPro;

public enum GameState
{
    inGame,
    pause
}

public enum TimerStates
{
    countdown,
    levelTime,
    none
}

public class GameManager : MonoBehaviour
{
    private PlayerMovement playerMovement;

    [SerializeField] private LevelCore[] _levelCores;
    private int _currentLevel = 0;
    public GameState _gameState = GameState.pause;
    public bool _timersOn = false;
    public bool _countdownOn = false;
    [Space]
    [SerializeField] private TMP_Text _countdownTimerText;
    [SerializeField] private float _countdownTextFadeTime = 0.5f;
    private int _nextCountdownTimeAnnouncement = 3;
    private bool _countdownTextFadeDone = false;
    private float _countdownTimerValue;
    [Space]
    [SerializeField] private TMP_Text _levelTimerText;
    private float _levelTimerValue;
    [SerializeField] private float _levelFailScreenDelayTime = 1f;
    private float _levelFailScreenDelayTimer;
    [SerializeField] private UnityEvent _levelTimeTrigger;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();

        _countdownTimerText.text = "";
        _levelTimerText.text = "";

    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (_timersOn)
        {
            if (_countdownOn)
            {
                _countdownTimerText.gameObject.SetActive(true);
                _countdownTimerValue -= Time.deltaTime;
            }
            if ((_countdownTimerValue) < _nextCountdownTimeAnnouncement)
            {
                CountdownSetText(_nextCountdownTimeAnnouncement);
            }
            if (_countdownTimerValue < 0)
            {
                _levelTimerValue -= Time.deltaTime;
                if ((10 - _levelTimerValue) > _countdownTextFadeTime)
                {
                    _countdownTimerText.text = "";
                }
                if (_levelTimerValue <= 0)
                {
                    _levelTimerValue = 0;
                    StopTiming();
                    _levelTimerText.text = "";
                    _levelTimeTrigger.Invoke();
                }
                _levelTimerText.text = string.Format("{0:00.00}", _levelTimerValue);
            }
        }
    }

    private void CountdownSetText(int value)
    {
        if (value > 0)
        {
            _countdownTimerText.text = value.ToString();
            _nextCountdownTimeAnnouncement--;
        }
        else
        {
            _countdownTimerText.text = "GO";
            _countdownOn = false;
        }


    }

    public void StopTiming()
    {
        _timersOn = false;
        _levelTimerText.text = "";
    }

    private void ResetPlayer()
    {
        playerMovement._playerVelocity = Vector2.zero;
        playerMovement.transform.position = _levelCores[_currentLevel]._playerStartPoint.position;
        playerMovement.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void StartCountdown()
    {
        _timersOn = true;

    }

    public void GameOver()
    {


    }

    public void RestartLevel()
    {
        _levelCores[_currentLevel].RestartLevel();
        ResetPlayer();
        _countdownTimerValue = 3f;
        _nextCountdownTimeAnnouncement = 3;
        _countdownOn = true;
        _levelTimerValue = 10f;
        StartCountdown();
    }

    public void NextLevel()
    {
        _levelCores[_currentLevel].gameObject.SetActive(false);
        _currentLevel++;

        if (_currentLevel < _levelCores.Length)
        {
            /*Next level*/
            _levelCores[_currentLevel].gameObject.SetActive(true);
            RestartLevel();
        }
    }

    public void StartGame()
    {
        _currentLevel = 0;
        _levelCores[2].gameObject.SetActive(false);
        _levelCores[1].gameObject.SetActive(false);
        _levelCores[_currentLevel].gameObject.SetActive(true);
        RestartLevel();
        StartCountdown();
    }
}
