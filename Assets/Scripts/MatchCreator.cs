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
                Name = "GS",
                MainColor = new Color32(227, 74, 74, 255),
                SecondaryColor = new Color32(226, 192, 76, 255)
            },

            Team2 = new Team()
            {
                Name = "FB",
                MainColor = new Color32(0, 0, 255, 255),
                SecondaryColor = new Color32(226, 192, 76, 255)
            },

            speed = 2
        };
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