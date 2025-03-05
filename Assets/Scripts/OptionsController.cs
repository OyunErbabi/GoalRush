using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Side { Left, Right }

public class OptionsController : MonoBehaviour
{
    public static OptionsController Instance;
    void Awake() { Instance = this; }

    public TextMeshProUGUI SpeedText;

    public TMP_InputField LeftTeamName;
    public TMP_InputField RightTeamName;


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

        // Save To MatchDATA

    }

}
