using UnityEngine;

public class Tetromino : MonoBehaviour
{
    public Block[] blocks;

    void Start()
    {

    }

    void Update()
    {

    }

    public bool Fall()
    {
        foreach (var block in blocks)
        {
            block.MoveDown();
        }
    }

    public void Lock()
    {

    }
}
