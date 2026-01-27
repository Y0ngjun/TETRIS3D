using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool MoveRight { get; private set; }
    public bool MoveLeft { get; private set; }
    public bool MoveForward { get; private set; }
    public bool MoveBackward { get; private set; }

    public bool RotateBlock { get; private set; }
    public bool ChangeLying { get; private set; }
    public bool SoftDrop { get; private set; }
    public bool HardDrop { get; private set; }

    public bool RotateCameraRight { get; private set; }
    public bool RotateCameraLeft { get; private set; }

    void Update()
    {
        MoveRight = Input.GetKeyDown(KeyCode.D);
        MoveLeft = Input.GetKeyDown(KeyCode.A);
        MoveForward = Input.GetKeyDown(KeyCode.W);
        MoveBackward = Input.GetKeyDown(KeyCode.S);

        RotateBlock = Input.GetKeyDown(KeyCode.J);
        ChangeLying = Input.GetKeyDown(KeyCode.K);
        SoftDrop = Input.GetKeyDown(KeyCode.L);
        HardDrop = Input.GetKeyDown(KeyCode.Space);

        RotateCameraRight = Input.GetKeyDown(KeyCode.E);
        RotateCameraLeft = Input.GetKeyDown(KeyCode.Q);
    }
}
