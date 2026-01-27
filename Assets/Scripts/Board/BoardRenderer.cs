using UnityEngine;

public class BoardRenderer : MonoBehaviour
{
    // Empty, I, O, Z, S, J, L, T, Ghost
    public Material[] cubeMaterials;
    public GameObject blockPrefab;
    // Non-shadow, shadow
    public Material[] quadMaterials;
    public GameObject yxQuadPrefab;
    public GameObject zyQuadPrefab;
    public GameObject zxQuadPrefab;

    private Board _gameBoard = null;
    private int _depth;
    private int _height;
    private int _width;
    private MeshRenderer[,,] _cubeRenderers = null;
    private MeshRenderer[,] _yxQuadRenderers = null;
    private MeshRenderer[,] _zyQuadRenderers = null;
    private MeshRenderer[,] _zxQuadRenderers = null;
    private Vector3 _pos0;

    private void Start()
    {
        _gameBoard = GameManager.Instance.GameBoard;
        _depth = _gameBoard.Depth;
        _height = _gameBoard.Height;
        _width = _gameBoard.Width;
        _cubeRenderers = new MeshRenderer[_depth, _height, _width];
        _yxQuadRenderers = new MeshRenderer[_height, _width];
        _zyQuadRenderers = new MeshRenderer[_depth, _height];
        _zxQuadRenderers = new MeshRenderer[_depth, _width];
        _pos0 = transform.rotation * (transform.position + new Vector3(-_width / 2, _height, -_depth / 2));
        FillBoards();
    }

    private void FillBoards()
    {
        // 큐브 채우기
        for (int z = 0; z < _depth; z++)
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    Vector3 blockPos = _pos0 + transform.right * x + transform.up * -y + transform.forward * z;
                    _cubeRenderers[z, y, x] = Instantiate(blockPrefab, blockPos, transform.rotation, transform)
                        .GetComponentInChildren<MeshRenderer>();
                }
            }
        }

        // Quad 채우기
        //for (int y = 0; y < _height; y++)
        //{
        //    for (int x = 0; x < _width; x++)
        //    {
        //        Vector3 quadPos = _pos0 + transform.right * x + transform.up * -y + transform.forward * _depth;
        //        _yxQuadRenderers[y, x] = Instantiate(yxQuadPrefab, quadPos, transform.rotation, transform)
        //            .GetComponentInChildren<MeshRenderer>(); ;
        //    }
        //}

        //for (int z = 0; z < _depth; z++)
        //{
        //    for (int y = 0; y < _height; y++)
        //    {
        //        Vector3 quadPos = _pos0 + transform.right * _width + transform.up * -y + transform.forward * z;
        //        _zyQuadRenderers[z, y] = Instantiate(zyQuadPrefab, quadPos, transform.rotation, transform)
        //            .GetComponentInChildren<MeshRenderer>(); ;
        //    }
        //}

        //for (int z = 0; z < _depth; z++)
        //{
        //    for (int x = 0; x < _width; x++)
        //    {
        //        Vector3 quadPos = _pos0 + transform.right * x + transform.up * -_height + transform.forward * z;
        //        _zxQuadRenderers[z, x] = Instantiate(zxQuadPrefab, quadPos, transform.rotation, transform)
        //            .GetComponentInChildren<MeshRenderer>(); ;
        //    }
        //}
    }

    public void RenderUpdate()
    {
        // 그림자 맵 초기화
        //bool[,] yxShadow = new bool[_height, _width];
        //bool[,] zyShadow = new bool[_depth, _height];
        //bool[,] zxShadow = new bool[_depth, _width];

        // 큐브 렌더링 및 그림자 누적
        for (int z = 0; z < _depth; z++)
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    // 큐브 렌더링
                    _cubeRenderers[z, y, x].material = cubeMaterials[(int)_gameBoard.LogicBoard[z, y, x]];

                    // 그림자 계산
                    //if (_gameBoard.IsCurrentMinoAt(z, y, x))
                    //{
                    //    yxShadow[y, x] = true;
                    //    zyShadow[z, y] = true;
                    //    zxShadow[z, x] = true;
                    //}
                }
            }
        }

        // 고스트 렌더링
        if (_gameBoard.CurrentMino != null)
        {
            int ghostY = _gameBoard.GetGhostY();
            int shapeDepth = _gameBoard.CurrentMino.Shape.GetLength(0);
            int shapeHeight = _gameBoard.CurrentMino.Shape.GetLength(1);
            int shapeWidth = _gameBoard.CurrentMino.Shape.GetLength(2);

            for (int z = 0; z < shapeDepth; z++)
            {
                for (int y = 0; y < shapeHeight; y++)
                {
                    for (int x = 0; x < shapeWidth; x++)
                    {
                        if (_gameBoard.CurrentMino.Shape[z, y, x])
                        {
                            int boardZ = _gameBoard.CurrentMino.z + z;
                            int boardY = ghostY + y;
                            int boardX = _gameBoard.CurrentMino.x + x;

                            if (_gameBoard.LogicBoard[boardZ, boardY, boardX] == Tetromino.Type.Empty)
                            {
                                _cubeRenderers[boardZ, boardY, boardX].material = cubeMaterials[8];
                            }
                        }
                    }
                }
            }
        }

        // 그림자 렌더링
        //for (int y = 0; y < _height; y++)
        //{
        //    for (int x = 0; x < _width; x++)
        //    {
        //        _yxQuadRenderers[y, x].material = quadMaterials[yxShadow[y, x] ? 1 : 0];
        //    }
        //}

        //for (int z = 0; z < _depth; z++)
        //{
        //    for (int y = 0; y < _height; y++)
        //    {
        //        _zyQuadRenderers[z, y].material = quadMaterials[zyShadow[z, y] ? 1 : 0];
        //    }
        //}

        //for (int z = 0; z < _depth; z++)
        //{
        //    for (int x = 0; x < _width; x++)
        //    {
        //        _zxQuadRenderers[z, x].material = quadMaterials[zxShadow[z, x] ? 1 : 0];
        //    }
        //}
    }
}
