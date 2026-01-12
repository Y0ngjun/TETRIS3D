public class Imino : Tetromino
{
    public Imino()
    {
        type = Type.I;

        shape = new bool[4, 4, 4]
        {
            {
                { true, false, false, false },
                { true, false, false, false },
                { true, false, false, false },
                { true, false, false, false }
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
