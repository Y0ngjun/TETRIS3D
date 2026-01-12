public class Jmino : Tetromino
{
    public Jmino()
    {
        type = Type.J;

        shape = new bool[4, 4, 4]
        {
            {
                { false, true, false, false },
                { false, true, false, false },
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
