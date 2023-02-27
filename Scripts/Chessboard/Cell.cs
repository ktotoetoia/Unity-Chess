using UnityEngine;

public class Cell
{
    public readonly int x;
    public readonly int y;

    private Vector3 offset = new Vector3(-3.5f, -3.5f);
    public readonly Vector3 WorldPosition;

    private bool isOcuppied;
    public bool IsOcuppied { get { return isOcuppied; } }

    private Piece currentPiece = null;
    public Piece CurrentPiece { get { return currentPiece; } }

    public Cell(int x, int y)
    {
        this.x = x;
        this.y = y;

        WorldPosition = new Vector3(x, y) + offset;
    }

    public void Occupy(Piece piece)
    {
        if (isOcuppied)
        {
            currentPiece.Destroy();
            Unoccupy();
        }

        isOcuppied = true;
        currentPiece = piece;
    }

    public void Unoccupy()
    {
        currentPiece = null;
        isOcuppied = false;
    }
}