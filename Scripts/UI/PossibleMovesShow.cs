using System.Collections.Generic;
using UnityEngine;

public class ShowPossibleMoves : MonoBehaviour
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
        foreach (Cell cell in selector.SelectedPiece.GetAllPossibleMoves())
        {
            shows.Add(Instantiate(prefab, new Vector3(cell.x,cell.y), prefab.transform.rotation));
        }
    }

    private void Hide()
    {
        foreach(GameObject go in shows)
        {
            Destroy(go);
        }
    }
}
