using UnityEngine;

public class Pawn : Piece
{
    [SerializeField] private GameObject queen;

    private int direction = 1;

    private int promoteCell;

    private void Start()
    {
        if(currentCell.y>4) direction = -1;
        promoteCell = direction > 0 ? chessboard.Size-1 : 0;
        OnPieceMoved += TryPromote;
    }

    public override bool CanMove(Cell cell)
    {
        if( !base.CanMove(cell)) return false;

        int x = cell.x - currentCell.x;
        int y = cell.y - currentCell.y;

        if(cell.y - currentCell.y == direction)
        {
            if (x == 0) 
            {
                return !cell.IsOcuppied;
            }
            else if (Mathf.Abs(x) == 1)
            {
                return cell.IsOcuppied;
            }
        }

        if(cell.y-CurrentCell.y == direction * 2&&x==0)
        {
            return !IsMoved && !cell.IsOcuppied;
        }
        return false;
    }

    private void TryPromote()
    {
        if(currentCell.y == promoteCell)
        {
            Promote();
        }
    }

    private void Promote()
    {
        GameObject obj = Instantiate(queen, transform.position, queen.transform.rotation);

        Piece piece = obj.GetComponent<Piece>();
        piece.ChangeColor(pieceColor);
        piece.SetToCell(CurrentCell);
        Destroy();
    }
}
