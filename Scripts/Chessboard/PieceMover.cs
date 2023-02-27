using UnityEngine;

public class PieceMover : MonoBehaviour
{
    private Clock clock;

    private void Start()
    {
        clock = GetComponent<Clock>();
    }

    public bool TryMove(Piece piece,Cell cell)
    {
        if(clock.CurrentColor == piece.PieceColor)
            return piece.TryMove(cell);

        return false;
    }
}
