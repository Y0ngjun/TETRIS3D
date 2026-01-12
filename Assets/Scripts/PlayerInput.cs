using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool moveRight { get; private set; }
    public bool moveLeft { get; private set; }
    public bool moveForward { get; private set; }
    public bool moveBackward { get; private set; }

    public bool rotateBlock { get; private set; }
    public bool toggleLie { get; private set; }
    public bool softDrop { get; private set; }
    public bool hardDrop { get; private set; }

    void Update()
    {
        moveRight = Input.GetKeyDown(KeyCode.D);
        moveLeft = Input.GetKeyDown(KeyCode.A);
        moveForward = Input.GetKeyDown(KeyCode.W);
        moveBackward = Input.GetKeyDown(KeyCode.S);

        rotateBlock = Input.GetKeyDown(KeyCode.J);
        toggleLie = Input.GetKeyDown(KeyCode.K);
        softDrop = Input.GetKeyDown(KeyCode.L);
        hardDrop = Input.GetKeyDown(KeyCode.Space);
    }
}
