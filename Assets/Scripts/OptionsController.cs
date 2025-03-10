using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum Side { Left, Right }
public enum  OptionsColorType { LeftMainColor, LeftSecondaryColor, RightMainColor, RightSecondaryColor }

public class OptionsController : MonoBehaviour
{
    public static OptionsController Instance;
    void Awake() { Instance = this; }

    public TextMeshProUGUI SpeedText;
    public TMP_InputField LeftTeamName;
    public TMP_InputField RightTeamName;

    public GameObject LeftMainColor;
    public GameObject LeftSecondaryColor;
    public GameObject RightMainColor;
    public GameObject RightSecondaryColor;

    public GameObject PreviewLeftTeamMainColor;
    public GameObject PreviewLeftTeamSecondaryColor;
    public GameObject PreviewRightTeamMainColor;
    public GameObject PreviewRightTeamSecondaryColor;

    public GameObject PreviewUpperLeftTeamMainColor;
    public GameObject PreviewUpperLeftTeamSecondaryColor;
    public GameObject PreviewUpperRightTeamMainColor;
    public GameObject PreviewUpperRightTeamSecondaryColor;

    public bool LeftTeamIconAdded;
    public bool RightTeamIconAdded;

    public Sprite LeftTeamIcon;
    public Sprite RightTeamIcon;

    OptionsColorType ActiveColorType;
    public Color32 SelectedColor;

    public void OnLeftTeamNameEntered()
    {
        ChangeTeamText(Side.Left);
    }

    public void OnRightTeamNameEntered()
    {
        ChangeTeamText(Side.Right);
    }

    void ChangeTeamText(Side team)
    {
        switch (team)
        {
            case Side.Left:
                MatchCreator.Instance.MatchData.Team1.Name = LeftTeamName.text.ToUpper();
                break;
            case Side.Right:
                MatchCreator.Instance.MatchData.Team2.Name = RightTeamName.text.ToUpper();
                break;
        }
    }

    public void SpeedToggle()
    {
        MatchCreator.Instance.MatchData.speed = MatchCreator.Instance.MatchData.speed == 2 ? 1 : 2;
        SpeedText.text = MatchCreator.Instance.MatchData.speed.ToString() + "X";
    }
    
    public void ChangeColor(string colorType,bool OpenWindow=true)
    {
        //Debug.Log("Color " + colorType + " selected");

        switch (colorType)
        {
            case "LM":
                ActiveColorType = OptionsColorType.LeftMainColor;
                break;
            case "LS":
                ActiveColorType = OptionsColorType.LeftSecondaryColor;
                break;
            case "RM":
                ActiveColorType = OptionsColorType.RightMainColor;
                break;
            case "RS":
                ActiveColorType = OptionsColorType.RightSecondaryColor;
                break;
        }

        if (OpenWindow)
        {
            
        }   
    }

    public void ChangeColor(string colorType)
    {
        //Debug.Log("Color " + colorType + " selected");

        switch (colorType)
        {
            case "LM":
                ActiveColorType = OptionsColorType.LeftMainColor;
                break;
            case "LS":
                ActiveColorType = OptionsColorType.LeftSecondaryColor;
                break;
            case "RM":
                ActiveColorType = OptionsColorType.RightMainColor;
                break;
            case "RS":
                ActiveColorType = OptionsColorType.RightSecondaryColor;
                break;
        }

        WindowController.Instance.OpenColorSelectWindow();
    }

    public void AppySelectedColor()
    {
        switch (ActiveColorType)
        {
            case OptionsColorType.LeftMainColor:
                LeftMainColor.GetComponent<Image>().color = SelectedColor;
                PreviewLeftTeamMainColor.GetComponent<Image>().color = SelectedColor;
                MatchCreator.Instance.MatchData.Team1.MainColor = SelectedColor;
                if (!LeftTeamIconAdded)
                {
                    PreviewUpperLeftTeamMainColor.GetComponent<Image>().color = SelectedColor;
                }
                break;
            case OptionsColorType.LeftSecondaryColor:
                LeftSecondaryColor.GetComponent<Image>().color = SelectedColor;
                MatchCreator.Instance.MatchData.Team1.SecondaryColor = SelectedColor;
                PreviewLeftTeamSecondaryColor.GetComponent<Image>().color = SelectedColor;
                if (!LeftTeamIconAdded)
                {
                    PreviewUpperLeftTeamSecondaryColor.GetComponent<Image>().color = SelectedColor;
                }
                break;
            case OptionsColorType.RightMainColor:
                RightMainColor.GetComponent<Image>().color = SelectedColor;
                PreviewRightTeamMainColor.GetComponent<Image>().color = SelectedColor;
                MatchCreator.Instance.MatchData.Team2.MainColor = SelectedColor;
                if (!RightTeamIconAdded)
                {
                    PreviewUpperRightTeamMainColor.GetComponent<Image>().color = SelectedColor;
                }
                break;
            case OptionsColorType.RightSecondaryColor:
                RightSecondaryColor.GetComponent<Image>().color = SelectedColor;
                PreviewRightTeamSecondaryColor.GetComponent<Image>().color = SelectedColor;
                MatchCreator.Instance.MatchData.Team2.SecondaryColor = SelectedColor;
                if (!RightTeamIconAdded)
                {
                    PreviewUpperRightTeamSecondaryColor.GetComponent<Image>().color = SelectedColor;
                }
                break;
            
        }
    }


}
