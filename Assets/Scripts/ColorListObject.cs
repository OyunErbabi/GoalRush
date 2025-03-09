using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorListObject : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {  
        OptionsController.Instance.SelectedColor = GetComponent<Image>().color;
        WindowController.Instance.CloseColorSelectWindow();
        OptionsController.Instance.AppySelectedColor();
    }

}
