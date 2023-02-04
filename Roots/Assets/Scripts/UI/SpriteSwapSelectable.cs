using UnityEngine;
using UnityEngine.UI;

public class SpriteSwapSelectable : Selectable
{
    public Sprite UnhighlightedSprite;
    public Sprite HighlightSprite;

    public override void OnHighlight()
    {
        gameObject.GetComponent<Image>().sprite = HighlightSprite;
    }

    public override void OnDehighlight()
    {
        gameObject.GetComponent<Image>().sprite = UnhighlightedSprite;
    }

    public override void OnSelectStyle()
    {
    }
}