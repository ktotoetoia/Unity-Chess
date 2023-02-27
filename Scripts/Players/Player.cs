using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ColorCode playerColor;

    private Chessboard chessboard;
    private PieceSelector selector;
    private Clock clock;

    void Awake()
    {
        chessboard = FindObjectOfType<Chessboard>();
        selector = FindObjectOfType<PieceSelector>();
        clock = FindObjectOfType<Clock>();
    }
    private void Update()
    {
        if (clock.CurrentColor == playerColor)
        {
            PlayerInput();
        }
    }

    private void PlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selector.TrySelectPiece(mousePos);
        }
    }
}
