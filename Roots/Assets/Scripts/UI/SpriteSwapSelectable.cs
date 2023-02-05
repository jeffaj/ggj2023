using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpriteSwapSelectable : Selectable
{
    public Sprite UnhighlightedSprite;
    public Sprite HighlightSprite;

    private bool IsSelected = false;

    public override void OnHighlight()
    {
        gameObject.GetComponent<Image>().sprite = HighlightSprite;

        IsSelected = true;

        StartCoroutine(GrowShrink());
    }

    public override void OnDehighlight()
    {
        gameObject.GetComponent<Image>().sprite = UnhighlightedSprite;

        IsSelected = false;
    }

    public override void OnSelectStyle()
    {
    }

    private IEnumerator GrowShrink()
    {
        float startTime = Time.unscaledTime;

        while (IsSelected)
        {
            float elapsed = Time.unscaledTime - startTime;
            gameObject.transform.localScale = Vector3.one + Vector3.one * 0.05f * Mathf.Sin(elapsed);
            yield return null;
        }

        // now not selected

        gameObject.transform.localScale = Vector3.one;

    }
}