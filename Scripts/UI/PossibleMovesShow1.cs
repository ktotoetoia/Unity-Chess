using System.Collections.Generic;
using UnityEngine;

public class PossibleMovesShow1 : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private PieceSelector selector;
    private List<GameObject> shows = new List<GameObject>();

    private void Start()
    {
        selector = GetComponent<PieceSelector>();
        selector.OnPieceSelected += Show;
        selector.OnPieceDeselected += Hide;
    }

    private void Show()
    {
        if(shows.Count>0)Hide();
        foreach (Cell cell in selector.SelectedPiece.GetAllPossibleMoves())
        {
            shows.Add(Instantiate(prefab, new Vector3(cell.x-3.5f, cell.y - 3.5f), prefab.transform.rotation));
        }
    }

    private void Hide()
    {
        foreach (GameObject go in shows)
        {
            Destroy(go);
        }
    }
}
