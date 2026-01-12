// 1. 미노의 모양을 명시하는 컨테이너
public class Lmino : Tetromino
{
    public Lmino()
    {
        type = Type.L;

        shape = new bool[4, 4, 4]
        {
            {
                { true, false, false, false },
                { true, false, false, false },
                { true, true, false, false },
                { false, false, false, false }
            },
            {
                { false, false, false, false },
                { false, false, false, false },
                { false, false, false, false },
                { false, false, false, false }
            },
            {
                { false, false, false, false },
                { false, false, false, false },
                { false, false, false, false },
                { false, false, false, false }
            },
            {
                { false, false, false, false },
                { false, false, false, false },
                { false, false, false, false },
                { false, false, false, false }
            }
        };
    }
}
