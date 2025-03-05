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


    OptionsColorType ActiveColorType;

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
    
    public void ChangeColor(string colorType)
    {
        Debug.Log("Color " + colorType + " selected");

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


        //

        switch (ActiveColorType)
        {
            case OptionsColorType.LeftMainColor:
                LeftMainColor.GetComponent<Image>().color = Color.gray;
                break;
            case OptionsColorType.RightMainColor:
                RightMainColor.GetComponent<Image>().color = Color.gray;
                break;
            case OptionsColorType.RightSecondaryColor:
                RightSecondaryColor.GetComponent<Image>().color = Color.gray;
                break;
            case OptionsColorType.LeftSecondaryColor:
                LeftSecondaryColor.GetComponent<Image>().color = Color.gray;
                break;
        }
    }


}
