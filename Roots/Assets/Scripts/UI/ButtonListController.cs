using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class ButtonListController : MonoBehaviour
{
    private Selectable[] Selectables;

    private int SelectedIndex = 0;

    private bool initialized = false;

    void Init()
    {
        if (initialized)
        {
            return;
        }

        initialized = true;

        Selectables = gameObject.transform.GetComponentsInChildren<Selectable>();
        Selectables[SelectedIndex].OnHighlight();
    }

    void Update()
    {
        Init();

        var select = Input.GetKeyDown(KeyCode.Return);
        if (select)
        {
            Selectables[SelectedIndex].OnSelect();
            return;
        }

        var down = Input.GetKeyDown(KeyCode.DownArrow) ? 1 : 0;
        var up = Input.GetKeyDown(KeyCode.UpArrow) ? -1 : 0;

        var change = down + up;

        var prevSelectedIdx = SelectedIndex;
        SelectedIndex = Math.Min(Math.Max(SelectedIndex + change, 0), Selectables.Length - 1);

        if (SelectedIndex == prevSelectedIdx)
        {
            return;
        }

        if (SelectedIndex < 0)
        {
            SelectedIndex += Selectables.Length;
        }
        else
        {
            SelectedIndex = SelectedIndex % Selectables.Length;
        }

        Selectables[prevSelectedIdx].OnDehighlight();
        Selectables[SelectedIndex].OnHighlight();
    }
}
