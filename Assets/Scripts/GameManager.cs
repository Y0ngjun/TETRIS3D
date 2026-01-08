using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        Paused,
        GameOver
    }

    public static GameManager Instance;

    public GameState State { get; private set; }
    public Spawner spawner;
    public Board board;
    public float tick = 1.0f;

    private Tetromino tetromino;
    private float tickTime = 0.0f;
    private int score = 0;

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
    }

    private void Update()
    {
        if (State == GameState.Playing)
        {
            tickTime += Time.deltaTime;

            if (tickTime > tick)
            {
                tickTime -= tick;
                GameUpdate();
            }
        }
    }

    private void GameUpdate()
    {
        if (tetromino == null)
        {
            tetromino = spawner.Spawn();

            if (tetromino == null)
            {
                // GameOver
                State = GameState.GameOver;
                return;
            }
        }

        if (!tetromino.Fall())
        {
            // tetromino Lock
            tetromino.Lock();
            board.Check();
        }
    }
}
