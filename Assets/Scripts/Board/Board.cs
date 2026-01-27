// 1. z, y, x 배열로 보드를 관리
// 2. (0, 0, 0)이 가장 앞면의 좌상단 꼭짓점
public class Board
{
    public int Depth { get; private set; }
    public int Height { get; private set; }
    public int Width { get; private set; }
    public Tetromino.Type[,,] LogicBoard { get; private set; }
    public Tetromino CurrentMino { get; private set; }

    private const int MINO_SHAPE_SIZE = 4;
    private Spawner _spawner = null;
    private int _spawnZ;
    private int _spawnY;
    private int _spawnX;

    public Board()
    {
        Depth = 8;
        Height = 20;
        Width = 8;
        LogicBoard = new Tetromino.Type[Depth, Height, Width];

        // minoShape Size 변경 시 4 <= 변경 필요
        _spawnZ = (Depth - MINO_SHAPE_SIZE) / 2;
        _spawnY = 0;
        _spawnX = (Width - MINO_SHAPE_SIZE) / 2;
    }

    public void Init()
    {
        _spawner = GameManager.Instance.MinoSpawner;
        SpawnMino();
    }

    public int BoardUpdate()
    {
        int clear = 0;

        if (!FallMino())
        {
            LockMino();
            clear = BoardCheck();

            if (!SpawnMino())
            {
                return -1;
            }
        }

        return clear;
    }

    public bool CheckMino(Tetromino mino)
    {
        int shapeDepth = mino.Shape.GetLength(0);
        int shapeHeight = mino.Shape.GetLength(1);
        int shapeWidth = mino.Shape.GetLength(2);

        for (int z = 0; z < shapeDepth; z++)
        {
            for (int y = 0; y < shapeHeight; y++)
            {
                for (int x = 0; x < shapeWidth; x++)
                {
                    if (mino.Shape[z, y, x])
                    {
                        int boardZ = mino.z + z;
                        int boardY = mino.y + y;
                        int boardX = mino.x + x;

                        if (boardZ < 0 || boardZ >= Depth ||
                            boardY < 0 || boardY >= Height ||
                            boardX < 0 || boardX >= Width ||
                            LogicBoard[boardZ, boardY, boardX] != Tetromino.Type.Empty)
                        {
                            return false;
                        }
                    }
                }
            }
        }

        return true;
    }

    private void DeleteMino(Tetromino mino)
    {
        int shapeDepth = mino.Shape.GetLength(0);
        int shapeHeight = mino.Shape.GetLength(1);
        int shapeWidth = mino.Shape.GetLength(2);

        for (int z = 0; z < shapeDepth; z++)
        {
            for (int y = 0; y < shapeHeight; y++)
            {
                for (int x = 0; x < shapeWidth; x++)
                {
                    if (mino.Shape[z, y, x])
                    {
                        int boardZ = mino.z + z;
                        int boardY = mino.y + y;
                        int boardX = mino.x + x;

                        LogicBoard[boardZ, boardY, boardX] = Tetromino.Type.Empty;
                    }
                }
            }
        }
    }

    private void DrawMino(Tetromino mino)
    {
        int shapeDepth = mino.Shape.GetLength(0);
        int shapeHeight = mino.Shape.GetLength(1);
        int shapeWidth = mino.Shape.GetLength(2);

        for (int z = 0; z < shapeDepth; z++)
        {
            for (int y = 0; y < shapeHeight; y++)
            {
                for (int x = 0; x < shapeWidth; x++)
                {
                    if (mino.Shape[z, y, x])
                    {
                        int boardZ = mino.z + z;
                        int boardY = mino.y + y;
                        int boardX = mino.x + x;

                        LogicBoard[boardZ, boardY, boardX] = mino.type;
                    }
                }
            }
        }
    }

    private bool SpawnMino()
    {
        if (CurrentMino == null)
        {
            CurrentMino = _spawner.Spawn(_spawnZ, _spawnY, _spawnX);

            if (CurrentMino == null)
            {
                return false;
            }
        }

        DrawMino(CurrentMino);

        return true;
    }

