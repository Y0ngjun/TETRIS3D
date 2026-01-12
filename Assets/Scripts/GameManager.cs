// 1. 게임 흐름 관리
// 2. tick 관리
using UnityEngine;

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

    public Board GameBoard { get; private set; }
    public Spawner MinoSpawner { get; private set; }
    public GameState State { get; private set; }

    private float _tickTime = 0.0f;

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
            _tickTime += Time.deltaTime;

            if (_tickTime > tick)
            {
                _tickTime -= tick;
                GameUpdate();
                boardRenderer.RenderUpdate();
            }
        }
    }

    private void GameUpdate()
    {
        if (!GameBoard.BoardUpdate())
        {
            State = GameState.GameOver;
        }
    }
}
