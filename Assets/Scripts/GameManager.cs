// 1. 게임 흐름 관리
// 2. tick 관리
using UnityEngine;
using UnityEngine.XR;

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

    private bool HandlingInput()
    {
        bool moved = false;

        int dx = (input.moveRight ? 1 : 0) + (input.moveLeft ? -1 : 0);
        int dz = (input.moveForward ? 1 : 0) + (input.moveBackward ? -1 : 0);

        if (dx != 0 || dz != 0)
        {
            moved |= GameBoard.MoveHorizontal(dx, dz);
        }

        if (input.rotateBlock)
        {
            moved |= GameBoard.RotateBlock();
        }

        if (input.toggleLie)
        {
            moved |= GameBoard.ToggleLie();
        }

        if (input.softDrop)
        {
            bool dropped = GameBoard.SoftDrop();
            moved |= dropped;

            if (dropped)
            {
                _tickTime = 0.0f;
            }
        }

        if (input.hardDrop)
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
        if (!GameBoard.BoardUpdate())
        {
            State = GameState.GameOver;
        }
    }
}
