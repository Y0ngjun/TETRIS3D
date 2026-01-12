public class Tmino : Tetromino
{
    public Tmino()
    {
        type = Type.T;

        shape = new bool[4, 4, 4]
        {
            {
                { true, true, true, false },
                { false, true, false, false },
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
