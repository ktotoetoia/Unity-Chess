using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum ColorCode
{
    black,
    white
}

public class Piece : MonoBehaviour, IPiece
{
    [SerializeField] private List<ColorCode> colors = new List<ColorCode>();
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();

    private Dictionary<ColorCode, Sprite> colorSprite = new Dictionary<ColorCode, Sprite>();

    protected ColorCode pieceColor;
    public ColorCode PieceColor { get { return pieceColor; } }

    protected bool isKilled;
    public bool IsKilled { get { return isKilled; } }

    protected Cell currentCell;
    public Cell CurrentCell { get { return currentCell; } }

    public delegate void PieceInteraction();
    public event PieceInteraction OnPieceMoved;

    public static event PieceInteraction StaticOnPieceMoved;

    protected Chessboard chessboard;

    private bool isMoved;

    public bool IsMoved { get { return isMoved; } }

    private void Awake()
    {

        for (int i = 0; i < colors.Count; i++)
        {
            colorSprite[colors[i]] = sprites[i];
        }

        chessboard = FindObjectOfType<Chessboard>();
    }
    public bool TryMove(Cell cell)
    {
        if (CanMove(cell))
        {
            cell.Occupy(this);
            currentCell.Unoccupy();

            currentCell = cell;
            UpdatePosition();

            isMoved = true;
            OnPieceMoved?.Invoke();
            StaticOnPieceMoved?.Invoke();
            return true;
        }
        return false;
    }

    public virtual bool CanMove(Cell cell)
    {
        return !cell.IsOcuppied||cell.CurrentPiece.PieceColor != pieceColor;
    }

    public void UpdatePosition()
    {
        transform.position = currentCell.WorldPosition;
    }

    public void Destroy()
    {
        currentCell.Unoccupy();
        Destroy(gameObject);
    }

    public void ChangeColor(ColorCode color)
    {
        pieceColor = color;
        GetComponent<SpriteRenderer>().sprite = colorSprite[color];
    }

    public void SetToCell(Cell cell)
    {
        cell.Unoccupy();
        cell.Occupy(this);
        currentCell = cell;
        UpdatePosition();
    }

    public List<Cell> GetAllPossibleMoves()
    {
        return chessboard.Cells.Where(x => CanMove(x)).ToList();
    }

    public List<Cell> DiagonalCheck()
    {
        List<Cell> possibleMoves = new List<Cell>();

        int size = chessboard.Size;
        int curX = currentCell.x;
        int curY = currentCell.y;

        for (int xDir = -1; xDir <= 1; xDir += 2)
        {
            for (int yDir = -1; yDir <= 1; yDir += 2)
            {
                for (int x = curX + xDir, y = curY + yDir; x >= 0 && y >= 0 && x < size && y < size; x += xDir, y += yDir)
                {
                    if (!chessboard.Cells2D[x, y].IsOcuppied)
                    {
                        possibleMoves.Add(chessboard.Cells2D[x, y]);
                    }
                    else
                    {
                        if (chessboard.Cells2D[x, y].CurrentPiece.PieceColor != pieceColor)
                        {
                            possibleMoves.Add(chessboard.Cells2D[x, y]);
                        }
                        break;
                    }
                }
            }
        }

        return possibleMoves;
    }

    public List<Cell> SideCheck()
    {
        List<Cell> possibleMoves = new List<Cell>();

        int size = chessboard.Size;
        int curX = currentCell.x;
        int curY = currentCell.y;

        for (int xDir = -1; xDir <= 1; xDir += 2)
        {
            for (int x = curX + xDir; x >= 0 && x < size; x += xDir) 
            {
                Cell cell = chessboard.Cells2D[x, curY];
                if (!cell.IsOcuppied)
                {
                    possibleMoves.Add(cell);
                }
                else
                {
                    if (cell.CurrentPiece.PieceColor != pieceColor)
                    {
                        possibleMoves.Add(cell);
                    }
                    break;
                }
            }
        }

        for (int yDir = -1; yDir <= 1; yDir += 2)
        {
            for (int y = curY + yDir; y >= 0 && y < size; y += yDir)
            {
                Cell cell = chessboard.Cells2D[curX, y];
                if (!cell.IsOcuppied)
                {
                    possibleMoves.Add(cell);
                }
                else
                {
                    if (cell.CurrentPiece.PieceColor != pieceColor)
                    {
                        possibleMoves.Add(cell);
                    }
                    break;
                }
            }
        }
        return possibleMoves;
    }
}

public interface IPiece : IMovable
{
    public ColorCode PieceColor { get; }
    public bool IsKilled { get; }

    public void ChangeColor(ColorCode color);
    public void Destroy();
}

public interface IMovable
{
    public Cell CurrentCell { get; }

    public bool TryMove(Cell cell);

    public  bool CanMove(Cell cell);

    public void SetToCell(Cell cell);

    public void UpdatePosition();

    public List<Cell> GetAllPossibleMoves();
}