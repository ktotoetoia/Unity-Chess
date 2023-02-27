using UnityEngine;

public class Queen : Piece
{
    public override bool CanMove(Cell cell)
    {
        if (!base.CanMove(cell)) return false;

        return DiagonalCheck().Contains(cell)||SideCheck().Contains(cell);
    }
}
