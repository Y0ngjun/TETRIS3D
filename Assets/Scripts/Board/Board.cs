// 1. z, y, x 배열로 보드를 관리
// 2. (0, 0, 0)이 가장 앞면의 좌상단 꼭짓점
public class Board
{
    public int Depth { get; private set; }
    public int Height { get; private set; }
    public int Width { get; private set; }
    public Tetromino.Type[,,] LogicBoard { get; private set; }

    private Spawner _spawner = null;
    private Tetromino _tetromino = null;
    private int _spawnZ = 0;
    private int _spawnY = 0;
    private int _spawnX = 3;

    public Board()
    {
        Depth = 10;
        Height = 20;
        Width = 10;
        LogicBoard = new Tetromino.Type[Depth, Height, Width];
    }

    public void Init()
    {
        _spawner = GameManager.Instance.MinoSpawner;
        SpawnMino();
    }

    public bool BoardUpdate()
    {
        if (!FallMino())
        {
            LockMino();

            if (!SpawnMino())
            {
                return false;
            }
        }

        return true;
    }

    public bool CheckMino(Tetromino mino)
    {
        int shapeDepth = mino.shape.GetLength(0);
        int shapeHeight = mino.shape.GetLength(1);
        int shapeWidth = mino.shape.GetLength(2);

        for (int z = 0; z < shapeDepth; z++)
        {
            for (int y = 0; y < shapeHeight; y++)
            {
                for (int x = 0; x < shapeWidth; x++)
                {
                    if (mino.shape[z, y, x])
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
        int shapeDepth = mino.shape.GetLength(0);
        int shapeHeight = mino.shape.GetLength(1);
        int shapeWidth = mino.shape.GetLength(2);

        for (int z = 0; z < shapeDepth; z++)
        {
            for (int y = 0; y < shapeHeight; y++)
            {
                for (int x = 0; x < shapeWidth; x++)
                {
                    if (mino.shape[z, y, x])
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
        int shapeDepth = mino.shape.GetLength(0);
        int shapeHeight = mino.shape.GetLength(1);
        int shapeWidth = mino.shape.GetLength(2);

        for (int z = 0; z < shapeDepth; z++)
        {
            for (int y = 0; y < shapeHeight; y++)
            {
                for (int x = 0; x < shapeWidth; x++)
                {
                    if (mino.shape[z, y, x])
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
        if (_tetromino == null)
        {
            _tetromino = _spawner.Spawn(_spawnZ, _spawnY, _spawnX);

            if (_tetromino == null)
            {
                return false;
            }
        }

        DrawMino(_tetromino);

        return true;
    }

    private bool FallMino()
    {
        DeleteMino(_tetromino);
        _tetromino.y++;

        if (CheckMino(_tetromino))
        {
            DrawMino(_tetromino);
            return true;
        }
        else
        {
            _tetromino.y--;
            DrawMino(_tetromino);
        }

        return false;
    }

    private void LockMino()
    {
        _tetromino = null;
    }
}
