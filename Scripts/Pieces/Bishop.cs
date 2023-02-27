using UnityEngine;

public class Bishop : Piece
{
    public override bool CanMove(Cell cell)
    {
        if (!base.CanMove(cell)) return false;

        return DiagonalCheck().Contains(cell);
    }
}