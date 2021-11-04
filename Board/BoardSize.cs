using UnityEngine;

public class BoardSize : MonoBehaviour
{
    [SerializeField] private Vector2Int _sizeBoard;  // Crate this script no MonoBehavior

    public int Height => _sizeBoard.y;
    public int Width => _sizeBoard.x;

}
