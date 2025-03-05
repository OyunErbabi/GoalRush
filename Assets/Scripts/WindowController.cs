using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class WindowController : MonoBehaviour
{
    public static WindowController Instance;
    void Awake() { Instance = this; }

    public GameObject MainWindow;
    public GameObject OptionsWindow;
    public GameObject PlayButton;
    public GameObject ColorSelectWindow;

    public void OpenOptions()
    {
        MainWindow.SetActive(false);
        OptionsWindow.SetActive(true);
    }

    public void CloseOptions()
    {
        MainWindow.SetActive(true);
        OptionsWindow.SetActive(false);
    }

    public void OpenColorSelectWindow()
    {
        ColorSelectWindow.SetActive(true);
    }

    public void CloseColorSelectWindow() 
    {
        ColorSelectWindow.SetActive(false);
    }

    public void ShowPlayButton()
    {
        StartCoroutine(ShowPlayButtonCor());
    }

    IEnumerator ShowPlayButtonCor()
    {
        PlayButton.transform.DOScale(1, 0.25f);
        yield return new WaitForSeconds(0.25f);
    }

    public void HidePlayButton()
    {
        StartCoroutine(HidePlayButtonCor());
    }

    public IEnumerator HidePlayButtonCor()
    {
        PlayButton.transform.DOScale(0, 0.25f);
        yield return new WaitForSeconds(0.25f);
        PlayButton.SetActive(false);
    }

}
