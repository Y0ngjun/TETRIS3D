// 1. 미노를 생성 및 반환
// 2. 다음 미노를 관리
using UnityEngine;

public class Spawner
{

    private Tetromino _nextMino;
    private Board _gameBoard;

    public Spawner()
    {
        _nextMino = TetrominoFactory.CreateRandom();
    }

    public void Init()
    {
        _gameBoard = GameManager.Instance.GameBoard;
    }

    public Tetromino Spawn(int spawnZ, int spawnY, int spawnX)
    {
        Tetromino mino = _nextMino;
        mino.z = spawnZ;
        mino.y = spawnY;
        mino.x = spawnX;

        if (!_gameBoard.CheckMino(mino))
        {
            return null;
        }

        _nextMino = TetrominoFactory.CreateRandom();
        return mino;
    }
}
