using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OptionsButtonController : MonoBehaviour
{
   public static OptionsButtonController Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    bool isOptionsButtonActive = true;
    public bool IgnoreClick = false;
    public GameObject OptionsButton;

    public void EmptyClick()
    {
        if(IgnoreClick || !GameController.Instance.isGameStarted)
        {
            return;
        }

        if (isOptionsButtonActive)
        {
            HideOptionsButton();
        }
        else
        {
            ShowOptionsButton();
        }
    }

    public void HideOptionsButton()
    {
        OptionsButton.transform.DOScale(0, 0.25f);
        isOptionsButtonActive = false;
    }

    public void ShowOptionsButton()
    {
        OptionsButton.transform.DOScale(1, 0.25f);
        isOptionsButtonActive = true;
    }

}
