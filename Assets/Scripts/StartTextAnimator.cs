using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class StartTextAnimator : MonoBehaviour
{
    public static StartTextAnimator Instance;
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

    public TextMeshProUGUI StartText;

    public IEnumerator StartTextAnimation()
    {
        WindowController.Instance.OptionsButton.GetComponent<Button>().interactable = false;

        StartText.gameObject.SetActive(true);
        StartText.transform.localScale = Vector3.zero;

        for (int i = 3; i > 0; i--)
        {
            StartText.text = i.ToString();
            StartText.transform.DOScale(1, 0.5f);
            yield return new WaitForSeconds(0.5f);
            StartText.transform.DOScale(0, 0.5f);
            yield return new WaitForSeconds(0.5f);
        }

        //StartText.text = "Go!";
        //StartText.transform.DOScale(1, 0.5f);
        //yield return new WaitForSeconds(0.5f);        
        //StartText.transform.DOScale(0, 0.5f);
        //yield return new WaitForSeconds(0.5f);

        SoundManager.Instance.PlayStartSound();

        StartText.text = "";
        StartText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        WindowController.Instance.OptionsButton.GetComponent<Button>().interactable = true;
    }
}
