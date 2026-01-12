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
    public bool[,,] shape;
    public Type type;
}
