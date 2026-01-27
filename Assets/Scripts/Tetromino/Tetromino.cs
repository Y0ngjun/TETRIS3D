// 1. 미노의 정보를 담는 컨테이너
public abstract class Tetromino
{
    public enum Type
    {
        Empty = 0,
        I = 1,
        O = 2,
        Z = 3,
        S = 4,
        J = 5,
        L = 6,
        T = 7
    }

    public int z;
    public int y;
    public int x;
    public Type type;

    // currentPlane: 0(xy) 1(xz) 2(yz), rotate: 0~3
    protected int _currentPlane;
    protected int _rotate;
    protected bool[,][,,] _shapes;

    public bool[,,] Shape
    {
        get
        {
            return _shapes[_currentPlane, _rotate];
        }
    }

    public void Rotate(int n)
    {
        _rotate = (_rotate + n + 4) % 4;
    }

    public void ChangePlane(int n)
    {
        _currentPlane = (_currentPlane + n + 3) % 3;
    }
}
