public class Smino : Tetromino
{
    public Smino()
    {
        type = Type.S;

        shape = new bool[4, 4, 4]
        {
            {
                { false, true, true, false },
                { true, true, false, false },
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
