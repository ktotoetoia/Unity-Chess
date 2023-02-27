using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Chessboard : MonoBehaviour
{
    [SerializeField] private GameObject king;
    [SerializeField] private GameObject bishop;
    [SerializeField] private GameObject rook;
    [SerializeField] private GameObject knight;
    [SerializeField] private GameObject queen;
    [SerializeField] private GameObject pawn;

    private readonly int size = 8;
    public int Size { get { return size; } }

    private Cell[,] cells;
    public Cell[,] Cells2D { get { return cells; } }

    private ColorCode firstPlayer = ColorCode.white;
    private ColorCode secondPlayer = ColorCode.black;

    private void Start()
    {
        InstantiateCells();
        InstantiatePieces();
    }

    private void InstantiateCells()
    {
        cells = new Cell[size, size];
        for (int i = size-1; i >= 0; i--)
        {
            for (int j = size - 1; j >= 0; j--)
            {
                cells[i, j] = new Cell(i, j);
            }
        }
    }

    private void InstantiatePieces()
    {
        for(int i = 0; i < size; i++)
        {
            AddPiece(pawn, Cells2D[i, 1], firstPlayer);
            AddPiece(pawn, Cells2D[i, size-2], secondPlayer);
        }
        AddPiece(rook, cells[0, 0], firstPlayer);
        AddPiece(rook, cells[size - 1, 0], firstPlayer);
        AddPiece(knight, cells[1, 0], firstPlayer);
        AddPiece(knight, cells[size - 2, 0], firstPlayer);
        AddPiece(bishop, cells[2, 0], firstPlayer);
        AddPiece(bishop, cells[size - 3, 0], firstPlayer);
        AddPiece(queen, cells[3, 0], firstPlayer);
        AddPiece(king, cells[4, 0], firstPlayer);


        AddPiece(rook, cells[0, size-1], secondPlayer);
        AddPiece(rook, cells[size - 1, size - 1], secondPlayer);
        AddPiece(knight, cells[1, size - 1], secondPlayer);
        AddPiece(knight, cells[size - 2, size - 1], secondPlayer);
        AddPiece(bishop, cells[2, size - 1], secondPlayer);
        AddPiece(bishop, cells[size - 3, size - 1], secondPlayer);
        AddPiece(queen, cells[3, size - 1], secondPlayer);
        AddPiece(king, cells[4, size - 1], secondPlayer);
    }
    
    private void AddPiece(GameObject piece,Cell cell,ColorCode color)
    {
        GameObject p = Instantiate(piece, cell.WorldPosition, piece.transform.rotation);
        Piece pi = p.GetComponent<Piece>();
        pi.ChangeColor(color);
        pi.SetToCell(cell);
    }

    public Cell GetNearestCell(Vector3 position)
    {
        Cell result = cells[0,0];

        foreach(Cell cell in cells)
        {
            if (Vector3.Distance(position, cell.WorldPosition)<Vector3.Distance(position, result.WorldPosition))
            {
                result = cell;
            }
        }

        return result;
    }

    public IEnumerable<Cell> Cells
    {
        get
        {
            foreach (Cell cell in cells) yield return cell;
        }
    }
}