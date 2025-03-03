using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;


public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public bool GameOver = false;

    public GameObject Area;

    int GameTimer = 0;
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI LeftScoreText;
    public TextMeshProUGUI RightScoreText;
    public TextMeshProUGUI LeftTeamNameText;
    public TextMeshProUGUI RightTeamNameText;

    int ScoreLeft = 0;
    int ScoreRight = 0;

    public GameObject LeftBall;
    public GameObject RightBall;


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

    public List<GameObject> GoalPoints;

    private void Start()
    {
        GoalPoints = new List<GameObject>();        
        StartGame();
    }

    public void AddGoalPoint(GameObject goalPoint)
    {
        GoalPoints.Add(goalPoint);
    }

    public void RemoveGoalPoints(GameObject GoalPoint)
    {
        GoalPoints.Remove(GoalPoint);
        GoalPoints.Clear();
    }

    public void Goal(GameObject go, BallSide side) 
    {
        switch (side)
        {
            case BallSide.Left:
                ScoreLeft++;
                LeftScoreText.text = ScoreLeft.ToString();
                break;
            case BallSide.Right:
                ScoreRight++;
                RightScoreText.text = ScoreRight.ToString();
                break;
        }
        
        StartCoroutine(GoalCoroutine(go));
    }

    IEnumerator GoalCoroutine(GameObject go)
    {
        yield return new WaitForSeconds(0.5f);
        go.GetComponent<BallController>().ResetBall();
    }
    
    public void StartGame()
    {
        MatchData matchData = new MatchData()
        {
            Team1 = new Team()
            {
                Name = "GS",
                MainColor = new Color32(255, 0, 0, 255),
                SecondaryColor = new Color32(255, 255, 255, 255)
            },

            Team2 = new Team()
            {
                Name = "FB",
                MainColor = new Color32(0, 0, 255, 255),
                SecondaryColor = new Color32(255, 255, 255, 255)
            },

            speed = 2
        };

        GameTimer = 90;
        ScoreLeft = 0;
        ScoreRight = 0;
        LeftScoreText.text = ScoreLeft.ToString();
        RightScoreText.text = ScoreRight.ToString();
        LeftTeamNameText.text = matchData.Team1.Name;
        RightTeamNameText.text = matchData.Team2.Name;
        StartCoroutine(GameTime(matchData.speed));
    }   
    
    IEnumerator GameTime(int speed)
    {   
        float _speed = 1f / speed;
        Debug.Log("Speed : " + _speed);

        yield return null;
        while (GameTimer > 0)
        {
            yield return new WaitForSeconds(_speed);
            GameTimer--;
            TimerText.text = GameTimer.ToString();
            if (GameTimer <= 45)
            {
                AreaController.Instance.reverse = true;
            }
        }        
     
        StopGame();
    }

    public void StopGame()
    {
        GameOver = true;
        LeftBall.GetComponent<Rigidbody2D>().simulated = false;
        RightBall.GetComponent<Rigidbody2D>().simulated = false;

        AreaController.Instance.rotate = false;
    }

}
