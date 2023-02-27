using UnityEngine;

public class GameOver : MonoBehaviour
{
    private void Start()
    {
        King.OnKingKilled += OnGameOver;
    }

    private void OnGameOver(King king)
    {
        if (king.PieceColor == ColorCode.white)
        {
            Debug.Log("Black win");
        }

        else
        {
            Debug.Log("White win");
        }
    }
}
