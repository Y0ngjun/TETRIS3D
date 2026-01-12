using UnityEngine;

public static class TetrominoFactory
{
    public static T Create<T>() where T : Tetromino, new()
    {
        return new T();
    }

    public static Tetromino CreateRandom()
    {
        int randomType = Random.Range(1, 8);

        return randomType switch
        {
            1 => Create<Imino>(),
            2 => Create<Omino>(),
            3 => Create<Zmino>(),
            4 => Create<Smino>(),
            5 => Create<Jmino>(),
            6 => Create<Lmino>(),
            7 => Create<Tmino>(),
            _ => Create<Lmino>()
        };
    }
}
