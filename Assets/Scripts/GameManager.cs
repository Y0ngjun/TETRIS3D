using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float tick = 1.0f;
    public int width = 10;
    public int height = 20;
    public int depth = 10;

    private Transform[,,] board;
    private float tickTime = 0.0f;

    void Awake()
    {
        GameSetup();
    }

    void Start()
    {

    }

    void Update()
    {
        tickTime += Time.deltaTime;

        if (tickTime > tick)
        {
            tickTime -= tick;
            GameUpdate();
        }
    }

    void GameSetup()
    {
        board = new Transform[width, height, depth];
        tickTime = tick;
    }

    void GameUpdate()
    {
        
    }
}
