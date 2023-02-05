using UnityEngine;
using UnityEngine.UI;

public class TextColorSelectable : Selectable {

    #region Inspector Fields

    [SerializeField]
    private Color _dehighlightColor = Color.white;
    [SerializeField]
    private Color _highlightColor = Color.green;

    [Header("Children")]

    [SerializeField]
    private TMPro.TextMeshProUGUI _text = null;

    #endregion

    public override void OnHighlight() {
        _text.color = _highlightColor;
    }

    public override void OnDehighlight() {
        _text.color = _dehighlightColor;
    }

    public override void OnSelectStyle() { }
}