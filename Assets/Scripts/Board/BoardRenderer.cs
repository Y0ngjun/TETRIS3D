using UnityEngine;

public class BoardRenderer : MonoBehaviour
{
    public Material[] materials;
    public GameObject blockPrefab;

    private Board _gameBoard = null;
    private int _depth;
    private int _height;
    private int _width;
    private GameObject[,,] _renderBoard = null;
    private Vector3 _pos0;

    private void Start()
    {
        _gameBoard = GameManager.Instance.GameBoard;
        _depth = _gameBoard.Depth;
        _height = _gameBoard.Height;
        _width = _gameBoard.Width;
        _renderBoard = new GameObject[_depth, _height, _width];
        _pos0 = new Vector3(-_width / 2, _height, -_depth / 2);
        FillRenderBoard();
    }

    private void FillRenderBoard()
    {
        for (int z = 0; z < _depth; z++)
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    Vector3 blockPos = _pos0 + transform.right * x + transform.up * -y + transform.forward * z;
                    _renderBoard[z, y, x] = Instantiate(blockPrefab, blockPos, transform.rotation, transform);
                }
            }
        }
    }

    public void RenderUpdate()
    {
        for (int z = 0; z < _depth; z++)
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    // 렌더링 업데이트
                    var mat = materials[(int)_gameBoard.LogicBoard[z, y, x]];

                    _renderBoard[z, y, x].GetComponentInChildren<MeshRenderer>().material = mat;
                }
            }
        }
    }
}
