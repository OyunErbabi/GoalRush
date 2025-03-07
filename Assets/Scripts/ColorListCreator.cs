using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorListCreator : MonoBehaviour
{
    public GameObject ColorPrefab;
    public GameObject Content;
    private void Start()
    {
        //Debug.Log(ColorList.DefinedColors.Length);

        foreach (Color32 color in ColorList.DefinedColors)
        {
            GameObject colorObject = Instantiate(ColorPrefab, Content.transform);
            colorObject.GetComponent<Image>().color = color;
        }
    }
}
