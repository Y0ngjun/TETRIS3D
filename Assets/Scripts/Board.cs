using UnityEngine;

public class Board : MonoBehaviour
{
    private Transform[,,] board;

    public int width = 10;
    public int height = 20;
    public int depth = 10;

    private void Awake()
    {
        board = new Transform[width, height, depth];
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    public void Check()
    {

    }
}
