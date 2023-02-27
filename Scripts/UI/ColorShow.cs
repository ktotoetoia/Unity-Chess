using UnityEngine;

public class ColorShow : MonoBehaviour
{
    [SerializeField] private Sprite blackKing;
    [SerializeField] private Sprite whiteKing;

    private SpriteRenderer spriteRenderer;

    private Clock clock;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        clock = FindObjectOfType<Clock>();
        clock.OnPlayerChanged += ChangeColor;
        ChangeColor();
    }

    private void ChangeColor()
    {
        if (spriteRenderer == null) return;
        if (clock.CurrentColor == ColorCode.white) spriteRenderer.sprite = whiteKing;
        else spriteRenderer.sprite = blackKing;
    }
}
