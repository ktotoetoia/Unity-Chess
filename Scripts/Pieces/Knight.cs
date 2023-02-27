using UnityEngine;

public class Knight : Piece
{

    public override bool CanMove(Cell cell)
    {
        if (!base.CanMove(cell)) return false;
        int x = Mathf.Abs(cell.x - currentCell.x);
        int y = Mathf.Abs(cell.y - currentCell.y);

        return   x * y == 2;
    }
}