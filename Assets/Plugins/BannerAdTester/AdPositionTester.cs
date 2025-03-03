using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdPositionTester : MonoBehaviour
{
    [Range(1, 100)]
    public float PercentageOfScreen = 15;
    RectTransform rt;

    public bool IsBottom = false;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        StartCoroutine(LateUpdatePos());
    }

    IEnumerator LateUpdatePos()
    {
        yield return null;
        float parentHeight = rt.parent.GetComponent<RectTransform>().rect.height;
        float height = parentHeight * PercentageOfScreen / 100f;

        rt.sizeDelta = new Vector2(Screen.width, height);

        if (IsBottom)
        {
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(1, 0);
            rt.pivot = new Vector2(0.5f, 0);
        }
        else
        {
            rt.anchorMin = new Vector2(0, 1);
            rt.anchorMax = new Vector2(1, 1);
            rt.pivot = new Vector2(0.5f, 1);
        }

        rt.anchoredPosition = Vector2.zero;
    }

}
