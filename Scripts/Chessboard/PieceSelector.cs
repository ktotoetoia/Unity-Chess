using UnityEngine;

public class PieceSelector : MonoBehaviour
{
    [SerializeField] LayerMask pieceMask;
    [SerializeField] LayerMask boardMask;

    private bool isSelected;
    public bool IsSelected {  get { return isSelected; } }

    private Piece selectedPiece;
    public Piece SelectedPiece { get { return selectedPiece; } }

    private Chessboard chessboard;
    private PieceMover pieceMover;
    private Clock clock;

    public delegate void PieceInteraction();
    public event PieceInteraction OnPieceSelected;
    public event PieceInteraction OnPieceDeselected;

    private void Start()
    {
        chessboard = GetComponent<Chessboard>();
        pieceMover = GetComponent<PieceMover>();
        clock = GetComponent<Clock>();

        clock.OnPlayerChanged += DeselectPiece;
    }

    public void TrySelectPiece(Vector3 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero,1,pieceMask);

        if (!hit&& Vector2.Distance(position, chessboard.GetNearestCell(position).WorldPosition) >= 1)
        {
            DeselectPiece();
            return;
        }

        if (isSelected && selectedPiece != null && selectedPiece.CanMove(chessboard.GetNearestCell(position)))
        {
            pieceMover.TryMove(selectedPiece, chessboard.GetNearestCell(position));

            DeselectPiece();
            return;
        }

        if (hit && hit.collider.gameObject.TryGetComponent(out Piece piece) && piece.PieceColor == clock.CurrentColor)
        {
            SelectPiece(piece);
            return;
        }
    }

    private void SelectPiece(Piece piece)
    {
        selectedPiece = piece;
        isSelected = true;

        OnPieceSelected?.Invoke();
    }

    private void DeselectPiece()
    {
        selectedPiece = null;
        isSelected = false;

        OnPieceDeselected?.Invoke();
    }
}