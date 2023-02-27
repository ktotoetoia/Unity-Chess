using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class King : Piece
{
    public delegate void KingKilled(King king);
    public static event KingKilled OnKingKilled;

    private void OnDestroy()
    {
        OnKingKilled?.Invoke(this);
    }

    public override bool CanMove(Cell cell)
    {
        if (!base.CanMove(cell)) return false;

        int x = Mathf.Abs(cell.x - currentCell.x);
        int y = Mathf.Abs(cell.y - currentCell.y);

        return x+y==1||x*y==1;
    }

    public bool UnderAttack(List<Cell> cells,ColorCode kingColor)
    {
        cells = cells.Where(x =>x.IsOcuppied).ToList();
        Cell kingCell = cells.Find(cell =>cell.CurrentPiece.PieceColor==kingColor&& cell.CurrentPiece is King);
        return cells
            .Any(x => x.CurrentPiece.PieceColor != kingColor &&
            x.CurrentPiece.CanMove(currentCell));
    }
}
