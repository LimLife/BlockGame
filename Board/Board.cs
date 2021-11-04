using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private BoardSize _boardSize;
    private int _height => _boardSize.Height;
    private int _width => _boardSize.Width;
    public int RandomWidth
    {
        get
        {
            return (int)Random.Range(0, _width);
        }
    }

    [SerializeField] private Blcok _block;
    private void Start()
    {
     InvokeRepeating(nameof(Spanwer),3,3);
    }
    private void Spanwer()
    {
        Vector3Int postionSpawn = new Vector3Int(RandomWidth, _height, 0);
        Instantiate(_block, postionSpawn, Quaternion.identity);
    }
}
