using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchCreator : MonoBehaviour
{
   
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