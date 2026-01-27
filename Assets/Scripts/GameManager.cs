// 1. 게임 흐름 관리
// 2. tick 관리
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        Paused,
        GameOver
    }

    public static GameManager Instance { get; private set; }

    // 인스펙터에서 변경하기 위해 public
    public float tick = 1.0f;
    public BoardRenderer boardRenderer;
    public PlayerInput input;
    public Transform boardTransform; // Board GameObject Transform
    public float rotationSpeed = 10f; // 회전 속도

    public Board GameBoard { get; private set; }
    public Spawner MinoSpawner { get; private set; }
    public GameState State { get; private set; }
    public int HighScore { get; private set; }
    public int Level
    {
        get => _level;
        private set
        {
            _level = value;
            UIManager.Instance.UpdateLevelUI();
        }
    }
    public int Score
    {
        get => _score;
        private set
        {
            _score = value;
            UIManager.Instance.UpdateScoreUI();
        }
    }

    private float _tickTime = 0.0f;
    private int _boardRot = 0;
    private float _targetRot = -45f; // 목표 회전 각도
    private float _currentRot = -45f; // 현재 회전 각도
    private int _level = 1;
    private int _score = 0;
    private int _clearedLine = 0;
    private int _clearedLinePerLevel = 4;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        State = GameState.Playing;
        GameBoard = new Board();
        MinoSpawner = new Spawner();
        HighScore = PlayerPrefs.GetInt("HighScore");
    }

    private void Start()
    {
        MinoSpawner.Init();
        GameBoard.Init();
        boardRenderer.RenderUpdate();
    }

    private void Update()
    {
        if (State == GameState.Playing)
        {
            HandlingRotate();

            if (HandlingInput())
            {
                boardRenderer.RenderUpdate();
            }

            _tickTime += Time.deltaTime;

            if (_tickTime > tick)
            {
                _tickTime -= tick;
                GameUpdate();
                boardRenderer.RenderUpdate();
            }
        }
    }

    private void HandlingRotate()
    {
        if (input.RotateCameraRight)
        {
            _boardRot = (_boardRot + 1) % 4;
            _targetRot = _boardRot * 90f - 45f;
        }
        else if (input.RotateCameraLeft)
        {
            _boardRot = (_boardRot + 3) % 4;
            _targetRot = _boardRot * 90f - 45f;
        }

        _currentRot = Mathf.LerpAngle(_currentRot, _targetRot, Time.deltaTime * rotationSpeed);
        boardTransform.rotation = Quaternion.Euler(0, _currentRot, 0);
    }

    private bool HandlingInput()
    {
        bool moved = false;

        int dx = (input.MoveRight ? 1 : 0) + (input.MoveLeft ? -1 : 0);
        int dz = (input.MoveForward ? 1 : 0) + (input.MoveBackward ? -1 : 0);

        // 회전 적용
        switch (_boardRot)
        {
            case 1: (dx, dz) = (-dz, dx); break;
            case 2: (dx, dz) = (-dx, -dz); break;
            case 3: (dx, dz) = (dz, -dx); break;
        }

        if (dx != 0 || dz != 0)
        {
            moved |= GameBoard.MoveHorizontal(dx, dz);
        }

        if (input.RotateBlock)
        {
            moved |= GameBoard.RotateBlock();
        }

        if (input.ChangeLying)
        {
            moved |= GameBoard.ChangeLying();
        }

        if (input.SoftDrop)
        {
            bool dropped = GameBoard.SoftDrop();
            moved |= dropped;

            if (dropped)
            {
                _tickTime = 0.0f;
            }
        }

        if (input.HardDrop)
        {
            bool dropped = GameBoard.HardDrop();
            moved |= dropped;

            if (dropped)
            {
                _tickTime = 0.0f;
            }
        }

        return moved;
    }

    public void GameUpdate()
    {
        int clear = GameBoard.BoardUpdate();

        if (clear == -1)
        {
            State = GameState.GameOver;

            if (Score > HighScore)
            {
                HighScore = Score;
                PlayerPrefs.SetInt("HighScore", HighScore);
            }

            UIManager.Instance.OnGameoverUI();
        }
        else if (clear > 0)
        {
            _clearedLine += clear;
            Level = _clearedLine / _clearedLinePerLevel + 1;

            switch (clear)
            {
                case 1: Score += 1000; break;
                case 2: Score += 3000; break;
                case 3: Score += 5000; break;
                case 4: Score += 8000; break;
            }
        }
    }

    public void Falling()
    {
        Score += 10;
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
