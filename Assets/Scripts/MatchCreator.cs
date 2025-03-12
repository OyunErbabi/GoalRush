using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchCreator : MonoBehaviour
{
    public static MatchCreator Instance;
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

    public MatchData MatchData;

    private void Start()
    {
        MatchData = new MatchData()
        {
            Team1 = new Team()
            {
                Name = "L",
                MainColor = new Color32(227, 74, 74, 255),
                SecondaryColor = new Color32(226, 192, 76, 255)
            },

            Team2 = new Team()
            {
                Name = "R",
                MainColor = new Color32(0, 0, 255, 255),
                SecondaryColor = new Color32(226, 192, 76, 255)
            },

            speed = 2
        };

        OptionsController.Instance.LeftTeamName.text = MatchData.Team1.Name;
        OptionsController.Instance.RightTeamName.text = MatchData.Team2.Name;

        OptionsController.Instance.SelectedColor = MatchData.Team1.MainColor;
        OptionsController.Instance.ChangeColor("LM",false);
        OptionsController.Instance.AppySelectedColor();

        OptionsController.Instance.SelectedColor = MatchData.Team1.SecondaryColor;
        OptionsController.Instance.ChangeColor("LS",false);
        OptionsController.Instance.AppySelectedColor();

        OptionsController.Instance.SelectedColor = MatchData.Team2.MainColor;
        OptionsController.Instance.ChangeColor("RM", false);
        OptionsController.Instance.AppySelectedColor();

        OptionsController.Instance.SelectedColor = MatchData.Team2.SecondaryColor;
        OptionsController.Instance.ChangeColor("RS", false);
        OptionsController.Instance.AppySelectedColor();

        GameController.Instance.SetTeamNamesOnMainScreen();
    }

}

public class Team
{
    public string Name;
    public Color32 MainColor;
    public Color32 SecondaryColor;
}          

public class MatchData
{
    public Team Team1;
    public Team Team2;
    public int speed;
}