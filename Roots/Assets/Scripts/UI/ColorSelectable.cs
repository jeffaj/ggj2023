using UnityEngine;
using UnityEngine.UI;

public class ColorSelectable : Selectable
{
    // TODO: configure colors

    public override void OnHighlight()
    {
        gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.9f, 1);
    }

    public override void OnDehighlight()
    {
        gameObject.GetComponent<Image>().color = Color.white;
    }

    public override void OnSelectStyle()
    {
        // nothing
    }
}