    private bool FallMino()
    {
        DeleteMino(CurrentMino);
        CurrentMino.y++;

        if (CheckMino(CurrentMino))
        {
            DrawMino(CurrentMino);
            GameManager.Instance.Falling();

            return true;
        }

        CurrentMino.y--;
        DrawMino(CurrentMino);

        return false;
    }

    private void LockMino()
    {
        CurrentMino = null;
    }

    public bool MoveHorizontal(int dx, int dz)
    {
        DeleteMino(CurrentMino);

        CurrentMino.x += dx;
        CurrentMino.z += dz;

        if (CheckMino(CurrentMino))
        {
            DrawMino(CurrentMino);
            return true;
        }

        CurrentMino.x -= dx;
        CurrentMino.z -= dz;

        DrawMino(CurrentMino);

        return false;
    }

    public bool RotateBlock()
    {
        DeleteMino(CurrentMino);

        CurrentMino.Rotate(1);

        if (CheckMino(CurrentMino))
        {
            DrawMino(CurrentMino);
            return true;
        }

        CurrentMino.Rotate(-1);

        DrawMino(CurrentMino);
        return false;
    }

    public bool ChangeLying()
    {
        DeleteMino(CurrentMino);

        CurrentMino.ChangePlane(1);

        if (CheckMino(CurrentMino))
        {
            DrawMino(CurrentMino);
            return true;
        }

        CurrentMino.ChangePlane(-1);

        DrawMino(CurrentMino);
        return false;
    }

    public bool SoftDrop()
    {
        return FallMino();
    }

    public bool HardDrop()
    {
        while (FallMino()) ;
        GameManager.Instance.GameUpdate();

        return true;
    }

    private int BoardCheck()
    {
        int clear = 0;
        //for (int y = 0; y < Height; y++)
        //{
        //    for (int z = 0; z < Depth; z++)
        //    {
        //        if (LineCheck(y, z))
        //        {
        //            LineDown(y, z);
        //        }
        //    }
        //}

        for (int y = 0; y < Height; y++)
        {
            if (FloorCheck(y))
            {
                clear++;
                FloorDown(y);
            }
        }

        return clear;
    }

    private bool FloorCheck(int y)
    {
        for (int z = 0; z < Depth; z++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (LogicBoard[z, y, x] == Tetromino.Type.Empty)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void FloorDown(int y)
    {
        for (; y >= 0; y--)
        {
            for (int z = 0; z < Depth; z++)
            {
                for (int x = 0; x < Width; x++)
                {
                    LogicBoard[z, y, x] = (y > 0 ? LogicBoard[z, y - 1, x] : Tetromino.Type.Empty);
                }
            }
        }
    }

    private bool LineCheck(int y, int z)
    {
        for (int x = 0; x < Width; x++)
        {
            if (LogicBoard[z, y, x] == Tetromino.Type.Empty)
            {
                return false;
            }
        }

        return true;
    }

    private void LineDown(int y, int z)
    {
        for (int floor = y; floor >= 0; floor--)
        {
            for (int x = 0; x < Width; x++)
            {
                LogicBoard[z, floor, x] = (floor > 0 ? LogicBoard[z, floor - 1, x] : Tetromino.Type.Empty);
            }
        }
    }

    public bool IsCurrentMinoAt(int z, int y, int x)
    {
        if (CurrentMino == null) return false;

        int localZ = z - CurrentMino.z;
        int localY = y - CurrentMino.y;
        int localX = x - CurrentMino.x;
        var shape = CurrentMino.Shape;

        if (localZ < 0 || localZ >= shape.GetLength(0) ||
            localY < 0 || localY >= shape.GetLength(1) ||
            localX < 0 || localX >= shape.GetLength(2))
        {
            return false;
        }

        return shape[localZ, localY, localX];
    }

    public int GetGhostY()
    {
        if (CurrentMino == null) return -1;

        DeleteMino(CurrentMino);

        int originY = CurrentMino.y;
        int ghostY = CurrentMino.y;

        while (true)
        {
            CurrentMino.y++;

            if (CheckMino(CurrentMino))
            {
                ghostY++;
            }
            else
            {
                CurrentMino.y = originY;
                DrawMino(CurrentMino);

                break;
            }
        }

        return ghostY;
    }
}
