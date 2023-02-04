using UnityEngine;
using UnityEngine.Events;

public abstract class Selectable : MonoBehaviour
{
    public UnityEvent OnSelectHandler;

    // when item is focused
    public abstract void OnHighlight();

    // when item looses focus
    public abstract void OnDehighlight();

    // when item is chosen
    public abstract void OnSelectStyle();

    public void OnSelect()
    {
        OnSelectStyle();

        if (OnSelectHandler != null)
        {
            OnSelectHandler.Invoke();
        }
    }
}