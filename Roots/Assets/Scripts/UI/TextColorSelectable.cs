using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextColorSelectable : Selectable
{
    #region Inspector Fields

    [SerializeField]
    private Color _dehighlightColor = Color.white;
    [SerializeField]
    private Color _highlightColor = Color.green;

    [Header("Children")]

    [SerializeField]
    private TMPro.TextMeshProUGUI _text = null;

    #endregion

    private bool IsSelected;

    public override void OnHighlight()
    {
        _text.color = _highlightColor;

        IsSelected = true;

        StartCoroutine(GrowShrink());
    }

    public override void OnDehighlight()
    {
        _text.color = _dehighlightColor;

        IsSelected = false;
    }

    public override void OnSelectStyle() { }

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