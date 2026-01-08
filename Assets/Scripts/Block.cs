using UnityEngine;

public class Block : MonoBehaviour
{
    public int w, h, d;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void MoveDown()
    {
        h--;
        transform.Translate(Vector3.down);
    }
}
