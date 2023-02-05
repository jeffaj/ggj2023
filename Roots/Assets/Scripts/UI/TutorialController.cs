using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialController : MonoBehaviour
{
    // sets on all components under this
    public void SetAlpha(float alpha)
    {
        transform.Find("Text").GetComponent<TextMeshProUGUI>().alpha = alpha;
        SetAlphaOnImage("LeftArrow", alpha);
        SetAlphaOnImage("RightArrow", alpha);
        SetAlphaOnImage("DownArrow", alpha);
        SetAlphaOnImage("Fuel", alpha);
        SetAlphaOnImage("Fuel (1)", alpha);
    }

    private void SetAlphaOnImage(string childName, float alpha)
    {
        var child = transform.Find(childName).GetComponent<Image>();
        child.color = new Color(child.color.r, child.color.g, child.color.b, alpha);
    }
}
