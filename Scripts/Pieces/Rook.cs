using UnityEngine;

public class Rook : Piece
{
    public override bool CanMove(Cell cell)
    {
        if (!base.CanMove(cell)) return false;

        return SideCheck().Contains(cell);
    }
}
