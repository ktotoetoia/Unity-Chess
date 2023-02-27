using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private ColorCode currentPlayer = ColorCode.white;
    public ColorCode CurrentColor { get { return currentPlayer; } }

    private List<ColorCode> currentPlayers = new List<ColorCode>();

    public delegate void PlayerChanged();
    public event PlayerChanged OnPlayerChanged;

    private void Awake()
    {
        Piece.StaticOnPieceMoved += ChangeCurrentPlayer;
    }

    private void ChangeCurrentPlayer()
    {
        if (currentPlayer == ColorCode.black) currentPlayer = ColorCode.white;
        else currentPlayer = ColorCode.black;
        OnPlayerChanged?.Invoke();
    }

    public void AddPlayer(ColorCode playerColor)
    {
        currentPlayers.Add(playerColor);
    }
}
